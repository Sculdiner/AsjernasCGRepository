

using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MouseHelper
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;

    public void DoMouseClick()
    {
        //Call the imported function with the cursor's current position
        float X = Input.mousePosition.x;
        float Y = Input.mousePosition.y;
        var x =Convert.ToInt32(X);
        var y = Convert.ToInt32(Y);
        mouse_event(MOUSEEVENTF_LEFTDOWN , x, y, 0, 0);
    }

    //...other code needed for the application
}