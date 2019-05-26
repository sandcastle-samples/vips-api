using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;
using VipsApi.Tests.Configuration;

namespace VipsApi.Tests
{
    public abstract class TestController : IClassFixture<TestApplicationFactory>
    {
        protected TestController(TestApplicationFactory factory, ITestOutputHelper output)
        {
            Configuration = factory.Configuration;
            Client = factory.CreateClient();
            NonCachingClient = factory.CreateClient();
            NonCachingClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            Output = output;
        }

        public ITestOutputHelper Output { get; }
        protected IConfiguration Configuration { get; }
        protected HttpClient Client { get; }
        protected HttpClient NonCachingClient { get; }
    }
}
