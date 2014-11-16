using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class TransmissionViewModel : AdminViewModel
    {
        public IList<transmission> TransmissionsList;

        public transmission CurrentTransmission { get; set; }

        public TransmissionGetModel get { get; set; }
        public TransmissionAddModel add { get; set; }
        public TransmissionEditModel edit { get; set; }
        public TransmissionDeleteModel delete { get; set; }

        public bool Success { get; set; }
    }
}