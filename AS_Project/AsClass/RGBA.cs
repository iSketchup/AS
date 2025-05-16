using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsClass
{

    public class RGBA
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public RGBA(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public RGBA(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }
        public byte[] GetBGRA()
        {
            return new byte[] { B, G, R, A };
        }

        public override string ToString()
        {
            return $"{R}, {G}, {B}, {A}";
        }


    }
}
