using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2bodysim.ODE_Solvers
{
    class ODESolverARK4
    {
        public delegate double Function(double[] x, double t);

        public static double[] ARungeKutta4(Function[] f, double[] x0, double t0, double dt)
        {
            int n = x0.Length;
            double[] k1 = new double[n];
            double[] k2 = new double[n];
            double[] k3 = new double[n];
            double[] k4 = new double[n];

            double t = t0;
            double[] x1 = new double[n];
            double[] x = x0;

            for (int i = 0; i < n; i++) { k1[i] = dt * f[i](x, t); }

            for (int i = 0; i < n; i++) { x1[i] = x[i] + k1[i] / 2; }

            for (int i = 0; i < n; i++) { k2[i] = dt * f[i](x1, t + dt / 2); }

            for (int i = 0; i < n; i++) { x1[i] = x[i] + k2[i] / 2; }

            for (int i = 0; i < n; i++) { k3[i] = dt * f[i](x1, t + dt / 2); }

            for (int i = 0; i < n; i++) { x1[i] = x[i] + k3[i]; }

            for (int i = 0; i < n; i++) { k4[i] = dt * f[i](x1, t + dt); }

            for (int i = 0; i < n; i++) { x[i] += (k1[i] + (2 * k2[i]) + (2 * k3[i]) + k4[i]) / 6; }

            double error = 0.0;
            double tolerance = 0.0001;

            for (int i = 0; i < n; i++) { error += Math.Abs((k1[i] + 2 * k2[i] + 2 * k3[i] + k4[i]) / 6); }

            if (error > tolerance)
            {
                dt /= 2.0;
                return ARungeKutta4(f, x0, t0, dt);
            }
            else
            {
                for (int i = 0; i < n; i++) { x[i] += (k1[i] + 2 * k2[i] + 2 * k3[i] + k4[i]) / 6; }
                return x;
            }
        }
    }
}
