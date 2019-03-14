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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Windows.Input;
using System.Windows;
//using System.Drawing;
//using System.Windows.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace tekenprogramma
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /*
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            var dataElement = row?.DataContext as MyItem;
            if (dataElement?.CanDoubleClick != true)
                return;

            // process double-click here
            return;
        }
        */

            /*
        void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                Close();
            }
        }
        */

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        /*
        private void MC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        */

        private void Ink_canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            double x = e.GetCurrentPoint(paintSurface).Position.X;
            double y = e.GetCurrentPoint(paintSurface).Position.Y;
        }

        private void Front_canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            front_canvas.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(0,0,0,0));
        }

        /*
    private void Canvas_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
            currentPoint = e.GetPosition(this);
    }

    private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            Line line = new Line();

            line.Stroke = SystemColors.WindowFrameBrush;
            line.X1 = currentPoint.X;
            line.Y1 = currentPoint.Y;
            line.X2 = e.GetPosition(this).X;
            line.Y2 = e.GetPosition(this).Y;

            currentPoint = e.GetPosition(this);

            paintSurface.Children.Add(line);
        }
    }
    */

        /*
        public void MyMouseDoubleClickEvent(object sender, Windows.UI.Xaml.Input.Mouse e)
        {
            // Get mouse position
            Point p = Mouse.GetPosition(front_canvas);

            // Initialize a new Rectangle
            Rectangle r = new Rectangle();

            // Set up rectangle's size
            r.Width = 5;
            r.Height = 5;

            // Set up the Background color
            r.Fill = Brushes.Black;

            // Set up the position in the window, at mouse coordonate
            Canvas.SetTop(r, p.Y);
            Canvas.SetLeft(r, p.X);

            // Add rectangle to the Canvas
            ink_canvas.Children.Add(r);
        }
        */

    }
}
