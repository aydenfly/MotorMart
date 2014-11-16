using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Cms.Controllers;

namespace MotorMart.Cms.Areas.Misc.Controllers
{
    public class UserAccountController : AdminMasterController
    {
        //
        // GET: /Misc/UserAccount/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Misc/UserAccount/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Misc/UserAccount/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Misc/UserAccount/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Misc/UserAccount/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Misc/UserAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Misc/UserAccount/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Misc/UserAccount/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
