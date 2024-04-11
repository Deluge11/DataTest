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

            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");

            tree.Add("1");
            tree.Add("1");
            tree.Add("1");

            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");

            tree.Add("sa3ed");
            tree.Add("86");
            tree.Add("61");
            tree.Add("ayham");
            tree.Add("40");
            tree.Add("23");
            tree.Add("58");
            tree.Add("ahmad");
            tree.Add("nohamed");
            tree.Add("ahlam");
            tree.Add("ali");
            tree.Add("mohamed");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("jmal");
            tree.Add("abdo");
            tree.Add("hmd");
            tree.Add("43");
            tree.Add("hmdan");
            tree.Add("12");
            tree.Add("hamedo");
            tree.Add("hnode");
            tree.Add("27");
            tree.Add("awesome");
            tree.Add("s3ed");
            tree.Add("s3ad");
            tree.Add("19");
            tree.Add("85");
            tree.Add("sare");
            tree.Add("44");
            tree.Add("45");
            tree.Add("sbak");
            tree.Add("61");
            tree.Add("nor");
            tree.Add("mostafa");
            tree.Add("7md");
            tree.Add("73");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("1");
            tree.Add("80");
            tree.Add("52");
            tree.Add("asama");
            tree.Add("aomar");
            tree.Add("klb");
            tree.Add("74");
            tree.Add("krestoforcolobos");
            tree.Add("smer");
            tree.Add("samr");
            tree.Add("18");
            tree.Add("sjda");
            tree.Add("83");
            tree.Add("35");
            tree.Add("mohaned");
            tree.Add("hnd");
  

            tree.Delete("sa3ed");
            tree.Delete("86");
            tree.Delete("61");
            tree.Delete("ayham");
            tree.Delete("40");
            tree.Delete("23");
            tree.Delete("58");
            tree.Delete("ahmad");
            tree.Delete("nohamed");
            tree.Delete("ahlam");
            tree.Delete("ali");
            tree.Delete("mohamed");
            tree.Delete("jmal");
            tree.Delete("abdo");
            tree.Delete("hmd");
            tree.Delete("43");
            tree.Delete("hmdan");
            tree.Delete("12");
            tree.Delete("hamedo");
            tree.Delete("hnode");
            tree.Delete("27");
            tree.Delete("awesome");
            tree.Delete("s3ed");
            tree.Delete("s3ad");
            tree.Delete("19");
            tree.Delete("85");
            tree.Delete("sare");
            tree.Delete("44");
            tree.Delete("45");
            tree.Delete("sbak");
            tree.Delete("61");
            tree.Delete("nor");
            tree.Delete("mostafa");
            tree.Delete("7md");
            tree.Delete("73");
            tree.Delete("80");
            tree.Delete("52");
            tree.Delete("asama");
            tree.Delete("aomar");
            tree.Delete("klb");
            tree.Delete("74");
            tree.Delete("krestoforcolobos");
            tree.Delete("smer");
            tree.Delete("samr");
            tree.Delete("18");
            tree.Delete("sjda");
            tree.Delete("83");
            tree.Delete("35");
            tree.Delete("mohaned");
            tree.Delete("hnd");

            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");
            tree.Delete("1");

            tree.Print();



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


