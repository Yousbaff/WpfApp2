using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CanvasCustom CanvasCustom;
        public MainWindow()
        {
            InitializeComponent();
            CanvasCustom = new CanvasCustom(TestCustomCanvas, PopUpNewLink, LinkInfoDisplayed.IsChecked.Value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TestRadioButton.IsChecked == true)
            {
                MessageBox.Show("IsChecked. " + TextBoxForButton.Text);
            }
            else if (TestRadioButton.IsChecked == false)
            {
                MessageBox.Show("Is Not Checked. " + TextBoxForButton.Text);
            }
        }
        
        private void OnClickAddMovableBlock(object sender, RoutedEventArgs e)
        {
            CanvasCustom.AddRectangle();
        }

        private void ButtonRandomGeneration_Click(object sender, RoutedEventArgs e)
        {
            int nbRectangle, nbLink = 0;
            int.TryParse(GenerationNbRectangle.Text, out nbRectangle);
            int.TryParse(GenerationNbLink.Text, out nbLink);
            CanvasCustom.RandomGenerationSchematic(nbRectangle, nbLink);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LinkInfoDisplayed_Click(object sender, RoutedEventArgs e)
        {
            CanvasCustom.ToggleDisplayLink();
        }
    }
}