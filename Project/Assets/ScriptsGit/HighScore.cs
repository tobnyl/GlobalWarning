using UnityEngine;
using System.Collections;

public static class HighScore
{
    private static int hiScore;

    public static int HiScore
    {
        get { return hiScore; }
        set { hiScore = value; }
    }

}
