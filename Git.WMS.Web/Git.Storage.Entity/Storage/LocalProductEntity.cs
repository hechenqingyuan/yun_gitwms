/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-05 22:31:46
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-05 22:31:46       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Storage
{
    public partial class LocalProductEntity
    {
        /// <summary>
        /// </summary>
        /// 产品规格
        [DataMapping(ColumnName = "Size", DbType = DbType.String)]
        public string Size { get; set; }

        /// <summary>
        /// 产品分类编号
        /// </summary>
        [DataMapping(ColumnName = "CateNum", DbType = DbType.String)]
        public string CateNum { get; set; }

        /// <summary>
        /// 产品分类名称
        /// </summary>
        [DataMapping(ColumnName = "CateName", DbType = DbType.String)]
        public string CateName { get; set; }

        /// <summary>
        /// 产品单位编号
        /// </summary>
        [DataMapping(ColumnName = "UnitNum", DbType = DbType.String)]
        public string UnitNum { get; set; }

        /// <summary>
        /// 产品单位名称
        /// </summary>
        [DataMapping(ColumnName = "UnitName", DbType = DbType.String)]
        public string UnitName { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        [DataMapping(ColumnName = "AvgPrice", DbType = DbType.Double)]
        public double AvgPrice { get; set; }

        /// <summary>
        /// 产品预警最小值
        /// </summary>
        [DataMapping(ColumnName = "MinNum", DbType = DbType.Double)]
        public double MinNum { get; set; }

        /// <summary>
        /// 产品预警最大值
        /// </summary>
        [DataMapping(ColumnName = "MaxNum", DbType = DbType.Double)]
        public double MaxNum { get; set; }

        /// <summary>
        /// 辅助数量
        /// </summary>
        public double Qty { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "LocalProduct", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class LocalProductEntity : BaseEntity
    {
        public LocalProductEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public LocalProductEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "Sn", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Sn { get; set; }

        public LocalProductEntity IncludeSn(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Sn"))
            {
                this.ColumnList.Add("Sn");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageNum { get; set; }

        public LocalProductEntity IncludeStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageNum"))
            {
                this.ColumnList.Add("StorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageName { get; set; }

        public LocalProductEntity IncludeStorageName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageName"))
            {
                this.ColumnList.Add("StorageName");
            }
            return this;
        }

        [DataMapping(ColumnName = "LocalNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string LocalNum { get; set; }

        public LocalProductEntity IncludeLocalNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LocalNum"))
            {
                this.ColumnList.Add("LocalNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "LocalName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string LocalName { get; set; }

        public LocalProductEntity IncludeLocalName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LocalName"))
            {
                this.ColumnList.Add("LocalName");
            }
            return this;
        }

        [DataMapping(ColumnName = "LocalType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 LocalType { get; set; }

        public LocalProductEntity IncludeLocalType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LocalType"))
            {
                this.ColumnList.Add("LocalType");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public LocalProductEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public LocalProductEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public LocalProductEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "BatchNum", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BatchNum { get; set; }

        public LocalProductEntity IncludeBatchNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BatchNum"))
            {
                this.ColumnList.Add("BatchNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public LocalProductEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public LocalProductEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public LocalProductEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateName { get; set; }

        public LocalProductEntity IncludeCreateName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateName"))
            {
                this.ColumnList.Add("CreateName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public LocalProductEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public LocalProductEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
