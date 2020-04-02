using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbleSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rnds = new int[50000];
            Random r = new Random();
            
            for(int i=0; i < 50000; i++)
            {
                rnds[i] = r.Next();
            }
            BubbleSort(rnds);
            
        }

        private static int[] BubbleSort(int[] toSort)
        {
            int temp = 0;
            for(int i=0; i<toSort.Length-1; i++)
            {
                for(int j=i+1; j<toSort.Length; j++)
                {
                    if(toSort[i] > toSort[j])
                    {
                        temp = toSort[i];
                        toSort[i] = toSort[j];
                        toSort[j] = temp;
                    }
                }
            }
            return toSort;
        }
    }
}
