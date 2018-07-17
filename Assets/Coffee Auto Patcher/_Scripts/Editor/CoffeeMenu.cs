using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CoffeeMenu
{

	[MenuItem("Tools/Coffee Auto Patcher/About Coffee", false, 0)]
    public static void About()
    {
        AboutBox.Create();
    }

}
