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


        /// <summary>
        /// Find unsorted by asc part of collection. Work O(2n) ~= O(n)
        /// </summary>
        /// <returns>max lenght of unsorted part of array, that need to sort</returns>
        public static int FindUnsortedLength(ICollection<int> collection)
        {
            if (collection == null
                || collection.Count == 0
                || collection.Count == 1)
                return 0;

            int sortedByAscFromLeftCount = 0;
            int sortedByAscFromRightCount = 0;

            bool sortedFromLeftInterrupted = false;

            int minValueAfterLeftSortedPart = int.MaxValue;
            int maxValueBeforeRightSortedPart = int.MinValue;
            int maxValueInRightSortedPart = int.MinValue;

            int prevVal = 0;
            bool firstValIsSet = false;
            foreach(var curVal in collection)
            {
                if (firstValIsSet == false)
                {
                    prevVal = curVal;
                    firstValIsSet = true;
                }

                if (curVal >= prevVal && sortedFromLeftInterrupted == false)
                {
                    sortedByAscFromLeftCount++;
                    if (curVal > maxValueBeforeRightSortedPart)
                    {
                        maxValueBeforeRightSortedPart = curVal;
                    }
                }
                else
                {
                    sortedFromLeftInterrupted = true;
                }

                // если массив уже отсортирован, то сюда никогда не придём
                // если же частично отсортирован слева, то как только нашли место
                // разрыва идёт другой алгоритм
                if (sortedFromLeftInterrupted)
                {
                    // запоминаю чтобы потом подвинуть левую часть, если будет нужно
                    if (minValueAfterLeftSortedPart > curVal)
                        minValueAfterLeftSortedPart = curVal;

                    // идёт возрастание
                    if (curVal >= prevVal)
                    {
                        sortedByAscFromRightCount++;

                        // если идёт сортированная по возрастанию последовательность
                        // запоминаем её максимум, на случай если она прервётся
                        if (curVal > maxValueInRightSortedPart)
                            maxValueInRightSortedPart = curVal;
                    }
                    else
                    {
                        sortedByAscFromRightCount = 0;
                        if (curVal > maxValueBeforeRightSortedPart)
                        {
                            maxValueBeforeRightSortedPart = curVal;
                        }

                        // идёт неотсортированная последовательность, проверяем не было ли ранее
                        // в отсортированной по возростанию элемента превышающего текущий макисмум
                        // элемента который превышает
                        if (maxValueInRightSortedPart > maxValueBeforeRightSortedPart)
                            maxValueBeforeRightSortedPart = maxValueInRightSortedPart;
                    }
                }

                prevVal = curVal;
            }

            // весь массив уже отсортирован
            if (sortedByAscFromLeftCount == collection.Count)
                return 0;

            // весь массив не отсортирован
            if (sortedByAscFromLeftCount == 0 && sortedByAscFromRightCount == 0)
                return collection.Count;


            // Делаем поправку на то, что в центре массива могли 
            // найтись элементы меньше чем в отсортированном начале sortedByAscFromLeftCount
            // найтись элементы больше чем в отсортированном конце sortedByAscFromRightCount
            int sortedFromLeftCountResult = 0;
            int sortedFromRightCountResult = 0;
            foreach (var curVal in collection)
            {
                if (sortedByAscFromLeftCount > 0)
                {
                    if (curVal <= minValueAfterLeftSortedPart)
                    {
                        sortedFromLeftCountResult++;
                        sortedByAscFromLeftCount--;
                    }
                    else
                    {
                        sortedByAscFromLeftCount = 0;
                    }
                }

                if (curVal >= maxValueBeforeRightSortedPart)
                {
                    sortedFromRightCountResult++;
                }
                else
                    sortedFromRightCountResult = 0;
            }


            return collection.Count - sortedFromLeftCountResult - sortedFromRightCountResult;


        }
    }
}

