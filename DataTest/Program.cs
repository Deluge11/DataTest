using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;


namespace DataTest
{
    public class Program
    {




        static void Main(string[] args)
        {

            NewTree tree = new NewTree();


            tree.Add("1000000");
            tree.Print();
            tree.Add("0100000");
            tree.Print();
            tree.Add("0010000");
            tree.Print();
            tree.Add("1001000");
            tree.Print();
            tree.Add("1000100");
            tree.Print();
            tree.Add("1000010");
            tree.Print();
            tree.Add("1000001");
            tree.Print();



            tree.Search("1000010");


















         
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


