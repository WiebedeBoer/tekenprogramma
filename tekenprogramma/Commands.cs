﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Input;
using System.Collections.Generic;

namespace tekenprogramma
{
    //interface command
    public interface ICommand
    {
        void Execute();
    }

    //class commands
    public class Commands
    {
        private double cpx;
        private double cpy;
        private double top;
        private double left;

        Rectangle backuprectangle; //rectangle shape
        Ellipse backupellipse; //ellipse shape

        string type = "Rectangle"; //default shape

        //give smallest
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

        //create rectangle
        public void MakeRectangle()
        {
            Rectangle newRectangle = new Rectangle(); //instance of new rectangle shape
            newRectangle.Height = Math.Abs(cpy - top); //set height
            newRectangle.Width = Math.Abs(cpx - left); //set width
            SolidColorBrush brush = new SolidColorBrush(); //brush
            brush.Color = Windows.UI.Colors.Blue; //standard brush color is blue
            newRectangle.Fill = brush; //fill color
            newRectangle.Name = "Rectangle"; //attach name
            Canvas.SetLeft(newRectangle, ReturnSmallest(left, cpx)); //set left position
            Canvas.SetTop(newRectangle, ReturnSmallest(top, cpy)); //set top position 
        }

        //create ellipse
        public void MakeEllipse()
        {
            Ellipse newEllipse = new Ellipse(); //instance of new ellipse shape
            newEllipse.Height = Math.Abs(cpy - top);//set height
            newEllipse.Width = Math.Abs(cpx - left);//set width
            SolidColorBrush brush = new SolidColorBrush();//brush
            brush.Color = Windows.UI.Colors.Blue;//standard brush color is blue
            newEllipse.Fill = brush;//fill color
            newEllipse.Name = "Ellipse";//attach name
            Canvas.SetLeft(newEllipse, ReturnSmallest(left, cpx));//set left position
            Canvas.SetTop(newEllipse, ReturnSmallest(top, cpy));//set top position
        }

        //undo
        public void Undo()
        {
            int LastInList = actionsList.Count - 1;
            ICommand lastcommand = actionsList[LastInList];
            redoList.Add(lastcommand); //add to redo list
            actionsList.RemoveAt(LastInList); //remove from undo list   
        }

        //redo
        public void Redo()
        {
            int LastInList = redoList.Count - 1;
            ICommand lastcommand = redoList[LastInList]; //find last command
            actionsList.Add(lastcommand); //add to undo list
            redoList.RemoveAt(LastInList); //remove from redo list
        }

        //resize
        public void Resize()
        {
            //if rectangle
            if (type == "Rectangle")
            {
                double top = Canvas.GetTop(c as FrameworkElement);
                double left = Canvas.GetLeft(c as FrameworkElement);
                double width = (c as FrameworkElement).Width;
                double height = (c as FrameworkElement).Height;
                backuprectangle.Height = Convert.ToDouble(Height.Text); //set width
                backuprectangle.Width = Convert.ToDouble(Width.Text); //set height
                
            }
            //else if ellipse
            else if (type == "Ellipse")
            {
                double top = Canvas.GetTop(c as FrameworkElement);
                double left = Canvas.GetLeft(c as FrameworkElement);
                double width = (c as FrameworkElement).Width;
                double height = (c as FrameworkElement).Height;
                backupellipse.Height = Convert.ToDouble(Height.Text); //set width
                backupellipse.Width = Convert.ToDouble(Width.Text); //set height
                
            }
        }

        //moving
        public void Moving()
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
            else if (type == "Ellipse")
            {
                Canvas.SetLeft(backupellipse, cpx);
                Canvas.SetTop(backupellipse, cpy);
                paintSurface.Children.Remove(backupellipse); //remove the backup
                paintSurface.Children.Add(backupellipse); //add the new backup shape
            }
            moving = !moving;
        }

    }


    //class undo
    public class Undo : ICommand
    {
        private Commands mycommand;

        public Undo(Commands mycommand)
        {
            this.mycommand = mycommand;
        }

        public void Execute()
        {
            mycommand.Undo();
        }
    }

    //class redo
    public class Redo : ICommand
    {
        private Commands mycommand;

        public Redo(Commands mycommand)
        {
            this.mycommand = mycommand;
        }

        public void Execute()
        {
            mycommand.Redo();
        }
    }


    //class moving
    public class Moving : ICommand
    {
        private Commands mycommand;

        public Moving(Commands mycommand)
        {
            this.mycommand = mycommand;
        }

        public void Execute()
        {
            mycommand.Moving();
        }
    }

    //class resize
    public class Resize : ICommand
    {
        private Commands mycommand;

        public Resize(Commands mycommand)
        {
            this.mycommand = mycommand;
        }

        public void Execute()
        {
            mycommand.Resize();
        }
    }

    //class make rectangle
    public class MakeRectangle : ICommand
    {
        private Commands mycommand;

        public MakeRectangle(Commands mycommand)
        {
            this.mycommand = mycommand;
        }

        public void Execute()
        {
            mycommand.MakeRectangle();
        }
    }

    //class make ellipse
    public class MakeEllipse : ICommand
    {
        private Commands mycommand;

        public MakeEllipse(Commands mycommand)
        {
            this.mycommand = mycommand;
        }

        public void Execute()
        {
            mycommand.MakeEllipse();
        }
    }
}
