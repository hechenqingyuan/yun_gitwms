/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-12 17:05:59
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-12 17:05:59       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Finance
{
    public partial class FinancePayEntity
    {
        [DataMapping(ColumnName = "CreateUserName", DbType = DbType.String)]
        public string CreateUserName { get; set; }

        [DataMapping(ColumnName = "Title", DbType = DbType.String)]
        public string Title { get; set; }

        [DataMapping(ColumnName = "FromName", DbType = DbType.String)]
        public string FromName { get; set; }

        [DataMapping(ColumnName = "ToName", DbType = DbType.String)]
        public string ToName { get; set; }

        [DataMapping(ColumnName = "TotalAmount", DbType = DbType.Double)]
        public double TotalAmount { get; set; }

        [DataMapping(ColumnName = "LastTime", DbType = DbType.DateTime)]
        public DateTime LastTime { get; set; }

        [DataMapping(ColumnName = "ContractSn", DbType = DbType.String)]
        public string ContractSn { get; set; }

        [DataMapping(ColumnName = "ContractNum", DbType = DbType.String)]
        public string ContractNum { get; set; }

        [DataMapping(ColumnName = "BillType", DbType = DbType.Int32)]
        public int BillType { get; set; }

        [DataMapping(ColumnName = "CateNum", DbType = DbType.String)]
        public string CateNum { get; set; }

        [DataMapping(ColumnName = "CateName", DbType = DbType.String)]
        public string CateName { get; set; }

        public string BeginTime { get; set; }

        public string EndTime { get; set; }

        /// <summary>
        /// 来源编号
        /// </summary>
        public string SourceObject { get; set; }
    }


    [TableAttribute(DbName = "GitWMS", Name = "FinancePay", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class FinancePayEntity : BaseEntity
    {
        public FinancePayEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public FinancePayEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public FinancePayEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "PayNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string PayNum { get; set; }

        public FinancePayEntity IncludePayNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PayNum"))
            {
                this.ColumnList.Add("PayNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BillSnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BillSnNum { get; set; }

        public FinancePayEntity IncludeBillSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BillSnNum"))
            {
                this.ColumnList.Add("BillSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "PayType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 PayType { get; set; }

        public FinancePayEntity IncludePayType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PayType"))
            {
                this.ColumnList.Add("PayType");
            }
            return this;
        }

        [DataMapping(ColumnName = "BankName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BankName { get; set; }

        public FinancePayEntity IncludeBankName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BankName"))
            {
                this.ColumnList.Add("BankName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public FinancePayEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "PayTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime PayTime { get; set; }

        public FinancePayEntity IncludePayTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PayTime"))
            {
                this.ColumnList.Add("PayTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public FinancePayEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public FinancePayEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public FinancePayEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 40, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public FinancePayEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public FinancePayEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
