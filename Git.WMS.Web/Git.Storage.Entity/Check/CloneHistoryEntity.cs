/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-06-13 14:12:16
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-06-13 14:12:16
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Check
{
	[TableAttribute(DbName = "GitWMS", Name = "CloneHistory", PrimaryKeyName = "CloneID", IsInternal = false)]
	public partial class CloneHistoryEntity:BaseEntity
	{
		public CloneHistoryEntity()
		{
		}

		[DataMapping(ColumnName = "CloneID", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=true,AutoIncrement=true,IsMap=true)]
		public Int32 CloneID { get;  set; }

		public CloneHistoryEntity IncludeCloneID (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CloneID"))
			{
				this.ColumnList.Add("CloneID");
			}
			return this;
		}

		[DataMapping(ColumnName = "SnNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string SnNum { get;  set; }

		public CloneHistoryEntity IncludeSnNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("SnNum"))
			{
				this.ColumnList.Add("SnNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "OrderSnNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string OrderSnNum { get;  set; }

		public CloneHistoryEntity IncludeOrderSnNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("OrderSnNum"))
			{
				this.ColumnList.Add("OrderSnNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "OrderNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string OrderNum { get;  set; }

		public CloneHistoryEntity IncludeOrderNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("OrderNum"))
			{
				this.ColumnList.Add("OrderNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "Sn", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string Sn { get;  set; }

		public CloneHistoryEntity IncludeSn (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Sn"))
			{
				this.ColumnList.Add("Sn");
			}
			return this;
		}

		[DataMapping(ColumnName = "StorageNum", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string StorageNum { get;  set; }

		public CloneHistoryEntity IncludeStorageNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("StorageNum"))
			{
				this.ColumnList.Add("StorageNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "StorageName", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string StorageName { get;  set; }

		public CloneHistoryEntity IncludeStorageName (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("StorageName"))
			{
				this.ColumnList.Add("StorageName");
			}
			return this;
		}

		[DataMapping(ColumnName = "LocalNum", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string LocalNum { get;  set; }

		public CloneHistoryEntity IncludeLocalNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("LocalNum"))
			{
				this.ColumnList.Add("LocalNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "LocalName", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string LocalName { get;  set; }

		public CloneHistoryEntity IncludeLocalName (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("LocalName"))
			{
				this.ColumnList.Add("LocalName");
			}
			return this;
		}

		[DataMapping(ColumnName = "LocalType", DbType = DbType.Int32,Length=4,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int32 LocalType { get;  set; }

		public CloneHistoryEntity IncludeLocalType (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("LocalType"))
			{
				this.ColumnList.Add("LocalType");
			}
			return this;
		}

		[DataMapping(ColumnName = "ProductNum", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string ProductNum { get;  set; }

		public CloneHistoryEntity IncludeProductNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ProductNum"))
			{
				this.ColumnList.Add("ProductNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "BarCode", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string BarCode { get;  set; }

		public CloneHistoryEntity IncludeBarCode (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("BarCode"))
			{
				this.ColumnList.Add("BarCode");
			}
			return this;
		}

		[DataMapping(ColumnName = "ProductName", DbType = DbType.String,Length=400,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string ProductName { get;  set; }

		public CloneHistoryEntity IncludeProductName (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ProductName"))
			{
				this.ColumnList.Add("ProductName");
			}
			return this;
		}

		[DataMapping(ColumnName = "BatchNum", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string BatchNum { get;  set; }

		public CloneHistoryEntity IncludeBatchNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("BatchNum"))
			{
				this.ColumnList.Add("BatchNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "Num", DbType = DbType.Double,Length=8,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public double Num { get;  set; }

		public CloneHistoryEntity IncludeNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Num"))
			{
				this.ColumnList.Add("Num");
			}
			return this;
		}

		[DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime,Length=8,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public DateTime CreateTime { get;  set; }

		public CloneHistoryEntity IncludeCreateTime (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CreateTime"))
			{
				this.ColumnList.Add("CreateTime");
			}
			return this;
		}

		[DataMapping(ColumnName = "CreateUser", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string CreateUser { get;  set; }

		public CloneHistoryEntity IncludeCreateUser (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CreateUser"))
			{
				this.ColumnList.Add("CreateUser");
			}
			return this;
		}

		[DataMapping(ColumnName = "CreateName", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string CreateName { get;  set; }

		public CloneHistoryEntity IncludeCreateName (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CreateName"))
			{
				this.ColumnList.Add("CreateName");
			}
			return this;
		}

		[DataMapping(ColumnName = "Remark", DbType = DbType.String,Length=400,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string Remark { get;  set; }

		public CloneHistoryEntity IncludeRemark (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Remark"))
			{
				this.ColumnList.Add("Remark");
			}
			return this;
		}

		[DataMapping(ColumnName = "CompanyID", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string CompanyID { get;  set; }

		public CloneHistoryEntity IncludeCompanyID (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CompanyID"))
			{
				this.ColumnList.Add("CompanyID");
			}
			return this;
		}

	}
}
