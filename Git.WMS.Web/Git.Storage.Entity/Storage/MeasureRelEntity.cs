/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-03-03 22:19:20
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-03-03 22:19:20
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Storage
{
    public partial class MeasureRelEntity
    {
        /// <summary>
        /// 元单位名称
        /// </summary>
        [DataMapping(ColumnName = "SourceName", DbType = DbType.String)]
        public string SourceName { get; set; }

        /// <summary>
        /// 目标单位名称
        /// </summary>
        [DataMapping(ColumnName = "TargetName", DbType = DbType.String)]
        public string TargetName { get; set; }
    }

	[TableAttribute(DbName = "GitWMS", Name = "MeasureRel", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class MeasureRelEntity : BaseEntity
    {
        public MeasureRelEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public MeasureRelEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SN", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SN { get; set; }

        public MeasureRelEntity IncludeSN(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SN"))
            {
                this.ColumnList.Add("SN");
            }
            return this;
        }

        [DataMapping(ColumnName = "ProductNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ProductNum { get; set; }

        public MeasureRelEntity IncludeProductNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ProductNum"))
            {
                this.ColumnList.Add("ProductNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "MeasureSource", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string MeasureSource { get; set; }

        public MeasureRelEntity IncludeMeasureSource(bool flag)
        {
            if (flag && !this.ColumnList.Contains("MeasureSource"))
            {
                this.ColumnList.Add("MeasureSource");
            }
            return this;
        }

        [DataMapping(ColumnName = "MeasureTarget", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string MeasureTarget { get; set; }

        public MeasureRelEntity IncludeMeasureTarget(bool flag)
        {
            if (flag && !this.ColumnList.Contains("MeasureTarget"))
            {
                this.ColumnList.Add("MeasureTarget");
            }
            return this;
        }

        [DataMapping(ColumnName = "Rate", DbType = DbType.Double, Length = 8, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public double Rate { get; set; }

        public MeasureRelEntity IncludeRate(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Rate"))
            {
                this.ColumnList.Add("Rate");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public MeasureRelEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
