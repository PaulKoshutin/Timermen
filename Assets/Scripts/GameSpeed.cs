using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    private static int gameSpeedPrt;
    public static int gameSpeed
    { get { return gameSpeedPrt; } set { if (!forcePaused) gameSpeedPrt = value; } }
    public static int prevGameSpeed = 0;
    public static bool forcePaused;

    private void Awake()
    {
        gameSpeed = 4;
    }

    private void Update()
    {
        if (gameSpeed == 0)
            Time.timeScale = 0;
        else if (gameSpeed == 1)
            Time.timeScale = 0.025f;
        else if (gameSpeed == 2)
            Time.timeScale = 0.05f;
        else if (gameSpeed == 3)
            Time.timeScale = 0.1f;
        else if (gameSpeed == 4)
            Time.timeScale = 0.2f;
        else if (gameSpeed == 5)
            Time.timeScale = 0.4f;
    }
    public static void ForcePause(bool pause)
    {
        if (pause)
        {
            prevGameSpeed = gameSpeed;
            gameSpeed = 0;
            forcePaused = true;
        }
        else
        {
            forcePaused = false;
            gameSpeed = prevGameSpeed;
        }
    }
}
