using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    private static Joystick joystick;

    public static void InitJoystick(Joystick joystickObj)
    {
        joystick = joystickObj;
    }

    public static float GetJoystickHorizontal()
    {
        if (Pause.paused)
        {
            return 0;
        }

        return joystick.Horizontal;
    }

    public static float GetJoystickVertical()
    {
        if (Pause.paused)
        {
            return 0;
        }

        return joystick.Vertical;
    }
}
