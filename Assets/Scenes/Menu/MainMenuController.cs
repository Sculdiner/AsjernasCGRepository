using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : ViewController
{

    public MainMenuView _view;

    public MainMenuController(View controlledView) : base(controlledView)
    {
        _view = controlledView as MainMenuView;
    }
}
