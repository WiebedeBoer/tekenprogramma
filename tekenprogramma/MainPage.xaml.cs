using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Input;
using System.Collections.Generic;

namespace tekenprogramma
{


    public class MakeRectangle : ICommand
    {
        private double cpx;
        private double cpy;
        private double top;
        private double left;

        public double ReturnSmallest(double first, double last)
        {
            if (first < last)
            {
                return first;
            }
            else
            {
                return last;
            }
        }

        void ICommand.Execute()
        {
            //throw new NotImplementedException();
            Rectangle newRectangle = new Rectangle(); //instance of new rectangle shape
            newRectangle.Height = Math.Abs(cpy - top); //set height
            newRectangle.Width = Math.Abs(cpx - left); //set width
            SolidColorBrush brush = new SolidColorBrush(); //brush
            brush.Color = Windows.UI.Colors.Blue; //standard brush color is blue
            newRectangle.Fill = brush; //fill color
            newRectangle.Name = "Rectangle"; //attach name
            Canvas.SetLeft(newRectangle, ReturnSmallest(left, cpx)); //set left position
            Canvas.SetTop(newRectangle, ReturnSmallest(top, cpy)); //set top position
            //newRectangle.PointerPressed += Drawing_pressed;
            //paintSurface.Children.Add(newRectangle); //add shape to canvas    
        }
    }

    public class MakeEllipse : ICommand
    {
        private double cpx;
        private double cpy;
        private double top;
        private double left;

        public double ReturnSmallest(double first, double last)
        {
            if (first < last)
            {
                return first;
            }
            else
            {
                return last;
            }
        }

        void ICommand.Execute()
        {
            //throw new NotImplementedException();
            Ellipse newEllipse = new Ellipse(); //instance of new ellipse shape
            newEllipse.Height = Math.Abs(cpy - top);//set height
            newEllipse.Width = Math.Abs(cpx - left);//set width
            SolidColorBrush brush = new SolidColorBrush();//brush
            brush.Color = Windows.UI.Colors.Blue;//standard brush color is blue
            newEllipse.Fill = brush;//fill color
            newEllipse.Name = "Ellipse";//attach name
            Canvas.SetLeft(newEllipse, ReturnSmallest(left, cpx));//set left position
            Canvas.SetTop(newEllipse, ReturnSmallest(top, cpy));//set top position
            //newEllipse.PointerPressed += Drawing_pressed;
            //paintSurface.Children.Add(newEllipse); //add shape to canvas
        }
    }


    public sealed partial class MainPage : Page
    {

        //mainpage class variables
        Receiver action = new Receiver();
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
        //List<Actions> redoList = new List<Actions>();

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
            MakeRectangle makerec = new MakeRectangle();
            Rectangle newRectangle = new Rectangle(); //instance of new rectangle shape
            //newRectangle.Height = Math.Abs(cpy - top); //set height
            //newRectangle.Width = Math.Abs(cpx - left); //set width
            //SolidColorBrush brush = new SolidColorBrush(); //brush
            //brush.Color = Windows.UI.Colors.Blue; //standard brush color is blue
            //newRectangle.Fill = brush; //fill color
            //newRectangle.Name = "Rectangle"; //attach name
            //Canvas.SetLeft(newRectangle, ReturnSmallest(left, cpx)); //set left position
            //Canvas.SetTop(newRectangle, ReturnSmallest(top, cpy)); //set top position
            newRectangle.PointerPressed += Drawing_pressed;
            paintSurface.Children.Add(newRectangle); //add shape to canvas

        }

        //make an ellipse
        public void MakeEllipse(double left, double top)
        {
            MakeEllipse makeelip = new MakeEllipse();
            Ellipse newEllipse = new Ellipse(); //instance of new ellipse shape
            //newEllipse.Height = Math.Abs(cpy - top);//set height
            //newEllipse.Width = Math.Abs(cpx - left);//set width
            //SolidColorBrush brush = new SolidColorBrush();//brush
            //brush.Color = Windows.UI.Colors.Blue;//standard brush color is blue
            //newEllipse.Fill = brush;//fill color
            //newEllipse.Name = "Ellipse";//attach name
            //Canvas.SetLeft(newEllipse, ReturnSmallest(left, cpx));//set left position
            //Canvas.SetTop(newEllipse, ReturnSmallest(top, cpy));//set top position
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
            type = button.Name;//fetch button name to put into type
        }

        //group
        private void Group_Click(object sender, RoutedEventArgs e)
        {

        }

        //undo
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            //actionsList.Find(x => x.actionType.Contains("selected"));
            //actionsList.FindLastIndex();
            //public Actions FindLast(Predicate<Actions> match);
            //List<Actions>.FindLast(Predicate<Actions>);
            //actionsList.FindLast(actionsList(Receiver){ });
            ICommand lastCommand = actionsList[actionsList.Count - 1]{ (
            {
                    redoList.Add(lastCommand);
                    actionsList.Remove(lastCommand);
                    //return lc;
                })};
        }

        //redo
        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            ICommand lastCommand = redoList.FindLast(delegate (ICommand nc)
            {
                actionsList.Add(nc);
                redoList.Remove(nc);
                return nc;
            });
        }

        //save
        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        //load
        private void Load_Click(object sender, RoutedEventArgs e)
        {

        }

        //resize

        //move

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