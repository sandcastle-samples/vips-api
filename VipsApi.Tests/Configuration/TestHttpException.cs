using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;

namespace VipsApi.Tests.Configuration
{
    public class TestHttpException : Exception
    {
        public TestHttpException(HttpResponseMessage response, string content)
            : base("Http Error - See properties")
        {
            Response = response;
            Content = content;
        }

        /// <summary>
        /// The HTTP response messgae that had the error.
        /// </summary>
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public HttpResponseMessage Response { get; }

        /// <summary>
        /// The HTTP response message - dont read from Response.Content - this has been
        /// disposed of.
        /// </summary>
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public string Content { get; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("HTTP Error!");
            builder.AppendLine(Content);
            return builder.ToString();
        }
    }
}
