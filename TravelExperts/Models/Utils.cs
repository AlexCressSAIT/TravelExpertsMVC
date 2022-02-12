using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelExperts.Models
{
    public static class Utils
    {
        public static SelectList SelectRange(int lower, int upper)
        {
            List<int> nums = new List<int>();
            for (int i = lower; i <= upper; i++)
            {
                nums.Add(i);
            }
            var selectList = nums.Select(i => new { Value = i, Text = i });
            return new SelectList(selectList, "Value", "Text");
        }
    }
}
