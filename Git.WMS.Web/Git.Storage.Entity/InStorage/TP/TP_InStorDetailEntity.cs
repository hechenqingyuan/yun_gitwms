/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017-01-16 21:20:45
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017-01-16 21:20:45       情缘
*********************************************************************************/

using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.InStorage.TP
{
    public partial class TP_InStorDetailEntity : BaseEntity
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
        /// 入库部门
        /// </summary>
        [DataMapping(ColumnName = "DepartName", DbType = DbType.String)]
        public string DepartName { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        [DataMapping(ColumnName = "DepartNum", DbType = DbType.String)]
        public string DepartNum { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [DataMapping(ColumnName = "PlateNum", DbType = DbType.String)]
        public string PlateNum { get; set; }

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

    [TableAttribute(DbName = "GitWMS", Name = "TP_InStorDetail", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class TP_InStorDetailEntity : BaseEntity
    {
        public TP_InStorDetailEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public TP_InStorDetailEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public TP_InStorDetailEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderSnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderSnNum { get; set; }

        public TP_InStorDetailEntity IncludeOrderSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderSnNum"))
            {
                this.ColumnList.Add("OrderSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public TP_InStorDetailEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public TP_InStorDetailEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public TP_InStorDetailEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BatchNum", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BatchNum { get; set; }

        public TP_InStorDetailEntity IncludeBatchNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BatchNum"))
            {
                this.ColumnList.Add("BatchNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsSingle", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsSingle { get; set; }

        public TP_InStorDetailEntity IncludeIsSingle(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsSingle"))
            {
                this.ColumnList.Add("IsSingle");
            }
            return this;
        }

        [DataMapping(ColumnName = "LeaveTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime LeaveTime { get; set; }

        public TP_InStorDetailEntity IncludeLeaveTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LeaveTime"))
            {
                this.ColumnList.Add("LeaveTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "PalletNum", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 PalletNum { get; set; }

        public TP_InStorDetailEntity IncludePalletNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PalletNum"))
            {
                this.ColumnList.Add("PalletNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "BoxNum", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 BoxNum { get; set; }

        public TP_InStorDetailEntity IncludeBoxNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BoxNum"))
            {
                this.ColumnList.Add("BoxNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public TP_InStorDetailEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsPick", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsPick { get; set; }

        public TP_InStorDetailEntity IncludeIsPick(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsPick"))
            {
                this.ColumnList.Add("IsPick");
            }
            return this;
        }

        [DataMapping(ColumnName = "RealPalletNum", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 RealPalletNum { get; set; }

        public TP_InStorDetailEntity IncludeRealPalletNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("RealPalletNum"))
            {
                this.ColumnList.Add("RealPalletNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "RealBoxNum", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 RealBoxNum { get; set; }

        public TP_InStorDetailEntity IncludeRealBoxNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("RealBoxNum"))
            {
                this.ColumnList.Add("RealBoxNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "RealNum", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double RealNum { get; set; }

        public TP_InStorDetailEntity IncludeRealNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("RealNum"))
            {
                this.ColumnList.Add("RealNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "InPrice", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double InPrice { get; set; }

        public TP_InStorDetailEntity IncludeInPrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InPrice"))
            {
                this.ColumnList.Add("InPrice");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public TP_InStorDetailEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractOrder", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractOrder { get; set; }

        public TP_InStorDetailEntity IncludeContractOrder(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractOrder"))
            {
                this.ColumnList.Add("ContractOrder");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public TP_InStorDetailEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "LocalNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string LocalNum { get; set; }

        public TP_InStorDetailEntity IncludeLocalNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LocalNum"))
            {
                this.ColumnList.Add("LocalNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageNum { get; set; }

        public TP_InStorDetailEntity IncludeStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageNum"))
            {
                this.ColumnList.Add("StorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public TP_InStorDetailEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
