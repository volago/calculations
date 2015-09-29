using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations
{
    public static class Helpers
    {
        private static Random _rnd = new Random();

        public static int GetRandomInt(int minValue, int maxValue)
        {
            return _rnd.Next(minValue, maxValue);
        }

        public static string GetRandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _rnd.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString().ToLower();
        }
    }
}
