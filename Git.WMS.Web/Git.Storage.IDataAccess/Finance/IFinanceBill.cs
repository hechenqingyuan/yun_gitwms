/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-12 17:07:38
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-12 17:07:38       情缘
*********************************************************************************/

using Git.Framework.ORM;
using Git.Storage.Entity.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.Finance
{
    public partial interface IFinanceBill : IDbHelper<FinanceBillEntity>
    {
    }
}
