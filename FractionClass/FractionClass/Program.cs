using System;

namespace FractionClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction a = new Fraction(4, 6);
            Fraction b = new Fraction(1, 2);

            Console.WriteLine(a++);
            Console.WriteLine(b--);

            a = new Fraction(2, 3, 7);
            b = new Fraction(3, 2, 4);

            Console.WriteLine(a + b);
            Console.WriteLine(a * b);
            Console.WriteLine(a - b);
            Console.WriteLine(a / b);
            Console.WriteLine(a > b);
            Console.WriteLine(a < b);
            
            Console.ReadLine();
        }
    }
}
