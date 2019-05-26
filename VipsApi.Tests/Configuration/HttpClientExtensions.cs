using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace VipsApi.Tests.Configuration
{
    public static class HttpClientExtensions
    {
        public static async Task EnsureValid(this HttpResponseMessage response, ITestOutputHelper output)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            if (response.Content == null)
            {
                return;
            }

            try
            {
                string content = await response.Content.ReadAsStringAsync();

                output.WriteLine("Error: " + content);

                throw new TestHttpException(response, content);
            }
            finally
            {
                response.Content?.Dispose();
            }
        }

        public static bool HasMimeType(this HttpResponseMessage response, string mimeType)
        {
            return mimeType.Equals(response.Content.Headers.ContentType.MediaType, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}
