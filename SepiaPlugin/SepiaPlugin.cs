using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModularImageProcessing.Contracts;
using System.ComponentModel.Composition;
using System.Drawing.Imaging;
using System.Drawing;

namespace SepiaPlugin
{
    [Export("Filter", typeof(IMenuPlugin))]
    [MenuPluginMetaData("Sepia")]
    class SepiaPlugin : IMenuPlugin
    {
        private Bitmap convertBitmapToSepia(Bitmap bitmap)
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                       {
                            new float[]{.393f, .349f, .272f, 0, 0},
                            new float[]{.769f, .686f, .534f, 0, 0},
                            new float[]{.189f, .168f, .131f, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                            new float[]{0, 0, 0, 0, 1}
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
            return convertBitmapToSepia(bitmap);
        }
    }
}
