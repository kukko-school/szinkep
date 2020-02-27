using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szinkep
{
    class Pixel
    {
        private int r;
        private int g;
        private int b;
        public Pixel(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public int GetR()
        {
            return r;
        }
        public int GetG()
        {
            return g;
        }
        public int GetB()
        {
            return b;
        }
        public bool Equals(Pixel pixel)
        {
            return r == pixel.GetR() && g == pixel.GetG() && b == pixel.GetB();
        }
    }
}
