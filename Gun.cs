using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsSector
{
    class Gun : PictureBox
    {
        public double PositionX { get; set; }

        public double PositionY { get; set; }

        public int Angle { get; set; }

        public int Cooldown { get; set; }

        public int index { get; set; }
    }
}
