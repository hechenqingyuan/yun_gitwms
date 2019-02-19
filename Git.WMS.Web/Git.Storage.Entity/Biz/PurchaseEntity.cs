/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-06-01 23:07:43
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-06-01 23:07:43
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Biz
{
    public partial class PurchaseEntity : BaseEntity
    {
        /// <summary>
        /// 采购订单创建人
        /// </summary>
        [DataMapping(ColumnName = "CreateUserName", DbType = DbType.String)]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 采购订单审核人
        /// </summary>
        [DataMapping(ColumnName = "AuidteUserName", DbType = DbType.String)]
        public string AuidteUserName { get; set; }

        /// <summary>
        /// 查询辅助作用-创建日期开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 查询辅助作用-创建日期结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 查询辅助作用
        /// </summary>
        public string BeginOrderTime { get; set; }

        /// <summary>
        /// 查询辅助作用
        /// </summary>
        public string EndOrderTime { get; set; }

        /// <summary>
        /// 查询辅助作用
        /// </summary>
        public string BeginRevDate { get; set; }

        /// <summary>
        /// 查询辅助作用
        /// </summary>
        public string EndRevDate { get; set; }
    }

	[TableAttribute(DbName = "GitWMS", Name = "Purchase", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class PurchaseEntity : BaseEntity
    {
        public PurchaseEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public PurchaseEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public PurchaseEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public PurchaseEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderType", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 OrderType { get; set; }

        public PurchaseEntity IncludeOrderType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderType"))
            {
                this.ColumnList.Add("OrderType");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupSnNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupSnNum { get; set; }

        public PurchaseEntity IncludeSupSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupSnNum"))
            {
                this.ColumnList.Add("SupSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupNum { get; set; }

        public PurchaseEntity IncludeSupNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupNum"))
            {
                this.ColumnList.Add("SupNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupName { get; set; }

        public PurchaseEntity IncludeSupName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupName"))
            {
                this.ColumnList.Add("SupName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Contact", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Contact { get; set; }

        public PurchaseEntity IncludeContact(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Contact"))
            {
                this.ColumnList.Add("Contact");
            }
            return this;
        }

        [DataMapping(ColumnName = "Phone", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Phone { get; set; }

        public PurchaseEntity IncludePhone(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Phone"))
            {
                this.ColumnList.Add("Phone");
            }
            return this;
        }

        [DataMapping(ColumnName = "Address", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Address { get; set; }

        public PurchaseEntity IncludeAddress(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Address"))
            {
                this.ColumnList.Add("Address");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractOrder", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractOrder { get; set; }

        public PurchaseEntity IncludeContractOrder(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractOrder"))
            {
                this.ColumnList.Add("ContractOrder");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractSn", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractSn { get; set; }

        public PurchaseEntity IncludeContractSn(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractSn"))
            {
                this.ColumnList.Add("ContractSn");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public PurchaseEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public PurchaseEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "Weight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Weight { get; set; }

        public PurchaseEntity IncludeWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Weight"))
            {
                this.ColumnList.Add("Weight");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public PurchaseEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime OrderTime { get; set; }

        public PurchaseEntity IncludeOrderTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderTime"))
            {
                this.ColumnList.Add("OrderTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "RevDate", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime RevDate { get; set; }

        public PurchaseEntity IncludeRevDate(bool flag)
        {
            if (flag && !this.ColumnList.Contains("RevDate"))
            {
                this.ColumnList.Add("RevDate");
            }
            return this;
        }

        [DataMapping(ColumnName = "AuditeStatus", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 AuditeStatus { get; set; }

        public PurchaseEntity IncludeAuditeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AuditeStatus"))
            {
                this.ColumnList.Add("AuditeStatus");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public PurchaseEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "HasReturn", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 HasReturn { get; set; }

        public PurchaseEntity IncludeHasReturn(bool flag)
        {
            if (flag && !this.ColumnList.Contains("HasReturn"))
            {
                this.ColumnList.Add("HasReturn");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public PurchaseEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public PurchaseEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "AuidteUser", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string AuidteUser { get; set; }

        public PurchaseEntity IncludeAuidteUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AuidteUser"))
            {
                this.ColumnList.Add("AuidteUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "Reason", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Reason { get; set; }

        public PurchaseEntity IncludeReason(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Reason"))
            {
                this.ColumnList.Add("Reason");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public PurchaseEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public PurchaseEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
