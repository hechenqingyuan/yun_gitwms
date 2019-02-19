/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-16 22:05:44
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-16 22:05:44       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.Check;
using Git.Storage.IDataAccess.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Check
{
    public partial class Proc_CreateCheckDataAccess : DbProcHelper<Proc_CreateCheckEntity>, IProc_CreateCheck
    {
        public Proc_CreateCheckDataAccess()
        {
        }

    }
}
