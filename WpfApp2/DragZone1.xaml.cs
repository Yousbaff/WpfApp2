using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for DragZone1.xaml
    /// </summary>
    public partial class DragZone1 : UserControl
    {
        public static readonly DependencyProperty IsChildHitTestVisibleProperty =
            DependencyProperty.Register("IsChildHitTestVisible", typeof(bool), typeof(DragZone1), new PropertyMetadata(true));

        public bool IsChildHitTestVisible
        {
            get {  return (bool) GetValue(IsChildHitTestVisibleProperty);}
            set { SetValue(IsChildHitTestVisibleProperty, value);}
        }
        public DragZone1()
        {
            InitializeComponent();
        }

        private void Rectangle1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                IsChildHitTestVisible = false;
                DragDrop.DoDragDrop(Rectangle1, new DataObject(DataFormats.Serializable, Rectangle1), DragDropEffects.Move);
                IsChildHitTestVisible = true;
            }
        }

        private void DragZone1_Canvas_Drop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);

            if (data is UIElement element)
            {
                Point dropPosition = e.GetPosition(DragZone1_Canvas);
                Canvas.SetLeft(element, dropPosition.X);
                Canvas.SetTop(element, dropPosition.Y);

                if (!DragZone1_Canvas.Children.Contains(element))
                {
                    DragZone1_Canvas.Children.Add(element);
                }
            }
        }

        private void DragZone1_Canvas_DragLeave(object sender, DragEventArgs e)
        {
            if (e.OriginalSource == DragZone1_Canvas)
            {
                object data = e.Data.GetData(DataFormats.Serializable);

                if (data is UIElement element)
                {
                    DragZone1_Canvas.Children.Remove(element);
                }
            }
        }
    }
}