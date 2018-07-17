using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindow : MonoBehaviour
{
    [SerializeField]
    private Material m_Material;

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    // Define function signatures to import from Windows APIs

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    static extern int SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte bAlpha, int dwFlags);

    // Definitions of window styles
    const int GWL_STYLE = -16;
    const uint WS_POPUP = 0x80000000;
    const uint WS_VISIBLE = 0x10000000;

    void Start()
    {
        RetoggleTransparency();     
    }

    // Pass the output of the camera to the custom material
    // for chroma replacement
    void OnRenderImage(RenderTexture from, RenderTexture to)
    {
        #if UNITY_STANDALONE_WIN
        Graphics.Blit(from, to, m_Material);
        #endif
    }

    public void RetoggleTransparency()
    {
    #if UNITY_STANDALONE_WIN
        var margins = new MARGINS() { cxLeftWidth = -1 };
 
            // Get a handle to the window
            var hwnd = GetActiveWindow();
 
            // Set properties of the window
            // See: https://msdn.microsoft.com/en-us/library/windows/desktop/ms633591%28v=vs.85%29.aspx
            SetWindowLong(hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
         
            // Extend the window into the client area
            //See: https://msdn.microsoft.com/en-us/library/windows/desktop/aa969512%28v=vs.85%29.aspx 
            DwmExtendFrameIntoClientArea(hwnd, ref margins);

            SetLayeredWindowAttributes(hwnd, 0x00FF00FF, 0, 1);

            BorderlessWindow bw = new BorderlessWindow();
            bw.ToggleWindow();
    #endif


    }


}
