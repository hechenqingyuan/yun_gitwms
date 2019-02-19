/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-11 10:08:18
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-11 10:08:18       情缘
*********************************************************************************/

using Git.Framework.ORM;
using Git.Storage.Entity.Allocate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.Allocate
{
    public partial interface IAllocateOrder : IDbHelper<AllocateOrderEntity>
    {
    }
}
