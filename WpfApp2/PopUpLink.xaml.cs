using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp2.Model;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for PopUpLink.xaml
    /// </summary>
    public partial class PopUpLink : UserControl
    {
        public List<string> ListItems = new List<string>() { "Before load 1", "before load 2" };
        public string ItemSelectedInComboBox { get; set; } //Useless Binding not working
        public ILinkCreator LinkCreator { get; set; }
        public ObjectLinkableModel Object1 { get; set; }
        public ObjectLinkableModel Object2 { get; set; }

        public PopUpLink()
        {
            ListItems.Add("Item 1");
            ListItems.Add("Item 2");
            ListItems.Add("Item 3");
            ListItems.Add("Item 4");
            ListItems.Add("Item 5");
            InitializeComponent();
            ListOptions.ItemsSource = ListItems;
        }
        public void OpenPopUp(ILinkCreator linkCreator, ObjectLinkableModel object1, ObjectLinkableModel object2)
        {
            this.Visibility = Visibility.Visible;
            LinkCreator = linkCreator;
            Object1 = object1;
            Object2 = object2;
        }
        private void ValidatePopUp_Click(object sender, RoutedEventArgs e)
        {
            string ItemSelected = ListOptions.SelectedItem.ToString();
            LinkCreator.AddNewLink(ItemSelected, Object1, Object2);
            ListOptions.SelectedItem = null;
            this.Visibility = Visibility.Collapsed;
        }
    }
}