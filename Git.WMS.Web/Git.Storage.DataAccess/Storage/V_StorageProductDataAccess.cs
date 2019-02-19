/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-07-26 22:06:13
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-07-26 22:06:13       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.Storage;
using Git.Storage.IDataAccess.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Storage
{
    public partial class V_StorageProductDataAccess : DbHelper<V_StorageProductEntity>, IV_StorageProduct
    {
        public V_StorageProductDataAccess()
        {
        }

    }
}
