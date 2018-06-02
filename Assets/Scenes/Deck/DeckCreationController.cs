using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCreationController : ViewController
{
    public DeckCreationView _view;
    public DeckCreationController(View controlledView) : base(controlledView)
    {
        _view = controlledView as DeckCreationView;
    }
}
