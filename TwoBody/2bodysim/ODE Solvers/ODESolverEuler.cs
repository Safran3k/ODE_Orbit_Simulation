using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2bodysim.ODE_Solvers
{
    class ODESolverEuler
    {
        public delegate double Function(double[] x, double t);

        public static double[] EulerMethod(Function[] f, double[] x0, double t0, double dt)
        {
            int n = x0.Length;
            double[] x = x0;
            double t = t0;

            for (int i = 0; i < n; i++)
            {
                x[i] += dt * f[i](x, t);
            }

            return x;
        }
    }
}
