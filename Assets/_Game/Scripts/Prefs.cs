using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Prefs : MonoBehaviour
{
    public static int highScore
    {
        get => PlayerPrefs.GetInt(Constant.HIGH_SCORE, 0);
        set
        {
            int currentScore = PlayerPrefs.GetInt(Constant.HIGH_SCORE);
            if (value > currentScore)
            {
                PlayerPrefs.SetInt(Constant.HIGH_SCORE, value);
            }
        }
    }
}
