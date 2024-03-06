using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace WpfApp2.Model
{
    public abstract class ObjectLinkableModel
    {
        public Shape ShapeInCanvas;
        public List<LinkModel> Links = new List<LinkModel>();
        public bool Selected = false;

        public void GenericShape_OnMouseMove(object sender, MouseEventArgs e)
        {
            object data = e.Source as UIElement;

            if (data is UIElement element)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop(element, new DataObject(DataFormats.Serializable, element), DragDropEffects.Move);
                }
            }
        }
        private void GenericShape_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            object data = e.Source as UIElement;

            if (data is UIElement element)
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    ToggleSelection();
                }
            }
        }
        public void ToggleSelection()
        {
            BrushConverter bc = new BrushConverter();
            if (Selected)
            {
                Brush brush = (Brush)bc.ConvertFrom("Blue");
                ShapeInCanvas.Fill = brush;
                Selected = false;
            }
            else
            {
                Brush brush = (Brush)bc.ConvertFrom("Red");
                ShapeInCanvas.Fill = brush;
                Selected = true;
            }
        }
    }
}