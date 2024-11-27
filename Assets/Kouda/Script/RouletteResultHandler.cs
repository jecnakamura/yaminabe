using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RouletteResultHandler
{
    private static int result = 0;

    public static void SetResult(int value)
    {
        result = value;
    }

    public static int GetResult()
    {
        return result;
    }
}
