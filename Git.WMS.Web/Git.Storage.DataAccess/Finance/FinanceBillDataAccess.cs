/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-12 17:10:13
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-12 17:10:13       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.Finance;
using Git.Storage.IDataAccess.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Finance
{
    public partial class FinanceBillDataAccess : DbHelper<FinanceBillEntity>, IFinanceBill
    {
        public FinanceBillDataAccess()
        {
        }

    }
}
