using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp2.Model;

namespace WpfApp2
{
    public class CanvasCustom : ILinkCreator
    {
        List<(ObjectLinkableModel, ObjectLinkableModel, UIElement)> ListCanvasLightElement = new List<(ObjectLinkableModel, ObjectLinkableModel, UIElement)>();
        Canvas CanvasUsed { get; set; }
        PopUpLink PopUpLinkUsed{ get; set; }
        SchematicModel SchematicDisplayed = new SchematicModel();
        bool LinkInfoDisplayed = true;
        
        public CanvasCustom(Canvas canvas, PopUpLink popUpLink, bool linkInfoDisplayed)
        {
            CanvasUsed = canvas;
            CanvasUsed.AllowDrop = true;
            CanvasUsed.Drop += new DragEventHandler(CanvasWithDraggable_Drop);
            CanvasUsed.DragOver += new DragEventHandler(CanvasWithDraggable_DragOver);
            PopUpLinkUsed = popUpLink;
            PopUpLinkUsed.Visibility = Visibility.Collapsed;
            LinkInfoDisplayed = linkInfoDisplayed;
        }
        public void AddRectangle()
        {
            RectangleLinkable rectangleLinkable = new RectangleLinkable(new MouseButtonEventHandler(GenericRectangle_OnMouseDown));

            SchematicDisplayed.RectangleLinkables.Add(rectangleLinkable);
            CanvasUsed.Children.Add(rectangleLinkable.ShapeInCanvas);
            Canvas.SetLeft(rectangleLinkable.ShapeInCanvas, 50);
            Canvas.SetTop(rectangleLinkable.ShapeInCanvas, 50);
        }
        private void CanvasWithDraggable_Drop(object sender, DragEventArgs e){ }
        private void CanvasWithDraggable_DragOver(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);

            if (data is UIElement element)
            {
                Point dropPosition = e.GetPosition(CanvasUsed);
                Canvas.SetLeft(element, dropPosition.X);
                Canvas.SetTop(element, dropPosition.Y);

                if (!CanvasUsed.Children.Contains(element))
                {
                    CanvasUsed.Children.Add(element);
                }
                ObjectLinkableModel objectLinkable = SchematicDisplayed.RectangleLinkables.FirstOrDefault(rectangleLinkable => rectangleLinkable.ShapeInCanvas == element);
                UpdateLinksOfABlock(objectLinkable);
            }
        }
        private void GenericRectangle_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                object data = e.Source as UIElement;

                if (data is Rectangle element)
                {
                    RectangleLinkable rectangleLinkable = SchematicDisplayed.RectangleLinkables.FirstOrDefault(s => s.ShapeInCanvas == element);
                    rectangleLinkable.ToggleSelection();
                    CheckTwoRectangleSelected();
                    UpdateLinksOfABlock(rectangleLinkable);
                }
            }
        }
        private void CheckTwoRectangleSelected()
        {
            if(SchematicDisplayed.RectangleLinkables.Where(rect => rect.Selected).Count() >= 2)
            {
                RectangleLinkable rectangleLinkableSelected1 = SchematicDisplayed.RectangleLinkables.Where(rect => rect.Selected).ToList()[0];
                RectangleLinkable rectangleLinkableSelected2 = SchematicDisplayed.RectangleLinkables.Where(rect => rect.Selected).ToList()[1];

                if (!rectangleLinkableSelected1.Links.Any(link => link.ObjectLinkableEnd == rectangleLinkableSelected2))
                {
                    PopUpLinkUsed.OpenPopUp(this, rectangleLinkableSelected1, rectangleLinkableSelected2);
                }
                else
                {
                    MessageBox.Show("Les 2 objets sont déjà liés.");
                }
                rectangleLinkableSelected1.ToggleSelection();
                rectangleLinkableSelected2.ToggleSelection();
            }
        }
        public void AddNewLink(string popUpResponse, ObjectLinkableModel objectLinkableModel1, ObjectLinkableModel objectLinkableModel2)
        {
            LinkModel linkModel = new LinkModel(objectLinkableModel1, objectLinkableModel2, popUpResponse);
            objectLinkableModel1.Links.Add(linkModel);
            objectLinkableModel2.Links.Add(linkModel);
            UpdateLinksOfABlock(objectLinkableModel1);
        }
        private void UpdateLinks()
        {
            List<LinkModel> linksAdded = new List<LinkModel>();

            foreach (UIElement canvasLinks in ListCanvasLightElement.Select(item => item.Item3))
            {
                CanvasUsed.Children.Remove(canvasLinks);
            }

            foreach (RectangleLinkable rectangle1 in SchematicDisplayed.RectangleLinkables)
            {
                foreach(LinkModel link in rectangle1.Links)
                {
                    if(!linksAdded.Contains(link))
                    {
                        Line newLine = new Line();
                        newLine.Stroke = new SolidColorBrush(Colors.Black);
                        newLine.StrokeThickness = 2.0;
                        newLine.X1 = Canvas.GetLeft(link.ObjectLinkableStart.ShapeInCanvas) + link.ObjectLinkableStart.ShapeInCanvas.Width / 2;
                        newLine.Y1 = Canvas.GetTop(link.ObjectLinkableStart.ShapeInCanvas) + link.ObjectLinkableStart.ShapeInCanvas.Height / 2;
                        newLine.X2 = Canvas.GetLeft(link.ObjectLinkableEnd.ShapeInCanvas) + link.ObjectLinkableEnd.ShapeInCanvas.Width / 2;
                        newLine.Y2 = Canvas.GetTop(link.ObjectLinkableEnd.ShapeInCanvas) + link.ObjectLinkableEnd.ShapeInCanvas.Height / 2;
                        CanvasUsed.Children.Add(newLine);
                        ListCanvasLightElement.Add((link.ObjectLinkableStart, link.ObjectLinkableEnd, newLine));
                        linksAdded.Add(link);

                        TextBlock newTextBlock = new TextBlock();
                        newTextBlock.Text = link.Info;
                        newTextBlock.Visibility = LinkInfoDisplayed ? Visibility.Visible : Visibility.Collapsed;
                        newTextBlock.FontSize = 15;
                        newTextBlock.FontWeight = FontWeights.Bold;
                        CanvasUsed.Children.Add(newTextBlock);
                        Canvas.SetTop(newTextBlock, (newLine.Y1 + newLine.Y2) / 2);
                        Canvas.SetLeft(newTextBlock, (newLine.X1 + newLine.X2) / 2);
                        ListCanvasLightElement.Add((link.ObjectLinkableStart, link.ObjectLinkableEnd, newTextBlock));
                    }
                }
            }
        }
        private void UpdateLinksOfABlock(ObjectLinkableModel objectLinkable)
        {
            List<LinkModel> linksAdded = new List<LinkModel>();

            foreach (UIElement canvasLinks in ListCanvasLightElement.Where(item => item.Item1 == objectLinkable ||
                    item.Item2 == objectLinkable).Select(item => item.Item3))
            {
                CanvasUsed.Children.Remove(canvasLinks);
            }
            foreach (LinkModel link in objectLinkable.Links)
            {
                if (!linksAdded.Contains(link))
                {
                    Line newLine = new Line();
                    newLine.Stroke = new SolidColorBrush(Colors.Black);
                    newLine.StrokeThickness = 2.0;
                    newLine.X1 = Canvas.GetLeft(link.ObjectLinkableStart.ShapeInCanvas) + link.ObjectLinkableStart.ShapeInCanvas.Width / 2;
                    newLine.Y1 = Canvas.GetTop(link.ObjectLinkableStart.ShapeInCanvas) + link.ObjectLinkableStart.ShapeInCanvas.Height / 2;
                    newLine.X2 = Canvas.GetLeft(link.ObjectLinkableEnd.ShapeInCanvas) + link.ObjectLinkableEnd.ShapeInCanvas.Width / 2;
                    newLine.Y2 = Canvas.GetTop(link.ObjectLinkableEnd.ShapeInCanvas) + link.ObjectLinkableEnd.ShapeInCanvas.Height / 2;
                    CanvasUsed.Children.Add(newLine);
                    ListCanvasLightElement.Add((link.ObjectLinkableStart, link.ObjectLinkableEnd, newLine));
                    linksAdded.Add(link);

                    TextBlock newTextBlock = new TextBlock();
                    newTextBlock.Text = link.Info;
                    newTextBlock.Visibility = LinkInfoDisplayed ? Visibility.Visible : Visibility.Collapsed;
                    newTextBlock.FontSize = 15;
                    newTextBlock.FontWeight = FontWeights.Bold;
                    CanvasUsed.Children.Add(newTextBlock);
                    Canvas.SetTop(newTextBlock, (newLine.Y1 + newLine.Y2) / 2);
                    Canvas.SetLeft(newTextBlock, (newLine.X1 + newLine.X2) / 2);
                    ListCanvasLightElement.Add((link.ObjectLinkableStart, link.ObjectLinkableEnd, newTextBlock));
                }
            }
        }
        public void RandomGenerationSchematic(int nbRectangle, int nbLink)
        {
            CanvasUsed.Children.Clear();
            SchematicDisplayed = new SchematicModel();

            RectangleLinkable rectangleLinkable;
            Random rnd = new Random();
            List<int> indexLinkPossible = new List<int> { };
            int MaxIndex = 0;
            for (int i = 0; i < nbRectangle; i++)
            {
                rectangleLinkable = new RectangleLinkable(new MouseButtonEventHandler(GenericRectangle_OnMouseDown));

                SchematicDisplayed.RectangleLinkables.Add(rectangleLinkable);
                CanvasUsed.Children.Add(rectangleLinkable.ShapeInCanvas);
                Canvas.SetLeft(rectangleLinkable.ShapeInCanvas, rnd.Next(0, (int)CanvasUsed.ActualWidth));
                Canvas.SetTop(rectangleLinkable.ShapeInCanvas, rnd.Next(0, (int)CanvasUsed.ActualHeight));

                for (int j = 0; j < nbRectangle; j++)
                {
                    int startingRectangleStart = MaxIndex / (nbRectangle);
                    int startingRectangleEnd = MaxIndex % (nbRectangle);
                    if(startingRectangleEnd * (nbRectangle) + startingRectangleStart < MaxIndex &&
                        startingRectangleStart != startingRectangleEnd)
                    {
                        indexLinkPossible.Add(MaxIndex);
                    }
                    MaxIndex++;
                }
            }
            //link: int index = rectangleStart.index*(nbRectangle-1) + rectangleEnd.index
            Shuffle(rnd, indexLinkPossible);
            List<int> linkToCreate = indexLinkPossible.Take(nbLink).ToList();
            foreach(int indexNewLink in linkToCreate)
            {
                int startingRectangleStart = indexNewLink / (nbRectangle);
                int startingRectangleEnd = indexNewLink % (nbRectangle);

                LinkModel linkModel = new LinkModel(
                    SchematicDisplayed.RectangleLinkables[startingRectangleStart], 
                    SchematicDisplayed.RectangleLinkables[startingRectangleEnd], 
                    "auto-generated: " + startingRectangleStart + " to "+ startingRectangleEnd);

                SchematicDisplayed.RectangleLinkables[startingRectangleStart].Links.Add(linkModel);
                SchematicDisplayed.RectangleLinkables[startingRectangleEnd].Links.Add(linkModel);
            }
            UpdateLinks();
        }
        private void Shuffle<T>(Random rnd, IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public void ToggleDisplayLink()
        {
            LinkInfoDisplayed = !LinkInfoDisplayed;

            foreach(TextBlock textBlock in ListCanvasLightElement.Where(item => item.Item3 is TextBlock).Select(item => item.Item3).ToList())
            {
                textBlock.Visibility = LinkInfoDisplayed ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}