using System;
using System.Collections.Generic;

namespace MathUtils.Rand
{
    public interface IGenerator<out T> : IEnumerable<T>
    {
        T Next();
        NumberGeneratorType NumberGeneratorType { get; }
        int Seed { get; }
        long UseCount { get; }
    }

    public interface IRandomDouble : IRandoCore, IGenerator<double>
    {
    }

    public interface IRandomGuid : IRandoCore, IGenerator<Guid>
    {
    }

    public interface IRandomInt : IRandoCore, IGenerator<int>
    {
        int MaxVal { get; }
        int Next(int maxVal);
    }

    public interface IRandomBool : IRandoCore, IGenerator<bool>
    {
        bool Next(double trueProbability);
        double TrueProbability { get; }
    }

    public interface IRandoCore
    {
        IRando Rando { get; }
    }

    public enum NumberGeneratorType
    {
        NonRandom,
        RandomDotNet,
        RandomFast
    }

    public interface IRando : IRandoCore
    {
        double NextDouble();
        int NextInt();
        NumberGeneratorType NumberGeneratorType { get; }
        int Seed { get; }
        long UseCount { get; }
    }

}