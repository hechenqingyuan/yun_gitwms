/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-04-22 16:57:54
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-04-22 16:57:54       情缘
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
    public partial class ReportParamsDataAccess : DbHelper<ReportParamsEntity>, IReportParams
    {
        public ReportParamsDataAccess()
        {
        }

    }
}
