using System;
using System.Collections.Generic;
using System.IO;
using ImageResizer;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public static class ImageResizers
    {
        [FunctionName("ImageResizer")]
        public static void Run(
            [BlobTrigger("espa-clients-profle-images/{name}", Connection = "BlobConnectionSstring")]Stream image,
            [Blob("espa-clients-profle-images-sm/{name}", FileAccess.Write, Connection = "BlobConnectionSstring")]Stream imageSmall,
            [Blob("espa-clients-profle-images-md/{name}", FileAccess.Write, Connection = "BlobConnectionSstring")]Stream imageMedium, ILogger log)  // output blobs
        {
            var imageBuilder = ImageBuilder.Current;
            var size = imageDimensionsTable[ImageSize.Small];

            imageBuilder.Build(
                image, imageSmall,
                new ResizeSettings(size.Item1, size.Item2, FitMode.Max, null), false);
            log.LogInformation("Output to small res folder");

            image.Position = 0;
            size = imageDimensionsTable[ImageSize.Medium];

            imageBuilder.Build(
                image, imageMedium,
                new ResizeSettings(size.Item1, size.Item2, FitMode.Max, null), false);
            log.LogInformation("Output to small medium folder");

        }

        public enum ImageSize
        {
            ExtraSmall, Small, Medium
        }

        private static Dictionary<ImageSize, Tuple<int, int>> imageDimensionsTable = new Dictionary<ImageSize, Tuple<int, int>>()
        {
            { ImageSize.ExtraSmall, Tuple.Create(320, 200) },
            { ImageSize.Small,      Tuple.Create(640, 400) },
            { ImageSize.Medium,     Tuple.Create(800, 600) }
        };
    }
}
