using Git.Framework.DataTypes;
using Git.Storage.Entity.Storage;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider.Store
{
    public partial class StoreCapacityProvider:DataFactory
    {
        public StoreCapacityProvider(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 仓库库存容量
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<V_LocalCapacityEntity> GetList(V_LocalCapacityEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(item => item.ID, Framework.ORM.EOrderBy.DESC);
            entity.And(item=>item.CompanyID==this.CompanyID);

            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(item=>item.StorageNum==entity.StorageNum);
            }
            if (entity.LocalType > 0)
            {
                entity.And(item=>item.LocalType==entity.LocalType);
            }
            if (entity.LocalName.IsNotEmpty())
            {
                entity.And("LocalName",ECondition.Like,"%"+entity.LocalName+"%");
            }

            int rowCount = 0;
            List<V_LocalCapacityEntity> listResult = this.V_LocalCapacity.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;

            return listResult;
        }
    }
}
