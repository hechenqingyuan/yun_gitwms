/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-05 22:33:16
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-05 22:33:16       情缘
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
    public partial class LocalProductDataAccess : DbHelper<LocalProductEntity>, ILocalProduct
    {
        public LocalProductDataAccess()
        {
        }

    }
}
