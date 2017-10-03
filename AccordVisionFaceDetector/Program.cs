using Accord.Imaging.Filters;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;
using System;
using System.Drawing;
using System.IO;

namespace AccordVisionFaceDetector
{
    public class Program
    {
        private static readonly string basedir = $"{AppDomain.CurrentDomain.BaseDirectory}";
        private static readonly string sep = $"..{Path.DirectorySeparatorChar}";

        public static void Main(string[] args)
        {
            var picture = Resource.lena_color;

            HaarCascade cascade = new FaceHaarCascade();
            var detector = new HaarObjectDetector(cascade, 30);

            detector.SearchMode = ObjectDetectorSearchMode.Average;
            detector.ScalingMode = ObjectDetectorScalingMode.GreaterToSmaller;
            detector.ScalingFactor = 1.5f;
            detector.UseParallelProcessing = false;
            detector.Suppression = 2;

            Rectangle[] objects = detector.ProcessFrame(picture);

            if (objects.Length > 0)
            {
                RectanglesMarker marker = new RectanglesMarker(objects, Color.Fuchsia);
                var markedup = marker.Apply(picture);
                markedup.Save($"{basedir}{sep}{sep}markedup.jpg");
            }
        }
    }
}
