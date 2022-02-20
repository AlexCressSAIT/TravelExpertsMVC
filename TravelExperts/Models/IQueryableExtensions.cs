using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelExperts.Models
{
    /*
     * Didn't end up using in this project, but could be used for easily dividing data
     * into pages or slices in the future
     * Author: Nate Penner
     * February 2022
     */
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Returns a slice of an IQueryable
        /// </summary>
        /// <typeparam name="T">The data type this queryable is holding</typeparam>
        /// <param name="queryable">The queryable object to work with</param>
        /// <param name="offset">Where in the queryable data to begin slicing from</param>
        /// <param name="count">The length of the slice</param>
        /// <returns>A queryable slice of the original data</returns>
        public static IQueryable<T> Slice<T>(this IQueryable<T> queryable, int offset, int count) {
            return queryable.Skip(offset).Take(count);
        }

        /// <summary>
        /// Returns a user-defined "page" of data from the queryable as defined by
        /// the pageSize and pageNumber
        /// </summary>
        /// <typeparam name="T">The data type this queryable is holding</typeparam>
        /// <param name="queryable">The queryable object to work with</param>
        /// <param name="pageSize">The size of one page of data</param>
        /// <param name="pageNumber">The page number to get from the data</param>
        /// <returns>A queryable page of the original data</returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int pageSize, int pageNumber)
        {
            int offset = pageSize * pageNumber - pageSize;
            return queryable.Slice(offset, pageSize);
        }


        // List implementations of the same above
        public static List<T> Slice<T>(this List<T> list, int offset, int count)
        {
            return list.Skip(offset).Take(count).ToList();
        }

        public static List<T> Page<T>(this List<T> list, int pageSize, int pageNumber)
        {
            int offset = pageSize * pageNumber - pageSize;
            return list.Slice(offset, pageSize);
        }
    }
}
