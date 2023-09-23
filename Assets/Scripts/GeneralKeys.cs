using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralKeys : MonoBehaviour
{
    int prevGameSpeed = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            if (prevGameSpeed == 0)
            {
                prevGameSpeed = GameSpeed.gameSpeed;
                GameSpeed.gameSpeed = 0;
            }
            else
            {
                GameSpeed.gameSpeed = prevGameSpeed;
                prevGameSpeed = 0;
            }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            GameSpeed.gameSpeed = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            GameSpeed.gameSpeed = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            GameSpeed.gameSpeed = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            GameSpeed.gameSpeed = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            GameSpeed.gameSpeed = 5;
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            if (GameSpeed.gameSpeed < 5)
                GameSpeed.gameSpeed++;
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            if (GameSpeed.gameSpeed > 0)
                GameSpeed.gameSpeed--;
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
