/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-04-22 16:49:19
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-04-22 16:49:19       情缘
*********************************************************************************/

using Git.Framework.ORM;
using Git.Storage.Entity.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.Report
{
    public partial interface IReportParams : IDbHelper<ReportParamsEntity>
    {
    }
}
