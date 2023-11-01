using _2bodysim.ODE_Solvers;
using System;
using System.Collections.Generic;
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

namespace _2bodysim
{
    public partial class MainWindow : Window
    {
        PhysicsEngine physicsEngine;
        VisualEngine visualEngine;

        public MainWindow()
        {
            InitializeComponent();
            physicsEngine = new PhysicsEngine((int)(canvas.Width / 2), 20, (int)(canvas.Height / 2), 20, 0.001);
            visualEngine = new VisualEngine();
            visualEngine.Initialize(20, 20);

            Canvas.SetLeft(visualEngine.Object1, (int)(canvas.Width / 2));
            Canvas.SetTop(visualEngine.Object1, (int)(canvas.Height / 2));
            canvas.Children.Add(visualEngine.Object1);

            Canvas.SetLeft(visualEngine.Object2, (int)(canvas.Width / 2));
            Canvas.SetTop(visualEngine.Object2, (int)(canvas.Height / 2));
            canvas.Children.Add(visualEngine.Object2);
        }

        private void canvas_Loaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering += StartAnimation;
        }

        private void StartAnimation(object sender, EventArgs e)
        {
            double[] results = physicsEngine.UpdateOrbit();
            double x = results[0];
            double y = results[2];
            Canvas.SetLeft(visualEngine.Object1, (Math.Sin(x) * 120) + x);
            Canvas.SetTop(visualEngine.Object1, (Math.Cos(y) * 120) + y);
        }
    }
}
