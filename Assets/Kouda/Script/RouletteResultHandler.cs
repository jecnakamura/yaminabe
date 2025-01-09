public static class RouletteResultHandler
{
    private static bool isEnd = false;
    private static int result = 0;

    public static void SetResult(int value)
    {
        result = value;
    }

    public static int GetResult()
    {
        return result;
    }
    public static void SetEnd(bool flag)
    {
        isEnd = flag;
    }

    public static bool IsEnd()
    {
        return isEnd;
    }
}
