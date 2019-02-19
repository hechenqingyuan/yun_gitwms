/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/5/12 22:16:21
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/5/12 22:16:21       情缘
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
    [TableAttribute(DbName = "GitWMS", Name = "V_Sku", PrimaryKeyName = "", IsInternal = false)]
    public partial class V_SkuEntity : BaseEntity
    {
        public V_SkuEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 ID { get; set; }

        public V_SkuEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public V_SkuEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ParentNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ParentNum { get; set; }

        public V_SkuEntity IncludeParentNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ParentNum"))
            {
                this.ColumnList.Add("ParentNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public V_SkuEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "Size", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Size { get; set; }

        public V_SkuEntity IncludeSize(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Size"))
            {
                this.ColumnList.Add("Size");
            }
            return this;
        }

        [DataMapping(ColumnName = "Color", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Color { get; set; }

        public V_SkuEntity IncludeColor(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Color"))
            {
                this.ColumnList.Add("Color");
            }
            return this;
        }

        [DataMapping(ColumnName = "InPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double InPrice { get; set; }

        public V_SkuEntity IncludeInPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InPrice"))
            {
                this.ColumnList.Add("InPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "OutPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double OutPrice { get; set; }

        public V_SkuEntity IncludeOutPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OutPrice"))
            {
                this.ColumnList.Add("OutPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "AvgPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double AvgPrice { get; set; }

        public V_SkuEntity IncludeAvgPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AvgPrice"))
            {
                this.ColumnList.Add("AvgPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "NetWeight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double NetWeight { get; set; }

        public V_SkuEntity IncludeNetWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("NetWeight"))
            {
                this.ColumnList.Add("NetWeight");
            }
            return this;
        }

        [DataMapping(ColumnName = "GrossWeight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double GrossWeight { get; set; }

        public V_SkuEntity IncludeGrossWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("GrossWeight"))
            {
                this.ColumnList.Add("GrossWeight");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public V_SkuEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public V_SkuEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public V_SkuEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public V_SkuEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

        [DataMapping(ColumnName = "FactoryNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string FactoryNum { get; set; }

        public V_SkuEntity IncludeFactoryNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("FactoryNum"))
            {
                this.ColumnList.Add("FactoryNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "InCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string InCode { get; set; }

        public V_SkuEntity IncludeInCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InCode"))
            {
                this.ColumnList.Add("InCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public V_SkuEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public V_SkuEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "MinNum", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double MinNum { get; set; }

        public V_SkuEntity IncludeMinNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("MinNum"))
            {
                this.ColumnList.Add("MinNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "MaxNum", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double MaxNum { get; set; }

        public V_SkuEntity IncludeMaxNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("MaxNum"))
            {
                this.ColumnList.Add("MaxNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitNum { get; set; }

        public V_SkuEntity IncludeUnitNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitNum"))
            {
                this.ColumnList.Add("UnitNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitName", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitName { get; set; }

        public V_SkuEntity IncludeUnitName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitName"))
            {
                this.ColumnList.Add("UnitName");
            }
            return this;
        }

        [DataMapping(ColumnName = "CateNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CateNum { get; set; }

        public V_SkuEntity IncludeCateNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CateNum"))
            {
                this.ColumnList.Add("CateNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CateName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CateName { get; set; }

        public V_SkuEntity IncludeCateName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CateName"))
            {
                this.ColumnList.Add("CateName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Description", DbType = DbType.String, Length = 16, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Description { get; set; }

        public V_SkuEntity IncludeDescription(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Description"))
            {
                this.ColumnList.Add("Description");
            }
            return this;
        }

        [DataMapping(ColumnName = "PicUrl", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string PicUrl { get; set; }

        public V_SkuEntity IncludePicUrl(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PicUrl"))
            {
                this.ColumnList.Add("PicUrl");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageNum { get; set; }

        public V_SkuEntity IncludeStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageNum"))
            {
                this.ColumnList.Add("StorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "DefaultLocal", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string DefaultLocal { get; set; }

        public V_SkuEntity IncludeDefaultLocal(bool flag)
        {
            if (flag && !this.ColumnList.Contains("DefaultLocal"))
            {
                this.ColumnList.Add("DefaultLocal");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusNum { get; set; }

        public V_SkuEntity IncludeCusNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusNum"))
            {
                this.ColumnList.Add("CusNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusName { get; set; }

        public V_SkuEntity IncludeCusName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusName"))
            {
                this.ColumnList.Add("CusName");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupNum { get; set; }

        public V_SkuEntity IncludeSupNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupNum"))
            {
                this.ColumnList.Add("SupNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupName { get; set; }

        public V_SkuEntity IncludeSupName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupName"))
            {
                this.ColumnList.Add("SupName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Display", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Display { get; set; }

        public V_SkuEntity IncludeDisplay(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Display"))
            {
                this.ColumnList.Add("Display");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 16, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public V_SkuEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

    }
}
