using CodeHollow.FeedReader;
using Epione.Domain.Entities;
using Epione.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epione
{
    class Program
    {
        static void Main(string[] args)
        {

            save();
            read();
            Console.ReadKey();
        }
        static public void save()
        {
            var feed = FeedReader.Read("https://politepol.com/feed/35450");
            StreamWriter Sw = new StreamWriter("Doctolib.txt");
            foreach (var item in feed.Items)
            {
                Sw.WriteLine(item.Title + " " + item.Description + "\n");
            }
            Sw.Close();
        }
        static public void read()
        {
            StreamReader Sw = new StreamReader("Doctolib.txt");
            for (int i = 0; i < File.ReadLines("Doctolib.txt").Count(); i++)
            {
                Console.WriteLine(Sw.ReadLine());
            }
        }
    }
}
