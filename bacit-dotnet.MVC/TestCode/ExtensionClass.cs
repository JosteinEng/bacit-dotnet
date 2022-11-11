
namespace TestCode;

public static class IntExtension
{
    public static bool IsBiggestInt(this int input, int otherValue)
    {
        return input > otherValue;
    }

    public static bool IsLowestInt(this int input, int otherValue)
    {
        return input < otherValue;
    }
}