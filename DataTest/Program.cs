using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;


namespace DataTest
{
    public class Program
    {
        public static void ChangeToNull(myNode node)
        {
            node = null;
        }



        static void Main(string[] args)
        {

            NewTree tree = new NewTree();


            //tree.Add("40");
            //tree.Add("20");
            //tree.Add("10");
            //tree.Add("25");
            //tree.Add("30");
            //tree.Add("22");
            //tree.Add("50");
            //tree.Add("60");
            //tree.Add("32");
            //tree.Add("57");
            //tree.Add("41");
            //tree.Add("37");
            //tree.Add("28");
            //tree.Add("75");
            //tree.Add("31");
            //tree.Add("12");
            //tree.Add("69");
            //tree.Add("65");
            //tree.Add("47");
            //tree.Add("34");
            //tree.Add("53");
            //tree.Add("91");
            //tree.Add("72");
            //tree.Add("49");
            //tree.Add("11");
            //tree.Add("48");
            //tree.Add("99");
            //tree.Add("42");
            //tree.Add("82");
            //tree.Add("17");


            tree.Add("00000001");
            tree.Add("00000010");
            tree.Add("00000100");
            tree.Add("00001000");
            tree.Add("00010000");
            tree.Add("00100000");
            tree.Add("01000000");

       
            tree.Print();

            Console.WriteLine("00000001 my Parent is :" + tree.Search("00000001",true).Value);
            Console.WriteLine("00000010 my Parent is :" + tree.Search("00000010",true).Value);
            Console.WriteLine("00000100 my Parent is :" + tree.Search("00000100",true).Value);
            Console.WriteLine("00000100 my Parent is :" + tree.Search("00000100",true).Value);
            Console.WriteLine("00010000 my Parent is :" + tree.Search("00010000",true).Value);
            Console.WriteLine("00100000 my Parent is :" + tree.Search("00100000",true).Value);
            Console.WriteLine("01000000 my Parent is :" + tree.Search("01000000",true).Value);











        }






        static void Sorting(int[] arr)
        {

            int Start = 0;
            int End = arr.Length - 1;

            while (End > Start)
            {
                int Min = arr[Start];
                int Max = arr[Start];
                int maxIndex = Start;
                int minIndex = Start;
                int tmp;
                int tmp2;

                for (int i = Start; i <= End; i++)
                {
                    if (arr[i] > Max)
                    {
                        Max = arr[i];
                        maxIndex = i;
                    }
                    if (arr[i] < Min)
                    {
                        Min = arr[i];
                        minIndex = i;
                    }
                }

                if (Min == arr[End] && Max == arr[Start])
                {

                    tmp = arr[Start];
                    tmp2 = arr[End];


                    arr[Start] = Min;
                    arr[End] = Max;


                    arr[minIndex] = tmp;
                    arr[maxIndex] = tmp2;
                }
                else if (Min == arr[End])
                {
                    tmp = arr[Start];
                    arr[Start] = Min;
                    arr[minIndex] = tmp;

                    tmp2 = arr[End];
                    arr[End] = Max;
                    arr[maxIndex] = tmp2;
                }
                else
                {
                    tmp2 = arr[End];
                    arr[End] = Max;
                    arr[maxIndex] = tmp2;
                    tmp = arr[Start];
                    arr[Start] = Min;
                    arr[minIndex] = tmp;
                }

                Start++;
                End--;

            }
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }

        }
    }
}


