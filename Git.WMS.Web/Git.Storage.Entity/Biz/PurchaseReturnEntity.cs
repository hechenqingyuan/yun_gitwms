using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Biz
{
    public partial class PurchaseReturnEntity
    { 
        /// <summary>
        /// 创建用户名
        /// </summary>
        [DataMapping(ColumnName = "CreateUserName", DbType = DbType.String)]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 查询开始时间--辅助作用
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 查询结束时间--辅助作用
        /// </summary>
        public string EndTime { get; set; }
    }

    [TableAttribute(DbName = "GitWMS", Name = "PurchaseReturn", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class PurchaseReturnEntity : BaseEntity
    {
        public PurchaseReturnEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public PurchaseReturnEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public PurchaseReturnEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "OrderNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string OrderNum { get; set; }

        public PurchaseReturnEntity IncludeOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("OrderNum"))
            {
                this.ColumnList.Add("OrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupSnNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupSnNum { get; set; }

        public PurchaseReturnEntity IncludeSupSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupSnNum"))
            {
                this.ColumnList.Add("SupSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupNum { get; set; }

        public PurchaseReturnEntity IncludeSupNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupNum"))
            {
                this.ColumnList.Add("SupNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "SupName", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SupName { get; set; }

        public PurchaseReturnEntity IncludeSupName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SupName"))
            {
                this.ColumnList.Add("SupName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Contact", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Contact { get; set; }

        public PurchaseReturnEntity IncludeContact(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Contact"))
            {
                this.ColumnList.Add("Contact");
            }
            return this;
        }

        [DataMapping(ColumnName = "Phone", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Phone { get; set; }

        public PurchaseReturnEntity IncludePhone(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Phone"))
            {
                this.ColumnList.Add("Phone");
            }
            return this;
        }

        [DataMapping(ColumnName = "PurchaseSnNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string PurchaseSnNum { get; set; }

        public PurchaseReturnEntity IncludePurchaseSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PurchaseSnNum"))
            {
                this.ColumnList.Add("PurchaseSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "PurchaseOrderNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string PurchaseOrderNum { get; set; }

        public PurchaseReturnEntity IncludePurchaseOrderNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PurchaseOrderNum"))
            {
                this.ColumnList.Add("PurchaseOrderNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public PurchaseReturnEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }

        [DataMapping(ColumnName = "Amount", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Amount { get; set; }

        public PurchaseReturnEntity IncludeAmount(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Amount"))
            {
                this.ColumnList.Add("Amount");
            }
            return this;
        }

        [DataMapping(ColumnName = "Weight", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Weight { get; set; }

        public PurchaseReturnEntity IncludeWeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Weight"))
            {
                this.ColumnList.Add("Weight");
            }
            return this;
        }

        [DataMapping(ColumnName = "ReturnTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime ReturnTime { get; set; }

        public PurchaseReturnEntity IncludeReturnTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ReturnTime"))
            {
                this.ColumnList.Add("ReturnTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "PrintTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime PrintTime { get; set; }

        public PurchaseReturnEntity IncludePrintTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("PrintTime"))
            {
                this.ColumnList.Add("PrintTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "Status", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 Status { get; set; }

        public PurchaseReturnEntity IncludeStatus(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Status"))
            {
                this.ColumnList.Add("Status");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public PurchaseReturnEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public PurchaseReturnEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateUser", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CreateUser { get; set; }

        public PurchaseReturnEntity IncludeCreateUser(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateUser"))
            {
                this.ColumnList.Add("CreateUser");
            }
            return this;
        }

        [DataMapping(ColumnName = "Reason", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Reason { get; set; }

        public PurchaseReturnEntity IncludeReason(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Reason"))
            {
                this.ColumnList.Add("Reason");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 800, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public PurchaseReturnEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public PurchaseReturnEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
