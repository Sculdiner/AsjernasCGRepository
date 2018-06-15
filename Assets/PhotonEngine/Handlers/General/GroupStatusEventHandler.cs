using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.PhotonEngine.Handlers.General
{
    public class GroupStatusEventHandler<TModel> : BaseEventHandler<TModel> where TModel : GroupStatusModel
    {
        public override byte EventCode
        {
            get
            {
                return (byte)ClientGeneralEventCode.GroupStatus;
            }
        }

        public override void OnHandleEvent(View view, TModel model)
        {
            GroupManager.StoreGroupStatus(model);
            view.ChangeScene("PlayMenu");
        }
    }
}
