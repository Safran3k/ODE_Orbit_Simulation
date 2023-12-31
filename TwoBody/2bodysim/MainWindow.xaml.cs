﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _2bodysim
{
    public partial class MainWindow : Window
    {
        PhysicsEngine physicsEngine;
        VisualEngine visualEngine;
        DispatcherTimer timer;
        int remainingSeconds = 90;
        bool isSimulationRunning = false;
        SolversEnum selectedSolver;

        public MainWindow()
        {
            InitializeComponent();
            cbSolvers.ItemsSource = Enum.GetValues(typeof(SolversEnum));

            visualEngine = new VisualEngine();
            visualEngine.InitializeStaticObject(20);
            visualEngine.InitializeDynamicObject(20, Colors.Blue);

            Canvas.SetLeft(visualEngine.Object1, (int)(mainCanvas.Width / 2));
            Canvas.SetTop(visualEngine.Object1, (int)(mainCanvas.Height / 2));
            mainCanvas.Children.Add(visualEngine.Object1);

            Canvas.SetLeft(visualEngine.Object2, (int)(mainCanvas.Width / 2));
            Canvas.SetTop(visualEngine.Object2, (int)(mainCanvas.Height / 2));
            mainCanvas.Children.Add(visualEngine.Object2);

            mainCanvas.Children.Add(visualEngine.Trajectory);
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (remainingSeconds > 0)
            {
                remainingSeconds--;
                UpdateTimerLabel();
            }
            else
            {
                timer.Stop();
                CompositionTarget.Rendering -= StartAnimation;
                lbInfo.Content = "A szimuláció véget ért.";
            }
        }

        private void UpdateTimerLabel()
        {
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            lbInfo.Content = $"Fennmaradt idő: {minutes:D2}:{seconds:D2}";
        }

        private void UpdateMeasurementsLabel(double x, double vx, double y, double vy)
        {
            lbX.Content = $"x: {x:F2}";
            lbY.Content = $"Vx: {vx:F2}";
            lbVx.Content = $"y: {y:F2}";
            lbVy.Content = $"Vy: {vy:F2}";
        }

        private void StartAnimation(object sender, EventArgs e)
        {
            double[] result = physicsEngine.UpdateOrbit();
            double x = (Math.Sin(result[0]) * 120) + result[0];
            double y = (Math.Cos(result[2]) * 120) + result[2];
            UpdateMeasurementsLabel(x, result[1], y, result[3]);
            visualEngine.DrawOrbitLine(new Point(x + 10, y + 10)); // The +10 is because the size of the ball is 20 pixels
            Canvas.SetLeft(visualEngine.Object1, x);
            Canvas.SetTop(visualEngine.Object1, y);
        }

        private void ResetSimulation()
        {
            if (mainCanvas.Children.Count > 1)
            {
                visualEngine.InitializeStaticObject(20);
                visualEngine.InitializeDynamicObject(20, Colors.Blue);
                isSimulationRunning = false;
                remainingSeconds = 60;
                lbX.Content = "x: 0.00";
                lbY.Content = "y: 0.00";
                lbVx.Content = "Vx: 0.00";
                lbVy.Content = "Vy: 0.00";
                Canvas.SetLeft(visualEngine.Object2, (int)(mainCanvas.Width / 2));
                Canvas.SetTop(visualEngine.Object2, (int)(mainCanvas.Height / 2));
                visualEngine.ClearOrbitLine();
                //physicsEngine.ResetSimulation((int)(mainCanvas.Width / 2), (int)(mainCanvas.Height / 2));
                physicsEngine = new PhysicsEngine(
                        selectedSolver,
                        (int)(mainCanvas.Width / 2),
                        20,
                        (int)(mainCanvas.Height / 2),
                        20,
                        0.001
                    );
            }
        }



        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!isSimulationRunning)
            {
                if (visualEngine == null)
                {
                    visualEngine = new VisualEngine();
                    visualEngine.InitializeStaticObject(20);
                    visualEngine.InitializeDynamicObject(20, Colors.Blue);
                }

                physicsEngine = new PhysicsEngine(
                        selectedSolver,
                        (int)(mainCanvas.Width / 2),
                        20,
                        (int)(mainCanvas.Height / 2),
                        20,
                        0.001
                    );

                isSimulationRunning = true;
                InitializeTimer();
                timer.Tick += Timer_Tick;
                timer.Start();
                CompositionTarget.Rendering += StartAnimation;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetSimulation();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cbSolvers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedSolver = (SolversEnum)((ComboBox)sender).SelectedItem;
            if (isSimulationRunning)
            {
                ResetSimulation();
                isSimulationRunning = !isSimulationRunning;
            }
        }
    }
}
