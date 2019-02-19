/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-07-26 22:05:05
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-07-26 22:05:05       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Storage
{
    public partial class V_StorageProductEntity
    {
        [DataMapping(ColumnName = "MinNum", DbType = DbType.Double)]
        public double MinNum { get; set; }

        [DataMapping(ColumnName = "MaxNum", DbType = DbType.Double)]
        public double MaxNum { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "V_StorageProduct", PrimaryKeyName = "", IsInternal = false)]
    public partial class V_StorageProductEntity : BaseEntity
    {
        public V_StorageProductEntity()
        {
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public V_StorageProductEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public V_StorageProductEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public V_StorageProductEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageNum { get; set; }

        public V_StorageProductEntity IncludeStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageNum"))
            {
                this.ColumnList.Add("StorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageName { get; set; }

        public V_StorageProductEntity IncludeStorageName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageName"))
            {
                this.ColumnList.Add("StorageName");
            }
            return this;
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 ID { get; set; }

        public V_StorageProductEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public V_StorageProductEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public V_StorageProductEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Size", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Size { get; set; }

        public V_StorageProductEntity IncludeSize(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Size"))
            {
                this.ColumnList.Add("Size");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitNum { get; set; }

        public V_StorageProductEntity IncludeUnitNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitNum"))
            {
                this.ColumnList.Add("UnitNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitName", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitName { get; set; }

        public V_StorageProductEntity IncludeUnitName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitName"))
            {
                this.ColumnList.Add("UnitName");
            }
            return this;
        }

        [DataMapping(ColumnName = "CateNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CateNum { get; set; }

        public V_StorageProductEntity IncludeCateNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CateNum"))
            {
                this.ColumnList.Add("CateNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CateName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CateName { get; set; }

        public V_StorageProductEntity IncludeCateName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CateName"))
            {
                this.ColumnList.Add("CateName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Display", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Display { get; set; }

        public V_StorageProductEntity IncludeDisplay(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Display"))
            {
                this.ColumnList.Add("Display");
            }
            return this;
        }

        [DataMapping(ColumnName = "Color", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Color { get; set; }

        public V_StorageProductEntity IncludeColor(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Color"))
            {
                this.ColumnList.Add("Color");
            }
            return this;
        }

        [DataMapping(ColumnName = "InPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double InPrice { get; set; }

        public V_StorageProductEntity IncludeInPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InPrice"))
            {
                this.ColumnList.Add("InPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "OutPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double OutPrice { get; set; }

        public V_StorageProductEntity IncludeOutPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OutPrice"))
            {
                this.ColumnList.Add("OutPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "AvgPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double AvgPrice { get; set; }

        public V_StorageProductEntity IncludeAvgPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AvgPrice"))
            {
                this.ColumnList.Add("AvgPrice");
            }
            return this;
        }

    }
}
