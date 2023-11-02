using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _2bodysim
{
    public class VisualEngine
    {
        Ellipse object1;
        Ellipse object2;
        Polyline trajectory;
        public Ellipse Object1 { get => object1; set => object1 = value; }
        public Ellipse Object2 { get => object2; set => object2 = value; }
        public Polyline Trajectory { get => trajectory; set => trajectory = value; }

        public VisualEngine()
        {
            Object1 = new Ellipse();
            Object2 = new Ellipse();
            Trajectory = new Polyline();
        }

        public void Initialize(int object1Size, int object2Size)
        {
            Object1.Fill = new SolidColorBrush(Colors.Blue);
            Object1.Width = object1Size;
            Object1.Height = object1Size;

            Object2.Fill = new SolidColorBrush(Colors.Orange);
            Object2.Width = object2Size;
            Object2.Height = object2Size;

            Trajectory.Stroke = Brushes.Blue;
            Trajectory.StrokeThickness = 1;
        }

        public void DrawOrbitLine(Point point)
        {
            Trajectory.Points.Add(point);
        }

        public void ClearOrbitLine()
        {
            Trajectory.Points.Clear();
        }
    }
}
