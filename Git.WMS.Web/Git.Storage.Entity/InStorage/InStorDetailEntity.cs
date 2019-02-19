/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2013-11-29 23:27:16
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2013-11-29 23:27:16
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.InStorage
{
    public partial class InStorDetailEntity : BaseEntity
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
        /// 产品规格
        /// </summary>
        [DataMapping(ColumnName = "Size", DbType = DbType.String)]
        public string Size { get; set; }

        /// <summary>
        /// 入库单单号
        /// </summary>
        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String)]
        public string OrderNum { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        [DataMapping(ColumnName = "SupNum", DbType = DbType.String)]
        public string SupNum { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [DataMapping(ColumnName = "SupName", DbType = DbType.String)]
        public string SupName { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        [DataMapping(ColumnName = "OrderTime", DbType = DbType.DateTime)]
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        [DataMapping(ColumnName = "AuditeTime", DbType = DbType.DateTime)]
        public DateTime AuditeTime { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMapping(ColumnName = "Status", DbType = DbType.Int32)]
        public int Status { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String)]
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [DataMapping(ColumnName = "CreateUserName", DbType = DbType.String)]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 审核人编号
        /// </summary>
        [DataMapping(ColumnName = "AuditeUser", DbType = DbType.String)]
        public string AuditeUser { get; set; }

        /// <summary>
        /// 审核人名称
        /// </summary>
        [DataMapping(ColumnName = "AuditeUserName", DbType = DbType.String)]
        public string AuditeUserName { get; set; }

        /// <summary>
        /// 入库单类型
        /// </summary>
        [DataMapping(ColumnName = "InType", DbType = DbType.Int32)]
        public int InType { get; set; }

        /// <summary>
        /// 辅助作用-查询开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 辅助作用-查询结束时间
        /// </summary>
        public string EndTime { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "InStorDetail", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class InStorDetailEntity : BaseEntity
    {
        public InStorDetailEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public InStorDetailEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public InStorDetailEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderSnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderSnNum { get; set; }

        public InStorDetailEntity IncludeOrderSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderSnNum"))
            {
                this.ColumnList.Add("OrderSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public InStorDetailEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public InStorDetailEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public InStorDetailEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BatchNum", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BatchNum { get; set; }

        public InStorDetailEntity IncludeBatchNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BatchNum"))
            {
                this.ColumnList.Add("BatchNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public InStorDetailEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsPick", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsPick { get; set; }

        public InStorDetailEntity IncludeIsPick(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsPick"))
            {
                this.ColumnList.Add("IsPick");
            }
            return this;
        }

        [DataMapping(ColumnName = "RealNum", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double RealNum { get; set; }

        public InStorDetailEntity IncludeRealNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("RealNum"))
            {
                this.ColumnList.Add("RealNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "InPrice", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double InPrice { get; set; }

        public InStorDetailEntity IncludeInPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InPrice"))
            {
                this.ColumnList.Add("InPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public InStorDetailEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractOrder", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractOrder { get; set; }

        public InStorDetailEntity IncludeContractOrder(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractOrder"))
            {
                this.ColumnList.Add("ContractOrder");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public InStorDetailEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "LocalNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string LocalNum { get; set; }

        public InStorDetailEntity IncludeLocalNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LocalNum"))
            {
                this.ColumnList.Add("LocalNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageNum { get; set; }

        public InStorDetailEntity IncludeStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageNum"))
            {
                this.ColumnList.Add("StorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public InStorDetailEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
