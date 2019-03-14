using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace tekenprogramma
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        double height = 1;
        double width = 1;
        string type = "Rectangle";
        double cpx;
        double cpy;
        bool firstcp = true;
        int elements;

        public MainPage()
        {
            this.InitializeComponent();
        }

        
        public void MyMouseDoubleClickEvent(object sender, RoutedEventArgs e)
        {
            // Get mouse position
            FrameworkElement button = e.OriginalSource as FrameworkElement;
            type = button.Name;
            Rectangle.Content = type;
            //double x = e.GetCurrentPoint(front_canvas).Position.X;
            //double y = e.GetCurrentPoint(front_canvas).Position.Y;
            // Initialize a new Rectangle
            //Rectangle r = new Rectangle();

            // Set up rectangle's size
            //r.Width = 5;
            //r.Height = 5;

            // Set up the Background color
            //r.Fill = Brushes.Black;

            // Set up the position in the window, at mouse coordonate
            //Canvas.SetTop(r, p.Y);
            //Canvas.SetLeft(r, p.X);

            // Add rectangle to the Canvas
            //ink_canvas.Children.Add(r);
        }

        private void Ink_canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (firstcp)
            {
                cpx = e.GetCurrentPoint(front_canvas).Position.X;
                cpy = e.GetCurrentPoint(front_canvas).Position.Y;
                Rectangle.Content = "Klik 1";
            }
            else
            {
                height = Math.Abs(cpy - e.GetCurrentPoint(front_canvas).Position.Y);
                width = Math.Abs(cpx - e.GetCurrentPoint(front_canvas).Position.X);
                if(type == "Rectangle")
                {
                    Rectangle tmp = new Rectangle();
                    tmp.Width = width;
                    tmp.Height = height;
                    tmp.Opacity = 1;
                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Windows.UI.Colors.Blue;
                    tmp.Fill = brush;
                    tmp.Stroke = brush;
                    Canvas.SetLeft(tmp, cpx);
                    Canvas.SetTop(tmp, cpy);
                    tmp.PointerPressed += Ink_canvas_PointerPressed;
                    front_canvas.Children.Add(tmp);
                    Rectangle.Content = "Klik 2";
                }
                else
                {
                    Ellipse tmp = new Ellipse();
                    tmp.Width = width;
                    tmp.Height = height;
                    tmp.Opacity = 1;
                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Windows.UI.Colors.Blue;
                    tmp.Fill = brush;
                    tmp.Stroke = brush;
                    tmp.Name = elements.ToString();
                    Canvas.SetLeft(tmp, cpx);
                    Canvas.SetTop(tmp, cpy);
                    tmp.PointerPressed += Ink_canvas_PointerPressed;
                    front_canvas.Children.Add(tmp);
                    Rectangle.Content = "Klik 2";
                }
            }
            firstcp = !firstcp;
        }
    }
}
