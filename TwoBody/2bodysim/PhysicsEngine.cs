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
        ODESolverRK4.Function[] FRK4;
        ODESolverEuler.Function[] FE;
        ODESolverARK4.Function[] FARK4;
        ODESolverAMEuler.Function[] FAME;
        SolversEnum solversEnum;
        #endregion

        #region Getter/Setter
        public double Time { get => time; set => time = value; }
        public double Dt { get => dt; set => dt = value; }
        public SolversEnum SolversEnum { get => solversEnum; set => solversEnum = value; }
        #endregion

        public PhysicsEngine(SolversEnum solversEnum, double x0, double v0x, double y0, double v0y, double dt)
        {
            xx = new double[4] { x0, v0x, y0, v0y };
            Time = 0;
            Dt = dt;
            SolversEnum = solversEnum;
            switch (SolversEnum)
            {
                case SolversEnum.Euler:
                    FE = new ODESolverEuler.Function[4] { F1, F2, F3, F4 };
                    break;
                case SolversEnum.Adaptive_Modified_Euler:
                    FAME = new ODESolverAMEuler.Function[4] { F1, F2, F3, F4 };
                    break;
                case SolversEnum.Runge_Kutta_4:
                    FRK4 = new ODESolverRK4.Function[4] { F1, F2, F3, F4 };
                    break;
                case SolversEnum.Adaptive_Runge_Kutta_4:
                    FARK4 = new ODESolverARK4.Function[4] { F1, F2, F3, F4 };
                    break;
                default:
                    break;
            }
            
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
            double[] result = new double[4];
            switch (SolversEnum)
            {
                case SolversEnum.Euler:
                    result = ODESolverEuler.EulerMethod(FE, xx, Time, Dt);
                    break;
                case SolversEnum.Adaptive_Modified_Euler:
                    result = ODESolverAMEuler.AMEulerMethod(FAME, xx, Time, Dt);
                    break;
                case SolversEnum.Runge_Kutta_4:
                    result = ODESolverRK4.RungeKutta4(FRK4, xx, Time, Dt);
                    break;
                case SolversEnum.Adaptive_Runge_Kutta_4:
                    result = ODESolverARK4.ARungeKutta4(FARK4, xx, Time, Dt);
                    break;
                default:
                    break;
            }
            xx = result;
            Time += Dt;
            return result;
        }

        //public void ResetSimulation(double borderWidth, double borderHeight)
        //{
        //    xx[0] = borderWidth;
        //    xx[1] = 20;
        //    xx[2] = borderHeight;
        //    xx[3] = 20;
        //}
    }
}
