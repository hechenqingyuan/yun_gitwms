/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-08 22:35:58
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-08 22:35:58       情缘
*********************************************************************************/

using Git.Framework.ORM;
using Git.Storage.Entity.OutStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.OutStorage
{
    public partial interface IProc_AuditeOutStorage : IDbProcHelper<Proc_AuditeOutStorageEntity>
    {
    }
}
