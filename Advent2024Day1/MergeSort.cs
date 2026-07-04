
namespace Advent2024Day1
{
    public class MergeSort
    {
        public static int[] Sort(IEnumerable<int> arr)
        {
            if (arr.Count() <= 1)
            {
                return arr.ToArray();
            }

            var array = arr.ToArray();
            int arrayMidpoint = array.Length / 2;

            var subArray1 = array.Skip(0).Take(arrayMidpoint);
            var subArray2 = array.Skip(arrayMidpoint);

            var sorted1 = Sort(subArray1);
            var sorted2 = Sort(subArray2);

            var sortedArr = Merge(sorted1, sorted2);

            return sortedArr;
        }

        private static int[] Merge(int[] arr1, int[] arr2)
        {
            var n1 = arr1.Length;
            var n2 = arr2.Length;

            // initial indices of subarrays
            var i = 0;
            var j = 0;

            // initial index of result
            var k = 0;

            var result = new int[n1 + n2];

            while (i < n1 && j < n2)
            {
                if (arr1[i] <= arr2[j])
                {
                    result[k] = arr1[i];
                    i++;
                }
                else
                {
                    result[k] = arr2[j];
                    j++;
                }
                k++;
            }

            // copy remaining elements of arr1
            while (i < n1)
            {
                result[k] = arr1[i];
                i++; k++;
            }

            // copy remaining elements of arr2
            while (j < n2)
            {
                result[k] = arr2[j];
                j++; k++;
            }

            return result;
        }
    }
}
