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
        #region Fields
        Ellipse object1;
        Ellipse object2;
        Polyline trajectory;
        #endregion

        #region Getter/Setter
        public Ellipse Object1 { get => object1; set => object1 = value; }
        public Ellipse Object2 { get => object2; set => object2 = value; }
        public Polyline Trajectory { get => trajectory; set => trajectory = value; }
        #endregion

        public VisualEngine()
        {
            Object1 = new Ellipse();
            Object2 = new Ellipse();
            Trajectory = new Polyline();
        }

        public void InitializeStaticObject(int object2Size)
        {
            Object2.Fill = new SolidColorBrush(Colors.Orange);
            Object2.Width = object2Size;
            Object2.Height = object2Size;
        }

        public void InitializeDynamicObject(int object1Size, Color color)
        {
            Object1.Fill = new SolidColorBrush(color);
            Object1.Width = object1Size;
            Object1.Height = object1Size;

            Trajectory.Stroke = new SolidColorBrush(color);
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
