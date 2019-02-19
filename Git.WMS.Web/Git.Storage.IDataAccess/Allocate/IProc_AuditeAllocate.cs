/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-15 18:40:37
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-15 18:40:37       情缘
*********************************************************************************/

using Git.Framework.ORM;
using Git.Storage.Entity.Allocate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.Allocate
{
    public partial interface IProc_AuditeAllocate : IDbProcHelper<Proc_AuditeAllocateEntity>
    {
    }
}
