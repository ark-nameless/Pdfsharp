using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationExport.utils
{
    public class Interpolation
    {
        public double linearInterpolation(double x0, double y0, double x1, double y1, double xd)
        {
            return (y0 + ((y1 - y0) * ((xd - x0) / (x1 - x0))));
        }
    }
}
