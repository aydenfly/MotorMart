using MotorMart.Core.Models;

namespace MotorMart.Core.Models
{
    public class VehicleViewModel : MasterViewModel
    {
        public vehicle CurrentVehicle { get; set; }

        public VehicleGetModel get { get; set; }
    }
}