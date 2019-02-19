/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-11 8:38:15
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-11 8:38:15       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.Move;
using Git.Storage.IDataAccess.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Move
{
    public partial class Proc_AuditeMoveDataAccess : DbProcHelper<Proc_AuditeMoveEntity>, IProc_AuditeMove
    {
        public Proc_AuditeMoveDataAccess()
        {
        }

    }
}
