using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ffTicketingCsharp
{
    internal static class ScreenReader
    {
        public static Bitmap GetScreen(Size s, Point loc)
        {
            var memoryImage = new Bitmap(s.Width, s.Height);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(loc.X, loc.Y, 0, 0, s);
            return memoryImage;
        }

        public static Point? FindBitmap(Bitmap screen, Bitmap bitmap)
        {
            for (var i = 0; i < screen.Width - bitmap.Width; i++)
            {
                for (var j = 0; j < screen.Height - bitmap.Height; j++)
                {
                    var match = true;
                    for (var k = 0; k < bitmap.Width; k++)
                    {
                        for (var l = 0; l < bitmap.Height; l++)
                        {
                            if (screen.GetPixel(i, j) != bitmap.GetPixel(k, l))
                            {
                                match = false;
                                break;
                            }
                        }
                        if (!match)
                        {
                            break;
                        }
                    }
                    if (match)
                    {
                        return new Point(i, j);
                    }
                }
            }
            return null;
        }

        public static IEnumerable<Rectangle> FindColorblocks(Bitmap screen, int size)
        {
            var matches = new List<Rectangle>();
            for (var i = 0; i < screen.Width - size; i++)
            {
                var foundMatch = false;
                for (var j = 0; j < screen.Height - size; j++)
                {
                    Color? color = null;
                    bool match = false;
                    for (var k = i; k < i + size; k++)
                    {
                        for (var l = j; l < j + size; l++)
                        {
                            if (color == null)
                            {
                                var pixel = screen.GetPixel(i, j);
                                if (!pixel.IsGraytone())
                                {
                                    color = pixel;
                                    match = true;
                                }
                            } else if (color != screen.GetPixel(k, l))
                            {
                                match = false;
                                color = null;
                                break;
                            }
                        }
                        if (!match)
                        {
                            break;
                        }
                       
                    }
                    if (match)
                    {
                        matches.Add(new Rectangle(i, j, size, size));
                        j += size;
                        foundMatch = true;
                    }
                }
                if (foundMatch)
                {
                    i += size;
                }
            }
            return matches;
        }
    }
}
