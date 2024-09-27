﻿using DataAccess.AzureStorage;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class SMSBankLogic : ConnectionString
    {
        public SMSBankLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public bool Deduction()
        {
            string Query = " IsActive eq true";
            TableManager = new ExecuteTableManager("SMSBank", CommonObj.NoSqlConnectionString);
            SMSBank sMSBank = TableManager.RetrieveEntity<SMSBank>(Query).FirstOrDefault();
            if (sMSBank != null && sMSBank.Balance > 0)
            {
                sMSBank.Balance--;
                TableManager.UpdateEntity(sMSBank);
                return true;
            }
            return false;
        }

        public async Task Addition(long Balance)
        {
            #pragma warning disable CS4014
            Task.Run(() =>
            {
                string Query = " IsActive eq true";
                TableManager = new ExecuteTableManager("SMSBank", CommonObj.NoSqlConnectionString);
                SMSBank sMSBank = TableManager.RetrieveEntity<SMSBank>(Query).FirstOrDefault();
                if (sMSBank != null)
                {
                    sMSBank.Balance += Balance;
                    TableManager.ReplaceEntity(sMSBank);
                    return;
                }
                else
                {
                    sMSBank = new SMSBank
                    {
                        PartitionKey = "MyCompany",
                        RowKey = GenericLogic.IstNow.TimeStamp().ToString("X"),
                        Balance = Balance,
                        IsActive = true,
                        RequestId = CommonObj.RequestId,
                        TransactionDate = GenericLogic.IstNow
                    };
                    TableManager.InsertEntity(sMSBank);
                    return;
                }
            });
            #pragma warning restore CS4014
        }

        public SMSBank Get()
        {
            string Query = " IsActive eq true";
            TableManager = new ExecuteTableManager("SMSBank", CommonObj.NoSqlConnectionString);
            return TableManager.RetrieveEntity<SMSBank>(Query).FirstOrDefault();
        }
    }
}
