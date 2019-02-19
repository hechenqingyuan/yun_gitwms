/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-12 17:05:22
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-12 17:05:22       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Finance
{
    public partial class FinanceBillEntity
    {
        /// <summary>
        /// 创建人名称
        /// </summary>
        [DataMapping(ColumnName = "CreateUserName", DbType = DbType.String)]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 查询开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 查询结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 剩余金额
        /// </summary>
        public double LeavAmount { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "FinanceBill", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class FinanceBillEntity : BaseEntity
    {
        public FinanceBillEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public FinanceBillEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public FinanceBillEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BillNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BillNum { get; set; }

        public FinanceBillEntity IncludeBillNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BillNum"))
            {
                this.ColumnList.Add("BillNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CateNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CateNum { get; set; }

        public FinanceBillEntity IncludeCateNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CateNum"))
            {
                this.ColumnList.Add("CateNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CateName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CateName { get; set; }

        public FinanceBillEntity IncludeCateName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CateName"))
            {
                this.ColumnList.Add("CateName");
            }
            return this;
        }

        [DataMapping(ColumnName = "BillType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 BillType { get; set; }

        public FinanceBillEntity IncludeBillType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BillType"))
            {
                this.ColumnList.Add("BillType");
            }
            return this;
        }

        [DataMapping(ColumnName = "FromNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string FromNum { get; set; }

        public FinanceBillEntity IncludeFromNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("FromNum"))
            {
                this.ColumnList.Add("FromNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "FromName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string FromName { get; set; }

        public FinanceBillEntity IncludeFromName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("FromName"))
            {
                this.ColumnList.Add("FromName");
            }
            return this;
        }

        [DataMapping(ColumnName = "ToNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ToNum { get; set; }

        public FinanceBillEntity IncludeToNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ToNum"))
            {
                this.ColumnList.Add("ToNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ToName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ToName { get; set; }

        public FinanceBillEntity IncludeToName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ToName"))
            {
                this.ColumnList.Add("ToName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public FinanceBillEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "PrePayCount", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 PrePayCount { get; set; }

        public FinanceBillEntity IncludePrePayCount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PrePayCount"))
            {
                this.ColumnList.Add("PrePayCount");
            }
            return this;
        }

        [DataMapping(ColumnName = "PrePayRate", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string PrePayRate { get; set; }

        public FinanceBillEntity IncludePrePayRate(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PrePayRate"))
            {
                this.ColumnList.Add("PrePayRate");
            }
            return this;
        }

        [DataMapping(ColumnName = "RealPayCount", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 RealPayCount { get; set; }

        public FinanceBillEntity IncludeRealPayCount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("RealPayCount"))
            {
                this.ColumnList.Add("RealPayCount");
            }
            return this;
        }

        [DataMapping(ColumnName = "RealPayAmount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double RealPayAmount { get; set; }

        public FinanceBillEntity IncludeRealPayAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("RealPayAmount"))
            {
                this.ColumnList.Add("RealPayAmount");
            }
            return this;
        }

        [DataMapping(ColumnName = "LastTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime LastTime { get; set; }

        public FinanceBillEntity IncludeLastTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LastTime"))
            {
                this.ColumnList.Add("LastTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "Title", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Title { get; set; }

        public FinanceBillEntity IncludeTitle(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Title"))
            {
                this.ColumnList.Add("Title");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractSn", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractSn { get; set; }

        public FinanceBillEntity IncludeContractSn(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractSn"))
            {
                this.ColumnList.Add("ContractSn");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractNum { get; set; }

        public FinanceBillEntity IncludeContractNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractNum"))
            {
                this.ColumnList.Add("ContractNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public FinanceBillEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public FinanceBillEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public FinanceBillEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public FinanceBillEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public FinanceBillEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public FinanceBillEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
