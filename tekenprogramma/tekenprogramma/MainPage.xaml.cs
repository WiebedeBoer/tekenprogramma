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
using System.Windows;
using System.Windows.Input;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Markup;


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
        string type = "Rectangle";

        public MainPage()
        {
            this.InitializeComponent();

            var c = new Canvas();

            Nullable<Point> dragStart = null;

            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Colors.Red);

            var shapes = new UIElement[] {
                new Ellipse() { Fill = myBrush, Width = 100, Height = 100 },
                new Rectangle() { Fill = myBrush, Width = 200, Height = 100 },
            };

            MouseButtonEventHandler mouseDown = (sender, args) => {
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
                    var p2 = args.GetPosition(c);
                    Canvas.SetLeft(element, p2.X - dragStart.Value.X);
                    Canvas.SetTop(element, p2.Y - dragStart.Value.Y);
                }
            };

            Action<UIElement> enableDrag = (element) => {
            //paintSurface enableDrag = (element) => {
                element.MouseDown += mouseDown;
                element.MouseMove += mouseMove;
                element.MouseUp += mouseUp;
            };

            foreach (var shape in shapes)
            {
                enableDrag(shape);
                c.Children.Add(shape);
            }

        }

        /*
        var w = new Window();
        w.Width = 600;
        w.Height = 400;
        var c = new Canvas();      
        */

        /*
        w.Content = c;
        w.ShowDialog();
        */

        //ellipse
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement button = e.OriginalSource as FrameworkElement;
            type = button.Name;
        }

        //rectangle
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FrameworkElement button = e.OriginalSource as FrameworkElement;
            type = button.Name;
        }

        //resize
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

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
