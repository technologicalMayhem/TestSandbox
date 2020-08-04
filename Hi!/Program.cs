using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Hi_
{
    class Program
    {
        public static void Main(string[] args)
        {
            Random random = new Random();
            var list = new List<Test>();
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(new Test() { 
                    A = random.Next(0, 10000), 
                    B = random.Next(0, 10000), 
                    C = random.Next(0, 10000) 
                });
            }
            Console.ReadLine();
        }
    }

    class Test
    {
        public int A;
        public int B;
        public int C;
    }
}
