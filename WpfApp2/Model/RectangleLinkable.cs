using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp2.Model;

namespace WpfApp2
{
    public class RectangleLinkable : ObjectLinkableModel
    {
        public RectangleLinkable(MouseButtonEventHandler genericRectangle_OnMouseDown)
        {
            ShapeInCanvas = new Rectangle();
            ShapeInCanvas.Width = 50;
            ShapeInCanvas.Height = 50;
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("Blue");
            ShapeInCanvas.Fill = brush;
            ShapeInCanvas.MouseMove += new MouseEventHandler(GenericShape_OnMouseMove);
            ShapeInCanvas.MouseDown += genericRectangle_OnMouseDown;
        }
        public RectangleLinkable(Rectangle rectangle)
        {
            ShapeInCanvas = rectangle;
        }
    }
}