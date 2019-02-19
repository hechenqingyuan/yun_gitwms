/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-03-03 22:07:01
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-03-03 22:07:01
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Sys
{
	[TableAttribute(DbName = "GitWMS", Name = "Resource", PrimaryKeyName = "ID", IsInternal = false)]
	public partial class ResourceEntity:BaseEntity
	{
		public ResourceEntity()
		{
		}

		[DataMapping(ColumnName = "ID", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=true,AutoIncrement=true,IsMap=true)]
		public Int32 ID { get;  set; }

		public ResourceEntity IncludeID (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ID"))
			{
				this.ColumnList.Add("ID");
			}
			return this;
		}

		[DataMapping(ColumnName = "ResNum", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string ResNum { get;  set; }

		public ResourceEntity IncludeResNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ResNum"))
			{
				this.ColumnList.Add("ResNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "ResName", DbType = DbType.String,Length=100,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string ResName { get;  set; }

		public ResourceEntity IncludeResName (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ResName"))
			{
				this.ColumnList.Add("ResName");
			}
			return this;
		}

		[DataMapping(ColumnName = "ParentNum", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string ParentNum { get;  set; }

		public ResourceEntity IncludeParentNum (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ParentNum"))
			{
				this.ColumnList.Add("ParentNum");
			}
			return this;
		}

		[DataMapping(ColumnName = "Depth", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int32 Depth { get;  set; }

		public ResourceEntity IncludeDepth (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Depth"))
			{
				this.ColumnList.Add("Depth");
			}
			return this;
		}

		[DataMapping(ColumnName = "ParentPath", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string ParentPath { get;  set; }

		public ResourceEntity IncludeParentPath (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ParentPath"))
			{
				this.ColumnList.Add("ParentPath");
			}
			return this;
		}

		[DataMapping(ColumnName = "ChildCount", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int32 ChildCount { get;  set; }

		public ResourceEntity IncludeChildCount (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ChildCount"))
			{
				this.ColumnList.Add("ChildCount");
			}
			return this;
		}

		[DataMapping(ColumnName = "Sort", DbType = DbType.Int32,Length=4,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int32 Sort { get;  set; }

		public ResourceEntity IncludeSort (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Sort"))
			{
				this.ColumnList.Add("Sort");
			}
			return this;
		}

		[DataMapping(ColumnName = "IsHide", DbType = DbType.Int16,Length=2,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int16 IsHide { get;  set; }

		public ResourceEntity IncludeIsHide (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("IsHide"))
			{
				this.ColumnList.Add("IsHide");
			}
			return this;
		}

		[DataMapping(ColumnName = "IsDelete", DbType = DbType.Int16,Length=2,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int16 IsDelete { get;  set; }

		public ResourceEntity IncludeIsDelete (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("IsDelete"))
			{
				this.ColumnList.Add("IsDelete");
			}
			return this;
		}

		[DataMapping(ColumnName = "Url", DbType = DbType.String,Length=200,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string Url { get;  set; }

		public ResourceEntity IncludeUrl (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Url"))
			{
				this.ColumnList.Add("Url");
			}
			return this;
		}

		[DataMapping(ColumnName = "CssName", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string CssName { get;  set; }

		public ResourceEntity IncludeCssName (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CssName"))
			{
				this.ColumnList.Add("CssName");
			}
			return this;
		}

		[DataMapping(ColumnName = "CreateTime", DbType = DbType.DateTime,Length=8,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public DateTime CreateTime { get;  set; }

		public ResourceEntity IncludeCreateTime (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CreateTime"))
			{
				this.ColumnList.Add("CreateTime");
			}
			return this;
		}

		[DataMapping(ColumnName = "Depart", DbType = DbType.Int16,Length=2,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int16 Depart { get;  set; }

		public ResourceEntity IncludeDepart (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Depart"))
			{
				this.ColumnList.Add("Depart");
			}
			return this;
		}

		[DataMapping(ColumnName = "ResType", DbType = DbType.Int16,Length=2,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public Int16 ResType { get;  set; }

		public ResourceEntity IncludeResType (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("ResType"))
			{
				this.ColumnList.Add("ResType");
			}
			return this;
		}

		[DataMapping(ColumnName = "UpdateTime", DbType = DbType.DateTime,Length=8,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public DateTime UpdateTime { get;  set; }

		public ResourceEntity IncludeUpdateTime (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("UpdateTime"))
			{
				this.ColumnList.Add("UpdateTime");
			}
			return this;
		}

		[DataMapping(ColumnName = "CreateUser", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string CreateUser { get;  set; }

		public ResourceEntity IncludeCreateUser (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CreateUser"))
			{
				this.ColumnList.Add("CreateUser");
			}
			return this;
		}

		[DataMapping(ColumnName = "UpdateUser", DbType = DbType.String,Length=100,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string UpdateUser { get;  set; }

		public ResourceEntity IncludeUpdateUser (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("UpdateUser"))
			{
				this.ColumnList.Add("UpdateUser");
			}
			return this;
		}

		[DataMapping(ColumnName = "CreateIp", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string CreateIp { get;  set; }

		public ResourceEntity IncludeCreateIp (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CreateIp"))
			{
				this.ColumnList.Add("CreateIp");
			}
			return this;
		}

		[DataMapping(ColumnName = "UpdateIp", DbType = DbType.String,Length=50,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string UpdateIp { get;  set; }

		public ResourceEntity IncludeUpdateIp (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("UpdateIp"))
			{
				this.ColumnList.Add("UpdateIp");
			}
			return this;
		}

		[DataMapping(ColumnName = "Remark", DbType = DbType.String,Length=400,CanNull=true,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string Remark { get;  set; }

		public ResourceEntity IncludeRemark (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("Remark"))
			{
				this.ColumnList.Add("Remark");
			}
			return this;
		}

		[DataMapping(ColumnName = "CompanyID", DbType = DbType.String,Length=50,CanNull=false,DefaultValue=null,PrimaryKey=false,AutoIncrement=false,IsMap=true)]
		public string CompanyID { get;  set; }

		public ResourceEntity IncludeCompanyID (bool flag) 
		{
			if (flag && !this.ColumnList.Contains("CompanyID"))
			{
				this.ColumnList.Add("CompanyID");
			}
			return this;
		}

	}
}
