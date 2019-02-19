/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-11 10:06:23
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-11 10:06:23       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Allocate
{
    public partial class AllocateDetailEntity
    {

        /// <summary>
        /// 调拨原仓库
        /// </summary>
        public string FromStorageName { get; set; }

        /// <summary>
        /// 调拨原仓库库位
        /// </summary>
        public string FromLocalName { get; set; }

        /// <summary>
        /// 调拨目标仓库
        /// </summary>
        public string ToStorageName { get; set; }

        /// <summary>
        /// 调拨目标仓库库位
        /// </summary>
        public string ToLocalName { get; set; }


        /// <summary>
        /// 查询辅助开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 查询辅助结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 查询仓库
        /// </summary>
        public string StorageNum { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        [DataMapping(ColumnName = "Size", DbType = DbType.String)]
        public string Size { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [DataMapping(ColumnName = "UnitName", DbType = DbType.String)]
        public string UnitName { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMapping(ColumnName = "Status", DbType = DbType.Int32)]
        public int Status { get; set; }

        /// <summary>
        /// 调拨类型
        /// </summary>
        [DataMapping(ColumnName = "AllocateType", DbType = DbType.Int32)]
        public int AllocateType { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [DataMapping(ColumnName = "CreateUserName", DbType = DbType.String)]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 审核人名称
        /// </summary>
        [DataMapping(ColumnName = "AuditUserName", DbType = DbType.String)]
        public string AuditUserName { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [DataMapping(ColumnName = "AuditeTime", DbType = DbType.DateTime)]
        public DateTime AuditeTime { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "AllocateDetail", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class AllocateDetailEntity : BaseEntity
    {
        public AllocateDetailEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public AllocateDetailEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public AllocateDetailEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderSnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderSnNum { get; set; }

        public AllocateDetailEntity IncludeOrderSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderSnNum"))
            {
                this.ColumnList.Add("OrderSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public AllocateDetailEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public AllocateDetailEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public AllocateDetailEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public AllocateDetailEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BatchNum", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BatchNum { get; set; }

        public AllocateDetailEntity IncludeBatchNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BatchNum"))
            {
                this.ColumnList.Add("BatchNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public AllocateDetailEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "InPrice", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double InPrice { get; set; }

        public AllocateDetailEntity IncludeInPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InPrice"))
            {
                this.ColumnList.Add("InPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public AllocateDetailEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsPick", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsPick { get; set; }

        public AllocateDetailEntity IncludeIsPick(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsPick"))
            {
                this.ColumnList.Add("IsPick");
            }
            return this;
        }

        [DataMapping(ColumnName = "RealNum", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double RealNum { get; set; }

        public AllocateDetailEntity IncludeRealNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("RealNum"))
            {
                this.ColumnList.Add("RealNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public AllocateDetailEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "FromStorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string FromStorageNum { get; set; }

        public AllocateDetailEntity IncludeFromStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("FromStorageNum"))
            {
                this.ColumnList.Add("FromStorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "FromLocalNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string FromLocalNum { get; set; }

        public AllocateDetailEntity IncludeFromLocalNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("FromLocalNum"))
            {
                this.ColumnList.Add("FromLocalNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ToStorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ToStorageNum { get; set; }

        public AllocateDetailEntity IncludeToStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ToStorageNum"))
            {
                this.ColumnList.Add("ToStorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ToLocalNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ToLocalNum { get; set; }

        public AllocateDetailEntity IncludeToLocalNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ToLocalNum"))
            {
                this.ColumnList.Add("ToLocalNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public AllocateDetailEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
