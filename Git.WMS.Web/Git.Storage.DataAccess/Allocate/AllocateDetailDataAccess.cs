/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-11 10:09:40
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-11 10:09:40       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.Allocate;
using Git.Storage.IDataAccess.Allocate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Allocate
{
    public partial class AllocateDetailDataAccess : DbHelper<AllocateDetailEntity>, IAllocateDetail
    {
        public AllocateDetailDataAccess()
        {
        }

    }
}
