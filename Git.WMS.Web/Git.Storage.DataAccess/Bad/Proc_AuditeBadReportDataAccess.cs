/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-11 9:25:13
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-11 9:25:13       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.Bad;
using Git.Storage.IDataAccess.Bad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Bad
{
    public partial class Proc_AuditeBadReportDataAccess : DbProcHelper<Proc_AuditeBadReportEntity>, IProc_AuditeBadReport
    {
        public Proc_AuditeBadReportDataAccess()
        {
        }

    }
}
