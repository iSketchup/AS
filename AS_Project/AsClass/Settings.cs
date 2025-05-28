using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AsClass
{
    public class Settings
    {
        public double FrameWidth { get; set; } = 800;

        public double FrameHeight { get; set; } = 600;

        public int FPS { get; set; } = 5;

       


        public Settings()
        {

        }

        public Settings(double frameWidth, double frameHeight, int fps)
        {
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FPS = fps;
        }
           
    }
}
