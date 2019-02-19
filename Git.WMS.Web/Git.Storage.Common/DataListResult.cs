/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-04 10:17:06
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-04 10:17:06       情缘
*********************************************************************************/

using Git.Framework.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Common
{
    public partial class DataListResult<T> : DataResult
    {
        public List<T> Result { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
