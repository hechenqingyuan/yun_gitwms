using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Entity.Storage
{
    [TableAttribute(DbName = "GitWMS", Name = "V_LocalCapacity", PrimaryKeyName = "", IsInternal = false)]
    public partial class V_LocalCapacityEntity : BaseEntity
    {
        public V_LocalCapacityEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 ID { get; set; }

        public V_LocalCapacityEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "LocalNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string LocalNum { get; set; }

        public V_LocalCapacityEntity IncludeLocalNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LocalNum"))
            {
                this.ColumnList.Add("LocalNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "LocalBarCode", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string LocalBarCode { get; set; }

        public V_LocalCapacityEntity IncludeLocalBarCode(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LocalBarCode"))
            {
                this.ColumnList.Add("LocalBarCode");
            }
            return this;
        }

        [DataMapping(ColumnName = "LocalName", DbType = DbType.String, Length = 100, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string LocalName { get; set; }

        public V_LocalCapacityEntity IncludeLocalName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LocalName"))
            {
                this.ColumnList.Add("LocalName");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageNum { get; set; }

        public V_LocalCapacityEntity IncludeStorageNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageNum"))
            {
                this.ColumnList.Add("StorageNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageType", DbType = DbType.Int32, Length = 4, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 StorageType { get; set; }

        public V_LocalCapacityEntity IncludeStorageType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageType"))
            {
                this.ColumnList.Add("StorageType");
            }
            return this;
        }

        [DataMapping(ColumnName = "LocalType", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public Int32 LocalType { get; set; }

        public V_LocalCapacityEntity IncludeLocalType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("LocalType"))
            {
                this.ColumnList.Add("LocalType");
            }
            return this;
        }

        [DataMapping(ColumnName = "Rack", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Rack { get; set; }

        public V_LocalCapacityEntity IncludeRack(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Rack"))
            {
                this.ColumnList.Add("Rack");
            }
            return this;
        }

        [DataMapping(ColumnName = "Length", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Length { get; set; }

        public V_LocalCapacityEntity IncludeLength(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Length"))
            {
                this.ColumnList.Add("Length");
            }
            return this;
        }

        [DataMapping(ColumnName = "Width", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Width { get; set; }

        public V_LocalCapacityEntity IncludeWidth(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Width"))
            {
                this.ColumnList.Add("Width");
            }
            return this;
        }

        [DataMapping(ColumnName = "Height", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Height { get; set; }

        public V_LocalCapacityEntity IncludeHeight(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Height"))
            {
                this.ColumnList.Add("Height");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitNum { get; set; }

        public V_LocalCapacityEntity IncludeUnitNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitNum"))
            {
                this.ColumnList.Add("UnitNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "UnitName", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string UnitName { get; set; }

        public V_LocalCapacityEntity IncludeUnitName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("UnitName"))
            {
                this.ColumnList.Add("UnitName");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public V_LocalCapacityEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

        [DataMapping(ColumnName = "StorageName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string StorageName { get; set; }

        public V_LocalCapacityEntity IncludeStorageName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("StorageName"))
            {
                this.ColumnList.Add("StorageName");
            }
            return this;
        }

        [DataMapping(ColumnName = "Num", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Num { get; set; }

        public V_LocalCapacityEntity IncludeNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Num"))
            {
                this.ColumnList.Add("Num");
            }
            return this;
        }
       
    }
}
