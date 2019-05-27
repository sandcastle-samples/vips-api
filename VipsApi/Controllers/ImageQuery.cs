using System;
using System.Collections.Generic;
using System.Linq;

namespace VipsApi.Controllers
{
    public class ImageQuery
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
        public string Format { get; set; }
        public bool Thumbnail { get; set; }
        public int? Quality { get; set; }
    }
}
