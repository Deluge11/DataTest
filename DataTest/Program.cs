using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;


namespace DataTest
{
    public class Program
    {
        static void Main(string[] args)
        {


            //Graph graph = new Graph();

            //GraphNode A = new GraphNode('A');
            //GraphNode B = new GraphNode('B');
            //GraphNode C = new GraphNode('C');
            //GraphNode D = new GraphNode('D');
            //GraphNode E = new GraphNode('E');
            //GraphNode F = new GraphNode('F');
            //GraphNode G = new GraphNode('G');


            //A.Con(B,D,E);
            //B.Con(A,C,E);
            //C.Con(B,E,F,G);
            //D.Con(A,E);
            //E.Con(D,A,B,F,C);
            //F.Con(E,C,G);
            //G.Con(C,F);
     


            //graph.Add(A);
            //graph.Add(B);
            //graph.Add(C);
            //graph.Add(D);
            //graph.Add(E);
            //graph.Add(F);
            //graph.Add(G);
 

            NewTree tree = new NewTree();


            tree.Add("1");
            tree.Add("2");
            tree.Add("3");
            tree.Add("4");
            tree.Add("5");
            tree.Add("6");
            tree.Add("7");
            tree.Add("8");
            tree.Add("9");
            tree.Add("10");
            tree.Add("11");
            tree.Add("12");
            tree.Add("13");
            tree.Add("14");
            tree.Add("15");
            tree.Add("17");
            tree.Add("18");

            tree.Print(true);


        }
    }
}


