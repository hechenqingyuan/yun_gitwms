using Git.Framework.ORM;
using Git.Storage.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.IDataAccess.Sys
{
    public partial interface ICarrier : IDbHelper<CarrierEntity>
    {
    }
}
