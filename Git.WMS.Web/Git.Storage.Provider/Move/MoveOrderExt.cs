/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/5/8 8:54:53
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/5/8 8:54:53       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Entity.Move;
using Git.Storage.Entity.Sys;
using Git.Storage.Provider.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider.Move
{
    public partial class MoveOrderExt:MoveOrder
    {
        public MoveOrderExt(string _CompanyID)
            : base(_CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 根据移库单号查询移库单
        /// </summary>
        /// <param name="OrderNum"></param>
        /// <returns></returns>
        public MoveOrderEntity GetOrderByNum(string OrderNum)
        {
            MoveOrderEntity entity = new MoveOrderEntity();
            entity.IncludeAll();

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            entity.Where(a => a.OrderNum == OrderNum)
                .And(a => a.CompanyID == this.CompanyID);

            entity = this.MoveOrder.GetSingle(entity);
            return entity;
        }
    }
}
