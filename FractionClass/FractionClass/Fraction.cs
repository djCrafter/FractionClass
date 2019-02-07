using System;

namespace FractionClass
{
    class Fraction
    {
        private int num, denom, whole;
        private bool sign = true;
        private bool fractPart = true;

        public int Num
        {
            get => num;
            set => num = value;
        }

        public int Denom
        {
            get => denom;
            set
            {
                if (value == 0) throw new DivideByZeroException();
                denom = value;
            }
        }

        public int Whole
        {
            get => whole;
            set => whole = value;
        }

        public bool Sign
        {
            get => sign;
            set => sign = value;
        }

        public bool FractPart
        {
            get => fractPart;
            private set => fractPart = value;
        }


        public Fraction(int numerator, int denominator)
        {
            num = numerator;
            denom = denominator;
        }

        public Fraction(int numerator, int denominator, int whole) : this(numerator, denominator)
        {
            if (whole < 0)
            {
                sign = false;
                this.whole = -whole;
            }
            else
                this.whole = whole;
        }

        //Наибольший общий делитель
        private static int NOD(int x, int y)
        {
            if (y == 0) return x;
            return NOD(y, x % y);
        }

        //Выведение целого числа     
        private static Fraction WholeFraction(Fraction a)
        {
            bool flag = true;
            while (flag)
            {
                if (a.num == a.denom)
                {
                    a.whole += 1;
                    a.FractOff();
                    flag = false;
                }
                else if (a.num > a.denom)
                {
                    a.num -= a.denom;
                    a.whole++;
                }
                else if (a.num < a.denom)
                    flag = false;
            }

            return a;
        }

        //Сокращение дроби
        private static Fraction FractionReduction(Fraction a)
        {
            if (a.fractPart)
            {
                int nod = NOD(a.num, a.denom);
                a.num /= nod;
                a.denom /= nod;
            }

            return a;
        }

        //Убрать дробную часть
        private void FractOff()
        {
            num = 0;
            denom = 0;
            fractPart = false;
        }


        public override string ToString()
        {
            return whole != 0 ? $"{GetSign()}{whole}({num}/{denom})" : $"{GetSign()}({num}/{denom})";
        }

        private string GetSign() => sign ? "" : "-";


        public static Fraction operator +(Fraction a)
        {
            return new Fraction(a.num, a.denom, a.whole);
        }

        public static Fraction operator -(Fraction a)
        {
            Fraction temp = new Fraction(a.num, a.denom, a.whole);
            temp.sign = !a.sign;
            return temp;
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            return FractionReduction(WholeFraction(new Fraction(a.num * b.denom + b.num * a.denom, a.denom * b.denom,
                a.whole + b.whole)));
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            return FractionReduction(WholeFraction(new Fraction(a.num * b.denom - b.num * a.denom, a.denom * b.denom)));
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            if (a.whole != 0)
                a.num += a.whole * a.denom;
            if (b.whole != 0)
                b.num += b.whole * b.denom;

            return FractionReduction(WholeFraction(new Fraction(a.num * b.num, a.denom * b.denom)));
        }

        //TO DO: Считает не правильно. Переделеть!
        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (a.whole != 0)
                a.Num += a.whole * a.denom;
            if (b.whole != 0)
                b.Num += b.whole * b.denom;

            return FractionReduction(WholeFraction(new Fraction(a.num * b.denom, a.denom * b.num)));
        }

        public static Fraction operator ++(Fraction a)
        {
            return new Fraction(a.num, a.denom, a.whole++);
        }

        public static Fraction operator --(Fraction a)
        {
            return new Fraction(a.num, a.denom, a.whole--);
        }


        public static bool operator ==(Fraction a, Fraction b)
        {
            return a.num * b.denom == b.num * a.denom && a.whole == b.whole;
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return a.num * b.denom != b.num * a.denom || a.whole != b.Whole;
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            if (a.whole != b.whole)
                return a.whole < b.whole;
            else
                return a.num * b.denom < b.num * a.denom;
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            if (a.whole != b.whole)
                return a.whole > b.whole;
            return a.num * b.denom > b.num * a.denom;
        }

        public static bool operator true(Fraction a)
        {
            return a.num != 0;
        }

        public static bool operator false(Fraction a)
        {
            return a.num == 0;
        }

        public static explicit operator int(Fraction a)
        {
            return a.whole + (a.num / a.denom);
        }
       
    }
}
