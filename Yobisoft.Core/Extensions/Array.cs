using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yobisoft.Core.Extensions
{
    public static class ArrayExtension
    {
        public static void CopyFrom<T>(this T[] array, IEnumerable<T> source, int destinationIndex = 0)
        {
            int index = destinationIndex;
            foreach (var item in source)
                array[index++] = item;
        }
    }
}
