/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-04-22 16:51:59
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-04-22 16:51:59       情缘
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Framework.MsSql.DataAccess;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Report;
using Git.Storage.IDataAccess.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Report
{
    public partial class ReportsDataAccess : DbHelper<ReportsEntity>, IReports
    {
        public ReportsDataAccess()
        {
        }

        /// <summary>
        /// 根据存储过程名称查询元数据
        /// </summary>
        /// <param name="argProceName"></param>
        /// <returns></returns>
        public List<ProceMetadata> GetMetadataList(string argProceName)
        {
            DataCommand command = DataCommandManager.GetDataCommand("Common.GetProceParam");
            command.SetParameterValue("@SPECIFIC_NAME", argProceName);
            List<ProceMetadata> list = command.ExecuteEntityList<ProceMetadata>();

            return list;
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataSet GetDataSource(ReportsEntity entity, List<ReportParamsEntity> list)
        {
            DataCommand command = null;
            DataSet ds = null;
            if (entity.DsType == (int)EDataSourceType.SQL)
            {
                command = DataCommandManager.CreateCustomDataCommand("GitWMS", CommandType.Text, entity.DataSource);
            }
            else
            {
                command = DataCommandManager.CreateCustomDataCommand("GitWMS", CommandType.StoredProcedure, entity.DataSource);
            }
            if (list != null)
            {
                foreach (ReportParamsEntity item in list)
                {
                    DbType dbType = DbType.String;
                    if (item.ParamType == "datetime" || item.ParamType == "date")
                    {
                        dbType = DbType.DateTime;
                        item.DefaultValue = string.IsNullOrEmpty(item.DefaultValue) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : item.DefaultValue;
                    }
                    else if (item.ParamType == "int")
                    {
                        dbType = DbType.Int32;
                        item.DefaultValue = string.IsNullOrEmpty(item.DefaultValue) ? "0" : item.DefaultValue;
                    }
                    else
                    {
                        item.DefaultValue = string.IsNullOrEmpty(item.DefaultValue) ? "" : item.DefaultValue;
                    }
                    command.AddParameterValue(item.ParamName, item.DefaultValue, dbType);
                }
            }
            ds = command.ExecuteDataSet();

            return ds;
        }
    }
}
