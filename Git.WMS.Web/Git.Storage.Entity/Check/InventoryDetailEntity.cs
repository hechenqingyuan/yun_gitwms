/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-06-13 14:11:49
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-06-13 14:11:49
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Check
{
    public partial class InventoryDetailEntity
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMapping(ColumnName = "ProductName", DbType = DbType.String)]
        public string ProductName { get; set; }

        /// <summary>
        /// 产品条码
        /// </summary>
        [DataMapping(ColumnName = "BarCode", DbType = DbType.String)]
        public string BarCode { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        [DataMapping(ColumnName = "Size", DbType = DbType.String)]
        public string Size { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [DataMapping(ColumnName = "CateName", DbType = DbType.String)]
        public string CateName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [DataMapping(ColumnName = "UnitName", DbType = DbType.String)]
        public string UnitName { get; set; }
    }

	[TableAttribute(DbName = "GitWMS", Name = "InventoryDetail", PrimaryKeyName = "ID", IsInternal = false)]
	public partial class InventoryDetailEntity:BaseEntity
	{
		public InventoryDetailEntity()
		{
		}

		[DataMapping(ColumnName = "ID", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=true,AutoIncrement=true,IsMap=true)]
		public Int32 ID { get;  set; }

		public InventoryDetailEntity IncludeID (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ID"))
			{
				this.ColumnList.Add("ID");
			}
			return this;
		}

		[DataMapping(ColumnName = "SnNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string SnNum { get;  set; }

		public InventoryDetailEntity IncludeSnNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("SnNum"))
			{
				this.ColumnList.Add("SnNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "OrderSnNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string OrderSnNum { get;  set; }

		public InventoryDetailEntity IncludeOrderSnNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("OrderSnNum"))
			{
				this.ColumnList.Add("OrderSnNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "OrderNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string OrderNum { get;  set; }

		public InventoryDetailEntity IncludeOrderNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("OrderNum"))
			{
				this.ColumnList.Add("OrderNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "StorageNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string StorageNum { get;  set; }

		public InventoryDetailEntity IncludeStorageNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("StorageNum"))
			{
				this.ColumnList.Add("StorageNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "TargetNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string TargetNum { get;  set; }

		public InventoryDetailEntity IncludeTargetNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("TargetNum"))
			{
				this.ColumnList.Add("TargetNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime,Length=8,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public DateTime CreateTime { get;  set; }

		public InventoryDetailEntity IncludeCreateTime (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CreateTime"))
			{
				this.ColumnList.Add("CreateTime");
			}
			return this;
		}

		[DataMapping(ColumnName = "CompanyID", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string CompanyID { get;  set; }

		public InventoryDetailEntity IncludeCompanyID (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CompanyID"))
			{
				this.ColumnList.Add("CompanyID");
			}
			return this;
		}

	}
}
