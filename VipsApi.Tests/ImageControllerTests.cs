using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using VipsApi.Tests.Configuration;
using VipsApi.Utils;

namespace VipsApi.Tests
{
    public class ImageControllerTests : TestController
    {
        public ImageControllerTests(TestApplicationFactory factory, ITestOutputHelper output)
            : base(factory, output) { }

        [Fact]
        public async Task Can_resize_jpg_from_url()
        {
            var result = await Client.GetAsync("/api/v1/image?width=100&url=https%3A%2F%2Fvia.placeholder.com%2F300");

            await result.EnsureValid(Output);

            var data = await result.Content.ReadAsByteArrayAsync();

            Assert.True(result.IsSuccessStatusCode);
            Assert.True(data.Length > 0);
            Assert.True(result.HasMimeType(MimeTypes.Jpg));
        }

        [Fact]
        public async Task Can_thumbnail_jpg_from_url()
        {
            var result = await Client.GetAsync("/api/v1/image?width=100&thumbnail=true&url=https%3A%2F%2Fvia.placeholder.com%2F300");

            await result.EnsureValid(Output);

            var data = await result.Content.ReadAsByteArrayAsync();

            Assert.True(result.IsSuccessStatusCode);
            Assert.True(data.Length > 0);
            Assert.True(result.HasMimeType(MimeTypes.Jpg));
        }
    }
}
