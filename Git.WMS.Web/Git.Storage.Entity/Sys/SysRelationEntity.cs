/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-03-03 22:07:05
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-03-03 22:07:05
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Sys
{
	[TableAttribute(DbName = "GitWMS", Name = "SysRelation", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class SysRelationEntity : BaseEntity
    {
        public SysRelationEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public SysRelationEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public SysRelationEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "RoleNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string RoleNum { get; set; }

        public SysRelationEntity IncludeRoleNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("RoleNum"))
            {
                this.ColumnList.Add("RoleNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ResNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ResNum { get; set; }

        public SysRelationEntity IncludeResNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ResNum"))
            {
                this.ColumnList.Add("ResNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ResType", DbType = DbType.Int16, Length = 2, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int16 ResType { get; set; }

        public SysRelationEntity IncludeResType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ResType"))
            {
                this.ColumnList.Add("ResType");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public SysRelationEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
