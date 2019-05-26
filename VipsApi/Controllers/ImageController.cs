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
                    ? image.ThumbnailImage(query.Width, (int)(query.Height * ratio))
                    : image.Resize(1 / ratio, vscale: 1 / ratio);

                return File(result.WriteToBuffer(".jpg", GetJpgOptions()), MimeTypes.Jpg);
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

        static VOption GetJpgOptions(int quality = 82) =>
            new VOption
                {
                    { "strip", "true" },
                    { "Q", quality.ToString() },
                    { "optimize_coding", "true" },
                    { "interlace", "false" }
                };
    }
}
