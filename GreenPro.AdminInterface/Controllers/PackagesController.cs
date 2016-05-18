﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GreenPro.Data;
using GreenPro.AdminInterface.ViewModels;

namespace GreenPro.AdminInterface.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PackagesController : BaseController
    {
        private GreenProDbEntities db ;


        public PackagesController()
        {
            db = new GreenProDbEntities();

        }

        

        // GET: Packages
        public ActionResult Index()
        {
            var packages = db.Packages.Include(p => p.Package_Services);
            return View(packages.ToList());
        }

        // GET: Packages/Details/5
        // GET: UserPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPackage userPackage = db.UserPackages.Find(id);
            if (userPackage == null)
            {
                return HttpNotFound();
            }

            UserPackageDetailViewModel userPackageModel = new UserPackageDetailViewModel();
            userPackageModel.Id = userPackage.Id;
            userPackageModel.ActualPrice = userPackage.ActualPrice;
            userPackageModel.TotalPrice = userPackage.TotalPrice;
            userPackageModel.TaxAmount = userPackage.TaxAmount;
            userPackageModel.TipAmount = userPackage.TipAmount;
            userPackageModel.ServiceDay = userPackage.ServiceDay;
            userPackageModel.SubscribedDate = userPackage.SubscribedDate;

            userPackageModel.Package = new PackageDetailViewModel();
            userPackageModel.Package.Package_Name = userPackage.Package.Package_Name;
            userPackageModel.Package.Package_Description = userPackage.Package.Package_Description;


            userPackageModel.Services = new List<PackageCarServiceViewModel>();

            var ServiceList = userPackage.Package.Package_Services.Select(s => s.Service).ToList();
            foreach (var service in ServiceList)
            {
                PackageCarServiceViewModel serviceModel = new PackageCarServiceViewModel();
                serviceModel.Service_Name = service.Service_Name;
                userPackageModel.Services.Add(serviceModel);
            }

            if (userPackage.UserPackagesAddons.Count > 0)
            {
                foreach (var userPackagesAddon in userPackage.UserPackagesAddons)
                {
                    PackageCarServiceViewModel serviceModel = new PackageCarServiceViewModel();
                    serviceModel.Service_Name = userPackagesAddon.Service.Service_Name;
                    serviceModel.IsAddOn = true;
                    userPackageModel.Services.Add(serviceModel);
                }
            }

            /// Prepare Car Model
            /// 
            PackageCarViewModel carModel = new PackageCarViewModel();

            var item = db.CarUsers.Where(c => c.CarId == userPackage.CarId).SingleOrDefault();

            if (item == null)
                return RedirectToAction("Index");

            userPackageModel.CarId = item.CarId;

            carModel = new PackageCarViewModel();
            carModel.AutoRenewal = item.AutoRenewal;
            carModel.CarId = item.CarId;
            carModel.Color = item.Color;
            carModel.DisplayName = item.DisplayName;
            carModel.GarageId = item.GarageId;
            carModel.IsDeleted = item.IsDeleted;
            carModel.LicenseNumber = item.LicenseNumber;
            carModel.Make = item.Make;
            carModel.PurchaseYear = item.PurchaseYear;
            //carModel.Type = item.Type; //comment by circus
            carModel.UserId = item.UserId;

            // carModel.CarType = item.CarType.Description; //comment by circus
            if (item.Garage != null)
                carModel.Garage = item.Garage.Garage_Name;

            userPackageModel.CarModel = carModel;


            /// Prepare Payments Historys
            var paypalAutoPaymentList = db.PaypalAutoPayments.Where(p => p.UserPackageID == id && p.IsPaid == true).ToList();
            if (paypalAutoPaymentList.Count > 0)
            {
                foreach (var payment in paypalAutoPaymentList)
                {
                    PaypalAutoPaymentsViewModel paymentViewModel = new PaypalAutoPaymentsViewModel();
                    paymentViewModel.Id = payment.Id;
                    paymentViewModel.ReferenceID = payment.ReferenceID;
                    paymentViewModel.TrasactionID = payment.TrasactionID;
                    paymentViewModel.TransactionDate = payment.TransactionDate;
                    paymentViewModel.GrossAmount = payment.GrossAmount;
                    paymentViewModel.CreatedOn = payment.CreatedOn;
                    paymentViewModel.ServiceDate = payment.ServiceDate;
                    userPackageModel.PaymentHistorys.Add(paymentViewModel);

                }
            }

            return View(userPackageModel);


        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            ViewBag.Services = db.Services.ToList();
            ViewBag.CarTypeId = new SelectList(db.CarTypes, "Id", "Description");
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Package_Name,Package_Description,Package_Price,CarTypeId")] Package package, string[] selectedServices)
        {
            //ViewBag.CarTypeId = new SelectList(db.CarTypes, "Id", "Description", package.CarTypeId); //comment by circus
            ViewBag.Services = db.Services.ToList();
            if (selectedServices != null)
            {
                ModelState.Remove("CreatedBy");
                ModelState.Remove("CreateDt");
               
                package.CreatedBy = "Admin";
                package.CreateDt = DateTime.Now;
                if (ModelState.IsValid)
                {

                    db.Packages.Add(package);
                   // db.SaveChanges();
                    List<Package_Services> packageServices = new List<Package_Services>();
                    var packageService = new Package_Services();
                    foreach (var service in selectedServices)
                    {
                        packageService = new Package_Services { PackageID = package.PackageId, ServiceID = Convert.ToInt32(service)};
                        packageServices.Add(packageService);
                    }
                    db.Package_Services.AddRange(packageServices);
                    db.SaveChanges();
                    AddNotification(Models.NotifyType.Success, "Records Successfully Saved.", true);
                    return RedirectToAction("Index");

                }
            }
            else
            {

                ViewBag.Message = "Please select atleast one service";
                return View();
            }




            return View(package);
        }

        // GET: Packages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.ServicesAvailable = db.Services.
            //    ViewBag.ServicesAdded = db.Services.
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            ViewBag.Services = db.Services.ToList();
            ViewBag.CheckedServices = package.Package_Services.Select(a => a.Service).ToList();
            //ViewBag.CarTypeId = new SelectList(db.CarTypes, "Id", "Description", package.CarTypeId);
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Package package, string[] selectedServices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                package.CreateDt = DateTime.Now;
                db.SaveChanges();
                var deleteList = db.Package_Services.Where(a => a.PackageID == package.PackageId).ToList();
                db.Package_Services.RemoveRange(deleteList);
                var packageServices = new List<Package_Services>();
                foreach (var service in selectedServices)
                {
                    var packageService = new Package_Services { PackageID = package.PackageId, ServiceID = Convert.ToInt32(service) };
                    packageServices.Add(packageService);
                }
                db.Package_Services.AddRange(packageServices);

                db.SaveChanges(); // added by sachin 18 March 2016
                AddNotification(Models.NotifyType.Success, "Records Successfully Updated.", true);
                return RedirectToAction("Index");

            }

            ViewBag.Services = db.Services.ToList();
            ViewBag.CheckedServices = package.Package_Services.Select(a => a.Service).ToList();

            //ViewBag.CarTypeId = new SelectList(db.CarTypes, "Id", "Description", package.CarTypeId);
            return View(package);
        }

        // GET: Packages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Package package = db.Packages.Find(id);
            db.Packages.Remove(package);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        #region Users Packages

        List<UserPackageViewModel> PrepareUserPackageList(List<UserPackage> userPackageList)
        {
            List<UserPackageViewModel> UserPackages = new List<UserPackageViewModel>();
            foreach (var item in userPackageList)
            {
                UserPackageViewModel userPackage = new UserPackageViewModel();
                userPackage.Id = item.Id;
                userPackage.Ipaddress = item.Ipaddress;
                userPackage.SubscribedDate = item.SubscribedDate;
                userPackage.CreatedDt = item.CreatedDt;
                userPackage.CustomerName = item.AspNetUser.Email;

                userPackage.Package = new PackageViewModel();
                userPackage.Package.PackageId = item.Package.PackageId;
                userPackage.Package.Package_Name = item.Package.Package_Name;
                userPackage.CarId = item.CarId;

                UserPackages.Add(userPackage);
            }

            return UserPackages;
        }

        public ActionResult UserPackageList()
        {
            var userPackageList = db.UserPackages.Where(p => p.PaymentRecieved == true).ToList();

            UserPackageListViewModel model = new UserPackageListViewModel();

            model.UserPackages = PrepareUserPackageList(userPackageList);


            return View(model);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



    }
}
