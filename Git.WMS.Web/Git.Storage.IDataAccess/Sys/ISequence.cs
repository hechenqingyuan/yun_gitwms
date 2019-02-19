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
using Git.Storage.Entity.Sys;

namespace Git.Storage.IDataAccess.Sys
{
	public partial interface ISequence : IDbHelper<SequenceEntity>
	{
        /// <summary>
        /// 查询系统所有的自定义表信息
        /// </summary>
        /// <returns></returns>
        DataTable GetTables();
	}
}
