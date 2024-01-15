namespace Transactions.Application.SeedWork.Extensions;
public static class ArrayExtensions
{
    public static async Task<T[]> ToArray<T>(this Task<IEnumerable<T>> task)
    {
        return (await task).ToArray();
    }
}
