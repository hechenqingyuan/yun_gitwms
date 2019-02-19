/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-03-03 22:19:04
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-03-03 22:19:04
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Storage
{
	[TableAttribute(DbName = "GitWMS", Name = "Customer", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class CustomerEntity : BaseEntity
    {
        public CustomerEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public CustomerEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public CustomerEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusNum { get; set; }

        public CustomerEntity IncludeCusNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusNum"))
            {
                this.ColumnList.Add("CusNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusName", DbType = DbType.String, Length = 100, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusName { get; set; }

        public CustomerEntity IncludeCusName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusName"))
            {
                this.ColumnList.Add("CusName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Phone", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Phone { get; set; }

        public CustomerEntity IncludePhone(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Phone"))
            {
                this.ColumnList.Add("Phone");
            }
            return this;
        }

        [DataMapping(ColumnName = "Email", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Email { get; set; }

        public CustomerEntity IncludeEmail(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Email"))
            {
                this.ColumnList.Add("Email");
            }
            return this;
        }

        [DataMapping(ColumnName = "Fax", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Fax { get; set; }

        public CustomerEntity IncludeFax(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Fax"))
            {
                this.ColumnList.Add("Fax");
            }
            return this;
        }

        [DataMapping(ColumnName = "Address", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Address { get; set; }

        public CustomerEntity IncludeAddress(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Address"))
            {
                this.ColumnList.Add("Address");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 CusType { get; set; }

        public CustomerEntity IncludeCusType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusType"))
            {
                this.ColumnList.Add("CusType");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public CustomerEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public CustomerEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public CustomerEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public CustomerEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public CustomerEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
