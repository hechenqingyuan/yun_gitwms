/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-31 9:28:27
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-31 9:28:27       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.InStorage;
using Git.Storage.IDataAccess.InStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.InStorage
{
    public partial class Proc_AuditeInStorageDataAccess : DbProcHelper<Proc_AuditeInStorageEntity>, IProc_AuditeInStorage
    {
        public Proc_AuditeInStorageDataAccess()
        {
        }

    }
}
