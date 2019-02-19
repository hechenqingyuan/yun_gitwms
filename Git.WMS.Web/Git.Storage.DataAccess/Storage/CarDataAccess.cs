/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017-01-13 11:48:14
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017-01-13 11:48:14       情缘
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
    public partial class CarDataAccess : DbHelper<CarEntity>, ICar
    {
        public CarDataAccess()
        {
        }

    }
}
