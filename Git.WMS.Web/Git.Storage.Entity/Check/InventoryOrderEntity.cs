/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-06-13 14:11:38
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-06-13 14:11:38
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Check
{

    public partial class InventoryOrderEntity
    {
        /// <summary>
        /// 盘点单创建人
        /// </summary>
        [DataMapping(ColumnName = "CreateUserName", DbType = DbType.String)]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 盘点单审核人名称
        /// </summary>
        [DataMapping(ColumnName = "AuditeUserName", DbType = DbType.String)]
        public string AuditeUserName { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        [DataMapping(ColumnName = "StorageName", DbType = DbType.String)]
        public string StorageName { get; set; }

        /// <summary>
        /// 查询开始时间-辅助作用
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 查询结束时间-辅助作用
        /// </summary>
        public string EndTime { get; set; }
    }

	[TableAttribute(DbName = "GitWMS", Name = "InventoryOrder", PrimaryKeyName = "ID", IsInternal = false)]
	public partial class InventoryOrderEntity:BaseEntity
	{
		public InventoryOrderEntity()
		{
		}

		[DataMapping(ColumnName = "ID", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=true,AutoIncrement=true,IsMap=true)]
		public Int32 ID { get;  set; }

		public InventoryOrderEntity IncludeID (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ID"))
			{
				this.ColumnList.Add("ID");
			}
			return this;
		}

		[DataMapping(ColumnName = "SnNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string SnNum { get;  set; }

		public InventoryOrderEntity IncludeSnNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("SnNum"))
			{
				this.ColumnList.Add("SnNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "OrderNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string OrderNum { get;  set; }

		public InventoryOrderEntity IncludeOrderNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("OrderNum"))
			{
				this.ColumnList.Add("OrderNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "Type", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int32 Type { get;  set; }

		public InventoryOrderEntity IncludeType (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Type"))
			{
				this.ColumnList.Add("Type");
			}
			return this;
		}

		[DataMapping(ColumnName = "ProductType", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int32 ProductType { get;  set; }

		public InventoryOrderEntity IncludeProductType (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ProductType"))
			{
				this.ColumnList.Add("ProductType");
			}
			return this;
		}

		[DataMapping(ColumnName = "StorageNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string StorageNum { get;  set; }

		public InventoryOrderEntity IncludeStorageNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("StorageNum"))
			{
				this.ColumnList.Add("StorageNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "ContractOrder", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string ContractOrder { get;  set; }

		public InventoryOrderEntity IncludeContractOrder (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ContractOrder"))
			{
				this.ColumnList.Add("ContractOrder");
			}
			return this;
		}

		[DataMapping(ColumnName = "Status", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int32 Status { get;  set; }

		public InventoryOrderEntity IncludeStatus (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Status"))
			{
				this.ColumnList.Add("Status");
			}
			return this;
		}

		[DataMapping(ColumnName = "LocalQty", DbType = DbType.Double,Length=8,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public double LocalQty { get;  set; }

		public InventoryOrderEntity IncludeLocalQty (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("LocalQty"))
			{
				this.ColumnList.Add("LocalQty");
			}
			return this;
		}

		[DataMapping(ColumnName = "CheckQty", DbType = DbType.Double,Length=8,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public double CheckQty { get;  set; }

		public InventoryOrderEntity IncludeCheckQty (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CheckQty"))
			{
				this.ColumnList.Add("CheckQty");
			}
			return this;
		}

		[DataMapping(ColumnName = "IsDelete", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int32 IsDelete { get;  set; }

		public InventoryOrderEntity IncludeIsDelete (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("IsDelete"))
			{
				this.ColumnList.Add("IsDelete");
			}
			return this;
		}

		[DataMapping(ColumnName = "IsComplete", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int32 IsComplete { get;  set; }

		public InventoryOrderEntity IncludeIsComplete (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("IsComplete"))
			{
				this.ColumnList.Add("IsComplete");
			}
			return this;
		}

		[DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime,Length=8,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public DateTime CreateTime { get;  set; }

		public InventoryOrderEntity IncludeCreateTime (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CreateTime"))
			{
				this.ColumnList.Add("CreateTime");
			}
			return this;
		}

		[DataMapping(ColumnName = "CreateUser", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string CreateUser { get;  set; }

		public InventoryOrderEntity IncludeCreateUser (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CreateUser"))
			{
				this.ColumnList.Add("CreateUser");
			}
			return this;
		}

		[DataMapping(ColumnName = "AuditUser", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string AuditUser { get;  set; }

		public InventoryOrderEntity IncludeAuditUser (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("AuditUser"))
			{
				this.ColumnList.Add("AuditUser");
			}
			return this;
		}

		[DataMapping(ColumnName = "AuditeTime", DbType = DbType.DateTime,Length=8,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public DateTime AuditeTime { get;  set; }

		public InventoryOrderEntity IncludeAuditeTime (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("AuditeTime"))
			{
				this.ColumnList.Add("AuditeTime");
			}
			return this;
		}

		[DataMapping(ColumnName = "PrintUser", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string PrintUser { get;  set; }

		public InventoryOrderEntity IncludePrintUser (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("PrintUser"))
			{
				this.ColumnList.Add("PrintUser");
			}
			return this;
		}

		[DataMapping(ColumnName = "PrintTime", DbType = DbType.DateTime,Length=8,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public DateTime PrintTime { get;  set; }

		public InventoryOrderEntity IncludePrintTime (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("PrintTime"))
			{
				this.ColumnList.Add("PrintTime");
			}
			return this;
		}

		[DataMapping(ColumnName = "Reason", DbType = DbType.String,Length=800,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string Reason { get;  set; }

		public InventoryOrderEntity IncludeReason (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Reason"))
			{
				this.ColumnList.Add("Reason");
			}
			return this;
		}

		[DataMapping(ColumnName = "OperateType", DbType = DbType.Int32,Length=4,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int32 OperateType { get;  set; }

		public InventoryOrderEntity IncludeOperateType (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("OperateType"))
			{
				this.ColumnList.Add("OperateType");
			}
			return this;
		}

		[DataMapping(ColumnName = "EquipmentNum", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string EquipmentNum { get;  set; }

		public InventoryOrderEntity IncludeEquipmentNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("EquipmentNum"))
			{
				this.ColumnList.Add("EquipmentNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "EquipmentCode", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string EquipmentCode { get;  set; }

		public InventoryOrderEntity IncludeEquipmentCode (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("EquipmentCode"))
			{
				this.ColumnList.Add("EquipmentCode");
			}
			return this;
		}

		[DataMapping(ColumnName = "Remark", DbType = DbType.String,Length=800,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string Remark { get;  set; }

		public InventoryOrderEntity IncludeRemark (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Remark"))
			{
				this.ColumnList.Add("Remark");
			}
			return this;
		}

		[DataMapping(ColumnName = "CompanyID", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string CompanyID { get;  set; }

		public InventoryOrderEntity IncludeCompanyID (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CompanyID"))
			{
				this.ColumnList.Add("CompanyID");
			}
			return this;
		}

	}
}
