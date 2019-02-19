using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Biz
{
    public partial class SaleReturnDetailEntity
    {
        /// <summary>
        /// 产品单位名称
        /// </summary>
        [DataMapping(ColumnName = "UnitName", DbType = DbType.String)]
        public string UnitName { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        [DataMapping(ColumnName = "Size", DbType = DbType.String)]
        public string Size { get; set; }

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
        /// 客户联系人
        /// </summary>
        [DataMapping(ColumnName = "Contact", DbType = DbType.String)]
        public string Contact { get; set; }

        /// <summary>
        /// 客户联系电话
        /// </summary>
        [DataMapping(ColumnName = "Phone", DbType = DbType.String)]
        public string Phone { get; set; }

        /// <summary>
        /// 销售订单唯一编号
        /// </summary>
        [DataMapping(ColumnName = "SaleSnNum", DbType = DbType.String)]
        public string SaleSnNum { get; set; }

        /// <summary>
        /// 销售订单号
        /// </summary>
        [DataMapping(ColumnName = "SaleOrderNum", DbType = DbType.String)]
        public string SaleOrderNum { get; set; }

        /// <summary>
        /// 开始时间--查询辅助
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 结束时间--查询辅助
        /// </summary>
        public string EndTime { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "SaleReturnDetail", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class SaleReturnDetailEntity : BaseEntity
    {
        public SaleReturnDetailEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public SaleReturnDetailEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public SaleReturnDetailEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderSnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderSnNum { get; set; }

        public SaleReturnDetailEntity IncludeOrderSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderSnNum"))
            {
                this.ColumnList.Add("OrderSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public SaleReturnDetailEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public SaleReturnDetailEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public SaleReturnDetailEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public SaleReturnDetailEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public SaleReturnDetailEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "ReturnNum", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double ReturnNum { get; set; }

        public SaleReturnDetailEntity IncludeReturnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ReturnNum"))
            {
                this.ColumnList.Add("ReturnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitNum { get; set; }

        public SaleReturnDetailEntity IncludeUnitNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitNum"))
            {
                this.ColumnList.Add("UnitNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Price", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Price { get; set; }

        public SaleReturnDetailEntity IncludePrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Price"))
            {
                this.ColumnList.Add("Price");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public SaleReturnDetailEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public SaleReturnDetailEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "ReturnTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime ReturnTime { get; set; }

        public SaleReturnDetailEntity IncludeReturnTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ReturnTime"))
            {
                this.ColumnList.Add("ReturnTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "SaleDetailSn", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SaleDetailSn { get; set; }

        public SaleReturnDetailEntity IncludeSaleDetailSn(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SaleDetailSn"))
            {
                this.ColumnList.Add("SaleDetailSn");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public SaleReturnDetailEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public SaleReturnDetailEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public SaleReturnDetailEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
