using AsjernasCG.Common.BusinessModels.CardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ClientCardTemplate : BaseCardTemplate
{
    public string ImagePath { get; set; }
    public int? CurrentQuestPoints { get; set; }
    public int? RemainingCooldown
    {
        get
        {
            return base.InternalCooldownTarget - base.InternalCooldownCurrent;
        }
    }
}
