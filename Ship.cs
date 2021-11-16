using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsSector
{
    class Ship : PictureBox
    {
        public double PositionX { get; set; }

        public double PositionY { get; set; }

        public int Angle { get; set; }

        public double SpeedX { get; set; }
        
        public double SpeedY { get; set; }

        public int index { get; set; }

        public int Health { get; set; }
    }
}
