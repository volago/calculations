using System;
using System.Text;

namespace CalculationsHelpers
{
    public static class Helpers
    {
        private static Random _rnd = new Random();

        public static int GetRandomInt(int minValue, int maxValue)
        {
            return _rnd.Next(minValue, maxValue);
        }

        public static int GetRandomInt(int maxValue)
        {
            return GetRandomInt(0, maxValue);
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

        public static string GetRandomEmail()
        {
            return string.Format("{0}@client.com", GetRandomString(10));
        }

        public static int GetRandomLoadId()
        {
            return GetRandomInt(1000000, 2000000);
        }
    }
}
