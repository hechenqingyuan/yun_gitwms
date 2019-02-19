/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-31 9:27:57
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-31 9:27:57       情缘
*********************************************************************************/

using Git.Framework.ORM;
using Git.Storage.Entity.InStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.InStorage
{
    public partial interface IProc_AuditeInStorage : IDbProcHelper<Proc_AuditeInStorageEntity>
    {
    }
}
