using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSystem.Domain.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Find<T>(this IEnumerable<T> source, Func<T, bool> isMatch)
        {
            foreach(var item in source)
            {
                if(isMatch(item))
                {
                    yield return item;
                }
            }
        }
    }
}
