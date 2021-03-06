﻿using System;
using System.Drawing;

namespace ZHAW.SoftwareProjekt.Fractals.Calculation
{
    public class Mandelbrot : IFractal<double>
    {
        private int _maxIterations = 100;

        public int MaxIterations
        {
            get { return _maxIterations; }
            set { _maxIterations = value; }
        }

        public string Name { get; set; }

        public double Xmin { get; set; }
        public double Xmax { get; set; }
        public double Ymin { get; set; }
        public double Ymax { get; set; }

        private double GetDeltaX(int resolutionX)
        {
            return (Xmax - Xmin) / resolutionX;
        }

        private double GetDeltaY(int resolutionY)
        {
            return (Ymax - Ymin) / resolutionY;
        }

        public double CalculateAtPosition(int xPos, int yPos, int resolutionX, int resolutionY)
        {
            double x0 = RealXPosition(xPos, resolutionX);
            double y0 = RealYPosition(yPos, resolutionY);

            var iterations = 0;
            var x = 0.0;
            var y = 0.0;
            while (iterations < MaxIterations && (x * x) + (y * y) < 4)
            {
                var xtemp = (x * x) - (y * y) + x0;
                y = 2 * x * y + y0;
                x = xtemp;

                iterations++;
            }

            return iterations / (double)MaxIterations;
        }

        public IFractal Zoom(double factor, Point center, int width, int height)
        {
            var posX = RealXPosition(center.X, width);
            var posY = RealYPosition(center.Y, height);

            var x = Math.Abs(Xmin - Xmax);
            var y = Math.Abs(Ymin - Ymax);

            Xmin = posX - (x / factor);
            Xmax = posX + (x / factor);
            Ymin = posY - (y / factor);
            Ymax = posY + (y / factor);

            return this;
        }

        private double RealXPosition(int x, int width)
        {
            return Xmin + (x * GetDeltaX(width));
        }

        private double RealYPosition(int y, int height)
        {
            return Ymax - (y * GetDeltaY(height));
        }

        public string GetRealXPosition(int x, int width)
        {
            return RealXPosition(x, width).ToString();
        }


        public string GetRealYPosition(int y, int height)
        {
            return RealYPosition(y, height).ToString();
        }
    }
}