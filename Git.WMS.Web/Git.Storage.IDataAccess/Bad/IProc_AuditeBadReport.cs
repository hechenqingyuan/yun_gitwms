/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-11 9:24:39
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-11 9:24:39       情缘
*********************************************************************************/

using Git.Framework.ORM;
using Git.Storage.Entity.Bad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.Bad
{
    public partial interface IProc_AuditeBadReport : IDbProcHelper<Proc_AuditeBadReportEntity>
    {
    }
}
