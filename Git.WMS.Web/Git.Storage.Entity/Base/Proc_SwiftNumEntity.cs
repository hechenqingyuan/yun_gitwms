/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-05 8:21:54
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-05 8:21:54       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Base
{
    [TableAttribute(DbName = "GitWMS", Name = "Proc_SwiftNum", IsInternal = false, MapType = MapType.Proc)]
    public partial class Proc_SwiftNumEntity : BaseEntity
    {
        public Proc_SwiftNumEntity()
        {
        }

        [DataMapping(ColumnName = "Day", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string Day { get; set; }

        [DataMapping(ColumnName = "TabName", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string TabName { get; set; }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string CompanyID { get; set; }

        [DataMapping(ColumnName = "Num", DbType = DbType.Int32, Length = 4000, ColumnType = ColumnType.InOutPut)]
        public Int32 Num { get; set; }

    }
}
