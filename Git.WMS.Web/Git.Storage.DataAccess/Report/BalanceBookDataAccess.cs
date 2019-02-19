/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/3/21 15:45:56
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/3/21 15:45:56       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.Report;
using Git.Storage.IDataAccess.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Report
{
    public partial class BalanceBookDataAccess : DbHelper<BalanceBookEntity>, IBalanceBook
    {
        public BalanceBookDataAccess()
        {
        }

    }
}
