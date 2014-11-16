using System;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Misc.Services
{
    public interface IDealerService
    {
        bool AddDealer(DealerAddModel add);

        bool DealerDown(int? Id);

        bool DealerUp(int? Id);

        bool DeleteDealer(DealerDeleteModel model);

        bool EditDealer(DealerEditModel edit);

        bool GetDealer(DealerGetModel get, out dealer Dealer);

        DealerAddModel PopulateDealerAddModel(dealer Dealer);

        DealerEditModel PopulateDealerEditModel(dealer Dealer);

        void PopulateDealerViewModel(DealerViewModel model);
    }
}
