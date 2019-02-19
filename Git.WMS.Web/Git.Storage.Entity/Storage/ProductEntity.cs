/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-03-03 22:19:12
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-03-03 22:19:12
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Storage
{
    public partial class ProductEntity : BaseEntity
    {
        /// <summary>
        /// 仓库名称
        /// </summary>
        [DataMapping(ColumnName = "StorageName", DbType = DbType.String)]
        public string StorageName { get; set; }

        /// <summary>
        /// 库位名称
        /// </summary>
        [DataMapping(ColumnName = "LocalName", DbType = DbType.String)]
        public string LocalName { get; set; }

        /// <summary>
        /// 父类编号名称(SKU的情况)
        /// </summary>
        [DataMapping(ColumnName = "ParentNum", DbType = DbType.String)]
        public string ParentNum { get; set; }

        /// <summary>
        /// 库存数
        /// </summary>
        public float LocalNum { get; set; }

        /// <summary>
        /// 选择数量 辅助作用
        /// </summary>
        public float Qty { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "Product", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class ProductEntity : BaseEntity
    {
        public ProductEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public ProductEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public ProductEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public ProductEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "FactoryNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string FactoryNum { get; set; }

        public ProductEntity IncludeFactoryNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("FactoryNum"))
            {
                this.ColumnList.Add("FactoryNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "InCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string InCode { get; set; }

        public ProductEntity IncludeInCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InCode"))
            {
                this.ColumnList.Add("InCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 200, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public ProductEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public ProductEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "MinNum", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double MinNum { get; set; }

        public ProductEntity IncludeMinNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("MinNum"))
            {
                this.ColumnList.Add("MinNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "MaxNum", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double MaxNum { get; set; }

        public ProductEntity IncludeMaxNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("MaxNum"))
            {
                this.ColumnList.Add("MaxNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitNum { get; set; }

        public ProductEntity IncludeUnitNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitNum"))
            {
                this.ColumnList.Add("UnitNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitName", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitName { get; set; }

        public ProductEntity IncludeUnitName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitName"))
            {
                this.ColumnList.Add("UnitName");
            }
            return this;
        }

        [DataMapping(ColumnName = "CateNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CateNum { get; set; }

        public ProductEntity IncludeCateNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CateNum"))
            {
                this.ColumnList.Add("CateNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CateName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CateName { get; set; }

        public ProductEntity IncludeCateName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CateName"))
            {
                this.ColumnList.Add("CateName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Size", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Size { get; set; }

        public ProductEntity IncludeSize(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Size"))
            {
                this.ColumnList.Add("Size");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsSingle", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsSingle { get; set; }

        public ProductEntity IncludeIsSingle(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsSingle"))
            {
                this.ColumnList.Add("IsSingle");
            }
            return this;
        }

        [DataMapping(ColumnName = "Color", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Color { get; set; }

        public ProductEntity IncludeColor(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Color"))
            {
                this.ColumnList.Add("Color");
            }
            return this;
        }

        [DataMapping(ColumnName = "InPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double InPrice { get; set; }

        public ProductEntity IncludeInPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InPrice"))
            {
                this.ColumnList.Add("InPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "OutPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double OutPrice { get; set; }

        public ProductEntity IncludeOutPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OutPrice"))
            {
                this.ColumnList.Add("OutPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "AvgPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double AvgPrice { get; set; }

        public ProductEntity IncludeAvgPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("AvgPrice"))
            {
                this.ColumnList.Add("AvgPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "NetWeight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double NetWeight { get; set; }

        public ProductEntity IncludeNetWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("NetWeight"))
            {
                this.ColumnList.Add("NetWeight");
            }
            return this;
        }

        [DataMapping(ColumnName = "GrossWeight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double GrossWeight { get; set; }

        public ProductEntity IncludeGrossWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("GrossWeight"))
            {
                this.ColumnList.Add("GrossWeight");
            }
            return this;
        }

        [DataMapping(ColumnName = "Description", DbType = DbType.String, Length = 16, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Description { get; set; }

        public ProductEntity IncludeDescription(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Description"))
            {
                this.ColumnList.Add("Description");
            }
            return this;
        }

        [DataMapping(ColumnName = "PicUrl", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string PicUrl { get; set; }

        public ProductEntity IncludePicUrl(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PicUrl"))
            {
                this.ColumnList.Add("PicUrl");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public ProductEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public ProductEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public ProductEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageNum { get; set; }

        public ProductEntity IncludeStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageNum"))
            {
                this.ColumnList.Add("StorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "DefaultLocal", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string DefaultLocal { get; set; }

        public ProductEntity IncludeDefaultLocal(bool flag)
        {
            if (flag && !this.ColumnList.Contains("DefaultLocal"))
            {
                this.ColumnList.Add("DefaultLocal");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusNum { get; set; }

        public ProductEntity IncludeCusNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusNum"))
            {
                this.ColumnList.Add("CusNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CusName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CusName { get; set; }

        public ProductEntity IncludeCusName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CusName"))
            {
                this.ColumnList.Add("CusName");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupNum { get; set; }

        public ProductEntity IncludeSupNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupNum"))
            {
                this.ColumnList.Add("SupNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupName { get; set; }

        public ProductEntity IncludeSupName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupName"))
            {
                this.ColumnList.Add("SupName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Display", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Display { get; set; }

        public ProductEntity IncludeDisplay(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Display"))
            {
                this.ColumnList.Add("Display");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 16, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public ProductEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public ProductEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
