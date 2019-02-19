/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-21 16:09:11
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-21 16:09:11       情缘
*********************************************************************************/

using Git.Framework.ORM;
using Git.Storage.Entity.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.Check
{
    public partial interface IProc_AuditeCheck : IDbProcHelper<Proc_AuditeCheckEntity>
    {
    }
}
