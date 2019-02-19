/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-06-01 23:07:17
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-06-01 23:07:17
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Biz
{

    public partial class SaleOrderEntity : BaseEntity
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
        /// 查询辅助-开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 查询辅助-结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 查询辅助-开始时间
        /// </summary>
        public string BeginOrderTime { get; set; }

        /// <summary>
        /// 查询辅助-结束时间
        /// </summary>
        public string EndOrderTime { get; set; }

        /// <summary>
        /// 查询辅助-开始时间
        /// </summary>
        public string BeginSendTime { get; set; }

        /// <summary>
        /// 查询辅助-结束时间
        /// </summary>
        public string EndSendTime { get; set; }
    }

	[TableAttribute(DbName = "GitWMS", Name = "SaleOrder", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class SaleOrderEntity : BaseEntity
    {
        public SaleOrderEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public SaleOrderEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public SaleOrderEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public SaleOrderEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderType", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 OrderType { get; set; }

        public SaleOrderEntity IncludeOrderType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderType"))
            {
                this.ColumnList.Add("OrderType");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusSnNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusSnNum { get; set; }

        public SaleOrderEntity IncludeCusSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusSnNum"))
            {
                this.ColumnList.Add("CusSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusNum { get; set; }

        public SaleOrderEntity IncludeCusNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusNum"))
            {
                this.ColumnList.Add("CusNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusName { get; set; }

        public SaleOrderEntity IncludeCusName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusName"))
            {
                this.ColumnList.Add("CusName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Contact", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Contact { get; set; }

        public SaleOrderEntity IncludeContact(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Contact"))
            {
                this.ColumnList.Add("Contact");
            }
            return this;
        }

        [DataMapping(ColumnName = "Phone", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Phone { get; set; }

        public SaleOrderEntity IncludePhone(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Phone"))
            {
                this.ColumnList.Add("Phone");
            }
            return this;
        }

        [DataMapping(ColumnName = "Address", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Address { get; set; }

        public SaleOrderEntity IncludeAddress(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Address"))
            {
                this.ColumnList.Add("Address");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractOrder", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractOrder { get; set; }

        public SaleOrderEntity IncludeContractOrder(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractOrder"))
            {
                this.ColumnList.Add("ContractOrder");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractSn", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractSn { get; set; }

        public SaleOrderEntity IncludeContractSn(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractSn"))
            {
                this.ColumnList.Add("ContractSn");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusOrderNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusOrderNum { get; set; }

        public SaleOrderEntity IncludeCusOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusOrderNum"))
            {
                this.ColumnList.Add("CusOrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public SaleOrderEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public SaleOrderEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "Weight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Weight { get; set; }

        public SaleOrderEntity IncludeWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Weight"))
            {
                this.ColumnList.Add("Weight");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime OrderTime { get; set; }

        public SaleOrderEntity IncludeOrderTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderTime"))
            {
                this.ColumnList.Add("OrderTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "SendDate", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime SendDate { get; set; }

        public SaleOrderEntity IncludeSendDate(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SendDate"))
            {
                this.ColumnList.Add("SendDate");
            }
            return this;
        }

        [DataMapping(ColumnName = "HasReturn", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 HasReturn { get; set; }

        public SaleOrderEntity IncludeHasReturn(bool flag)
        {
            if (flag && !this.ColumnList.Contains("HasReturn"))
            {
                this.ColumnList.Add("HasReturn");
            }
            return this;
        }

        [DataMapping(ColumnName = "AuditeStatus", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 AuditeStatus { get; set; }

        public SaleOrderEntity IncludeAuditeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AuditeStatus"))
            {
                this.ColumnList.Add("AuditeStatus");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public SaleOrderEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public SaleOrderEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public SaleOrderEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public SaleOrderEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "AuditeTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime AuditeTime { get; set; }

        public SaleOrderEntity IncludeAuditeTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AuditeTime"))
            {
                this.ColumnList.Add("AuditeTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "AuditeUser", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string AuditeUser { get; set; }

        public SaleOrderEntity IncludeAuditeUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AuditeUser"))
            {
                this.ColumnList.Add("AuditeUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "Reason", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Reason { get; set; }

        public SaleOrderEntity IncludeReason(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Reason"))
            {
                this.ColumnList.Add("Reason");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public SaleOrderEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public SaleOrderEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
