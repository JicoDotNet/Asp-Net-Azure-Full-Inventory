﻿using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JicoDotNet.Inventory.BusinessLayer.Common;

namespace JicoDotNet.Inventory.UIControllers
{
    public class CompanyController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                return RedirectToAction("Detail");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Detail()
        {
            try
            {
                CompanyModels companyModels = new CompanyModels()
                {
                    _company =
                            new Company()
                            {
                                CompanyName = SessionCompany.CompanyName,
                                GSTNumber = SessionCompany.GSTNumber,
                                GSTStateCode = SessionCompany.GSTStateCode,
                                IsGSTRegistered = SessionCompany.IsGSTRegistered,
                                StateCode = SessionCompany.StateCode,

                                Address = WebConfigAppSettingsAccess.CompanyAddress,
                                City = WebConfigAppSettingsAccess.CompanyCity,
                                Email = WebConfigAppSettingsAccess.CompanyEmail,
                                PINCode = WebConfigAppSettingsAccess.CompanyPINCode,
                                Mobile = WebConfigAppSettingsAccess.CompanyMobile,
                                WebsiteUrl = WebConfigAppSettingsAccess.CompanyWebsite,
                            },

                    _config = new ConfigarationManager(LogicHelper).GetConfig(),
                    _companyBanks = new CompanyManagment(LogicHelper).BankGet(true),
                    _sessionCredential = SessionPerson
                };
                if (companyModels._company != null)
                {
                    return View(companyModels);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Bank()
        {
            try
            {
                CompanyManagment companyManagment = new CompanyManagment(LogicHelper);
                CompanyModels companyModels = new CompanyModels()
                {
                    _company = new Company()
                    {
                        CompanyName = SessionCompany.CompanyName,
                        GSTNumber = SessionCompany.GSTNumber,
                        GSTStateCode = SessionCompany.GSTStateCode,
                        IsGSTRegistered = SessionCompany.IsGSTRegistered,
                        StateCode = SessionCompany.StateCode,

                        Address = WebConfigAppSettingsAccess.CompanyAddress,
                        City = WebConfigAppSettingsAccess.CompanyCity,
                        Email = WebConfigAppSettingsAccess.CompanyEmail,
                        PINCode = WebConfigAppSettingsAccess.CompanyPINCode,
                        Mobile = WebConfigAppSettingsAccess.CompanyMobile,
                        WebsiteUrl = WebConfigAppSettingsAccess.CompanyWebsite,
                    },
                    _companyBanks = companyManagment.BankGet(),
                    _state = GenericLogic.State()
                };
                if (!string.IsNullOrEmpty(id))
                {
                    companyModels._companyBank = companyModels._companyBanks.FirstOrDefault(a => a.CompanyBankId == Convert.ToInt64(id));
                }
                return View(companyModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        [SessionAuthenticate]
        public ActionResult Bank(CompanyBank companyBank)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index", "Company", new { id = string.Empty });
                }
                companyBank.CompanyBankId = id == null ? 0 : Convert.ToInt64(id);

                #region Data Tracking...
                DataTrackingLogicSet(companyBank);
                #endregion

                CompanyManagment companyManagment = new CompanyManagment(LogicHelper);
                if (Convert.ToInt64(companyManagment.BankSet(companyBank)) > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Success",
                        Status = true
                    };
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Unsuccess",
                        Status = false
                    };
                }

                return RedirectToAction("Bank", new { id = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public JsonResult BankDeactivate(string Context)
        {
            try
            {
                if (new LoginManagement(LogicHelper).Authenticate(SessionPerson.UserEmail, Context))
                {
                    CompanyManagment companyManagment = new CompanyManagment(LogicHelper);
                    long deactivateId = Convert.ToInt64(companyManagment.BankDeactive(Convert.ToInt32(id)));
                    return Json(new JsonReturnModels
                    {
                        _isSuccess = true,
                        _returnObject = deactivateId > 0 ? id : "0"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new JsonReturnModels
                    {
                        _isSuccess = true,
                        _returnObject = "-1"
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToJson(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult BankPrint(CompanyBank companyBank)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Company", new { id = string.Empty });
            }
            companyBank.CompanyBankId = Convert.ToInt64(id);
            #region Data Tracking...
            DataTrackingLogicSet(companyBank);
            #endregion

            CompanyManagment companyManagment = new CompanyManagment(LogicHelper);
            if (Convert.ToInt64(companyManagment.BankPrintability(companyBank, true)) > 0)
            {
                ReturnMessage = new ReturnObject()
                {
                    Message = "Successfully set printability",
                    Status = true
                };
            }
            else
            {
                ReturnMessage = new ReturnObject()
                {
                    Message = "Unsuccess",
                    Status = false
                };
            }
            return RedirectToAction("Bank", new { id = string.Empty });
        }

        [SessionAuthenticate]
        public ActionResult BankUnPrint(CompanyBank companyBank)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Company", new { id = string.Empty });
            }
            companyBank.CompanyBankId = Convert.ToInt64(id);
            #region Data Tracking...
            DataTrackingLogicSet(companyBank);
            #endregion

            CompanyManagment companyManagment = new CompanyManagment(LogicHelper);
            if (Convert.ToInt64(companyManagment.BankPrintability(companyBank, false)) > 0)
            {
                ReturnMessage = new ReturnObject()
                {
                    Message = "Successfully remove printability",
                    Status = true
                };
            }
            else
            {
                ReturnMessage = new ReturnObject()
                {
                    Message = "Unsuccess",
                    Status = false
                };
            }
            return RedirectToAction("Bank", new { id = string.Empty });
        }
    }
}