/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-11 10:01:52
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-11 10:01:52       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Allocate
{
    public partial class AllocateOrderEntity
    {
        [DataMapping(ColumnName = "CreateUserName", DbType = DbType.String)]
        public string CreateUserName { get; set; }

        [DataMapping(ColumnName = "AuditUserName", DbType = DbType.String)]
        public string AuditUserName { get; set; }

        /// <summary>
        /// 查询辅助开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 查询辅助结束时间
        /// </summary>
        public string EndTime { get; set; }


    }


    [TableAttribute(DbName = "GitWMS", Name = "AllocateOrder", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class AllocateOrderEntity : BaseEntity
    {
        public AllocateOrderEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public AllocateOrderEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public AllocateOrderEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public AllocateOrderEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "AllocateType", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 AllocateType { get; set; }

        public AllocateOrderEntity IncludeAllocateType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AllocateType"))
            {
                this.ColumnList.Add("AllocateType");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductType", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 ProductType { get; set; }

        public AllocateOrderEntity IncludeProductType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductType"))
            {
                this.ColumnList.Add("ProductType");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageNum { get; set; }

        public AllocateOrderEntity IncludeStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageNum"))
            {
                this.ColumnList.Add("StorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractOrder", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractOrder { get; set; }

        public AllocateOrderEntity IncludeContractOrder(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractOrder"))
            {
                this.ColumnList.Add("ContractOrder");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public AllocateOrderEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public AllocateOrderEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public AllocateOrderEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public AllocateOrderEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "Weight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Weight { get; set; }

        public AllocateOrderEntity IncludeWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Weight"))
            {
                this.ColumnList.Add("Weight");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public AllocateOrderEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public AllocateOrderEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "AuditUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string AuditUser { get; set; }

        public AllocateOrderEntity IncludeAuditUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AuditUser"))
            {
                this.ColumnList.Add("AuditUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "AuditeTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime AuditeTime { get; set; }

        public AllocateOrderEntity IncludeAuditeTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AuditeTime"))
            {
                this.ColumnList.Add("AuditeTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "PrintUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string PrintUser { get; set; }

        public AllocateOrderEntity IncludePrintUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PrintUser"))
            {
                this.ColumnList.Add("PrintUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "PrintTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime PrintTime { get; set; }

        public AllocateOrderEntity IncludePrintTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PrintTime"))
            {
                this.ColumnList.Add("PrintTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "Reason", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Reason { get; set; }

        public AllocateOrderEntity IncludeReason(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Reason"))
            {
                this.ColumnList.Add("Reason");
            }
            return this;
        }

        [DataMapping(ColumnName = "OperateType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 OperateType { get; set; }

        public AllocateOrderEntity IncludeOperateType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OperateType"))
            {
                this.ColumnList.Add("OperateType");
            }
            return this;
        }

        [DataMapping(ColumnName = "EquipmentNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string EquipmentNum { get; set; }

        public AllocateOrderEntity IncludeEquipmentNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("EquipmentNum"))
            {
                this.ColumnList.Add("EquipmentNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "EquipmentCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string EquipmentCode { get; set; }

        public AllocateOrderEntity IncludeEquipmentCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("EquipmentCode"))
            {
                this.ColumnList.Add("EquipmentCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public AllocateOrderEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public AllocateOrderEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
