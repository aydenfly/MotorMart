using System;
using MotorMart.Cms.Areas.Misc.Models;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Misc.Services
{
    public interface IBodyTypeService
    {
        bool AddBodyType(BodyTypeAddModel add);

        bool BodyTypeDown(int? Id);

        bool BodyTypeUp(int? Id);

        bool DeleteBodyType(BodyTypeDeleteModel model);

        bool EditBodyType(BodyTypeEditModel edit);

        bool GetBodyType(BodyTypeGetModel get, out bodytype BodyType);

        BodyTypeAddModel PopulateBodyTypeAddModel(bodytype BodyType);

        BodyTypeEditModel PopulateBodyTypeEditModel(bodytype BodyType);

        void PopulateBodyTypeViewModel(BodyTypeViewModel model);
    }
}
