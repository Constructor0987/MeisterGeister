using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public static class FogMap
    {
        public static WriteableBitmap Expand(WriteableBitmap source, int minimumSize)
        {
            if (source.PixelWidth >= minimumSize && source.PixelHeight >= minimumSize)
                return source;

            int width = Math.Max(source.PixelWidth, minimumSize);
            int height = Math.Max(source.PixelHeight, minimumSize);
            var result = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            const int rowsPerChunk = 64;
            var pixels = new int[width * rowsPerChunk];
            for (int y = 0; y < height; y += rowsPerChunk)
            {
                int rows = Math.Min(rowsPerChunk, height - y);
                for (int i = 0; i < pixels.Length; i++)
                    pixels[i] = unchecked((int)0xff000000);
                if (y < source.PixelHeight)
                {
                    int sourceRows = Math.Min(rows, source.PixelHeight - y);
                    source.CopyPixels(new Int32Rect(0, y, source.PixelWidth, sourceRows), pixels, width * 4, 0);
                }
                result.WritePixels(new Int32Rect(0, y, width, rows), pixels, width * 4, 0);
            }
            return result;
        }

        public static void Paint(WriteableBitmap bitmap, Point position, int brushSize, int unitsPerPixel, bool covered)
        {
            int x = (int)Math.Floor(position.X / unitsPerPixel);
            int y = (int)Math.Floor(position.Y / unitsPerPixel);
            int size = 10 * brushSize + 1;
            int left = Math.Max(0, x);
            int top = Math.Max(0, y);
            int right = Math.Min(bitmap.PixelWidth, x + size);
            int bottom = Math.Min(bitmap.PixelHeight, y + size);
            if (right <= left || bottom <= top)
                return;

            int width = right - left;
            int height = bottom - top;
            var pixels = new int[width * height];
            if (covered)
                for (int i = 0; i < pixels.Length; i++)
                    pixels[i] = unchecked((int)0xff000000);
            bitmap.WritePixels(new Int32Rect(left, top, width, height), pixels, width * 4, 0);
        }
    }
}
