#if (UNITY_STANDALONE_WIN || UNITY_EDITOR)

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BorderlessWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{






    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    public const int WM_NCLBUTTONDOWN = 0xA1;
    public const int WM_NCLBUTTONUP = 0x00A2;
    public const int WM_LBUTTONUP = 0x0202;
    private const int WM_SYSCOMMAND = 0x112;
    private const int MOUSE_MOVE = 0xF012;
    public const int HTCAPTION = 0x2;

    [DllImport("User32.dll")]
    public static extern bool ReleaseCapture();

    [DllImport("User32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern bool SendMessageCallback(IntPtr hWnd, uint Msg, UIntPtr wParam,
    IntPtr lParam, SendMessageDelegate lpCallBack, UIntPtr dwData);

    delegate void SendMessageDelegate(IntPtr hWnd, uint uMsg, UIntPtr dwData, IntPtr lResult);



    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);


    // Definitions of window styles
    const int GWL_STYLE = -16;
    const uint WS_POPUP = 0x80000000;
    const uint WS_VISIBLE = 0x10000000;
    IntPtr window = GetActiveWindow();

    //bool draggingWindow = false;
    Vector2 lastPosition = Vector2.zero;
 


    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(System.String className, System.String windowName);

    public static void SetPosition(int x, int y, int resX = 0, int resY = 0)
    {
        SetWindowPos(FindWindow(null, "My window title"), 0, x, y, resX, resY, resX * resY == 0 ? 1 : 0);
    }



    public void OnMinimizeButtonClick()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

    public void OnCloseButtonClick()
    {
        if (Process.GetCurrentProcess().ToString() != "System.Diagnostics.Process (Unity)")
            Process.GetCurrentProcess().Kill();
    }

    public void ToggleWindow()
    {
        ShowWindow(GetActiveWindow(), 2);
        ShowWindow(GetActiveWindow(), 1);
    }





    public void OnBeginDrag(PointerEventData eventData)
    {

        //draggingWindow = true;

       
       // ReleaseCapture();
        //SendMessage(window, WM_NCLBUTTONDOWN, HTCAPTION, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
    
        

            //this.Location = new Point(
                //this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //draggingWindow = false;
        
   
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("got a pointer down event");
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ReleaseCapture();
            SendMessage(window, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
    }


}

#endif
