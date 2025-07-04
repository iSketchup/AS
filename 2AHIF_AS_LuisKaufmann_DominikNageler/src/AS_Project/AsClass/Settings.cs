﻿using System.IO;
using System.Text.Json;

namespace AsClass
{
    public class Settings
    {
        public int FrameWidth { get; set; } = 500;

        public int FrameHeight { get; set; } = 240;

        public int FPS { get; set; } = 2;

        public int TileSize { get; set; } = 8;


        public Settings()
        {

        }

        public Settings(int frameWidth, int frameHeight, int fps)
        {
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FPS = fps;
        }

        public Settings(int frameWidth, int frameHeight, int fps, int Tilesize)
        {
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FPS = fps;
            this.TileSize = Tilesize;
        }





        public void SaveToJsonFile(string filePath)
        {
            string jsonString = JsonSerializer.Serialize(this);



            using (StreamWriter stream = new StreamWriter(filePath, false))
            {
                stream.Write(jsonString);
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
