/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-06-01 23:07:36
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-06-01 23:07:36
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Biz
{

    public partial class SaleDetailEntity
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
        /// 客户编号
        /// </summary>
        [DataMapping(ColumnName = "CusNum", DbType = DbType.String)]
        public string CusNum { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [DataMapping(ColumnName = "CusName", DbType = DbType.String)]
        public string CusName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [DataMapping(ColumnName = "Phone", DbType = DbType.String)]
        public string Phone { get; set; }

        /// <summary>
        /// 客户地址
        /// </summary>
        [DataMapping(ColumnName = "Address", DbType = DbType.String)]
        public string Address { get; set; }

        /// <summary>
        /// 订单时间
        /// </summary>
        [DataMapping(ColumnName = "OrderTime", DbType = DbType.DateTime)]
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// 发货日期
        /// </summary>
        [DataMapping(ColumnName = "SendDate", DbType = DbType.DateTime)]
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 财务状态
        /// </summary>
        [DataMapping(ColumnName = "AuditeStatus", DbType = DbType.Int32)]
        public int AuditeStatus { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMapping(ColumnName = "OrderStatus", DbType = DbType.Int32)]
        public int OrderStatus { get; set; }

        /// <summary>
        /// 是否有退货
        /// </summary>
        [DataMapping(ColumnName = "HasReturn", DbType = DbType.Int32)]
        public int HasReturn { get; set; }

        /// <summary>
        /// 订单总额
        /// </summary>
        [DataMapping(ColumnName = "OrderAmount", DbType = DbType.Double)]
        public double OrderAmount { get; set; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        [DataMapping(ColumnName = "CusOrderNum", DbType = DbType.String)]
        public string CusOrderNum { get; set; }

        /// <summary>
        /// 关联单据号
        /// </summary>
        [DataMapping(ColumnName = "ContractOrder", DbType = DbType.String)]
        public string ContractOrder { get; set; }

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
        /// 审核人名称
        /// </summary>
        [DataMapping(ColumnName = "AuditeUserName", DbType = DbType.String)]
        public string AuditeUserName { get; set; }


        /// <summary>
        /// 查询辅助开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 查询辅助结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 辅助数量(拣货等)
        /// </summary>
        public double Qty { get; set; }

    }

    [TableAttribute(DbName = "GitWMS", Name = "SaleDetail", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class SaleDetailEntity : BaseEntity
    {
        public SaleDetailEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public SaleDetailEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public SaleDetailEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderSnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderSnNum { get; set; }

        public SaleDetailEntity IncludeOrderSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderSnNum"))
            {
                this.ColumnList.Add("OrderSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public SaleDetailEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public SaleDetailEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public SaleDetailEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public SaleDetailEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public SaleDetailEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "RealNum", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double RealNum { get; set; }

        public SaleDetailEntity IncludeRealNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("RealNum"))
            {
                this.ColumnList.Add("RealNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ReturnNum", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double ReturnNum { get; set; }

        public SaleDetailEntity IncludeReturnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ReturnNum"))
            {
                this.ColumnList.Add("ReturnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitNum { get; set; }

        public SaleDetailEntity IncludeUnitNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitNum"))
            {
                this.ColumnList.Add("UnitNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Price", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Price { get; set; }

        public SaleDetailEntity IncludePrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Price"))
            {
                this.ColumnList.Add("Price");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public SaleDetailEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public SaleDetailEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "SendTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime SendTime { get; set; }

        public SaleDetailEntity IncludeSendTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SendTime"))
            {
                this.ColumnList.Add("SendTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "ContractID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ContractID { get; set; }

        public SaleDetailEntity IncludeContractID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ContractID"))
            {
                this.ColumnList.Add("ContractID");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public SaleDetailEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public SaleDetailEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public SaleDetailEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
