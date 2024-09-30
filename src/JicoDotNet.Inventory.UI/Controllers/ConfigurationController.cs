﻿using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Web.Mvc;

namespace JicoDotNet.Inventory.UIControllers
{
    public class ConfigurationController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Setup()
        {
            try
            {

                ConfigarationManager configarationManager = new ConfigarationManager(LogicHelper);
                ConfigModels configModels = new ConfigModels
                {
                    _YesNo = GenericLogic.YesNo(),
                    _config = configarationManager.GetConfig(),
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
                    }
                };
                return View(configModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Setup(Config config)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(config);
                #endregion

                ConfigarationManager configarationManager = new ConfigarationManager(LogicHelper);
                configarationManager.SetConfig(config);
                ReturnMessage = new ReturnObject()
                {
                    Message = "Success",
                    Status = true
                };
                return RedirectToAction("Detail", "Company", new { id = UrlIdEncrypt(UrlParameterId, false) });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Details()
        {
            try
            {
                ConfigModels configModels = new ConfigModels
                {
                    _company = SessionCompany,
                    _config = new ConfigarationManager(LogicHelper).GetConfig(),
                    _YesNo = GenericLogic.YesNo()
                };
                return View(configModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Default()
        {
            ConfigModels configModels = new ConfigModels
            {
                _config = new ConfigarationManager(LogicHelper).GetConfig(),
            };
            return View(configModels);
        }

        [HttpPost]
        public ActionResult Default(Config config)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(config);
                #endregion

                ConfigarationManager configarationManager = new ConfigarationManager(LogicHelper);
                configarationManager.SetConfig(config);
                ReturnMessage = new ReturnObject()
                {
                    Message = "Success",
                    Status = true
                };
                return RedirectToAction("Default", "Configuration");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
    }
}