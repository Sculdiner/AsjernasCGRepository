using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BoardController : ViewController
{
    public BoardView _view;

    public BoardController(View controlledView) : base(controlledView)
    {
        _view = controlledView as BoardView;
    }
}
