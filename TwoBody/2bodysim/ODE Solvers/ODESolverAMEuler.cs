using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2bodysim.ODE_Solvers
{
    class ODESolverAMEuler
    {
        public delegate double Function(double[] x, double t);

        public static double[] AMEulerMethod(Function[] f, double[] x0, double t0, double dt)
        {
            int n = x0.Length;
            double tolerance = 0.1;
            double[] x1 = new double[n];
            double[] x2 = new double[n];

            for (int i = 0; i < n; i++)
            {
                x1[i] = x0[i] + dt * f[i](x0, t0);
            }

            for (int i = 0; i < n; i++)
            {
                x2[i] = x1[i] + dt * f[i](x1, t0 + dt);
            }

            double error = 0.0;
            for (int i = 0; i < n; i++)
            {
                error += Math.Abs(x2[i] - x1[i]);
            }

            if (error > tolerance)
            {
                dt = dt / 2.0;
                return AMEulerMethod(f, x0, t0, dt);
            }

            t0 = t0 + dt;
            return x2;
        }
    }
}
