using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class GroupRequestResponseEventHandler<TModel> : BaseEventHandler<TModel> where TModel : GroupRequestResponseModel
    {
        public override byte EventCode
        {
            get
            {
                return (byte)ClientGeneralEventCode.GroupRequestResponse;
            }
        }

        public override void OnHandleEvent(View view, TModel model)
        {
            if (model.GroupAccepted)
            {
                view.LogInfo("group request accepted ");
            }
            else
            {
                view.LogInfo("group request declined");
            }
            
        }
    }
