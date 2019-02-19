/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-05 8:24:27
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-05 8:24:27       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.Base;
using Git.Storage.IDataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Base
{
    public partial class Proc_SwiftNumDataAccess : DbProcHelper<Proc_SwiftNumEntity>, IProc_SwiftNum
    {
        public Proc_SwiftNumDataAccess()
        {
        }

    }
}
