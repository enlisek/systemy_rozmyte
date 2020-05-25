using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rozmyte
{
    class Program
    {
        //funkcje przynaleznosci
        static double Triangle(double a, double b, double c, double x) //trojkatna
        {
            if ((a < x) && (x <= b)) return (x - a) / (b - a);
            if ((b < x) && (x < c)) return (c - x) / (c - b);
            return 0;
        }
        static double Rtrap(double a, double b, double x) //prawa część trapezu
        {
            if (x <= a) return 1;
            if ((x > a) && (x <= b)) return (b - x) / (b - a);
            return 0;
        }
        static double Ltrap(double a, double b, double x) //lewa część trapezu
        {
            if (x > b) return 1;
            if ((x >= a) && (x <= b)) return (x - a) / (b - a);
            return 0;
        }

        static double Max(double a, double b)
        {
            if (a >= b) return a;
            return b;

        }

        static List<double> FuzzyRule(double naslonecznienie, double skazenie) //reguly
        {
            double niskie_naslonecznienie = Rtrap(0.2, 0.45 , naslonecznienie);
            double srednie_naslonecznienie = Triangle(0.35, 0.5, 0.70, naslonecznienie);
            double wysokie_naslonecznienie = Ltrap(0.6, 0.8, naslonecznienie);

            double niskie_skazenie = Rtrap(0.2, 0.45, skazenie);
            double srednie_skazenie = Triangle(0.35, 0.5, 0.70, skazenie);
            double wysokie_skazenie = Ltrap(0.6, 0.8, skazenie);

            List<double> fr = new List<double>();
            //niska jakosc zycia
            fr.Add(Max(niskie_naslonecznienie,wysokie_skazenie));
            //srednia jakosc zycia
            fr.Add(Max(srednie_naslonecznienie,srednie_skazenie));
            //wysoka jakos zycia
            fr.Add(Max(wysokie_naslonecznienie,niskie_skazenie));

            if (niskie_skazenie>0 && wysokie_naslonecznienie > 0)
            {
                fr.Add(1);
            }

            return fr;

        }

        static void Concl(List<double> fr) //wnioskowanie
        {

            double sum = fr[0] + fr[1] + fr[2];
            double dec = (0 * fr[0] + 50 * fr[1] + 100 * fr[2]) / sum;
            Console.WriteLine($"Jakość życia jest na poziomie {dec}%");

        }

        static void WnNs(List<double> fr,string miasto)
        {
            if (fr.Count()>3)
            {
                Console.WriteLine($"{miasto} jest wysoko nasłonecznione i nisko skażone");
            }
        }




        static void Main(string[] args)
        {
            List<String> miasta = new List<String>() { "Warszawa", "Kraków", "Gdańsk", "Wrocław", "Katowice", "Poznań", "Gliwice" };
            List<double> naslonecznienie = new List<double>() { 0.6, 1, 0.9, 0.8, 0.3, 0.7, 0.3 };
            List<double> skazenie = new List<double>() { 0.3, 0.1, 0.9, 0.7, 0.1, 0.6, 0.1 };

            for (int i = 0; i < miasta.Count(); i++)
            {
                Console.WriteLine(miasta[i]);
                Concl(FuzzyRule(naslonecznienie[i],skazenie[i]));
                WnNs(FuzzyRule(naslonecznienie[i], skazenie[i]),miasta[i]);
            }

            //które miasto jest wysoko nasłonecznione i nisko skażone
            Console.ReadKey();
        }



    

    }
}
