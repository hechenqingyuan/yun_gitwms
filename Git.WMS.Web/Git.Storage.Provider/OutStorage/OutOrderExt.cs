/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/3/17 16:36:09
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/3/17 16:36:09       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using Git.Storage.Provider.Base;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Sys;
using Git.Storage.Entity.Storage;
using Git.Storage.Provider.Sys;
using Git.Storage.Entity.OutStorage;

namespace Git.Storage.Provider.OutStorage
{
    public class OutOrderExt : OutStorageOrder
    {
        public OutOrderExt(string _CompanyID)
            : base(_CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 设置承运商
        /// </summary>
        /// <param name="SnNum"></param>
        /// <param name="CarrierNum"></param>
        /// <param name="LogisticsNo"></param>
        /// <returns></returns>
        public int SetCarrier(string SnNum, string CarrierNum, string LogisticsNo)
        {
            CarrierProvider provider = new CarrierProvider(this.CompanyID);

            OutStorageEntity entity = new OutStorageEntity();
            entity.CarrierNum = CarrierNum;
            CarrierEntity carrier = provider.GetSingle(CarrierNum);
            if (carrier != null)
            {
                entity.CarrierName = carrier.CarrierName;
            }
            entity.LogisticsNo = LogisticsNo;

            entity.Include(item => new { item.CarrierNum, item.CarrierName, item.LogisticsNo });
            entity.Where(item => item.SnNum == SnNum).And(item => item.CompanyID == this.CompanyID);
            int line = this.OutStorage.Update(entity);

            return line;
        }

        /// <summary>
        /// 根据出库单编号查询出库单
        /// </summary>
        /// <param name="OrderNum"></param>
        /// <returns></returns>
        public OutStorageEntity GetOrderByNum(string OrderNum)
        {
            OutStorageEntity entity = new OutStorageEntity();

            entity.IncludeAll();

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeAdmin = new AdminEntity();
            auditeAdmin.Include(item => new { AuditeUserName = item.UserName });
            entity.Left<AdminEntity>(auditeAdmin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            entity.Where(a => a.OrderNum == OrderNum)
                .And(a => a.CompanyID == this.CompanyID)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                ;
            entity = this.OutStorage.GetSingle(entity);
            return entity;
        }
    }
}
