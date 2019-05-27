using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetVips;
using VipsApi.Utils;

namespace VipsApi.Controllers
{
    [ApiController]
    public class ImageController : ControllerBase
    {
        static HttpClient client = new HttpClient();

        [HttpGet("/api/v1/image")]
        public async Task<ActionResult> ConvertUrl([FromQuery] ImageQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.Url))
            {
                return BadRequest("The 'url' was not specified");
            }

            var source = await client.GetByteArrayAsync(query.Url);

            using (var image = Image.NewFromBuffer(source))
            {
                double ratio = Ratio(image.Width, query.Width);

                var result = query.Thumbnail
                    ? image.ThumbnailImage(query.Width, query.Height == 0 ? (int?)null : (int?)(query.Height * ratio))
                    : image.Resize(1 / ratio);

                return File(GetWriteBuffer(result, query), MimeTypes.Jpg);
            }
        }

        static double Ratio(int x, int y)
        {
            if (x == y)
            {
                return 1;
            }

            return (double)x / y;
        }

        static byte[] GetWriteBuffer(Image image, ImageQuery query)
        {
            var format = GetWriteFormat(query.Format);
            var options = GetOptions(format, query);

            return image.WriteToBuffer(format, options);
        }

        static string GetWriteFormat(string format)
        {
            switch ((format ?? string.Empty).ToLower())
            {
                case "webp":
                    return ".webp";
                case "png":
                    return ".png";
                default:
                    return ".jpg";
            }
        }

        static VOption GetOptions(string writeFormat, ImageQuery query)
        {
            switch (writeFormat)
            {
                case ".webp":
                    return GetWebpOptions(query.Quality ?? 82);
                case ".png":
                    return GetPngOptions(query.Quality ?? 100);
                default:
                    return GetJpgOptions(query.Quality ?? 82);
            }
        }

        static VOption GetJpgOptions(int quality = 82) =>
            new VOption
                {
                    { "strip", "true" },
                    { "Q", quality.ToString() },
                    { "optimize_coding", "true" },
                    { "interlace", "false" }
                };

        static VOption GetWebpOptions(int quality = 82) =>
            new VOption
                {
                { "strip", "true" },
                    { "Q", quality.ToString() },
                { "lossless", "true" }
                };

        static VOption GetPngOptions(int quality = 100) =>
            new VOption
            {
                { "strip", "true" },
                { "Q", quality.ToString() },
                { "compression", "9" },
                { "interlace", "false" },
                // { "filter", "0" } // none = will cause error
            };
    }
}
