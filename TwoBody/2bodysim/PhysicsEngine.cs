using _2bodysim.ODE_Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2bodysim
{
    public class PhysicsEngine
    {
        #region Fields
        double[] xx;
        double time;
        double dt;
        double r;
        ODESolverRK4.Function[] F;
        #endregion

        #region Getter/Setter
        public double Time { get => time; set => time = value; }
        public double Dt { get => dt; set => dt = value; }
        #endregion

        public PhysicsEngine(double x0, double v0x, double y0, double v0y, double dt)
        {
            xx = new double[4] { x0, v0x, y0, v0y };
            Time = 0;
            Dt = dt;
            F = new ODESolverRK4.Function[4] { F1, F2, F3, F4 };
        }

        private double F1(double[] xx, double t)
        {
            return xx[1];
        }

        private double F2(double[] xx, double t)
        {
            return -xx[0] / Math.Pow(r, 3);
        }

        private double F3(double[] xx, double t)
        {
            return xx[3];
        }

        private double F4(double[] xx, double t)
        {
            return -xx[2] / Math.Pow(r, 3);
        }

        public double[] UpdateOrbit()
        {
            r = Math.Sqrt(Math.Pow(xx[0], 2) + Math.Pow(xx[2], 2));
            double[] result = ODESolverRK4.RungeKutta4(F, xx, Time, Dt);
            xx = result;
            Time += Dt;
            return result;
        }

        public void ResetSimulation(double borderWidth, double borderHeight)
        {
            xx[0] = borderWidth;
            xx[1] = 20;
            xx[2] = borderHeight;
            xx[3] = 20;
        }
    }
}
