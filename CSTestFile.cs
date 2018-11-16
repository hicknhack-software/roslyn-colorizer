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

    internal class ControlFlow
    {
        public IEnumerable<int> Method()
        {
            int x = 1;
            yield return x;

            do {
                x++;
            } while (x < 5);

            try {
                x.ToString();
            }
            catch {
            }
            finally {
                while (true) { }
            }

            if (x == 1)
                yield return 1;
            else if (x == 2) {
                if (x == 0)
                    throw new Exception("nested");

                yield return 2;
            }
            else if (x == 3)
                yield return 3;
            else


            int y = 5;

            switch (y) {
                case 1:
                    yield break;
                default:
                    throw new System.Exception();
            }
        }
    }
}
