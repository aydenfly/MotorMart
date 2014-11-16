using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;

namespace MotorMart.Core.Models
{
    [MetadataType(typeof(vehicle))]
    public partial class vehicle
    {
        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Insert)
            {
                _numberofseats = 2;
                _mileage = 0;
                if (_dateofmanufacture == null) _dateofmanufacture = DateTime.Now;
                if (_enginesize == null) _enginesize = decimal.Round(decimal.Zero, 2);
                if (_co2emissions == null) _co2emissions = String.Empty;
                _manufacturerwarrantyyears = 0;
                _manufacturerwarrantymiles = 0;
                _paintworkguaranteeyears = 0;
                _corrosionguaranteeyears = 0;
                if (_taxband == null) _taxband = String.Empty;
                if (_yearofregistration == 0) _yearofregistration = DateTime.Now.Year;
                if (_sellingprice == null) _sellingprice = 0;
            }

            if (action == ChangeAction.Update)
            {
                //if (_numberofdoors == null) _numberofdoors = 2;
                //if (_numberofseats == null) _numberofseats = 2;
                //if (_mileage == null) _mileage = 0;
                if (_dateofmanufacture == null) _dateofmanufacture = DateTime.Now;
                if (_enginesize == null) _enginesize = decimal.Round(decimal.Zero, 2);
                if (_co2emissions == null) _co2emissions = String.Empty;
                //if (_manufacturerwarrantyyears == null) _manufacturerwarrantyyears = 0;
                //if (_manufacturerwarrantymiles == null) _manufacturerwarrantymiles = 0;
                //if (_paintworkguaranteeyears == null) _paintworkguaranteeyears = 0;
                //if (_corrosionguaranteeyears == null) _corrosionguaranteeyears = 0;
                if (_taxband == null) _taxband = String.Empty;
                if (_yearofregistration == 0) _yearofregistration = DateTime.Now.Year;
                if (_sellingprice == null) _sellingprice = 0;

            }
        }
    }
}
