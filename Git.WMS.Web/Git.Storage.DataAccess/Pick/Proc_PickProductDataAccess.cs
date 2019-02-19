/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-07-28 21:51:31
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-07-28 21:51:31       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.Pick;
using Git.Storage.IDataAccess.Pick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Pick
{
    public partial class Proc_PickProductDataAccess : DbProcHelper<Proc_PickProductEntity>, IProc_PickProduct
    {
        public Proc_PickProductDataAccess()
        {
        }

    }
}
