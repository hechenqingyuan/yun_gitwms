using Git.Framework.MsSql;
using Git.Storage.Entity.Sys;
using Git.Storage.IDataAccess.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.DataAccess.Sys
{
    public partial class CarrierDataAccess : DbHelper<CarrierEntity>, ICarrier
    {
        public CarrierDataAccess()
        {
        }

    }
}
