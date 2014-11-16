using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Models
{
    public class LinqBodyTypeRepository : ILinqBodyTypeRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        public bodytype GetBodyType(int BodyTypeId)
        {
            return _datacontext.bodytypes.Where(m => m.bodytypeid == BodyTypeId).FirstOrDefault();
        }
        
        public IList<bodytype> GetBodyTypes()
        {
            return _datacontext.bodytypes.ToList();
        }

        public bodytype GetBodyType(string type)
        {
            return _datacontext.bodytypes.Where(m => m.type.ToLower() == type.ToLower()).FirstOrDefault();
        }

        public bool BodyTypeExists(string type)
        {
            return _datacontext.bodytypes.Where(m => m.type.ToLower() == type.ToLower()).Any();
        }

        public IList<bodytype> GetBodyTypes(int BodyTypeId)
        {
            return _datacontext.bodytypes.Where(m => m.bodytypeid == BodyTypeId).ToList();
        }

        public void AddBodyType(bodytype BodyTypeToAdd)
        {
            _datacontext.bodytypes.InsertOnSubmit(BodyTypeToAdd);
            _datacontext.SubmitChanges();
        }

        public void DeleteBodyType(bodytype BodyType)
        {
            _datacontext.bodytypes.DeleteOnSubmit(BodyType);
            _datacontext.SubmitChanges();
        }
        
        public bodytype GetBodyTypeBelow(int BodyTypeId)
        {
            bodytype relativeBodyType = this.GetBodyType(BodyTypeId);
            return _datacontext.bodytypes.Where(v => v.sortorder > relativeBodyType.sortorder).OrderBy(p => p.sortorder).First();
        }

        public bodytype GetBodyTypeAbove(int BodyTypeId)
        {
            bodytype relativeBodyType = this.GetBodyType(BodyTypeId);
            return _datacontext.bodytypes.Where(v => v.sortorder < relativeBodyType.sortorder).OrderByDescending(p => p.sortorder).First();
        }

        public void Update()
        {
            _datacontext.SubmitChanges();
        }
    }
}