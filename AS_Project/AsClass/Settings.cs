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
        public int FrameWidth { get; set; } = 800;

        public int FrameHeight { get; set; } = 600;

        public int FPS { get; set; } = 2;

       


        public Settings()
        {

        }

        public Settings(int frameWidth, int frameHeight, int fps)
        {
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FPS = fps;
        }
           
    }
}
