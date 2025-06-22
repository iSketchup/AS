using System.Windows;
using System.Windows.Media;

namespace AsClass
{
    public class FillBucket
    {

        public bool active = false;
        public FillBucket()
        {

        }

        public void Fill(Frame frame, Point StartingPos, SolidColorBrush newColor)
        {
            SolidColorBrush oldColor = frame.GetPixelcolor(StartingPos);

            if (oldColor.Color == newColor.Color)
                return;

            Queue<Point> que = new();
            que.Enqueue(StartingPos);

            while (que.Count > 0)
            {
                Point pos = que.Dequeue();
                if (pos.X >= frame.Width || pos.X < 0 || pos.Y >= frame.Height || pos.Y < 0 || frame.GetPixelcolor(pos).Color != oldColor.Color)
                    continue;
                else
                {
                    frame.ChangePixelColor(pos, newColor, 1);
                    que.Enqueue(new Point(pos.X + 1, pos.Y)); 
                    que.Enqueue(new Point(pos.X - 1, pos.Y)); 
                    que.Enqueue(new Point(pos.X, pos.Y + 1)); 
                    que.Enqueue(new Point(pos.X, pos.Y - 1)); 
                }

            }

        }
    }
}
