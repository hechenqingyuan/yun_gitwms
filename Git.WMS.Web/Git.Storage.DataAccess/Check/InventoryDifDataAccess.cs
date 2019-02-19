/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 代码工具自动生成
 * Create Date: 2016-06-13 14:12:01
 * Blog: http://www.cnblogs.com/qingyuan/ 
 * Copyright:  
 * Description: Git.Framework
 * 
 * Revision History:
 * Date         Author               Description
 * 2016-06-13 14:12:01
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Git.Framework.ORM;
using Git.Framework.MsSql;
using Git.Storage.Entity.Check;
using Git.Storage.IDataAccess.Check;

namespace Git.Storage.DataAccess.Check
{
	public partial class InventoryDifDataAccess : DbHelper<InventoryDifEntity>, IInventoryDif
	{
		public InventoryDifDataAccess()
		{
		}

	}
}
