using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            Nullable<Point> dragStart = null;

            MouseButtonEventHandler mouseDown = (sender, args) => {
                foreach (var item in Canvascek.Children)
                {
                    item.GetType().GetProperty("Stroke").SetValue(item, new SolidColorBrush(Color.FromRgb(255, 255, 255)));
                    item.GetType().GetProperty("StrokeThickness").SetValue(item, 0);
                }
                sender.GetType().GetProperty("Stroke").SetValue(sender, new SolidColorBrush(Color.FromRgb(0, 0, 0)));
                sender.GetType().GetProperty("StrokeThickness").SetValue(sender, 2);

                DataObjektu.Text = sender.GetType().ToString().Replace("System.Windows.Shapes.", "") + "\nWidth: " + sender.GetType().GetProperty("Width").GetValue(sender) + "\n" + "Height: " + sender.GetType().GetProperty("Height").GetValue(sender);

                var element = (UIElement)sender;
                dragStart = args.GetPosition(element);
                element.CaptureMouse();
            };
            MouseButtonEventHandler mouseUp = (sender, args) => {
                var element = (UIElement)sender;
                dragStart = null;
                element.ReleaseMouseCapture();
            };
            MouseEventHandler mouseMove = (sender, args) => {
                if (dragStart != null && args.LeftButton == MouseButtonState.Pressed)
                {
                    var element = (UIElement)sender;
                    var p2 = args.GetPosition(Canvascek);
                    Canvas.SetLeft(element, p2.X - dragStart.Value.X);
                    Canvas.SetTop(element, p2.Y - dragStart.Value.Y);
                }
            };
            Action<UIElement> enableDrag = (element) => { element.MouseDown += mouseDown; element.MouseMove += mouseMove; element.MouseUp += mouseUp;};


            var rand = new Random();

            Elipsa.Click += (sender, e) => 
            {
                var myElipse = new Ellipse();
                myElipse.Width = rand.Next(50, 101);
                myElipse.Height = rand.Next(50, 101);
                if (myElipse.Width == myElipse.Height)
                    myElipse.Width -= 10;
                SolidColorBrush myColor = new SolidColorBrush();
                myColor.Color = Color.FromRgb(Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)));
                myElipse.Fill = myColor;
                myElipse.StrokeThickness = 0;
                myElipse.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));

                enableDrag(myElipse);

                Canvascek.Children.Add(myElipse);

                int j = Canvascek.Children.Count;
                int i = 0;
                DataObjektu.Text = myElipse.GetType().ToString().Replace("System.Windows.Shapes.","") +"\nWidth: " + myElipse.Width + "\n"+"Height: "+myElipse.Height;
                foreach (UIElement item in Canvascek.Children)
                {
                    var that = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    item.GetType().GetProperty("Stroke").SetValue(item, that);
                    item.GetType().GetProperty("StrokeThickness").SetValue(item, 0);
                    i++;
                    if (i == j)
                    {
                        Canvas.SetLeft(item, rand.NextDouble() * (Canvascek.ActualWidth - myElipse.Width));
                        Canvas.SetTop(item, rand.NextDouble() * (Canvascek.ActualHeight - myElipse.Height));
                    }
                }
               
            };

            Kruh.Click += (sender, e) => 
            {
                var myKruh = new Ellipse();
                myKruh.Width = rand.Next(50, 101);
                myKruh.Height = myKruh.Width;

                SolidColorBrush myColor = new SolidColorBrush();
                myColor.Color = Color.FromRgb(Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)));
                myKruh.Fill = myColor;
                myKruh.StrokeThickness = 0;
                myKruh.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));

                enableDrag(myKruh);

                Canvascek.Children.Add(myKruh);

                int j = Canvascek.Children.Count;
                int i = 0;
                DataObjektu.Text = myKruh.GetType().ToString().Replace("System.Windows.Shapes.", "") + "\nWidth: " + myKruh.Width + "\n" + "Height: " + myKruh.Height;
                foreach (UIElement item in Canvascek.Children)
                {
                    var that = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    item.GetType().GetProperty("Stroke").SetValue(item, that);
                    item.GetType().GetProperty("StrokeThickness").SetValue(item, 0);
                    i++;
                    if (i == j)
                    {
                        Canvas.SetLeft(item, rand.NextDouble() * (Canvascek.ActualWidth - myKruh.Width));
                        Canvas.SetTop(item, rand.NextDouble() * (Canvascek.ActualHeight - myKruh.Height));
                    }
                }
            };

            Stvorec.Click += (sender, e) =>
            {
                var mySquare = new Rectangle();
                mySquare.Width = rand.Next(50, 101);
                mySquare.Height = mySquare.Width;

                SolidColorBrush myColor = new SolidColorBrush();
                myColor.Color = Color.FromRgb(Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)));
                mySquare.Fill = myColor;
                mySquare.StrokeThickness = 0;
                mySquare.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));

                enableDrag(mySquare);

                Canvascek.Children.Add(mySquare);

                int j = Canvascek.Children.Count;
                int i = 0;
                DataObjektu.Text = mySquare.GetType().ToString().Replace("System.Windows.Shapes.", "") + "\nWidth: " + mySquare.Width + "\n" + "Height: " + mySquare.Height;
                foreach (UIElement item in Canvascek.Children)
                {
                    var that = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    item.GetType().GetProperty("Stroke").SetValue(item, that);
                    item.GetType().GetProperty("StrokeThickness").SetValue(item, 0);
                    i++;
                    if (i == j)
                    {
                        Canvas.SetLeft(item, rand.NextDouble() * (Canvascek.ActualWidth - mySquare.Width));
                        Canvas.SetTop(item, rand.NextDouble() * (Canvascek.ActualHeight - mySquare.Height));
                    }
                }
            };

            Obdlznik.Click += (sender, e) =>
            {
                var myRectangle = new Rectangle();
                myRectangle.Width = rand.Next(50, 101);
                myRectangle.Height = rand.Next(50, 101);
                if (myRectangle.Width == myRectangle.Height)
                    myRectangle.Width -= 10;

                SolidColorBrush myColor = new SolidColorBrush();
                myColor.Color = Color.FromRgb(Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)));
                myRectangle.Fill = myColor;
                myRectangle.StrokeThickness = 0;
                myRectangle.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));

                enableDrag(myRectangle);

                Canvascek.Children.Add(myRectangle);

                int j = Canvascek.Children.Count;
                int i = 0;
                DataObjektu.Text = myRectangle.GetType().ToString().Replace("System.Windows.Shapes.", "") + "\nWidth: " + myRectangle.Width + "\n" + "Height: " + myRectangle.Height;
                foreach (UIElement item in Canvascek.Children)
                {
                    var that = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    item.GetType().GetProperty("Stroke").SetValue(item, that);
                    item.GetType().GetProperty("StrokeThickness").SetValue(item, 0);
                    i++;
                    if (i == j)
                    {
                        Canvas.SetLeft(item, rand.NextDouble() * (Canvascek.ActualWidth - myRectangle.Width));
                        Canvas.SetTop(item, rand.NextDouble() * (Canvascek.ActualHeight - myRectangle.Height));
                    }
                }
            };

            Načítať.Click += (sender, e) => 
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Text file (*.txt)|*.txt";
                ofd.InitialDirectory = "c:\\";
                if (ofd.ShowDialog() == true)
                {
                    if (MessageBox.Show("Do you want to load this items?" + File.ReadAllText(ofd.FileName), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Canvascek.Children.Clear();
                        var Source = File.ReadAllText(ofd.FileName).Split("Color\n")[1].Split("\n");
                        foreach (var item in Source)
                        {
                            var splittedRow = item.Split(";");
                            var createItem = new Object();
                            switch (splittedRow[0])
                            {
                                case "System.Windows.Shapes.Ellipse":
                                    createItem = new Ellipse();
                                    break;
                                case "System.Windows.Shapes.Rectangle":
                                    createItem = new Rectangle();
                                    break;
                            }
                            if (splittedRow.Length > 1)
                            {
                                createItem.GetType().GetProperty("Width").SetValue(createItem, Convert.ToInt32(splittedRow[1]));
                                createItem.GetType().GetProperty("Height").SetValue(createItem, Convert.ToInt32(splittedRow[2]));
                                SolidColorBrush brush = new SolidColorBrush();
                                brush.Color = (Color)ColorConverter.ConvertFromString(splittedRow[5]);
                                createItem.GetType().GetProperty("Fill").SetValue(createItem, brush);
                                enableDrag((UIElement)createItem);
                                Canvascek.Children.Add((UIElement)createItem);
                                Canvas.SetLeft((UIElement)createItem, Convert.ToDouble(splittedRow[3]));
                                Canvas.SetTop((UIElement)createItem, Convert.ToDouble(splittedRow[4]));
                            }

                        }


                    }

                }
            };
        }


        /* //Zlepšenie - skrátenie kódu
        private UIElement Create_Element(UIElement uIElement)
        {
            var rand = new Random();
            uIElement.GetType().GetProperty("Width").SetValue(uIElement, that);
            uIElement.Width = rand.Next(50, 101);
            myElipse.Height = rand.Next(50, 101);
            if (myElipse.Width == myElipse.Height)
                myElipse.Width -= 10;
            SolidColorBrush myColor = new SolidColorBrush();
            myColor.Color = Color.FromRgb(Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)));
            myElipse.Fill = myColor;
            myElipse.StrokeThickness = 0;
            myElipse.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            myElipse.MouseDown += Element_MouseLeftButtonDown;
            return uIElement;
        }*/


        private void Uložiť_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.RestoreDirectory = true;
            var Source = "Type;Width;Height;LeftMargin;TopMargin;FillColor\n";
            if (sfd.ShowDialog()==true)
            {
                foreach(UIElement item in Canvascek.Children)
                {
                    Source += item.GetType()+";"+item.RenderSize +";" + Canvas.GetLeft(item)+";"+Canvas.GetTop(item)+";"+ item.GetType().GetProperty("Fill").GetValue(item) + "\n";
                  
                }
                File.WriteAllText(sfd.FileName, Source);
            }
        }

        private void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach ( var item in Canvascek.Children)
            {
                item.GetType().GetProperty("Stroke").SetValue(item, new SolidColorBrush(Color.FromRgb(255, 255, 255)));
                item.GetType().GetProperty("StrokeThickness").SetValue(item, 0);
            }
            sender.GetType().GetProperty("Stroke").SetValue(sender,new SolidColorBrush(Color.FromRgb(0,0,0)));
            sender.GetType().GetProperty("StrokeThickness").SetValue(sender, 2);

            DataObjektu.Text = sender.GetType().ToString().Replace("System.Windows.Shapes.","") + "\nWidth: " + sender.GetType().GetProperty("Width").GetValue(sender) + "\n" + "Height: " + sender.GetType().GetProperty("Height").GetValue(sender);
            
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Canvascek.Children.Clear();
        }

    }
}
