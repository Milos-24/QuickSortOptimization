using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ArhitektureProjektni
{
    class StandardQuickSort
    {
        
        public static void Quick_Sort(int[] arr, int left, int right)
        {
                if (left < right)
                {
                    int pivot = Partition(arr, left, right);
   
                    Quick_Sort(arr, left, pivot - 1);
                    Quick_Sort(arr, pivot + 1, right);
                }
        }

         private static int Partition(int[] arr, int low, int high)
         {
            int pivot = arr[high];

            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    swap(arr, i, j);
                }
            }
            swap(arr, i + 1, high);
            return (i + 1);
        }
        static void swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

    }

    class ParallelQuickSort
    {
        public static void QuickSort(int[] array, int left, int right, int maxDepth)
        {
            if (left >= right)
            {
                return;
            }

            SwapElements(array, left, (left + right) / 2);
            int last = left;
            for (int current = left + 1; current <= right; ++current)
            {
                if (array[current].CompareTo(array[left]) < 0)
                {
                    ++last;
                    SwapElements(array, last, current);
                }
            }

            SwapElements(array, left, last);

            if (maxDepth < 1)
            {
                QuickSort(array, left, last - 1, maxDepth);
                QuickSort(array, last + 1, right, maxDepth);
            }
            else
            {
                --maxDepth;
                Parallel.Invoke(
                    () => QuickSort(array, left, last - 1, maxDepth),
                    () => QuickSort(array, last + 1, right, maxDepth));
            }
        }

        static void SwapElements(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

    }


    class DualPivotQuickSort
    {

        static void swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        static public void QuickSort(int[] arr,
                                       int low, int high)
        {
            if (low < high)
            {
                int[] piv;
                piv = partition(arr, low, high);

                QuickSort(arr, low, piv[0] - 1);
                QuickSort(arr, piv[0] + 1, piv[1] - 1);
                QuickSort(arr, piv[1] + 1, high);
            }
        }

        static int[] partition(int[] arr, int low, int high)
        {
            if (arr[low] > arr[high])
                swap(arr, low, high);

            int j = low + 1;
            int g = high - 1, k = low + 1,
                p = arr[low], q = arr[high];

            while (k <= g)
            {
                if (arr[k] < p)
                {
                    swap(arr, k, j);
                    j++;
                }

                else if (arr[k] >= q)
                {
                    while (arr[g] > q && k < g)
                        g--;

                    swap(arr, k, g);
                    g--;

                    if (arr[k] < p)
                    {
                        swap(arr, k, j);
                        j++;
                    }
                }
                k++;
            }
            j--;
            g++;

            swap(arr, low, j);
            swap(arr, high, g);

            return new int[] { j, g };

        }
    }

    class OptimalQuickSort
    {

        static void swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        static public void QuickSort(int[] arr,
                                       int low, int high, int maxDepth)
        {
            if (low < high)
            {
                int[] piv;
                piv = partition(arr, low, high);
               
                if (maxDepth < 1)
                {
                    QuickSort(arr, low, piv[0] - 1, maxDepth);
                    QuickSort(arr, piv[0] + 1, piv[1] - 1, maxDepth);
                    QuickSort(arr, piv[1] + 1, high, maxDepth);
                }
                else
                {
                    --maxDepth;
                    Parallel.Invoke(
                                    () => QuickSort(arr, low, piv[0] - 1, maxDepth),
                                    () => QuickSort(arr, piv[0] + 1, piv[1] - 1, maxDepth),
                                    () => QuickSort(arr, piv[1] + 1, high, maxDepth));
                }
                
            }
        }

        static int[] partition(int[] arr, int low, int high)
        {
            if (arr[low] > arr[high])
                swap(arr, low, high);

            int j = low + 1;
            int g = high - 1, k = low + 1,
                p = arr[low], q = arr[high];

            while (k <= g)
            {

                if (arr[k] < p)
                {
                    swap(arr, k, j);
                    j++;
                }

                else if (arr[k] >= q)
                {
                    while (arr[g] > q && k < g)
                        g--;

                    swap(arr, k, g);
                    g--;

                    if (arr[k] < p)
                    {
                        swap(arr, k, j);
                        j++;
                    }
                }
                k++;
            }
            j--;
            g++;

            swap(arr, low, j);
            swap(arr, high, g);

          
            return new int[] { j, g };

        }
    }
    class Utility
    {
        public static int[] GenerateRandomArray(int size = 10000)
        {
            var array = new int[size];
            Random r = new Random();

            for (int i = 0; i < size; i++)
            {
                array[i] = r.Next(Int32.MinValue, Int32.MaxValue);
            }

            return array;

        }
        public static bool IsSorted(int[] a)
        {
            if (!a.Any())
                return true;

            var prev = a.First();

            for (int i = 1; i < a.Length; i++)
            {
                if (a[i].CompareTo(prev) < 0)
                    return false;

                prev = a[i];
            }

            return true;
        }
    }

    class Program
    {
        static public void Main(String[] args)
        {
            int size = 100_000;
          
            int[] array1 = Utility.GenerateRandomArray(size);
            int[] array2 = Utility.GenerateRandomArray(size);
            int[] array3 = Utility.GenerateRandomArray(size);
            int[] array4 = Utility.GenerateRandomArray(size);

            Console.WriteLine("Size of arrays is: {0}", size);


            //QuickSort
            Stopwatch sort1Stopwatch = new Stopwatch();
            sort1Stopwatch.Start();
            StandardQuickSort.Quick_Sort(array1, 0, array1.Length-1);
            sort1Stopwatch.Stop();
            Console.WriteLine($"Obicni QuickSort - Is sorted? {Utility.IsSorted(array1)}. ElapsedMS={sort1Stopwatch.ElapsedMilliseconds}");


            //Parallelized QuickSort
            Stopwatch sort2Stopwatch = new Stopwatch();
            sort2Stopwatch.Start();
            ParallelQuickSort.QuickSort(array2, 0, array2.Length - 1, 4);
            sort2Stopwatch.Stop();
            Console.WriteLine($"Rucno paralelizaovani QuickSort - Is sorted? {Utility.IsSorted(array2)}. ElapsedMS={sort2Stopwatch.ElapsedMilliseconds}");


            //DualPivot QuickSort
            Stopwatch sort3Stopwatch = new Stopwatch();
            sort3Stopwatch.Start();
            DualPivotQuickSort.QuickSort(array3,0,array3.Length-1);
            sort3Stopwatch.Stop();
            Console.WriteLine($"Dual pivot QuickSort - Is sorted? {Utility.IsSorted(array3)}. ElapsedMS={sort3Stopwatch.ElapsedMilliseconds}");


            //Optimal QuickSort (Parallelized+DualPivot)
            Stopwatch sort4Stopwatch = new Stopwatch();
            sort4Stopwatch.Start();
            OptimalQuickSort.QuickSort(array4, 0, array4.Length - 1, 4);
            sort4Stopwatch.Stop();
            Console.WriteLine($"Optimalni QuickSort - Is sorted? {Utility.IsSorted(array4)}. ElapsedMS={sort4Stopwatch.ElapsedMilliseconds}");
        }
    }
}

