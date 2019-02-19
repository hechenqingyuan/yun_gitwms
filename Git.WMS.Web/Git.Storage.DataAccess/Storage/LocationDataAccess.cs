/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-16 9:09:36
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-16 9:09:36       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;
using Git.Framework.MsSql;
using Git.Storage.Entity.Storage;
using Git.Storage.IDataAccess.Storage;

namespace Git.Storage.DataAccess.Storage
{
    public partial class LocationDataAccess : DbHelper<LocationEntity>, ILocation
    {
        public LocationDataAccess()
        {
        }

    }
}
