using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsClass
{
    public class Eraser
    {
        double Size { get; set; } = 5;

        public Eraser()
        {
        }

        public Eraser(double size)
        {
            Size = size;
        }

    }
}
