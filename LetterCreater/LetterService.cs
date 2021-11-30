using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LetterCreater
{
    static class LetterService
    {
        public static async Task FromCSVToPng(string source)
        {
            
        }
        private static async Task<Bitmap> FromStringToPng(string[] pixels)
        {
            Bitmap image = new Bitmap(28, 28);
            int k = 0;
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    var temp = Convert.ToDouble(pixels[k]);
                    Color color = Color.FromArgb(temp, Color.Black);
                    image.SetPixel(i, j, color);
                }
            }
        }
        public static async Task ResizeImages(string sourse, string destination)
        {
            var files = Directory.GetFiles(sourse, ".png");
            var tasks = new List<Task>(files.Length);
            foreach (var file in files)
            {
                tasks.Add(SaveImage(file, destination));
            }
            await Task.WhenAll(tasks);
        }
        private static async Task<Bitmap> ResizeImage(string file)
        {
            return await Task.Run(() =>
            {
                Image image = Image.FromFile(file);
                Size size = new Size(28, 23);
                var newImage = new Bitmap(image, size);
                return newImage;
            });
        }
        private static async Task<Bitmap> MergeImage(string file) =>
            await Task.Run(async () =>
            {
                var image = await ResizeImage(file);
                Graphics g = Graphics.FromImage(image);
                var twoPointsImage = await TwoPoints();
                g.DrawImage(twoPointsImage, 0, 0);
                g.Dispose();
                twoPointsImage.Dispose();
                return image;
            });
        // TODO: TwoPoints method
        private static async Task<Bitmap> TwoPoints() =>
            await Task.Run(() =>
            {
                var image = new Bitmap(28, 5);
                return image;
            });

        private static async Task SaveImage(string file, string destination)
        {
            var image = await MergeImage(file);
            string simpleName = Path.GetFileName(file);
            image.Save(Path.Combine(destination, simpleName));
            image.Dispose();
        }
    }
}
