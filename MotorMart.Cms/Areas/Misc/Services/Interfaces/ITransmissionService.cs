using System;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Misc.Services
{
    public interface ITransmissionService
    {
        bool AddTransmission(TransmissionAddModel add);

        bool DeleteTransmission(TransmissionDeleteModel model);

        bool EditTransmission(TransmissionEditModel edit);

        bool GetTransmission(TransmissionGetModel get, out transmission Transmission);

        TransmissionAddModel PopulateTransmissionAddModel(transmission Model);

        TransmissionEditModel PopulateTransmissionEditModel(transmission Model);

        void PopulateTransmissionViewModel(TransmissionViewModel model);
        
        bool TransmissionDown(int? Id);

        bool TransmissionUp(int? Id);
    }
}
