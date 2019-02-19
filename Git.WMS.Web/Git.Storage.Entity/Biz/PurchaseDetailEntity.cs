/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-06-01 23:07:49
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-06-01 23:07:49
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Biz
{
    public partial class PurchaseDetailEntity
    {
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

        /// <summary>
        /// 供应商编码 
        /// </summary>
        [DataMapping(ColumnName = "SupNum", DbType = DbType.String)]
        public string SupNum { get; set; }

        /// <summary>
        /// 供应商联系人
        /// </summary>
        [DataMapping(ColumnName = "Contact", DbType = DbType.String)]
        public string Contact { get; set; }

        /// <summary>
        /// 供应商联系电话
        /// </summary>
        [DataMapping(ColumnName = "Phone", DbType = DbType.String)]
        public string Phone { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [DataMapping(ColumnName = "SupName", DbType = DbType.String)]
        public string SupName { get; set; }

        /// <summary>
        /// 订单时间
        /// </summary>
        [DataMapping(ColumnName = "OrderTime", DbType = DbType.DateTime)]
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// 预计收货时间
        /// </summary>
        [DataMapping(ColumnName = "RevDate", DbType = DbType.DateTime)]
        public DateTime RevDate { get; set; }

        /// <summary>
        /// 采购总金额
        /// </summary>
        [DataMapping(ColumnName = "OrderAmount", DbType = DbType.Double)]
        public double OrderAmount { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMapping(ColumnName = "OrderStatus", DbType = DbType.Int32)]
        public int OrderStatus { get; set; }

        /// <summary>
        /// 财务状况
        /// </summary>
        [DataMapping(ColumnName = "AuditeStatus", DbType = DbType.Int32)]
        public int AuditeStatus { get; set; }

        /// <summary>
        /// 是否退货
        /// </summary>
        [DataMapping(ColumnName = "HasReturn", DbType = DbType.Int32)]
        public int HasReturn { get; set; }

        /// <summary>
        /// 关联单据号
        /// </summary>
        [DataMapping(ColumnName = "ContractOrder", DbType = DbType.String)]
        public string ContractOrder { get; set; }

        /// <summary>
        /// 关联单据唯一编号
        /// </summary>
        [DataMapping(ColumnName = "ContractSn", DbType = DbType.String)]
        public string ContractSn { get; set; }

        /// <summary>
        /// 单据创建人
        /// </summary>
        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String)]
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [DataMapping(ColumnName = "CreateUserName", DbType = DbType.String)]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 审核人名称
        /// </summary>
        [DataMapping(ColumnName = "AuidteUserName", DbType = DbType.String)]
        public string AuidteUserName { get; set; }

        /// <summary>
        /// 查询搜索条件开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 查询搜索条件结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 辅助作用数量
        /// </summary>
        public double Qty { get; set; }

        /// <summary>
        /// 辅助关联库位地址
        /// </summary>
        public string LocalNum { get; set; }

        /// <summary>
        /// 辅助关联库位名称
        /// </summary>
        public string LocalName { get; set; }

        /// <summary>
        /// 辅助批次
        /// </summary>
        public string BatchNum { get; set; }

        /// <summary>
        /// 辅助作用-仓库编号
        /// </summary>
        public string StorageNum { get; set; }
    }


	[TableAttribute(DbName = "GitWMS", Name = "PurchaseDetail", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class PurchaseDetailEntity : BaseEntity
    {
        public PurchaseDetailEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public PurchaseDetailEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public PurchaseDetailEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderSnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderSnNum { get; set; }

        public PurchaseDetailEntity IncludeOrderSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderSnNum"))
            {
                this.ColumnList.Add("OrderSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public PurchaseDetailEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public PurchaseDetailEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public PurchaseDetailEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public PurchaseDetailEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public PurchaseDetailEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "RealNum", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double RealNum { get; set; }

        public PurchaseDetailEntity IncludeRealNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("RealNum"))
            {
                this.ColumnList.Add("RealNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "InNum", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double InNum { get; set; }

        public PurchaseDetailEntity IncludeInNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InNum"))
            {
                this.ColumnList.Add("InNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ReturnNum", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double ReturnNum { get; set; }

        public PurchaseDetailEntity IncludeReturnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ReturnNum"))
            {
                this.ColumnList.Add("ReturnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitNum { get; set; }

        public PurchaseDetailEntity IncludeUnitNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitNum"))
            {
                this.ColumnList.Add("UnitNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Price", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Price { get; set; }

        public PurchaseDetailEntity IncludePrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Price"))
            {
                this.ColumnList.Add("Price");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public PurchaseDetailEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public PurchaseDetailEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "SendTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime SendTime { get; set; }

        public PurchaseDetailEntity IncludeSendTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SendTime"))
            {
                this.ColumnList.Add("SendTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractID { get; set; }

        public PurchaseDetailEntity IncludeContractID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractID"))
            {
                this.ColumnList.Add("ContractID");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public PurchaseDetailEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public PurchaseDetailEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public PurchaseDetailEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
