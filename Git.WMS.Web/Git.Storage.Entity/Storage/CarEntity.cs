/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017-01-13 11:46:37
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017-01-13 11:46:37       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Storage
{
    [TableAttribute(DbName = "GitWMS", Name = "Car", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class CarEntity : BaseEntity
    {
        public CarEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public CarEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public CarEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "PlateNum", DbType = DbType.String, Length = 100, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string PlateNum { get; set; }

        public CarEntity IncludePlateNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PlateNum"))
            {
                this.ColumnList.Add("PlateNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Driver", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Driver { get; set; }

        public CarEntity IncludeDriver(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Driver"))
            {
                this.ColumnList.Add("Driver");
            }
            return this;
        }

        [DataMapping(ColumnName = "Phone", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Phone { get; set; }

        public CarEntity IncludePhone(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Phone"))
            {
                this.ColumnList.Add("Phone");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public CarEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public CarEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public CarEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public CarEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public CarEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public CarEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
