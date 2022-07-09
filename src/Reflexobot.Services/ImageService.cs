using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Reflexobot.Entities;
using Reflexobot.Services.Interfaces;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Reflexobot.Services
{
    public class ImageService : IImageService
    {
        private readonly ILogger<ImageService> _logger;
        private readonly Font _font;
        private readonly Font _fontBold;
        public ImageService(ILogger<ImageService> logger)
        {
            _logger = logger;
            if (_font == null)
            {
                var collection = new FontCollection();
                FontFamily family = collection.Add("../Reflexobot.Services/Fonts/Roboto-Medium.ttf");
                _font = family.CreateFont((float)14);
                _fontBold = family.CreateFont((float)18, FontStyle.Regular);
            }
        }

        public byte[] GenerateImage(int width, int height, IEnumerable<Achievment> achievments)
        {
            var image = new Image<Rgba32>(width, height);
            image.Mutate(_ => _.Fill(Color.Wheat));
            image.Mutate(x => x.DrawText("Reflexobot", _font, Color.Orange, new PointF(5, 5)));

            //foreach (var achievment in achievments)
            //{
            //    Image achievmentImage = Image.Load(achievment.Img);
            //    image.Mutate(_ => _.DrawImage(achievmentImage, 1));
            //}

            var ms = new MemoryStream();
            image.SaveAsPng(ms);
            return ms.ToArray();
        }
    }
}
