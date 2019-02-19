/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-04-22 16:48:16
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-04-22 16:48:16       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;

namespace Git.Storage.Entity.Report
{
    [TableAttribute(DbName = "GitWMS", Name = "ReportParams", PrimaryKeyName = "ID", IsInternal = false)]
    public partial class ReportParamsEntity : BaseEntity
    {
        public ReportParamsEntity()
        {
        }

        [DataMapping(ColumnName = "ID", DbType = DbType.Int32, Length = 4, CanNull = false, DefaultValue = null, PrimaryKey = true, AutoIncrement = true, IsMap = true)]
        public Int32 ID { get; set; }

        public ReportParamsEntity IncludeID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ID"))
            {
                this.ColumnList.Add("ID");
            }
            return this;
        }

        [DataMapping(ColumnName = "SnNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string SnNum { get; set; }

        public ReportParamsEntity IncludeSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("SnNum"))
            {
                this.ColumnList.Add("SnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ParamNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ParamNum { get; set; }

        public ReportParamsEntity IncludeParamNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ParamNum"))
            {
                this.ColumnList.Add("ParamNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ReprtSnNum", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ReprtSnNum { get; set; }

        public ReportParamsEntity IncludeReprtSnNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ReprtSnNum"))
            {
                this.ColumnList.Add("ReprtSnNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "ReportNum", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ReportNum { get; set; }

        public ReportParamsEntity IncludeReportNum(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ReportNum"))
            {
                this.ColumnList.Add("ReportNum");
            }
            return this;
        }

        [DataMapping(ColumnName = "InputNo", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string InputNo { get; set; }

        public ReportParamsEntity IncludeInputNo(bool flag)
        {
            if (flag && !this.ColumnList.Contains("InputNo"))
            {
                this.ColumnList.Add("InputNo");
            }
            return this;
        }

        [DataMapping(ColumnName = "ParamName", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ParamName { get; set; }

        public ReportParamsEntity IncludeParamName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ParamName"))
            {
                this.ColumnList.Add("ParamName");
            }
            return this;
        }

        [DataMapping(ColumnName = "ShowName", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ShowName { get; set; }

        public ReportParamsEntity IncludeShowName(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ShowName"))
            {
                this.ColumnList.Add("ShowName");
            }
            return this;
        }

        [DataMapping(ColumnName = "ParamType", DbType = DbType.String, Length = 50, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ParamType { get; set; }

        public ReportParamsEntity IncludeParamType(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ParamType"))
            {
                this.ColumnList.Add("ParamType");
            }
            return this;
        }

        [DataMapping(ColumnName = "ParamData", DbType = DbType.String, Length = 1000, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ParamData { get; set; }

        public ReportParamsEntity IncludeParamData(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ParamData"))
            {
                this.ColumnList.Add("ParamData");
            }
            return this;
        }

        [DataMapping(ColumnName = "DefaultValue", DbType = DbType.String, Length = 100, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string DefaultValue { get; set; }

        public ReportParamsEntity IncludeDefaultValue(bool flag)
        {
            if (flag && !this.ColumnList.Contains("DefaultValue"))
            {
                this.ColumnList.Add("DefaultValue");
            }
            return this;
        }

        [DataMapping(ColumnName = "ParamElement", DbType = DbType.String, Length = 200, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string ParamElement { get; set; }

        public ReportParamsEntity IncludeParamElement(bool flag)
        {
            if (flag && !this.ColumnList.Contains("ParamElement"))
            {
                this.ColumnList.Add("ParamElement");
            }
            return this;
        }

        [DataMapping(ColumnName = "Remark", DbType = DbType.String, Length = 400, CanNull = true, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string Remark { get; set; }

        public ReportParamsEntity IncludeRemark(bool flag)
        {
            if (flag && !this.ColumnList.Contains("Remark"))
            {
                this.ColumnList.Add("Remark");
            }
            return this;
        }

        [DataMapping(ColumnName = "CompanyID", DbType = DbType.String, Length = 50, CanNull = false, DefaultValue = null, PrimaryKey = false, AutoIncrement = false, IsMap = true)]
        public string CompanyID { get; set; }

        public ReportParamsEntity IncludeCompanyID(bool flag)
        {
            if (flag && !this.ColumnList.Contains("CompanyID"))
            {
                this.ColumnList.Add("CompanyID");
            }
            return this;
        }

    }
}
