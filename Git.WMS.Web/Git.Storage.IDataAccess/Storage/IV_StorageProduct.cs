/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-07-26 22:05:48
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-07-26 22:05:48       情缘
*********************************************************************************/

using Git.Framework.ORM;
using Git.Storage.Entity.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.Storage
{
    public partial interface IV_StorageProduct : IDbHelper<V_StorageProductEntity>
    {
    }
}
