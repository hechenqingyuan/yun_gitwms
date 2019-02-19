/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-05 22:34:13
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-05 22:34:13       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Storage
{
    public partial class InventoryBookEntity
    {
        /// <summary>
        /// 时间查询辅助字段
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 时间查询辅助字段
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 原仓库名称
        /// </summary>
        [DataMapping(ColumnName = "FromStorageName", DbType = DbType.String)]
        public string FromStorageName { get; set; }

        /// <summary>
        /// 来源地址
        /// </summary>
        [DataMapping(ColumnName = "FromLocalName", DbType = DbType.String)]
        public string FromLocalName { get; set; }

        /// <summary>
        /// 目标仓库名称
        /// </summary>
        [DataMapping(ColumnName = "ToStorageName", DbType = DbType.String)]
        public string ToStorageName { get; set; }

        /// <summary>
        /// 去向地址
        /// </summary>
        [DataMapping(ColumnName = "ToLocalName", DbType = DbType.String)]
        public string ToLocalName { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        [DataMapping(ColumnName = "Size", DbType = DbType.String)]
        public string Size { get; set; }

        /// <summary>
        /// 产品单位
        /// </summary>
        [DataMapping(ColumnName = "UnitName", DbType = DbType.String)]
        public string UnitName { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "InventoryBook", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class InventoryBookEntity : BaseEntity
    {
        public InventoryBookEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public InventoryBookEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public InventoryBookEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public InventoryBookEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 100, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public InventoryBookEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "BatchNum", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BatchNum { get; set; }

        public InventoryBookEntity IncludeBatchNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BatchNum"))
            {
                this.ColumnList.Add("BatchNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public InventoryBookEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "Type", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Type { get; set; }

        public InventoryBookEntity IncludeType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Type"))
            {
                this.ColumnList.Add("Type");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContactOrder", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContactOrder { get; set; }

        public InventoryBookEntity IncludeContactOrder(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContactOrder"))
            {
                this.ColumnList.Add("ContactOrder");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public InventoryBookEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "FromStorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string FromStorageNum { get; set; }

        public InventoryBookEntity IncludeFromStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("FromStorageNum"))
            {
                this.ColumnList.Add("FromStorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "FromLocalNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string FromLocalNum { get; set; }

        public InventoryBookEntity IncludeFromLocalNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("FromLocalNum"))
            {
                this.ColumnList.Add("FromLocalNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ToStorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ToStorageNum { get; set; }

        public InventoryBookEntity IncludeToStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ToStorageNum"))
            {
                this.ColumnList.Add("ToStorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ToLocalNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ToLocalNum { get; set; }

        public InventoryBookEntity IncludeToLocalNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ToLocalNum"))
            {
                this.ColumnList.Add("ToLocalNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public InventoryBookEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public InventoryBookEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public InventoryBookEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
