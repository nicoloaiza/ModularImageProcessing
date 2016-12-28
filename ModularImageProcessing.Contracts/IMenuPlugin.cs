using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ModularImageProcessing.Contracts
{
    public interface IMenuPlugin
    {
        Bitmap ProcessImage(Bitmap bitmap);
    }
}
