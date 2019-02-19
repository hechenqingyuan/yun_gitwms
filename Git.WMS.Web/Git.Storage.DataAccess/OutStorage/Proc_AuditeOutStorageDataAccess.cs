/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-08 22:36:31
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-08 22:36:31       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.OutStorage;
using Git.Storage.IDataAccess.OutStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.OutStorage
{
    public partial class Proc_AuditeOutStorageDataAccess : DbProcHelper<Proc_AuditeOutStorageEntity>, IProc_AuditeOutStorage
    {
        public Proc_AuditeOutStorageDataAccess()
        {
        }

    }
}
