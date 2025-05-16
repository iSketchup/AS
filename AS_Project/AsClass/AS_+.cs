using System.Windows.Controls;

namespace AsClass
{
    public class As
    {
        public List<Button> FileButtons = new List<Button>();
        public List<Button> FrameButtons = new List<Button>();

        public List<Button> SettingButtons = new List<Button>();

        public As()
        {

            FillButtonLists(FileButtons, FrameButtons, SettingButtons);
        }







        public void FillButtonLists(List<Button> filelButtons, List<Button> frameButtons, List<Button> settingButtons)
        {
            // FileButton List
            // 

            Button ButtonNew = new Button();
            ButtonNew.Content = "New";
            ButtonNew.Name = "ButtonNew";
            // TODO Click Event Hinzufügen
            filelButtons.Add(ButtonNew);


            Button ButtonOpen = new Button();
            ButtonOpen.Content = "Open";
            ButtonOpen.Name = "ButtonOpen";
            // TODO Click Event Hinzufügen
            filelButtons.Add(ButtonOpen);


            Button SaveButton = new Button();
            SaveButton.Content = "Save";
            SaveButton.Name = "SaveButton";
            // TODO Click Event Hinzufügen

            filelButtons.Add(SaveButton);

            // FrameButton List

            Button ButtonNewFrame = new Button();
            ButtonNewFrame.Content = "New Frame";
            ButtonNewFrame.Name = "ButtonNewFrame";
            // TODO Click Event Hinzufügen
            frameButtons.Add(ButtonNewFrame);

            Button LastFrame = new Button();
            LastFrame.Content = "Last Frame";
            LastFrame.Name = "LastFrame";
            // TODO Click Event Hinzufügen
            frameButtons.Add(LastFrame);

            // SettingButton List

            Button ButtonSettings = new Button();
            ButtonSettings.Content = "Settings";
            ButtonSettings.Name = "ButtonSettings";
            // TODO Click Event Hinzufügen
            FileButtons.Add(ButtonSettings);







        }

    }



}
