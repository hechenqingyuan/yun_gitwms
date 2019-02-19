using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Biz
{
    public partial class PurchaseReturnDetailEntity
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
        /// 供应商编号
        /// </summary>
        [DataMapping(ColumnName = "SupNum", DbType = DbType.String)]
        public string SupNum { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [DataMapping(ColumnName = "SupName", DbType = DbType.String)]
        public string SupName { get; set; }

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
        /// 采购订单唯一编号
        /// </summary>
        [DataMapping(ColumnName = "PurchaseSnNum", DbType = DbType.String)]
        public string PurchaseSnNum { get; set; }

        /// <summary>
        /// 采购订单号
        /// </summary>
        [DataMapping(ColumnName = "PurchaseOrderNum", DbType = DbType.String)]
        public string PurchaseOrderNum { get; set; }

        /// <summary>
        /// 开始时间--查询辅助
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 结束时间--查询辅助
        /// </summary>
        public string EndTime { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "PurchaseReturnDetail", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class PurchaseReturnDetailEntity : BaseEntity
    {
        public PurchaseReturnDetailEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public PurchaseReturnDetailEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public PurchaseReturnDetailEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderSnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderSnNum { get; set; }

        public PurchaseReturnDetailEntity IncludeOrderSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderSnNum"))
            {
                this.ColumnList.Add("OrderSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public PurchaseReturnDetailEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductName { get; set; }

        public PurchaseReturnDetailEntity IncludeProductName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductName"))
            {
                this.ColumnList.Add("ProductName");
            }
            return this;
        }

        [DataMapping(ColumnName = "BarCode", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string BarCode { get; set; }

        public PurchaseReturnDetailEntity IncludeBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("BarCode"))
            {
                this.ColumnList.Add("BarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public PurchaseReturnDetailEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public PurchaseReturnDetailEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "ReturnNum", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double ReturnNum { get; set; }

        public PurchaseReturnDetailEntity IncludeReturnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ReturnNum"))
            {
                this.ColumnList.Add("ReturnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitNum { get; set; }

        public PurchaseReturnDetailEntity IncludeUnitNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitNum"))
            {
                this.ColumnList.Add("UnitNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Price", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Price { get; set; }

        public PurchaseReturnDetailEntity IncludePrice(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Price"))
            {
                this.ColumnList.Add("Price");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public PurchaseReturnDetailEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public PurchaseReturnDetailEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "ReturnTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime ReturnTime { get; set; }

        public PurchaseReturnDetailEntity IncludeReturnTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ReturnTime"))
            {
                this.ColumnList.Add("ReturnTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "PurchaseDetailSn", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string PurchaseDetailSn { get; set; }

        public PurchaseReturnDetailEntity IncludePurchaseDetailSn(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PurchaseDetailSn"))
            {
                this.ColumnList.Add("PurchaseDetailSn");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public PurchaseReturnDetailEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public PurchaseReturnDetailEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public PurchaseReturnDetailEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
