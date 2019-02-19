/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/5/12 22:15:35
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/5/12 22:15:35       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Sku
{
    [TableAttribute(DbName = "GitWMS", Name = "ProductSku", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class ProductSkuEntity : BaseEntity
    {
        public ProductSkuEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public ProductSkuEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public ProductSkuEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public ProductSkuEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public ProductSkuEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "Size", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Size { get; set; }

        public ProductSkuEntity IncludeSize(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Size"))
            {
                this.ColumnList.Add("Size");
            }
            return this;
        }

        [DataMapping(ColumnName = "Color", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Color { get; set; }

        public ProductSkuEntity IncludeColor(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Color"))
            {
                this.ColumnList.Add("Color");
            }
            return this;
        }

        [DataMapping(ColumnName = "InPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double InPrice { get; set; }

        public ProductSkuEntity IncludeInPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InPrice"))
            {
                this.ColumnList.Add("InPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "OutPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double OutPrice { get; set; }

        public ProductSkuEntity IncludeOutPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OutPrice"))
            {
                this.ColumnList.Add("OutPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "AvgPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double AvgPrice { get; set; }

        public ProductSkuEntity IncludeAvgPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AvgPrice"))
            {
                this.ColumnList.Add("AvgPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "NetWeight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double NetWeight { get; set; }

        public ProductSkuEntity IncludeNetWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("NetWeight"))
            {
                this.ColumnList.Add("NetWeight");
            }
            return this;
        }

        [DataMapping(ColumnName = "GrossWeight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double GrossWeight { get; set; }

        public ProductSkuEntity IncludeGrossWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("GrossWeight"))
            {
                this.ColumnList.Add("GrossWeight");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public ProductSkuEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public ProductSkuEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public ProductSkuEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public ProductSkuEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
