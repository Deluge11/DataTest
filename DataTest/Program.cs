using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;


namespace DataTest
{
    public class Program
    {



        static void Main(string[] args)
        {

            NewTree tree = new NewTree();




            Random w = new Random();



   

            for (int i = 0;i < 500;i++)
            {
                tree.Add(w.Next(300000000).ToString());
            }

  


            tree.Print();

            Console.WriteLine(tree.TreeHeight());

        }
    }
}


