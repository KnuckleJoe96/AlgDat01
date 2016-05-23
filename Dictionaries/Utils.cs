namespace Dictionaries
{
    public class Utils
    {
        // mathematically correct modulo operation
        public static int mathMod(int dividend, int divisor)
        {
            int x;
            return (x = dividend % divisor) < 0 ? x + divisor : x;
        }

        public static T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];

            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }
    }
}