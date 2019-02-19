/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-07-28 21:51:03
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-07-28 21:51:03       情缘
*********************************************************************************/

using Git.Framework.ORM;
using Git.Storage.Entity.Pick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.Pick
{
    public partial interface IProc_PickProduct : IDbProcHelper<Proc_PickProductEntity>
    {
    }
}
