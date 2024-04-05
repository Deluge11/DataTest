using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTest
{

    public class Stackk<Templet>
    {
        const int arrSize = 100;
        private int top;
        private Templet[] myArray = new Templet[arrSize];


        public Stackk()
        {
            top = -1;
        }

        public bool IsEmpty()
        {
            if (top < 0)
                return true;
            return false;
        }
        public void Push(Templet Value)
        {
            if (top != arrSize - 1)
            {
                this.top++;
                myArray[this.top] = Value;
            }
        }
        public void Pop()
        {
            if (!IsEmpty())
            {
                Array.Clear(myArray, top, 1);
                top--;
            }
            else
            {
                Console.WriteLine("Empty Array");
            }
        }
        public void GetTop(ref Templet y)
        {
            if (IsEmpty())
                Console.WriteLine("Empty array");
            else
            {
                y = myArray[top];
            }

        }

        public void Print()
        {
            for (int i = top; i >= 0; i--)
            {
                Console.WriteLine(myArray[i]);
            }
        }

    }


}
