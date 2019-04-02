using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Input;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace tekenprogramma
{

    //main
    public sealed partial class MainPage : Page
    {
        //mainpage class variables
        //Receiver action = new Receiver();
        string type = "Rectangle"; //default shape
        double cpx;
        double cpy;
        bool firstcp = true;
        bool moving = false;
        Rectangle backuprectangle; //rectangle shape
        Ellipse backupellipse; //ellipse shape
        string actionType ="create"; //default action
        private List<ICommand> actionsList = new List<ICommand>();
        private List<ICommand> redoList = new List<ICommand>();
      
        public MainPage()
        {
            InitializeComponent();
        }

        //pressing on canvas
        private void Drawing_pressed(object sender, PointerRoutedEventArgs e)
        {
            FrameworkElement backupprep = e.OriginalSource as FrameworkElement;
            //if shape is rectangle
            if (backupprep.Name == "Rectangle")
            {
                Rectangle tmp = backupprep as Rectangle;
                backuprectangle = tmp;
                type = "Rectangle";
            }
            //else if shape is ellipse
            else if(backupprep.Name == "Ellipse")
            {
                Ellipse tmp = backupprep as Ellipse;
                backupellipse = tmp;
                type = "Ellipse";
            }
            //if move
            if (moving)
            {
                cpx = e.GetCurrentPoint(paintSurface).Position.X; //x coordinate canvas
                cpy = e.GetCurrentPoint(paintSurface).Position.Y; //y coordinate canvas
                if (type == "Rectangle")
                {
                    Canvas.SetLeft(backuprectangle, cpx); //left
                    Canvas.SetTop(backuprectangle, cpy); //top
                    paintSurface.Children.Remove(backuprectangle); //remove the backup
                    paintSurface.Children.Add(backuprectangle); //add the new backup shape
                }
                else if(type == "Ellipse")
                {
                    Canvas.SetLeft(backupellipse, cpx);
                    Canvas.SetTop(backupellipse, cpy);
                    paintSurface.Children.Remove(backupellipse); //remove the backup
                    paintSurface.Children.Add(backupellipse); //add the new backup shape
                }
                moving = !moving;
            }
            //else create
            else
            {
                if (firstcp)
                {
                    cpx = e.GetCurrentPoint(paintSurface).Position.X; //left position
                    cpy = e.GetCurrentPoint(paintSurface).Position.Y; //top position
                }
                else
                {
                    if (type == "Rectangle")
                    {
                        MakeRectangle(e.GetCurrentPoint(paintSurface).Position.X, e.GetCurrentPoint(paintSurface).Position.Y); //create rectangle
                    }
                    else
                    {
                        MakeEllipse(e.GetCurrentPoint(paintSurface).Position.X, e.GetCurrentPoint(paintSurface).Position.Y); //create ellipse
                    }
                }
                firstcp = !firstcp;
            }
        }

        //make a rectangle
        public void MakeRectangle(double left, double top)
        {
            string action = "makerectangle";
            Receiver receiver = new Receiver(action);
            MakeRectangle makerec = new MakeRectangle();
            Rectangle newRectangle = new Rectangle(); //instance of new rectangle shape            
            receiver.Actions(action);
            makerec.Execute();
            newRectangle.PointerPressed += Drawing_pressed;
            paintSurface.Children.Add(newRectangle); //add shape to canvas
        }

        //make an ellipse
        public void MakeEllipse(double left, double top)
        {
            string action = "makeelipse";
            Receiver receiver = new Receiver(action);
            MakeEllipse makeelip = new MakeEllipse();
            Ellipse newEllipse = new Ellipse(); //instance of new ellipse shape
            
            receiver.Actions(action);
            makeelip.Execute();
            newEllipse.PointerPressed += Drawing_pressed;
            paintSurface.Children.Add(newEllipse); //add shape to canvas            
        }

        //moving shape
        private void Move_Click(object sender, RoutedEventArgs e)
        {
            moving = !moving;
        }

        //resize shape
        private void Resize_Click(object sender, RoutedEventArgs e)
        {
            //if rectangle
            if (type == "Rectangle")
            {
                paintSurface.Children.Remove(backuprectangle);
                backuprectangle.Height = Convert.ToDouble(Height.Text); //set width
                backuprectangle.Width = Convert.ToDouble(Width.Text); //set height
                paintSurface.Children.Add(backuprectangle); //add to canvas
            }
            //else if ellipse
            else if (type == "Ellipse")
            {
                paintSurface.Children.Remove(backupellipse);
                backupellipse.Height = Convert.ToDouble(Height.Text); //set width
                backupellipse.Width = Convert.ToDouble(Width.Text); //set height
                paintSurface.Children.Add(backupellipse); //add to canvas
            }
        }

        //first or last click
        public double ReturnSmallest(double first, double last)
        {
            if(first < last)
            {
                return first;
            }
            else
            {
                return last;
            }
        }


        //elipse
        private void Elipse_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement button = e.OriginalSource as FrameworkElement;
            type = button.Name; //fetch button name to put into type
        }

        //rectangle
        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement button = e.OriginalSource as FrameworkElement;
            type = button.Name; //fetch button name to put into type
        }

        //ornament
        private void Ornament_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement button = e.OriginalSource as FrameworkElement;
            type = button.Name; //fetch button name to put into type
        }

        //group
        private void Group_Click(object sender, RoutedEventArgs e)
        {

        }

        //undo
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
           int LastInList = actionsList.Count - 1;
           ICommand lastcommand = actionsList[LastInList]; //find last command                      
           redoList.Add(lastcommand); //add to redo list
           actionsList.RemoveAt(LastInList); //remove from undo list                           
        }

        //redo
        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            int LastInList = redoList.Count - 1;
            ICommand lastcommand = redoList[LastInList]; //find last command
            actionsList.Add(lastcommand); //add to undo list
            redoList.RemoveAt(LastInList); //remove from redo list
        }

        //save
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string[] lines;
            var list = new List<string>();
            var fileStream = new FileStream("file.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    foreach (var c in paintSurface.Children.OfType<Rectangle>())
                    {
                        c.Shape = Rectangle;
                        line = c.Shape;
                        list.Add(line);
                    }
                }
            }
            lines = list.ToArray();
        }

        //load
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            string[] lines;
            var list = new List<string>();
            var fileStream = new FileStream("file.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                    //if rectangle
                    if (line.Contains("rectangle"))
                    {
                        backuprectangle.Height = Convert.ToDouble(Height.Text); //set width
                        backuprectangle.Width = Convert.ToDouble(Width.Text); //set height
                        paintSurface.Children.Add(backuprectangle); //add to canvas
                    }
                    //else if ellipse
                    else if (line.Contains("ellipse"))
                    {
                        backupellipse.Height = Convert.ToDouble(Height.Text); //set width
                        backupellipse.Width = Convert.ToDouble(Width.Text); //set height
                        paintSurface.Children.Add(backupellipse); //add to canvas
                    }
                }
            }
            lines = list.ToArray();
        }

        private void Front_canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            //front_canvas.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(0,0,0,0));
        }

        private void Width_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        private void Height_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}