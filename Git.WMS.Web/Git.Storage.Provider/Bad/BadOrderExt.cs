/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/5/8 8:13:09
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/5/8 8:13:09       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Entity.Bad;
using Git.Storage.Entity.Sys;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider.Bad
{
    public partial class BadOrderExt : BadOrder
    {
        public BadOrderExt(string _CompanyID)
            : base(_CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 根据报损单单号查询报损单
        /// </summary>
        /// <param name="OrderNum"></param>
        /// <returns></returns>
        public BadReportEntity GetOrderByNum(string OrderNum)
        {
            BadReportEntity entity = new BadReportEntity();

            entity.IncludeAll();

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeAdmin = new AdminEntity();
            auditeAdmin.Include(a => new { AuditUserName = a.UserName });
            entity.Left<AdminEntity>(auditeAdmin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            entity.Where(a => a.OrderNum == entity.OrderNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;

            entity = this.BadReport.GetSingle(entity);
            return entity;
        }
    }
}
