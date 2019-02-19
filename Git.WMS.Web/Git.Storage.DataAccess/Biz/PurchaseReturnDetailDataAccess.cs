using Git.Framework.MsSql;
using Git.Storage.Entity.Biz;
using Git.Storage.IDataAccess.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Biz
{
    public partial class PurchaseReturnDetailDataAccess : DbHelper<PurchaseReturnDetailEntity>, IPurchaseReturnDetail
    {
        public PurchaseReturnDetailDataAccess()
        {
        }

    }
}
