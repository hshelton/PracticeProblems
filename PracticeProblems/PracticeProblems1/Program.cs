using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProblems1
{
    class Program
    {

        /// <summary>
        ///  Given a multidimensional array like below
        ///  0 1 0 0 3
        ///  0 3 3 0 0
        ///  0 0 1 0 2
        ///  0 0 0 0 0 
        ///  
        /// "objects are considered groups of numbers that touch along top, left, right, or bottom edges. Find the 
        /// number of objects. ie cardinality of {{1,3,3}, {3}, {1}, {2,2}}
        /// </summary>
        /// <returns></returns>
        public static int getGroups (int[,] numbers)
        {
            //calculate length of row
            HashSet<List<int>> objects = new HashSet<List<int>>();

            var rowLength = numbers.GetLength(0);
            int colLength = numbers.Length / rowLength;

            int top = 0;
            int bottom = 0;
            int prev = 0;
            int next = 0;

            int k;
            List<int>currentObject = new List<int>();
            for (int i = 0; i < rowLength; i++ )
            {
                for(int j = 0; j <colLength; j++ )
                {
                    if(numbers[i, j]!= 0)
                    {
                        //add tops 
                        if (i >= 1)
                        {
                            k = i;
                            while (k > 0 && numbers[i - k, j] != 0  )
                            {
                                currentObject.Add(numbers[i - k, j]);
                                    k--;
                            }
                        }

                        //add bottoms 
                        if (i< colLength)
                        {
                            k = i;
                            while( i-k > 0 && numbers[i+k, j] !=0 )
                            {
                                currentObject.Add(numbers[i - k, j]);
                                k++;
                            }
                        }

                        //add right neighbor
                        k = 1;
                        while(j+k < rowLength && k < rowLength && numbers[i, j+k] !=0)
                        {
                            currentObject.Add(numbers[i, j + k]);
                            k++;

                        }

                        k = 0;
                        //add left neighbor
                        if (i > 1 )
                        {
                            while((j-k)> i)
                            {
                                currentObject.Add(numbers[i, j - k]);
                                k++;
                            }
                        }
                        if (currentObject.Count >= 1)
                            objects.Add(currentObject);
                    }
                }


            }


                return rowLength;
        }





        /// <summary>
        /// Return the largest k integers from a large file. (10,000 or more entries).
        /// Assumptions: 1. the largest integers should be returned in descending order. 
        /// 2. Returned elements are unique. If there are not k unique elements in the file,
        /// return as many unique elements as exist.
        /// 
        /// </summary>
        /// <param name="k">Number of elements to return</param>
        /// <returns></returns>
        public static List<int> getLargest(int k, List<int> file)
        {
            int min = file[0];
            List<int> largest = new List<int>();
            largest.Add(min);

            HashSet<int> set = new HashSet<int>();
            foreach (int i in file)
            {
                if (i > min)
                {
                    set.Add(i);
                    min = set.Min();
                }
                if(set.Count > k)
                {
                    set.Remove(min);
                }
            }

            largest = set.ToList();
            largest.Sort();
            largest.Reverse();
            return largest;

        }






        static void Main(string[] args)
        {
            List<int> largeFile = new List<int>();

            Random gen = new Random();
            for (int i = 0; i < 10000; i++)
            {
                largeFile.Add(gen.Next(100000));
            }

             List<int> smallFile = new List<int> { 1, 2, 4, 5, 1, 6, 3, 43, 5, 324, 34, 21, 21, 12, 8 };

            var holder = getLargest(35, largeFile);
            var holder2 = getLargest(3, smallFile);

           
            foreach (int x in holder2)
            {
                Console.Write(x + ", ");
            }
            Console.WriteLine("_______________________________________");
            foreach(int x in holder)
            {
                Console.Write(x + ", ");
            }
            int[,]multi = new int[,] { { 0, 1, 0, 0, 3 }, { 0, 3, 3, 0, 0 }, { 0, 0, 0, 0, 2 }, { 0, 0, 1, 0, 2 }, { 0, 0, 0, 0, 0 } };

            print(getGroups(multi).ToString());


            Console.Read();

        }

        public static void print(string s)
        {
            Console.WriteLine();
            Console.WriteLine("**************************************");
            Console.Write(s);
            Console.WriteLine();

        }
    }
}
