using AsjernasCG.Common.OperationHelpers.Game;
using AsjernasCG.Common.OperationModels;
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

    public void SendClientReady()
    {
        SendOperation(new GameOperationHelper<GameStatusModel>(new GameStatusModel() { ClientGameSetupDone = true }), true, 0, false);
    }
}
