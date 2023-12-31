﻿using SimpleFuzzy.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR.Abstract
{
    public interface IGetClassificationSet : INameable
    {
        public string ConnctionString { get; set; }
        void Reload();
        float[,] GetTrainSet(int count);
        float[,] GetTrainSet(double? p);
        float[,] GetTestSet(int count);
        float[,] GetTestSet(double? p);

        string ClassFazififcation(double _class);
    }

    public interface IGetClassificationSetObject : INameable
    {
        public string ConnctionString { get; set; }
        void Reload();
        object[,] GetTrainSet(int count);
        object[,] GetTrainSet(double? p);
        object[,] GetTestSet(int count);
        object[,] GetTestSet(double? p);

        string ClassFazififcation(double _class);
    }
}
