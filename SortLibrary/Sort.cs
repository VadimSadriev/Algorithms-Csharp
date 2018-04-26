using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SortLibrary
{
     //статический класс, хранящий методы, реализующих основные алгоритмы сортировок
    public static class Sort 

    { 
        //Метод замены значений в массиве 
      private  static void Swap<T>(T[] items, int left, int right) 
        {
            if (left!=right)
            {
                T temp = items[left];
                items[left] = items[right];
                items[right] = items[left];
            }
        }

        //Сортировка Пузырьком
        //Алгоритм  проходит по массиву несколько раз, на каждом этапе перемещая
        //  самое большое значение из неотсортированных в конец массива.
        public static void BubbleSort<T>(T[] items) where T : IComparable<T>
        {
            bool swapped;
            int[] t = new int[20];
            do
            {
                swapped = false;

                for (int i = 1; i < items.Length; i++)
                {

                    if (items[i - 1].CompareTo(items[i]) > 0)
                    {
                        Swap(items, i - 1, i);
                        swapped = true;
                    }

                }
            } while (swapped != false);
        }



        //Алгоритм сортировки вставками
        //Элементы входной последовательности просматриваются по одному,
        //и каждый новый поступивший элемент размещается в подходящее место среди ранее упорядоченных элементов
        public static void InsertionSort<T>(T[] items) where T: IComparable<T>
        {
            int sortedRangeEndIndex = 1;

            while (sortedRangeEndIndex < items.Length)
            {
                if (items[sortedRangeEndIndex].CompareTo(items[sortedRangeEndIndex - 1]) < 0)
                {
                    int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex]);
                    Insert(items, insertIndex, sortedRangeEndIndex);
                }

                sortedRangeEndIndex++;
            }
        }


        //Находит место для вставки элемента в массив
        private static int FindInsertionIndex<T>(T[] items, T valueToInsert) where T : IComparable<T>
        {
            for (int index = 0; index < items.Length; index++)
            {
                if (items[index].CompareTo(valueToInsert)>0)
                {
                    return index;
                }
            }

            throw new InvalidOperationException("The insertion index was not found");
        }

        //Вствляет элемент в нужный индекс, удаляя вставляемое значение из массива и сдвигая все значения, 
        //начиная с индекса для вставки, вправо
        private static void Insert<T>(T[] itemArray, int indexInsertingAt, int indexInsertingFrom) where T:IComparable<T>
        {
            // itemArray =       0 1 2 4 5 6 3 7
            // insertingAt =     3
            // insertingFrom =   6
            // 
            // Действия:
            //  1: Сохранить текущий индекс в temp
            //  2: Заменить indexInsertingAt на indexInsertingFrom
            //  3: Заменить indexInsertingAt на indexInsertingFrom в позиции +1
            //     Сдвинуть элементы влево на один.
            //  4: Записать temp на позицию в массиве + 1.

            T temp = itemArray[indexInsertingAt];

            itemArray[indexInsertingAt] = itemArray[indexInsertingFrom];

            for (int current = indexInsertingFrom; current > indexInsertingAt; current--)
            {
                itemArray[current] = itemArray[current - 1];
            }

            itemArray[indexInsertingAt + 1] = temp;
        }


        //Алгоритм Сортировки выбором
        //Алгоритм проходит по массиву раз за разом, перемещая одно значение на правильную позицию.
        //Выбирает наименьшее неотсортированное значение.
        public static void SelectionSort<T>(T[] items) where T: IComparable<T>
        {
            int sortedRangeEnd = 0;

            while (sortedRangeEnd < items.Length)
            {
                int nextIndex = FindIndexOfSmallestFromIndex(items, sortedRangeEnd);
                Swap(items, sortedRangeEnd, nextIndex);

                sortedRangeEnd++;
            }
        }

        //Находит в массиве наименьшее значение и перемещает его в начало массива
        private static int FindIndexOfSmallestFromIndex<T>(T[] items, int sortedRangeEnd) where T: IComparable<T>
        {
            T currentSmallest = items[sortedRangeEnd];
            int currentSmallestIndex = sortedRangeEnd;

            for (int i = sortedRangeEnd + 1; i < items.Length; i++)
            {
                if (currentSmallest.CompareTo(items[i]) > 0)
                {
                    currentSmallest = items[i];
                    currentSmallestIndex = i;
                }
            }

            return currentSmallestIndex;
        }


        //Сортировка Слиянием
        //Разделяем массив пополам до тех пор, пока каждый участок не станет длиной в один элемент.
        //Затем эти участки возвращаются на место (сливаются) в правильном порядке.
        public static void MergeSort<T>(T[] items) where T: IComparable<T>
        {
            if (items.Length <= 1)
            {
                return;
            }

            int leftSize = items.Length / 2;
            int rightSize = items.Length - leftSize;
            T[] left = new T[leftSize];
            T[] right = new T[rightSize];
            Array.Copy(items, 0, left, 0, leftSize);
            Array.Copy(items, leftSize, right, 0, rightSize);
            MergeSort(left);
            MergeSort(right);
            Merge(items, left, right);
        }

        // Слияние в правильном порядке
        private static void Merge<T>(T[] items, T[] left, T[] right) where T: IComparable<T>
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int targetIndex = 0;
            int remaining = left.Length + right.Length;
            while (remaining > 0)
            {
                if (leftIndex >= left.Length)
                {
                    items[targetIndex] = right[rightIndex++];
                }
                else if (rightIndex >= right.Length)
                {
                    items[targetIndex] = left[leftIndex++];
                }
                else if (left[leftIndex].CompareTo(right[rightIndex]) < 0)
                {
                    items[targetIndex] = left[leftIndex++];
                }
                else
                {
                    items[targetIndex] = right[rightIndex++];
                }

                targetIndex++;
                remaining--;
            }
        }


        //Быстрая сортировка 
      private  static Random  _pivotRng = new Random();

        public static void QuickSort<T>(T[] items) where T: IComparable<T>
        {
            quicksort(items, 0, items.Length - 1);
        }

        private static void quicksort<T>(T[] items, int left, int right) where T: IComparable<T>
        {
            if (left < right)
            {
                int pivotIndex = _pivotRng.Next(left, right);
                int newPivot = partition(items, left, right, pivotIndex);

                quicksort(items, left, newPivot - 1);
                quicksort(items, newPivot + 1, right);
            }
        }

        // Перемещение значений
        private static int partition<T>(T[] items, int left, int right, int pivotIndex) where T: IComparable<T>
        {
            T pivotValue = items[pivotIndex];

            Swap(items, pivotIndex, right);

            int storeIndex = left;

            for (int i = left; i < right; i++)
            {
                if (items[i].CompareTo(pivotValue) < 0)
                {
                    Swap(items, i, storeIndex);
                    storeIndex += 1;
                }
            }

            Swap(items, storeIndex, right);
            return storeIndex;
        }
    }
}
