using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Saldainis_Laboras2
{

    class Saldainis
    {
        string pavadinimas;
        string tipas;
        double kgKaina;
        double kiekKg;



        public Saldainis(string pavadinimas, string tipas, double kgKaina, double kiekKg)
        {
            this.pavadinimas = pavadinimas;
            this.tipas = tipas;
            this.kgKaina = kgKaina;
            this.kiekKg = kiekKg;



        }
        public string imtiPavadinima() { return pavadinimas; }
        public string imtiTipa() { return tipas; }
        public double imtikgKaina() { return kgKaina; }

        public double ImtiKg() { return kiekKg; }



    }
    internal class Program
    {


        static void Main(string[] args)
        {

            const int Cn = 100;
            const int Cf = 100;
            string fv1 = "TextFile1.txt";
            string fv2 = "TextFile2.txt";


            int n1, n2;
            double N;
            double k;
            double kiekis1, kiekis2;
            string pav1, vard1;
            string pav2, vard2;
            int vard11, brangindeks;

            Saldainis[] S1 = new Saldainis[Cn];
            Saldainis[] S2 = new Saldainis[Cf];

            skaityti(fv1, S1, out n1, out kiekis1, out vard1);
            skaityti(fv2, S2, out n2, out kiekis2, out vard2);
            int a1 = Indeksas(S1, kiekis1);
            int a2 = Indeksas(S2, kiekis2);
            spausdinti(S1, kiekis1, vard1, a1, n1);


            spausdinti(S2, kiekis2, vard2, a2, n2);


            string vardas = kurisbrangiau(S1, S2, kiekis1, kiekis2, vard1, vard2, n1, n2);
            Console.WriteLine("Studentas, turintis brangesni saldainiu rinkini " + vardas);

            atranka(S1, S2, kiekis1, kiekis2);

        }


        static void skaityti(string FD, Saldainis[] S1,  out double n, out double kiekis, out string vardas)

        {
            string pavadinimas;
            string tipas;
            double kgKaina;
            double kiekKg;



            using (StreamReader reader = new StreamReader(FD))
            {
                string[] parts;
                string line;
                line = reader.ReadLine();
                vardas = line;
                line = reader.ReadLine();
                kiekis = double.Parse(line);

                for (int i = 0; i < kiekis; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    pavadinimas = parts[0];
                    tipas = parts[1];
                    kgKaina = double.Parse(parts[2]);
                    kiekKg = double.Parse(parts[3]);



                    S1[i] = new Saldainis(pavadinimas, tipas, kgKaina, kiekKg);

                }
                line = reader.ReadLine();


                parts = line.Split(';');
              
                n = double.Parse(parts[0]);

            }

        }



        static int Indeksas(Saldainis[] S1, double kiekis, double N, double n)
        {

            int brangindeks = 0;
            double brangiausias = S1[0].imtikgKaina() * N;

            for (int i = 0; i < kiekis; i++)
            {
                if (S1[i].imtikgKaina() * n > brangiausias)
                {
                    brangindeks = i;

                }
            }
            return brangindeks;
        }
        static void spausdinti(Saldainis[] S1, double kiekis, string vard, int a, int n)
        {

            const string virsus =
              "|-----------------|------------|------------------|--------------------|\r\n"
              + "| Pavadinimas   | Tipas      | Kaina kilogramui | esamas kiekis (kg) | \r\n"
              + "|---------------|------------|------------------|--------------------|";
            Console.WriteLine("PRADINIAI DUOMENYS :");
            Console.WriteLine("Studentas " + vard);
            Console.WriteLine(virsus);

            for (int i = 0; i < kiekis; i++)
            {
                Console.WriteLine("| {0,-10}   | {1,5}   | {2,5}         | {3,12}       |",
                                S1[i].imtiPavadinima(), S1[i].imtiTipa(), S1[i].imtikgKaina(), S1[i].ImtiKg());
                Console.WriteLine("----------------------------------------------------------------------");
            }
            int brangindeks = 0;

            for (int i = 0; i < kiekis; i++)
            {
                Console.WriteLine(S1[i].imtiPavadinima() + "  Saldainiai kainuoja  " + S1[i].imtikgKaina() * S1[i].ImtiKg());
                Console.WriteLine("----------------------------------------------------------------------|");

            }
            Console.WriteLine("Studentas kiekvieno saldainio pavadinimo turi po " + n + "Kg");
            for (int i = 0; i < kiekis; i++)
            {

                if (S1[i].imtikgKaina() * S1[i].ImtiKg() == S1[a].imtikgKaina() * S1[a].ImtiKg())
                {
                    Console.WriteLine("Brangiausias tipas " + S1[i].imtiTipa());

                }


            }
        }
        static string kurisbrangiau(Saldainis[] S1, Saldainis[] S2, double kiekis1, double kiekis2, string vard1, string vard2, int n1, int n2)
        {

            double sum1 = 0;
            double sum2 = 0;

            for (int i = 0; i < kiekis1; i++)
            {
                sum1 += S1[i].imtikgKaina() * n1;

            }

            for (int i = 0; i < kiekis2; i++)
            {
                sum2 += S2[i].imtikgKaina() * n2;

            }

            if (sum1 > sum2)
            {
                return vard1;
            }

            else return vard2;
        }

        static void atranka(Saldainis[] S1, Saldainis[] S2, double kiekis1, double kiekis2)
        {
            int n = 0;
            double[] C = new double[100];
            for (int i = 0; i < kiekis1; i++)
            {
                C[n] = S1[i].imtikgKaina() * S1[i].ImtiKg();
                n++;
            }
            for (int i = 0; i < kiekis2; i++)
            {
                C[n] = S2[i].imtikgKaina() * S2[i].ImtiKg();
                n++;
            }

            for (int i = 0; i < kiekis1 + kiekis2; i++)
            {
                Console.WriteLine(C[i]);

            }


        }
    }
}
