/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-11 8:37:40
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-11 8:37:40       情缘
*********************************************************************************/

using Git.Framework.ORM;
using Git.Storage.Entity.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.Move
{
    public partial interface IProc_AuditeMove : IDbProcHelper<Proc_AuditeMoveEntity>
    {
    }
}
