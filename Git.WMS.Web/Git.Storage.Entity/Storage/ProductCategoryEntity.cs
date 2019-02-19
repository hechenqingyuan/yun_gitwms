/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-03-03 22:19:09
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-03-03 22:19:09
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Storage
{
    public partial class ProductCategoryEntity
    {
        /// <summary>
        /// 父类名称
        /// </summary>
        public string ParentName { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "ProductCategory", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class ProductCategoryEntity : BaseEntity
    {
        public ProductCategoryEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public ProductCategoryEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public ProductCategoryEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CateNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CateNum { get; set; }

        public ProductCategoryEntity IncludeCateNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CateNum"))
            {
                this.ColumnList.Add("CateNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CateName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CateName { get; set; }

        public ProductCategoryEntity IncludeCateName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CateName"))
            {
                this.ColumnList.Add("CateName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Type", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Type { get; set; }

        public ProductCategoryEntity IncludeType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Type"))
            {
                this.ColumnList.Add("Type");
            }
            return this;
        }

        [DataMapping(ColumnName = "Depth", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Depth { get; set; }

        public ProductCategoryEntity IncludeDepth(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Depth"))
            {
                this.ColumnList.Add("Depth");
            }
            return this;
        }

        [DataMapping(ColumnName = "Left", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Left { get; set; }

        public ProductCategoryEntity IncludeLeft(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Left"))
            {
                this.ColumnList.Add("Left");
            }
            return this;
        }

        [DataMapping(ColumnName = "Right", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Right { get; set; }

        public ProductCategoryEntity IncludeRight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Right"))
            {
                this.ColumnList.Add("Right");
            }
            return this;
        }

        [DataMapping(ColumnName = "ParentNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ParentNum { get; set; }

        public ProductCategoryEntity IncludeParentNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ParentNum"))
            {
                this.ColumnList.Add("ParentNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public ProductCategoryEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public ProductCategoryEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public ProductCategoryEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public ProductCategoryEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public ProductCategoryEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
