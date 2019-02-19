/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2013-11-29 23:27:48
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2013-11-29 23:27:48
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.OutStorage
{

    public partial class OutStorageEntity
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
        /// 查询时间-开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 查询时间-结束时间
        /// </summary>
        public string EndTime { get; set; }
    }


    [TableAttribute(DbName = "GitWMS", Name = "OutStorage", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class OutStorageEntity : BaseEntity
    {
        public OutStorageEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public OutStorageEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public OutStorageEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public OutStorageEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OutType", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 OutType { get; set; }

        public OutStorageEntity IncludeOutType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OutType"))
            {
                this.ColumnList.Add("OutType");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 ProductType { get; set; }

        public OutStorageEntity IncludeProductType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductType"))
            {
                this.ColumnList.Add("ProductType");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageNum { get; set; }

        public OutStorageEntity IncludeStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageNum"))
            {
                this.ColumnList.Add("StorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusSnNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusSnNum { get; set; }

        public OutStorageEntity IncludeCusSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusSnNum"))
            {
                this.ColumnList.Add("CusSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusNum { get; set; }

        public OutStorageEntity IncludeCusNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusNum"))
            {
                this.ColumnList.Add("CusNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusName { get; set; }

        public OutStorageEntity IncludeCusName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusName"))
            {
                this.ColumnList.Add("CusName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Contact", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Contact { get; set; }

        public OutStorageEntity IncludeContact(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Contact"))
            {
                this.ColumnList.Add("Contact");
            }
            return this;
        }

        [DataMapping(ColumnName = "Phone", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Phone { get; set; }

        public OutStorageEntity IncludePhone(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Phone"))
            {
                this.ColumnList.Add("Phone");
            }
            return this;
        }

        [DataMapping(ColumnName = "Address", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Address { get; set; }

        public OutStorageEntity IncludeAddress(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Address"))
            {
                this.ColumnList.Add("Address");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractOrder", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractOrder { get; set; }

        public OutStorageEntity IncludeContractOrder(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractOrder"))
            {
                this.ColumnList.Add("ContractOrder");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public OutStorageEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public OutStorageEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "Weight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Weight { get; set; }

        public OutStorageEntity IncludeWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Weight"))
            {
                this.ColumnList.Add("Weight");
            }
            return this;
        }

        [DataMapping(ColumnName = "SendDate", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime SendDate { get; set; }

        public OutStorageEntity IncludeSendDate(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SendDate"))
            {
                this.ColumnList.Add("SendDate");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public OutStorageEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public OutStorageEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public OutStorageEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public OutStorageEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "AuditUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string AuditUser { get; set; }

        public OutStorageEntity IncludeAuditUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AuditUser"))
            {
                this.ColumnList.Add("AuditUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "AuditeTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime AuditeTime { get; set; }

        public OutStorageEntity IncludeAuditeTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AuditeTime"))
            {
                this.ColumnList.Add("AuditeTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "PrintUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string PrintUser { get; set; }

        public OutStorageEntity IncludePrintUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PrintUser"))
            {
                this.ColumnList.Add("PrintUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "PrintTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime PrintTime { get; set; }

        public OutStorageEntity IncludePrintTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PrintTime"))
            {
                this.ColumnList.Add("PrintTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "Reason", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Reason { get; set; }

        public OutStorageEntity IncludeReason(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Reason"))
            {
                this.ColumnList.Add("Reason");
            }
            return this;
        }

        [DataMapping(ColumnName = "OperateType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 OperateType { get; set; }

        public OutStorageEntity IncludeOperateType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OperateType"))
            {
                this.ColumnList.Add("OperateType");
            }
            return this;
        }

        [DataMapping(ColumnName = "EquipmentNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string EquipmentNum { get; set; }

        public OutStorageEntity IncludeEquipmentNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("EquipmentNum"))
            {
                this.ColumnList.Add("EquipmentNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "EquipmentCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string EquipmentCode { get; set; }

        public OutStorageEntity IncludeEquipmentCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("EquipmentCode"))
            {
                this.ColumnList.Add("EquipmentCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "CarrierNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CarrierNum { get; set; }

        public OutStorageEntity IncludeCarrierNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CarrierNum"))
            {
                this.ColumnList.Add("CarrierNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CarrierName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CarrierName { get; set; }

        public OutStorageEntity IncludeCarrierName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CarrierName"))
            {
                this.ColumnList.Add("CarrierName");
            }
            return this;
        }

        [DataMapping(ColumnName = "LogisticsNo", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string LogisticsNo { get; set; }

        public OutStorageEntity IncludeLogisticsNo(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LogisticsNo"))
            {
                this.ColumnList.Add("LogisticsNo");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public OutStorageEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public OutStorageEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
