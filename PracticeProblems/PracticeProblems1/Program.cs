using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeProblems
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] multi = new int[,] { { 0, 1, 0, 0, 3 }, { 0, 3, 3, 0, 0 }, { 0, 0, 0, 0, 2 }, { 0, 0, 1, 0, 2 }, { 0, 0, 0, 0, 0 } };
            Console.WriteLine(getGroups(multi).ToString());
            Console.Read();
        }

         /// <summary>
        /// Returns the groups of "objects" in a multidimensional integer array
        /// </summary>
        public static int getGroups(int[,] numbers)
        {
            HashSet<List<int>> objects = new HashSet<List<int>>(); //all the "objects found"
            //keep track of what's been explored, only need to explore each cell once
            HashSet<int> explored = new HashSet<int>();
            List<int> currentObject = new List<int>();
            //calculate length of rows & columns
            int rowLength = numbers.GetLength(0);
            int colLength = numbers.Length / rowLength;
            //for each cell in numbers, call visit to explore it's neighbors
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    visit(ref objects, ref explored, numbers, i, j, ref currentObject); //pass parameters by reference to avoid unnecessary copying
                }
            }
            return objects.Count;
        }

        /// <summary>
        /// Recursively visit neighbors of cell to determine the extent of the object. If the cell is part of an object, add the current object to main set of objects. 
        /// </summary>
        /// <param name="_objects"> main set of objects</param>
        /// <param name="_visited"> cells visited so far</param>
        /// <param name="numbers">2d array of numbers to check from</param>
        /// <param name="_i">current row</param>
        /// <param name="_j">current colum</param>
        /// <param name="_currentObject">an empty list to hold current object</param>
        private static void visit(ref HashSet<List<int>> _objects, ref HashSet<int>_visited, int[,] numbers, int _i, int _j, ref List<int> _currentObject)
        {
            //call recursive helper to visit neighbors and add to current object
            add(numbers, ref _currentObject, ref _visited, _i, _j);
            if (_currentObject.Count > 0)
                _objects.Add(new List<int>(_currentObject));
            _currentObject.Clear(); //reset current object after adding it to the main set
        }

        /// <summary>
        /// Traverse 2d array recursively, top to bottom, left to right
        /// </summary>
        private static void add(int[,] arr, ref List<int> obj, ref HashSet<int> _visited, int i, int j)
        {
            List<int> results = new List<int>();
            int rowLength = arr.GetLength(0);
            int colLength = arr.Length / rowLength;
            //stop looking if we've been here before or reached the end of an object
            if (arr[i, j] == 0 || _visited.Contains((rowLength * i) + j))
            {
                return;
            }
            else
            {
                _visited.Add((rowLength * i) + j); //count cell as visited
                obj.Add(arr[i, j]); //add to current object
                if (j + 1 < rowLength - 1)
                    add(arr, ref obj, ref _visited, i, j + 1); //recursively visit right neighbor
                if (i + 1 < colLength - 1)
                    add(arr, ref obj, ref _visited, i + 1, j); //recursively visit bottom neighbor
                return;
            }
        }

    }


}

