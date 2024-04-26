using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EvidenceHer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Databaze db = new Databaze();
            Menu menu = new Menu();

            char volba;
            do
            {
                volba = menu.VypisMenu();
                switch (volba)
                {
                    case '1':
                        db.VlozitHru();
                        break;
                    case '2':
                        db.VypisVsechHer();
                        break;
                    case '3':
                        db.VypisPristupnost();
                        break;
                    case '4':
                        db.VypisVyrobce();
                        break;
                    case '5':
                        db.SmazatHru();
                        break;
                    case '6':
                        db.Export();
                        break;
                    case 'k':
                        Console.WriteLine("Ukončuji program...");
                        break;
                    default:
                        Console.WriteLine("Špatná volba... pomocí stisknutí libovolné klávesy pokračuj");
                        Console.ReadKey();
                        break;
                }

            } while (volba != 'k');

            Console.ReadKey();
        }
    }
    class Menu
    {
        public char VypisMenu()
        {
            Console.Clear();
            Console.WriteLine("------------ M E N U ------------");
            Console.WriteLine("\nVYBERTE MOŽNOST");
            Console.WriteLine("\n1.Vložit hru");
            Console.WriteLine("2.Výpis všech her");
            Console.WriteLine("3.Výpis her podle přístupnosti");
            Console.WriteLine("4.Výpis her od zadaného výrobce");
            Console.WriteLine("5.Smazat hru z databáze");
            Console.WriteLine("6.Exportovat hry do txt");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("K - KONEC PROGRAMU");
            Console.WriteLine("---------------------------------");
            Console.Write("Váš výběr: ");

            char input;
            input = Console.ReadLine()[0];
            return input;
        }
    }
    class Hra
    {
        public string Nazev { get; private set; }
        public int RokVydani { get; private set; }
        public string Vyrobce { get; private set; }
        public string Pristupnost { get; private set; }
        private int pegi;
        public int Pegi
        {
            get
            {
                return pegi;
            }
            private set
            {
                pegi = value;
                if (pegi < 8)
                {
                    Pristupnost = "dětská hra";
                }
                else if (pegi < 18)
                {
                    Pristupnost = "mp";
                }
                else
                {
                    Pristupnost = "mn";
                }
            }
        }

        public Hra(string nazev, int rokVydani, string vyrobce, int pegi)
        {
            Nazev = nazev;
            RokVydani = rokVydani;
            Vyrobce = vyrobce;
            Pegi = pegi;
        }
        public void Info()
        {
            Console.WriteLine();
            Console.WriteLine("Název: " + Nazev);
            Console.WriteLine("Výrobce: " + Vyrobce);
            Console.WriteLine("Rok vydání: " + RokVydani);
            Console.WriteLine("Pegi: {0} - {1}", Pegi, Pristupnost);
            Console.WriteLine("-------------------------------------");
        }

    }
    class Databaze
    {
        public List<Hra> Db { get; private set; }

        public Databaze() { Db = new List<Hra>(); }
        public void VlozitHru()
        {
            Console.Clear();
            string nazev;
            string vyrobce;
            int rok;
            int pegi;

            Console.Write("Nazev: ");
            nazev = Console.ReadLine();
            Console.Write("Vyrobce: ");
            vyrobce = Console.ReadLine();
            Console.Write("Rok vydání: ");
            rok = int.Parse(Console.ReadLine());
            Console.Write("Pegi: ");
            pegi = int.Parse(Console.ReadLine());

            Db.Add(new Hra(nazev,rok,vyrobce,pegi));

            Console.WriteLine("Hra byla úspěšně vložena do databáze, stiskni libovolnou klávesu pro pokračování");
            Console.ReadKey();
        }
        public void VypisVsechHer()
        {
            Console.Clear();
            foreach(Hra hra in Db)
            {
                hra.Info();
                Console.WriteLine();
            }
            Console.WriteLine("Výpis skončil..stiskni libovolnou klávesu pro pokračování");
            Console.ReadKey();
        }
        public void VypisPristupnost()
        {
            Console.Clear();
            Console.WriteLine("Vyber jednu z možností:");
            Console.WriteLine("\n1. dětské hry");
            Console.WriteLine("2. mládeži přístupné");
            Console.WriteLine("3. mládeži nepřístupně");

            int volba = int.Parse(Console.ReadLine());
            string pristupnost = "";

            switch (volba)
            {
                case 1:
                    pristupnost = "dětská hra";
                    break;
                case 2:
                    pristupnost = "mp";
                    break;
                case 3:
                    pristupnost = "mn";
                    break;
                default:
                    pristupnost = "0";
                    Console.WriteLine("špatná volba chlape");
                    Console.ReadKey();
                    break;
            }

            Console.Clear();
            foreach (Hra hra in Db)
            {
                if(pristupnost == hra.Pristupnost)
                {
                    hra.Info();
                    Console.WriteLine();
                }
            }
            Console.WriteLine("stiskni libovolně tlačítko..");
            Console.ReadKey();
        }
        public void VypisVyrobce()
        {
            Console.Clear();
            string vyrobce;
            Console.WriteLine("Jakého výrobce hledáte:");
            vyrobce = Console.ReadLine();

            Console.Clear();
            foreach(Hra hra in Db)
            {
                if(vyrobce == hra.Vyrobce)
                {
                    hra.Info();
                    Console.WriteLine();
                }
            }
            Console.WriteLine("stiskni libovolně tlačítko..");
            Console.ReadKey();
        }
        public void SmazatHru()
        {
            Console.Clear();

            int x = 1;
            foreach (Hra a in Db)
            {
                Console.WriteLine($"{x}. {a.Nazev}");
                x++;
            }

            if (Db.Any())
            {
                Console.Write("Vyberte pozici hry, kterou chcete smazat: ");
                int volba = int.Parse(Console.ReadLine());
                volba--;
                if (volba < 0 || volba >= Db.Count)
                {
                    Console.WriteLine("Moc malý nebo moc velký index");
                    Console.ReadKey();
                }
                else
                    Db.RemoveAt(volba);
            }
            else
            {
                Console.WriteLine("Databáze je prázdná nejdřív ji naplň");
                Console.ReadKey();
            }
        }

        public void Export()
        {
            using(StreamWriter vypis = new StreamWriter("hry.txt")){
                 foreach (Hra hra in Db)
                {
                    vypis.WriteLine(hra);
                }   
            }
        } 
    }
}
