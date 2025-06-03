using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AsClass
{
    public class ColorPicker
    {
        private Color _selectedColor;

        public Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
               
            }
        }

        public ColorPicker(WriteableBitmap wb)
        {
           
        }


    }
}
