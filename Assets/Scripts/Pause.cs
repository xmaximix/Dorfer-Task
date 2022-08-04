using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause
{
    public static bool paused = false;

    public static void PauseGame()
    {
        Time.timeScale = 0f;
        paused = true;
    }

    public static void UnpauseGame()
    {
        Time.timeScale = 1f;
        paused = false;
    }
}