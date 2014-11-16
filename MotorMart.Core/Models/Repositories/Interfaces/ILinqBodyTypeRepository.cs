using System;
using System.Collections.Generic;
using MotorMart.Core.Models;

namespace MotorMart.Core.Models
{
    public interface ILinqBodyTypeRepository
    {
        void AddBodyType(bodytype BodyTypeToAdd);

        void DeleteBodyType(bodytype BodyType);

        bodytype GetBodyType(int BodyTypeId);

        bodytype GetBodyTypeAbove(int BodyTypeId);

        bodytype GetBodyTypeBelow(int BodyTypeId);

        IList<bodytype> GetBodyTypes();

        IList<bodytype> GetBodyTypes(int BodyTypeId);

        bodytype GetBodyType(string type);

        bool BodyTypeExists(string type);        

        void Update();
    }
}
