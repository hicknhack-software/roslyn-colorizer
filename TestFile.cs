using System;

namespace Test
{
    internal enum Enum
    {
        Value,
        Second = 2
    }

    internal struct Data
    {
        public int field { get; set; }
        private readonly string privately;

        public Data(int p)
        {
            field = p;
            privately = "Hello";
        }
    }

    internal static class ClassExt
    {
        public static int Sum(this Enum @enum, int num = 5)
        {
            return num + 2;
        }
    }
    internal interface Base
    {
        int Run(string arg);
    }

    internal class Class1 : Base
    {
        private Data data;
        Enum Runner {
            get => Enum.Value;
        }

        public Class1() // construct
        {
            var x = Enum.Value;
            x.Sum(3);
        }

        public static int NoWay() => 42;

        public int Run(string arg) => throw new NotImplementedException();
    }
}
