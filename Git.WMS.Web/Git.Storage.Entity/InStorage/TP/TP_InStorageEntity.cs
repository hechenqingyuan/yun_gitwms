/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017-01-16 21:20:02
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017-01-16 21:20:02       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.InStorage.TP
{
    public partial class TP_InStorageEntity : BaseEntity
    {
        /// <summary>
        /// 创建人名称
        /// </summary>
        [DataMapping(ColumnName = "CreateUserName", DbType = DbType.String)]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 审核人名称
        /// </summary>
        [DataMapping(ColumnName = "AuditeUserName", DbType = DbType.String)]
        public string AuditeUserName { get; set; }

        /// <summary>
        /// 入库部门
        /// </summary>
        [DataMapping(ColumnName = "DepartName", DbType = DbType.String)]
        public string DepartName { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [DataMapping(ColumnName = "PlateNum", DbType = DbType.String)]
        public string PlateNum { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "TP_InStorage", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class TP_InStorageEntity : BaseEntity
    {
        public TP_InStorageEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public TP_InStorageEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public TP_InStorageEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public TP_InStorageEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "InType", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 InType { get; set; }

        public TP_InStorageEntity IncludeInType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InType"))
            {
                this.ColumnList.Add("InType");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 ProductType { get; set; }

        public TP_InStorageEntity IncludeProductType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductType"))
            {
                this.ColumnList.Add("ProductType");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageNum { get; set; }

        public TP_InStorageEntity IncludeStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageNum"))
            {
                this.ColumnList.Add("StorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupSnNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupSnNum { get; set; }

        public TP_InStorageEntity IncludeSupSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupSnNum"))
            {
                this.ColumnList.Add("SupSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupNum { get; set; }

        public TP_InStorageEntity IncludeSupNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupNum"))
            {
                this.ColumnList.Add("SupNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupName { get; set; }

        public TP_InStorageEntity IncludeSupName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupName"))
            {
                this.ColumnList.Add("SupName");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContactName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContactName { get; set; }

        public TP_InStorageEntity IncludeContactName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContactName"))
            {
                this.ColumnList.Add("ContactName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Phone", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Phone { get; set; }

        public TP_InStorageEntity IncludePhone(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Phone"))
            {
                this.ColumnList.Add("Phone");
            }
            return this;
        }

        [DataMapping(ColumnName = "Address", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Address { get; set; }

        public TP_InStorageEntity IncludeAddress(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Address"))
            {
                this.ColumnList.Add("Address");
            }
            return this;
        }

        [DataMapping(ColumnName = "CarNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CarNum { get; set; }

        public TP_InStorageEntity IncludeCarNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CarNum"))
            {
                this.ColumnList.Add("CarNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Driver", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Driver { get; set; }

        public TP_InStorageEntity IncludeDriver(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Driver"))
            {
                this.ColumnList.Add("Driver");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusOrderNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusOrderNum { get; set; }

        public TP_InStorageEntity IncludeCusOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusOrderNum"))
            {
                this.ColumnList.Add("CusOrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractOrder", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractOrder { get; set; }

        public TP_InStorageEntity IncludeContractOrder(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractOrder"))
            {
                this.ColumnList.Add("ContractOrder");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 ContractType { get; set; }

        public TP_InStorageEntity IncludeContractType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractType"))
            {
                this.ColumnList.Add("ContractType");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public TP_InStorageEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public TP_InStorageEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "PalletNum", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 PalletNum { get; set; }

        public TP_InStorageEntity IncludePalletNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PalletNum"))
            {
                this.ColumnList.Add("PalletNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BoxNum", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 BoxNum { get; set; }

        public TP_InStorageEntity IncludeBoxNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BoxNum"))
            {
                this.ColumnList.Add("BoxNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public TP_InStorageEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public TP_InStorageEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "NetWeight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double NetWeight { get; set; }

        public TP_InStorageEntity IncludeNetWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("NetWeight"))
            {
                this.ColumnList.Add("NetWeight");
            }
            return this;
        }

        [DataMapping(ColumnName = "GrossWeight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double GrossWeight { get; set; }

        public TP_InStorageEntity IncludeGrossWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("GrossWeight"))
            {
                this.ColumnList.Add("GrossWeight");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime OrderTime { get; set; }

        public TP_InStorageEntity IncludeOrderTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderTime"))
            {
                this.ColumnList.Add("OrderTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "DepartNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string DepartNum { get; set; }

        public TP_InStorageEntity IncludeDepartNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("DepartNum"))
            {
                this.ColumnList.Add("DepartNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public TP_InStorageEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public TP_InStorageEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "AuditUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string AuditUser { get; set; }

        public TP_InStorageEntity IncludeAuditUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AuditUser"))
            {
                this.ColumnList.Add("AuditUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "AuditeTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime AuditeTime { get; set; }

        public TP_InStorageEntity IncludeAuditeTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AuditeTime"))
            {
                this.ColumnList.Add("AuditeTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "PrintUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string PrintUser { get; set; }

        public TP_InStorageEntity IncludePrintUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PrintUser"))
            {
                this.ColumnList.Add("PrintUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "PrintTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime PrintTime { get; set; }

        public TP_InStorageEntity IncludePrintTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PrintTime"))
            {
                this.ColumnList.Add("PrintTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "StoreKeeper", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StoreKeeper { get; set; }

        public TP_InStorageEntity IncludeStoreKeeper(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StoreKeeper"))
            {
                this.ColumnList.Add("StoreKeeper");
            }
            return this;
        }

        [DataMapping(ColumnName = "Reason", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Reason { get; set; }

        public TP_InStorageEntity IncludeReason(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Reason"))
            {
                this.ColumnList.Add("Reason");
            }
            return this;
        }

        [DataMapping(ColumnName = "OperateType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 OperateType { get; set; }

        public TP_InStorageEntity IncludeOperateType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OperateType"))
            {
                this.ColumnList.Add("OperateType");
            }
            return this;
        }

        [DataMapping(ColumnName = "EquipmentNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string EquipmentNum { get; set; }

        public TP_InStorageEntity IncludeEquipmentNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("EquipmentNum"))
            {
                this.ColumnList.Add("EquipmentNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "EquipmentCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string EquipmentCode { get; set; }

        public TP_InStorageEntity IncludeEquipmentCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("EquipmentCode"))
            {
                this.ColumnList.Add("EquipmentCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public TP_InStorageEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public TP_InStorageEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
