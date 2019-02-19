/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-03-03 22:07:15
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-03-03 22:07:15
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;
using Git.Framework.MsSql;
using Git.Storage.Entity.Sys;
using Git.Storage.IDataAccess.Sys;
using Git.Framework.MsSql.DataAccess;

namespace Git.Storage.DataAccess.Sys
{
	public partial class SequenceDataAccess : DbHelper<SequenceEntity>, ISequence
	{
		public SequenceDataAccess()
		{
		}

        /// <summary>
        /// 查询系统所有的自定义表信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetTables()
        {
            string sql = "select * from sysobjects where xtype='U'";
            DataCommand command = DataCommandManager.CreateCustomDataCommand("GitWMS", CommandType.Text, sql);
            DataSet ds = command.ExecuteDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
	}
}
