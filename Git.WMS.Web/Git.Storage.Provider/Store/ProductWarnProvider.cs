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
    public partial class ProductWarnProvider:DataFactory
    {
        public ProductWarnProvider(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 查询库存预警列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<V_StorageProductEntity> GetList(V_StorageProductEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(item => item.ID, EOrderBy.DESC);
            entity.And(item=>item.CompanyID==this.CompanyID);

            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(item=>item.StorageNum==entity.StorageNum);
            }

            if (entity.BarCode.IsNotEmpty())
            {
                entity.And("BarCode", ECondition.Like, "%" + entity.BarCode + "%");
            }
            if (entity.ProductName.IsNotEmpty())
            {
                entity.And("ProductName", ECondition.Like, "%" + entity.ProductName + "%");
            }

            ProductEntity Product = new ProductEntity();
            Product.Include(item => new { MinNum = item.MinNum, MaxNum = item.MaxNum });
            entity.Left<ProductEntity>(Product,new Params<string, string>() { Item1= "ProductNum", Item2="SnNum" });

            int rowCount = 0;
            List<V_StorageProductEntity> listResult = this.V_StorageProduct.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;

            return listResult;
        }
    }
}
