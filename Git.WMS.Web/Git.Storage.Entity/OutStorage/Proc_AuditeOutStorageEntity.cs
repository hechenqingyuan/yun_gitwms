/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-08 22:35:18
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-08 22:35:18       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.OutStorage
{
    [TableAttribute(DbName = "GitWMS", Name = "Proc_AuditeOutStorage", IsInternal = false, MapType = MapType.Proc)]
    public partial class Proc_AuditeOutStorageEntity : BaseEntity
    {
        public Proc_AuditeOutStorageEntity()
        {
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string OrderNum { get; set; }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4000, ColumnType = ColumnType.InPut)]
        public Int32 Status { get; set; }

        [DataMapping(ColumnName = "AuditUser", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string AuditUser { get; set; }

        [DataMapping(ColumnName = "Reason", DbType = DbType.String, Length = 400, ColumnType = ColumnType.InPut)]
        public string Reason { get; set; }

        [DataMapping(ColumnName = "OperateType", DbType = DbType.Int32, Length = 4000, ColumnType = ColumnType.InPut)]
        public Int32 OperateType { get; set; }

        [DataMapping(ColumnName = "EquipmentNum", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string EquipmentNum { get; set; }

        [DataMapping(ColumnName = "EquipmentCode", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string EquipmentCode { get; set; }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, ColumnType = ColumnType.InPut)]
        public string Remark { get; set; }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InPut)]
        public string CompanyID { get; set; }

        [DataMapping(ColumnName = "ReturnValue", DbType = DbType.String, Length = 50, ColumnType = ColumnType.InOutPut)]
        public string ReturnValue { get; set; }

    }
}
