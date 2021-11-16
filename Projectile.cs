using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsSector
{
    class Projectile : PictureBox
    {
        public double PozitionX { get; set; }

        public double PozitionY { get; set; }

        public int Angle { get; set; }

        public double SpeedX { get; set; }

        public double SpeedY { get; set; }

        public int index { get; set; }
    }
}
