/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/5/7 22:53:30
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/5/7 22:53:30       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.InStorage;
using Git.Storage.Entity.Sys;
using Git.Framework.ORM;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider.InStorage
{
    public partial class InOrderExt:InStorageOrder
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_CompanyID"></param>
        public InOrderExt(string _CompanyID)
            : base(_CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 根据入库单编号查询入库单
        /// </summary>
        /// <returns></returns>
        public InStorageEntity GetOrderByNum(string OrderNum)
        {
            InStorageEntity entity = new InStorageEntity();

            entity.IncludeAll();

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                .And(a => a.OrderNum == OrderNum);

            entity = this.InStorage.GetSingle(entity);
            return entity;
        }
    }
}
