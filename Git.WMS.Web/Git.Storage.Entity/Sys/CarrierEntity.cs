using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Sys
{
    [TableAttribute(DbName = "GitWMS", Name = "Carrier", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class CarrierEntity : BaseEntity
    {
        public CarrierEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public CarrierEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public CarrierEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CarrierNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CarrierNum { get; set; }

        public CarrierEntity IncludeCarrierNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CarrierNum"))
            {
                this.ColumnList.Add("CarrierNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "CarrierName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CarrierName { get; set; }

        public CarrierEntity IncludeCarrierName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CarrierName"))
            {
                this.ColumnList.Add("CarrierName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public CarrierEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 IsDelete { get; set; }

        public CarrierEntity IncludeIsDelete(bool flag)
        {
            if (flag && !this.ColumnList.Contains("IsDelete"))
            {
                this.ColumnList.Add("IsDelete");
            }
            return this;
        }

        [DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public DateTime CreateTime { get; set; }

        public CarrierEntity IncludeCreateTime(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CreateTime"))
            {
                this.ColumnList.Add("CreateTime");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public CarrierEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
