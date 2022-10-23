namespace ThousandWords.Core.Extensions;

public static class ArrayExtensions
{
    public static T[] Randomize<T>(this T[] array)
    {
        var random = new Random();
        for (var i = 0; i < array.Length; i++)
        {
            var firstIndex = random.Next(array.Length - 1);
            var secondIndex = random.Next(array.Length - 1);
            (array[firstIndex], array[secondIndex]) = (array[secondIndex], array[firstIndex]);
        }

        return array;
    }
}