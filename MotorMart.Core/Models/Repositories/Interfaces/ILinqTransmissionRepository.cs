using System;
using MotorMart.Core.Models;
using System.Collections.Generic;

namespace MotorMart.Core.Models
{
    public interface ILinqTransmissionRepository
    {
        void AddTransmission(transmission TransmissionToAdd);

        void DeleteTransmission(transmission Transmission);

        transmission GetTransmission(int TransmissionId);

        transmission GetTransmission(string name);

        bool TransmissionExists(string name);

        transmission GetTransmissionAbove(int TransmissionId);

        transmission GetTransmissionBelow(int TransmissionId);

        IList<transmission> GetTransmissions();

        IList<transmission> GetTransmissions(int TransmissionId);

        void Update();
    }
}
