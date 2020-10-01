using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Schema;

namespace Rualekseev.Interview.ArrayHelper
{
    public class ArrayHelper
    {
        /// <summary>
        /// Find unsorted by asc part of array. Work ~ O(n)
        /// </summary>
        /// <returns>max lenght of unsorted part of array, that need to sort</returns>
        public static int FindUnsortedLength(int[] array)
        {
            if (array.Length == 0 
                || array.Length == 1)
                return 0;

            int leftBorder = 0;

            // find left border when array is sorted
            while (leftBorder < array.Length - 1
                && array[leftBorder] <= array[leftBorder + 1])
                leftBorder++;

            // array is sorted
            if (leftBorder == array.Length - 1)
                return 0;

            int rightBorder = array.Length-1;
            while (rightBorder > 0
                && array[rightBorder] >= array[rightBorder - 1])
                rightBorder--;

            // all array is't sorted
            if (rightBorder == array.Length - 1 &&
                leftBorder == 0)
                return array.Length;

            int minValue = int.MaxValue;
            int maxValue = int.MinValue;
            for (int i = leftBorder; i<=rightBorder; i++)
            {
                if (array[i] < minValue)
                    minValue = array[i];

                if (array[i] > maxValue)
                    maxValue = array[i];
            }

            // leftBorder == -1 when all left part need to sorrt
            while (leftBorder >= 0 && minValue < array[leftBorder])
                leftBorder--;

            // rightBorder == array.Lenght when all right part need to sort
            while (rightBorder <= array.Length -1 && maxValue > array[rightBorder])
                rightBorder++;

            // already sorted left and right array parts
            return array.Length - (leftBorder+1) - (array.Length - rightBorder);
        }


    }
}
