using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for DragZone2.xaml
    /// </summary>
    public partial class DragZone2 : UserControl
    {
        public static readonly DependencyProperty IsChildHitTestVisibleProperty =
            DependencyProperty.Register("IsChildHitTestVisible", typeof(bool), typeof(DragZone2), new PropertyMetadata(true));

        public bool IsChildHitTestVisible
        {
            get { return (bool)GetValue(IsChildHitTestVisibleProperty); }
            set { SetValue(IsChildHitTestVisibleProperty, value); }
        }
        public DragZone2()
        {
            InitializeComponent();
        }

        private void Rectangle2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IsChildHitTestVisible = false;
                DragDrop.DoDragDrop(Rectangle2, new DataObject(DataFormats.Serializable, Rectangle2), DragDropEffects.Move);
                IsChildHitTestVisible = true;
            }
        }

        private void DragZone2_Canvas_Drop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);

            if( data is UIElement element)
            {
                Point dropPosition = e.GetPosition(DragZone2_Canvas);
                Canvas.SetLeft(element, dropPosition.X);
                Canvas.SetTop(element, dropPosition.Y);

                if (!DragZone2_Canvas.Children.Contains(element))
                {
                    DragZone2_Canvas.Children.Add(element);
                }
            } 
        }

        private void DragZone2_Canvas_DragLeave(object sender, DragEventArgs e)
        {
            if (e.OriginalSource == DragZone2_Canvas)
            {
                object data = e.Data.GetData(DataFormats.Serializable);

                if (data is UIElement element)
                {
                    DragZone2_Canvas.Children.Remove(element);
                }
            }
        }
    }
}