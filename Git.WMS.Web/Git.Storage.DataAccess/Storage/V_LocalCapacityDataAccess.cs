using Git.Framework.MsSql;
using Git.Storage.Entity.Storage;
using Git.Storage.IDataAccess.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Storage
{
    public partial class V_LocalCapacityDataAccess : DbHelper<V_LocalCapacityEntity>, IV_LocalCapacity
    {
        public V_LocalCapacityDataAccess()
        {
        }

    }
}
