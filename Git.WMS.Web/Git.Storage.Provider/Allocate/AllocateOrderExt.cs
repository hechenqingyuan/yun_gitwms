/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/5/8 9:26:12
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/5/8 9:26:12       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Entity.Allocate;
using Git.Storage.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider.Allocate
{
    public partial class AllocateOrderExt : AllocateOrder
    {
        public AllocateOrderExt(string _CompanyID)
            : base(_CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 根据调拨单号查询调拨单
        /// </summary>
        /// <param name="OrderNum"></param>
        /// <returns></returns>
        public AllocateOrderEntity GetOrderByNum(string OrderNum)
        {
            AllocateOrderEntity entity = new AllocateOrderEntity();

            entity.IncludeAll();

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeAdmin = new AdminEntity();
            auditeAdmin.Include(a => new { AuditUserName = a.UserName });
            entity.Left<AdminEntity>(auditeAdmin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            entity.Where(a => a.OrderNum == OrderNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;

            entity = this.AllocateOrder.GetSingle(entity);
            return entity;
        }
    }
}
