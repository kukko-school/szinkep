using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Szinkep
{
    class Program
    {
        private static int width = 50;
        private static int height = 50;
        private static Pixel[,] pixels;
        static void Main(string[] args)
        {
            pixels = new Pixel[height, width];
            string[] rows = File.ReadAllLines("../../kep.txt");
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    string[] colors = rows[y * 50 + x].Split(' ');
                    pixels[x, y] = new Pixel(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]));
                }
            }
            Console.WriteLine("2. feladat");
            FindColorProvidedByUser();
            Console.WriteLine("3. feladat");
            PrintNumberOfOccurencesOfPixel(35, 8);
            Console.WriteLine("4. feladat");
            PrintMostCommonBaseColor();
            WriteBitmapToFile(GenerateBitmapFromPixels(pixels), "../../eredeti.bmp");
            AddFrameToImage(3, new Pixel(0, 0, 0));
            WriteBitmapToFile(GenerateBitmapFromPixels(pixels), "../../keretes.bmp");
            WritePixelsToFile(pixels, "../../keretes.txt");
            PrintAreaOfRectangleOfColor(pixels, new Pixel(255, 255, 0));
            Console.ReadKey();
        }

        private static void FindColorProvidedByUser()
        {
            Console.Write("Piros: ");
            int r = int.Parse(Console.ReadLine());
            Console.Write("Zöld: ");
            int g = int.Parse(Console.ReadLine());
            Console.Write("Kék: ");
            int b = int.Parse(Console.ReadLine());
            Pixel wantedPixel = new Pixel(r, g, b);
            bool found = false;
            for (int y = 0; y < height && !found; y++)
            {
                for (int x = 0; x < width && !found; x++)
                {
                    found = pixels[y, x].Equals(wantedPixel);
                }
            }
            Console.WriteLine(found ? "Szerepel" : "Nem szerepel");
        }

        private static void PrintNumberOfOccurencesOfPixel(int wantedY, int wantedX)
        {
            Pixel wantedPixel = pixels[wantedY - 1, wantedX - 1];
            int occurencesInRow = 0;
            int occurencesInColumn = 0;
            for (int x = 0; x < width; x++)
            {
                occurencesInRow += pixels[wantedY - 1, x].Equals(wantedPixel) ? 1 : 0;
            }
            for (int y = 0; y < height; y++)
            {
                occurencesInColumn += pixels[y, wantedX - 1].Equals(wantedPixel) ? 1 : 0;
            }
            Console.WriteLine("Sorban: " + occurencesInRow + " Oszlopban: " + occurencesInColumn);
        }

        private static void PrintMostCommonBaseColor()
        {
            Dictionary<string, Pixel> colorToPixel = new Dictionary<string, Pixel>();
            colorToPixel.Add("Piros", new Pixel(255, 0, 0));
            colorToPixel.Add("Zöld", new Pixel(0, 255, 0));
            colorToPixel.Add("Kék", new Pixel(0, 0, 255));
            Dictionary<string, int> colorCount = new Dictionary<string, int>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    foreach (KeyValuePair<string, Pixel> item in colorToPixel)
                    {
                        if (item.Value.Equals(pixels[y, x]))
                        {
                            if (!colorCount.ContainsKey(item.Key))
                            {
                                colorCount.Add(item.Key, 0);
                            }
                            colorCount[item.Key]++;
                        }
                    }
                }
            }
            FindExtremum(colorCount);
        }
        public static void FindExtremum(Dictionary<string, int> numbers)
        {

            int maxValue = 0;
            string color = "";
            foreach (KeyValuePair<string, int> item in numbers)
            {
                if (item.Value > maxValue)
                {
                    maxValue = item.Value;
                    color = item.Key;
                }

            }
            Console.WriteLine("A leggyakoribb szín: " + color);

        }

        protected static Bitmap GenerateBitmapFromPixels(Pixel[,] pixels)
        {
            Bitmap output = new Bitmap(pixels.GetLength(1), pixels.GetLength(0));
            Graphics canvas = Graphics.FromImage(output);
            for (int y = 0; y < pixels.GetLength(0); y++)
            {
                for (int x = 0; x < pixels.GetLength(1); x++)
                {
                    SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(1, (byte)pixels[y, x].GetR(), (byte)pixels[y, x].GetG(), (byte)pixels[y, x].GetB()));
                    canvas.FillRectangle(brush, x, y, 1, 1);
                }
            }
            return output;
        }
        protected static void WriteBitmapToFile(Bitmap bitmap, string filename)
        {
            bitmap.Save(filename, ImageFormat.Bmp);
        }
        protected static void AddFrameToImage(int frameThickness, Pixel frameColor)
        {
            for (int y = 0; y < pixels.GetLength(0); y++)
            {
                for (int x = 0; x < pixels.GetLength(1); x++)
                {
                    if (y < frameThickness || y >= pixels.GetLength(0) - frameThickness || x < frameThickness || x >= pixels.GetLength(1) - frameThickness)
                    {
                        pixels[y, x] = frameColor;
                    }
                }
            }
        }

        private static void WritePixelsToFile(Pixel[,] pixels, string filename)
        {
            List<string> lines = new List<string>();
            for (int y = 0; y < pixels.GetLength(0); y++)
            {
                for (int x = 0; x < pixels.GetLength(1); x++)
                {
                    lines.Add(String.Join(" ", new string[] { pixels[y, x].GetR().ToString(), pixels[y, x].GetG().ToString(), pixels[y, x].GetB().ToString() }));
                }
            }
            File.WriteAllLines(filename, lines);
        }

        private static void PrintAreaOfRectangleOfColor(Pixel[,] pixels, Pixel pixel)
        {
            int? startX = null;
            int? startY = null;
            int? endX = null;
            int? endY = null;
            for (int y = 0; y < pixels.GetLength(0); y++)
            {
                for (int x = 0; x < pixels.GetLength(1); x++)
                {
                    if (pixels[y, x].Equals(pixel))
                    {
                        if (startX == null || x < startX)
                        {
                            startX = x;
                        }
                        if (startY == null || y < startY)
                        {
                            startY = y;
                        }
                        if (endX == null || x > endX)
                        {
                            endX = x;
                        }
                        if (endY == null || y > endY)
                        {
                            endY = y;
                        }
                    }
                }
            }
            Console.WriteLine("Kezd: " + startY + ", " + startX);
            Console.WriteLine("Vége: " + endY + ", " + endX);
            Console.WriteLine("Képpontok száma: " + ((endX.Value - startX.Value + 1) * (endY.Value - startY.Value + 1)));
        }
    }
}
