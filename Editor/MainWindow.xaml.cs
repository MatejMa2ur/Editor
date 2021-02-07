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
        public Canvas Plocha { get; set; }
        public MainWindow()
        {
            InitializeComponent();
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
                Canvascek.Children.Add(myElipse);
                int j = Canvascek.Children.Count;
                int i = 0;
                DataObjektu.Text = "Count: "+j+"\nWidth: " + myElipse.Width + "\n"+"Height: "+myElipse.Height;
                foreach (UIElement item in Canvascek.Children)
                {
                    i++;
                    if (i == j)
                    {
                        Canvas.SetLeft(item, rand.NextDouble() * (Canvascek.ActualWidth - myElipse.Width));
                        Canvas.SetTop(item, rand.NextDouble() * (Canvascek.ActualHeight - myElipse.Height));
                    }
                }
               
            };
        }

        private void Načítať_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text file (*.txt)|*.txt";
            ofd.InitialDirectory = "c:\\";
            if (ofd.ShowDialog() == true)
            {
                if (MessageBox.Show("Do you want to load this items?"+File.ReadAllText(ofd.FileName),"Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Canvascek.Children.Clear();
                    var Source = File.ReadAllText(ofd.FileName).Split("Color\n")[1].Split("\n");
                    foreach(var item in Source)
                    {
                        var splittedRow = item.Split(";");
                        var createItem = new Object();
                        switch (splittedRow[0])
                        {
                            case "System.Windows.Shapes.Ellipse":
                                createItem = new Ellipse();
                                break;
                        }
                        if (splittedRow.Length > 1)
                        {
                            createItem.GetType().GetProperty("Width").SetValue(createItem, Convert.ToInt32(splittedRow[1]));
                            createItem.GetType().GetProperty("Height").SetValue(createItem, Convert.ToInt32(splittedRow[2]));
                            SolidColorBrush brush = new SolidColorBrush();
                            brush.Color = (Color)ColorConverter.ConvertFromString(splittedRow[5]);
                            createItem.GetType().GetProperty("Fill").SetValue(createItem, brush);
                            Canvascek.Children.Add((UIElement)createItem);
                            Canvas.SetLeft((UIElement)createItem, Convert.ToDouble(splittedRow[3]));
                            Canvas.SetTop((UIElement)createItem, Convert.ToDouble(splittedRow[4]));
                        }
                        
                    }
                    
                    
                }
                
            }
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
    }
}
