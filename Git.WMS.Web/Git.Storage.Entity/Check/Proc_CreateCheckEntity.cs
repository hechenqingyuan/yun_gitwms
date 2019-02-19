/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-16 22:04:22
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-16 22:04:22       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Check
{
    [TableAttribute(DbName = "GitWMS", Name = "Proc_CreateCheck", IsInternal = false, MapType = MapType.Proc)]
    public partial class Proc_CreateCheckEntity : BaseEntity
    {
        public Proc_CreateCheckEntity()
        {
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string OrderNum { get; set; }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string CreateUser { get; set; }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string CompanyID { get; set; }

        [DataMapping(ColumnName = "ReturnValue", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InOutPut)]
        public string ReturnValue { get; set; }

    }
}
