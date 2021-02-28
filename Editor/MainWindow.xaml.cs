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
                /*
                var element = (UIElement)sender;
                dragStart = args.GetPosition(element);
                element.CaptureMouse();
                */

                if (MouseHitType == HitType.None) return;
                LastPoint = Mouse.GetPosition(Canvascek);
                DragInProgress = true;
            };
            MouseButtonEventHandler mouseUp = (sender, args) => {
                /*
                var element = (UIElement)sender;
                dragStart = null;
                element.ReleaseMouseCapture();
                */
                DragInProgress = false;

            };
            MouseEventHandler mouseMove = (sender, args) => {
                /*
                MouseHitType = SetHitType((UIElement)sender, Mouse.GetPosition(Canvascek));
                SetMouseCursor();
                if (dragStart != null && args.LeftButton == MouseButtonState.Pressed)
                {
                    var element = (UIElement)sender;
                    var p2 = args.GetPosition(Canvascek);
                    Canvas.SetLeft(element, p2.X <= 0 ? 0 : Canvas.GetLeft(element) < 0 ? 0 : p2.X - dragStart.Value.X);
                    Canvas.SetTop(element, p2.Y <= 0 ? 0 : Canvas.GetTop(element) < 0 ? 0 : p2.Y - dragStart.Value.Y);
                }*/
                var fe = sender as FrameworkElement;
                if (DragInProgress)
                {
                    // See how much the mouse has moved.
                    Point point = Mouse.GetPosition(Canvascek);
                    double offset_x = point.X - LastPoint.X;
                    double offset_y = point.Y - LastPoint.Y;

                    // Get the rectangle's current position.
                    double new_x = Canvas.GetLeft(fe);
                    double new_y = Canvas.GetTop(fe);
                    double new_width = fe.Width;
                    double new_height = fe.Height;

                    // Update the rectangle.
                    switch (MouseHitType)
                    {
                        case HitType.Body:
                            new_x += offset_x;
                            new_y += offset_y;
                            break;
                        case HitType.UL:
                            new_x += offset_x;
                            new_y += offset_y;
                            new_width -= offset_x;
                            new_height -= offset_y;
                            break;
                        case HitType.UR:
                            new_y += offset_y;
                            new_width += offset_x;
                            new_height -= offset_y;
                            break;
                        case HitType.LR:
                            new_width += offset_x;
                            new_height += offset_y;
                            break;
                        case HitType.LL:
                            new_x += offset_x;
                            new_width -= offset_x;
                            new_height += offset_y;
                            break;
                        case HitType.L:
                            new_x += offset_x;
                            new_width -= offset_x;
                            break;
                        case HitType.R:
                            new_width += offset_x;
                            break;
                        case HitType.B:
                            new_height += offset_y;
                            break;
                        case HitType.T:
                            new_y += offset_y;
                            new_height -= offset_y;
                            break;
                    }

                    // Don't use negative width or height.
                    if ((new_width > 0) && (new_height > 0))
                    {
                        // Update the rectangle.
                        Canvas.SetLeft(fe, new_x);
                        Canvas.SetTop(fe, new_y);
                        fe.Width = new_width;
                        fe.Height = new_height;

                        // Save the mouse's new location.
                        LastPoint = point;
                    }
                }
                else
                {
                    MouseHitType = SetHitType(fe,
                        Mouse.GetPosition(Canvascek));
                    SetMouseCursor();
                }
            };
            MouseEventHandler mouseLeave = (sender, args) => {
                /*
                MouseHitType = HitType.None;
                SetMouseCursor();
                */
            };

            Action<UIElement> enableDrag = (element) => { 
                element.MouseDown += mouseDown; 
                element.MouseMove += mouseMove; 
                element.MouseUp += mouseUp; 
                element.MouseLeave += mouseLeave;
            };


            var rand = new Random();

            Elipsa.Click += (sender, e) => 
            {
                var myElipse = (Ellipse)Create_Element(new Ellipse());

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
                var myKruh = (Ellipse)Create_Element(new Ellipse());
                myKruh.Width = myKruh.Height;
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
                var mySquare = (Rectangle)Create_Element(new Rectangle());
                mySquare.Width = mySquare.Height;
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
                var myRectangle = (Rectangle)Create_Element(new Rectangle());

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


        private UIElement Create_Element(UIElement uIElement)
        {
            var rand = new Random();
            var Width = rand.Next(50, 101);
            var Height = rand.Next(50, 101);
            if (Width == Height)
                Width -= 10;
            uIElement.GetType().GetProperty("Width").SetValue(uIElement, Width);
            uIElement.GetType().GetProperty("Height").SetValue(uIElement, Height);

            SolidColorBrush myColor = new SolidColorBrush();
            myColor.Color = Color.FromRgb(Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)), Convert.ToByte(rand.Next(256)));

            uIElement.GetType().GetProperty("Fill").SetValue(uIElement, myColor);
            uIElement.GetType().GetProperty("StrokeThickness").SetValue(uIElement, 0);
            uIElement.GetType().GetProperty("Stroke").SetValue(uIElement, new SolidColorBrush(Color.FromRgb(255, 255, 255)));

            uIElement.MouseDown += Element_MouseLeftButtonDown;
            return uIElement;
        }


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



        private enum HitType
        {
            None, Body, UL, UR, LR, LL, L, R, T, B
        };

        // The part of the rectangle under the mouse.
        HitType MouseHitType = HitType.None;

        // True if a drag is in progress.
        private bool DragInProgress = false;

        // The drag's last point.
        private Point LastPoint;
        private HitType SetHitType(UIElement element, Point point)
        {
            var fe = element as FrameworkElement;
            double left = Canvas.GetLeft(element);
            double top = Canvas.GetTop(element);
            double right = left + fe.Width;
            double bottom = top + fe.Height;
            if (point.X < left) return HitType.None;
            if (point.X > right) return HitType.None;
            if (point.Y < top) return HitType.None;
            if (point.Y > bottom) return HitType.None;

            const double GAP = 10;
            if (point.X - left < GAP)
            {
                // Left edge.
                if (point.Y - top < GAP) return HitType.UL;
                if (bottom - point.Y < GAP) return HitType.LL;
                return HitType.L;
            }
            else if (right - point.X < GAP)
            {
                // Right edge.
                if (point.Y - top < GAP) return HitType.UR;
                if (bottom - point.Y < GAP) return HitType.LR;
                return HitType.R;
            }
            if (point.Y - top < GAP) return HitType.T;
            if (bottom - point.Y < GAP) return HitType.B;
            return HitType.Body;
        }

        // Set a mouse cursor appropriate for the current hit type.
        private void SetMouseCursor()
        {
            // See what cursor we should display.
            Cursor desired_cursor = Cursors.Arrow;
            switch (MouseHitType)
            {
                case HitType.None:
                    desired_cursor = Cursors.Arrow;
                    break;
                case HitType.Body:
                    desired_cursor = Cursors.ScrollAll;
                    break;
                case HitType.UL:
                case HitType.LR:
                    desired_cursor = Cursors.SizeNWSE;
                    break;
                case HitType.LL:
                case HitType.UR:
                    desired_cursor = Cursors.SizeNESW;
                    break;
                case HitType.T:
                case HitType.B:
                    desired_cursor = Cursors.SizeNS;
                    break;
                case HitType.L:
                case HitType.R:
                    desired_cursor = Cursors.SizeWE;
                    break;
            }

            // Display the desired cursor.
            if (Cursor != desired_cursor) Cursor = desired_cursor;
        }


    }
}
