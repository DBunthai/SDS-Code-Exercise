using System;
using System.Text;

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        public static int GetFactorial(int n)
        {
            int factorial = 1;
            for (int i = 1; i <= n; i++)
            {
                factorial *= i;
            }

            return factorial;
        }
        public static string FormatSeparators(params string[] items)
        {
            StringBuilder newString = new StringBuilder();
            for (int i = 0; i < items.Length; i++)
            {
                if (items.Length - 1 == i)
                {
                    newString.Append(" and " + items[i]);
                } else if(i == 0)
                {
                    newString.Append(items[i]);
                }
                else
                {
                    newString.Append(", " + items[i]);
                }
            }
            return newString.ToString();
        }
    }
}