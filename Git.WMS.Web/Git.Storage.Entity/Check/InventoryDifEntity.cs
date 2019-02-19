/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-06-13 14:12:01
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-06-13 14:12:01
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Check
{
    public partial class InventoryDifEntity
    {
        [DataMapping(ColumnName = "Size", DbType = DbType.String)]
        public string Size { get; set; }

        [DataMapping(ColumnName = "UnitName", DbType = DbType.String)]
        public string UnitName { get; set; }

        [DataMapping(ColumnName = "CateName", DbType = DbType.String)]
        public string CateName { get; set; }

        [DataMapping(ColumnName = "StorageName", DbType = DbType.String)]
        public string StorageName { get; set; }
    }


	[TableAttribute(DbName = "GitWMS", Name = "InventoryDif", PrimaryKeyName = "ID", IsInternal = false)]
	public partial class InventoryDifEntity:BaseEntity
	{
		public InventoryDifEntity()
		{
		}

		[DataMapping(ColumnName = "ID", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=true,AutoIncrement=true,IsMap=true)]
		public Int32 ID { get;  set; }

		public InventoryDifEntity IncludeID (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ID"))
			{
				this.ColumnList.Add("ID");
			}
			return this;
		}

		[DataMapping(ColumnName = "SnNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string SnNum { get;  set; }

		public InventoryDifEntity IncludeSnNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("SnNum"))
			{
				this.ColumnList.Add("SnNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "OrderSnNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string OrderSnNum { get;  set; }

		public InventoryDifEntity IncludeOrderSnNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("OrderSnNum"))
			{
				this.ColumnList.Add("OrderSnNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "OrderNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string OrderNum { get;  set; }

		public InventoryDifEntity IncludeOrderNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("OrderNum"))
			{
				this.ColumnList.Add("OrderNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "LocalNum", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string LocalNum { get;  set; }

		public InventoryDifEntity IncludeLocalNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("LocalNum"))
			{
				this.ColumnList.Add("LocalNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "LocalName", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string LocalName { get;  set; }

		public InventoryDifEntity IncludeLocalName (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("LocalName"))
			{
				this.ColumnList.Add("LocalName");
			}
			return this;
		}

		[DataMapping(ColumnName = "StorageNum", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string StorageNum { get;  set; }

		public InventoryDifEntity IncludeStorageNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("StorageNum"))
			{
				this.ColumnList.Add("StorageNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "ProductNum", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string ProductNum { get;  set; }

		public InventoryDifEntity IncludeProductNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ProductNum"))
			{
				this.ColumnList.Add("ProductNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "BarCode", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string BarCode { get;  set; }

		public InventoryDifEntity IncludeBarCode (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("BarCode"))
			{
				this.ColumnList.Add("BarCode");
			}
			return this;
		}

		[DataMapping(ColumnName = "ProductName", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string ProductName { get;  set; }

		public InventoryDifEntity IncludeProductName (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ProductName"))
			{
				this.ColumnList.Add("ProductName");
			}
			return this;
		}

		[DataMapping(ColumnName = "BatchNum", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string BatchNum { get;  set; }

		public InventoryDifEntity IncludeBatchNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("BatchNum"))
			{
				this.ColumnList.Add("BatchNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "LocalQty", DbType = DbType.Double,Length=8,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public double LocalQty { get;  set; }

		public InventoryDifEntity IncludeLocalQty (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("LocalQty"))
			{
				this.ColumnList.Add("LocalQty");
			}
			return this;
		}

		[DataMapping(ColumnName = "FirstQty", DbType = DbType.Double,Length=8,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public double FirstQty { get;  set; }

		public InventoryDifEntity IncludeFirstQty (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("FirstQty"))
			{
				this.ColumnList.Add("FirstQty");
			}
			return this;
		}

		[DataMapping(ColumnName = "SecondQty", DbType = DbType.Double,Length=8,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public double SecondQty { get;  set; }

		public InventoryDifEntity IncludeSecondQty (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("SecondQty"))
			{
				this.ColumnList.Add("SecondQty");
			}
			return this;
		}

		[DataMapping(ColumnName = "DifQty", DbType = DbType.Double,Length=8,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public double DifQty { get;  set; }

		public InventoryDifEntity IncludeDifQty (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("DifQty"))
			{
				this.ColumnList.Add("DifQty");
			}
			return this;
		}

		[DataMapping(ColumnName = "FirstUser", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string FirstUser { get;  set; }

		public InventoryDifEntity IncludeFirstUser (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("FirstUser"))
			{
				this.ColumnList.Add("FirstUser");
			}
			return this;
		}

		[DataMapping(ColumnName = "SecondUser", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string SecondUser { get;  set; }

		public InventoryDifEntity IncludeSecondUser (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("SecondUser"))
			{
				this.ColumnList.Add("SecondUser");
			}
			return this;
		}

		[DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime,Length=8,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public DateTime CreateTime { get;  set; }

		public InventoryDifEntity IncludeCreateTime (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CreateTime"))
			{
				this.ColumnList.Add("CreateTime");
			}
			return this;
		}

		[DataMapping(ColumnName = "CompanyID", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string CompanyID { get;  set; }

		public InventoryDifEntity IncludeCompanyID (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CompanyID"))
			{
				this.ColumnList.Add("CompanyID");
			}
			return this;
		}

	}
}
