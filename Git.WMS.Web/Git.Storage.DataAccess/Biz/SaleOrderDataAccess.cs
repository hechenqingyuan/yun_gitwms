/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-06-01 23:07:17
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-06-01 23:07:17
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;
using Git.Framework.MsSql;
using Git.Storage.Entity.Biz;
using Git.Storage.IDataAccess.Biz;

namespace Git.Storage.DataAccess.Biz
{
	public partial class SaleOrderDataAccess : DbHelper<SaleOrderEntity>, ISaleOrder
	{
		public SaleOrderDataAccess()
		{
		}

	}
}
