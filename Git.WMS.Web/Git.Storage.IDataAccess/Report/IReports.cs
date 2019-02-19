/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-04-22 16:50:02
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-04-22 16:50:02       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;
using Git.Storage.Entity.Report;

namespace Git.Storage.IDataAccess.Report
{
    public partial interface IReports : IDbHelper<ReportsEntity>
    {
        /// <summary>
        /// 根据存储过程名称查询元数据
        /// </summary>
        /// <param name="argProceName"></param>
        /// <returns></returns>
        List<ProceMetadata> GetMetadataList(string argProceName);

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        DataSet GetDataSource(ReportsEntity entity, List<ReportParamsEntity> list);
    }
}
