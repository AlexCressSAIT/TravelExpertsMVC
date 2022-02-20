using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelExperts.Models
{
    /*
     * Utility functions that don't really fit anywhere else
     * Author: Nate Penner
     * February 2022
     */
    public static class Utils
    {
        /// <summary>
        /// Generates a select list of numbers in a specified range (inclusive)
        /// </summary>
        /// <param name="lower">The lowest number</param>
        /// <param name="upper">The highest number</param>
        /// <returns>A select list filled with numbers</returns>
        public static SelectList SelectRange(int lower, int upper)
        {
            // List to store the numbers
            List<int> nums = new List<int>();
            for (int i = lower; i <= upper; i++)
            {
                nums.Add(i);
            }

            // make an anonymous object to use in constructing the list
            var selectList = nums.Select(i => new { Value = i, Text = i });

            // Build the new list and return it
            return new SelectList(selectList, "Value", "Text");
        }
    }
}
