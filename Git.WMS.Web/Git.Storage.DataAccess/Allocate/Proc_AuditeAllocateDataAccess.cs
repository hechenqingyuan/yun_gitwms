/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-15 18:41:06
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-15 18:41:06       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.Allocate;
using Git.Storage.IDataAccess.Allocate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Allocate
{
    public partial class Proc_AuditeAllocateDataAccess : DbProcHelper<Proc_AuditeAllocateEntity>, IProc_AuditeAllocate
    {
        public Proc_AuditeAllocateDataAccess()
        {
        }

    }
}
