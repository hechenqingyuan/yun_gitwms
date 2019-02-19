/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/5/12 22:18:49
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/5/12 22:18:49       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using Git.Framework.MsSql;
using Git.Storage.Entity.Sku;
using Git.Storage.IDataAccess.Sku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Sku
{
    public partial class ProductSkuDataAccess : DbHelper<ProductSkuEntity>, IProductSku
    {
        public ProductSkuDataAccess()
        {
        }

    }
}
