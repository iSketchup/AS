using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AsClass
{
    public class Settings
    {
        public int FrameWidth { get; set; } = 500;

        public int FrameHeight { get; set; } = 240;

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


       


        public void SaveToJsonFile(string filePath)
        {
            string jsonString = JsonSerializer.Serialize(this);
         
            using (StreamWriter stream = new StreamWriter(filePath,false))
            {
                stream.WriteLine(jsonString);
            }
        }

        public static Settings LoadFromJson(string filepath)
        {
            Settings settings = new Settings();

            if (File.Exists(filepath) == true)
            {
                settings = null;
                
                using (StreamReader stream = new StreamReader(filepath))
                {
                    string SerilizeData = stream.ReadToEnd();

                    settings = JsonSerializer.Deserialize<Settings>(SerilizeData);

                }
              
            }
            return settings;

        }
    }
}
