/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-07-28 21:50:24
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-07-28 21:50:24       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Pick
{
    [TableAttribute(DbName = "GitWMS", Name = "Proc_PickProduct", IsInternal = false, MapType = MapType.Proc)]
    public partial class Proc_PickProductEntity : BaseEntity
    {
        public Proc_PickProductEntity()
        {
        }

        [DataMapping(ColumnName = "InProductNum", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string InProductNum { get; set; }

        [DataMapping(ColumnName = "InCompanyID", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string InCompanyID { get; set; }

        [DataMapping(ColumnName = "InStorageNum", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string InStorageNum { get; set; }

        [DataMapping(ColumnName = "InNum", DbType = DbType.Double, Length = 4000, ColumnType = ColumnType.InPut)]
        public double InNum { get; set; }

    }
}
