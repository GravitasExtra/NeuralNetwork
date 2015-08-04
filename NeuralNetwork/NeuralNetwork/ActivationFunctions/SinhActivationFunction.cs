﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork.ActivationFunctions
{
    public class SinhActivationFunction: IActivationFunction
    {
        public double CalculateActivation(double signal)
        {
            return Math.Sinh(signal);
        }
    }
}
