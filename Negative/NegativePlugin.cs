using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModularImageProcessing.Contracts;
using System.ComponentModel.Composition;
using System.Drawing.Imaging;
using System.Drawing;

namespace Negative
{
    [Export("Filter", typeof(IMenuPlugin))]
    [MenuPluginMetaData("Negativo")]
    class NegativePlugin : IMenuPlugin
    {
        private Bitmap convertBitmapToGrayScale(Bitmap bitmap)
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                           {
                                new float[]{-1, 0, 0, 0, 0},
                                new float[]{0, -1, 0, 0, 0},
                                new float[]{0, 0, -1, 0, 0},
                                new float[]{0, 0, 0, 1, 0},
                                new float[]{1, 1, 1, 1, 1}
                           });

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
               0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attributes);

            g.Dispose();
            return newBitmap;
        }

        public Bitmap ProcessImage(Bitmap bitmap)
        {
            return convertBitmapToGrayScale(bitmap);
        }
    }
}
