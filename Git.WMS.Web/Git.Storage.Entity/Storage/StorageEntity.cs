/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-03-03 22:18:38
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-03-03 22:18:38
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Storage
{
    public partial class StorageEntity
    {
        /// <summary>
        /// 仓库所属部门
        /// </summary>
        [DataMapping(ColumnName = "DepartName", DbType = DbType.String)]
        public string DepartName { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "Storage", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class StorageEntity : BaseEntity
    {
        public StorageEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public StorageEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public StorageEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageNum { get; set; }

        public StorageEntity IncludeStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageNum"))
            {
                this.ColumnList.Add("StorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageName { get; set; }

        public StorageEntity IncludeStorageName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageName"))
            {
                this.ColumnList.Add("StorageName");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 StorageType { get; set; }

        public StorageEntity IncludeStorageType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageType"))
            {
                this.ColumnList.Add("StorageType");
            }
            return this;
        }

        [DataMapping(ColumnName = "Length", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Length { get; set; }

        public StorageEntity IncludeLength(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Length"))
            {
                this.ColumnList.Add("Length");
            }
            return this;
        }

        [DataMapping(ColumnName = "Width", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Width { get; set; }

        public StorageEntity IncludeWidth(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Width"))
            {
                this.ColumnList.Add("Width");
            }
            return this;
        }

        [DataMapping(ColumnName = "Height", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Height { get; set; }

        public StorageEntity IncludeHeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Height"))
            {
                this.ColumnList.Add("Height");
            }
            return this;
        }

        [DataMapping(ColumnName = "Action", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Action { get; set; }

        public StorageEntity IncludeAction(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Action"))
            {
                this.ColumnList.Add("Action");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public StorageEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public StorageEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsForbid", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsForbid { get; set; }

        public StorageEntity IncludeIsForbid(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsForbid"))
            {
                this.ColumnList.Add("IsForbid");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDefault", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDefault { get; set; }

        public StorageEntity IncludeIsDefault(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDefault"))
            {
                this.ColumnList.Add("IsDefault");
            }
            return this;
        }

        [DataMapping(ColumnName = "Contact", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Contact { get; set; }

        public StorageEntity IncludeContact(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Contact"))
            {
                this.ColumnList.Add("Contact");
            }
            return this;
        }

        [DataMapping(ColumnName = "Phone", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Phone { get; set; }

        public StorageEntity IncludePhone(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Phone"))
            {
                this.ColumnList.Add("Phone");
            }
            return this;
        }

        [DataMapping(ColumnName = "Area", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Area { get; set; }

        public StorageEntity IncludeArea(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Area"))
            {
                this.ColumnList.Add("Area");
            }
            return this;
        }

        [DataMapping(ColumnName = "Address", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Address { get; set; }

        public StorageEntity IncludeAddress(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Address"))
            {
                this.ColumnList.Add("Address");
            }
            return this;
        }

        [DataMapping(ColumnName = "LeaseTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime LeaseTime { get; set; }

        public StorageEntity IncludeLeaseTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LeaseTime"))
            {
                this.ColumnList.Add("LeaseTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "DepartNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string DepartNum { get; set; }

        public StorageEntity IncludeDepartNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("DepartNum"))
            {
                this.ColumnList.Add("DepartNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public StorageEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public StorageEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public StorageEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public StorageEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
