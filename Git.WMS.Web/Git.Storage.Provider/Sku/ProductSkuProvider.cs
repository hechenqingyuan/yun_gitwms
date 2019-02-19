/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/5/13 13:05:32
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/5/13 13:05:32       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Entity.Storage;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Storage.Entity.Sku;
using Git.Storage.Provider.Sys;
using Git.Storage.Provider.Base;
using System.Transactions;
using Git.Storage.Common.Enum;

namespace Git.Storage.Provider.Sku
{
    public partial class ProductSkuProvider:DataFactory
    {
        public ProductSkuProvider(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 新增带SKU的产品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(ProductEntity entity,List<ProductSkuEntity> listSku)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                entity.IncludeAll();
                entity.InPrice = entity.AvgPrice;
                entity.OutPrice = entity.AvgPrice;
                entity.SnNum = ConvertHelper.NewGuid();
                entity.BarCode = entity.BarCode.IsEmpty() ? new TNumProvider(CompanyID).GetSwiftNum(typeof(ProductEntity), 6) : entity.BarCode;

                //处理类别
                if (!entity.CateNum.IsEmpty())
                {
                    ProductCategoryEntity category = new ProductCategoryProvider(this.CompanyID).GetSingle(entity.CateNum);
                    entity.CateName = category != null ? category.CateName : "";
                }
                if (!entity.UnitNum.IsEmpty())
                {
                    MeasureEntity meaure = new MeasureProvider(this.CompanyID).GetMeasure(entity.UnitNum);
                    entity.UnitName = meaure != null ? meaure.MeasureName : "";
                }
                int line = this.Product.Add(entity);

                if (!listSku.IsNullOrEmpty())
                {
                    foreach (ProductSkuEntity item in listSku)
                    {
                        item.SnNum = ConvertHelper.NewGuid();
                        item.ProductNum = entity.SnNum;
                        item.BarCode = entity.BarCode + "-" + new TNumProvider(CompanyID).GetSwiftNum(entity.SnNum, 3);
                        item.IsDelete = (int)EIsDelete.NotDelete;
                        item.CreateTime = DateTime.Now;
                        item.CompanyID = this.CompanyID;
                        item.IncludeAll();
                        line += this.ProductSku.Add(item);
                    }
                }

                ts.Complete();
                return line;
            }
        }

        /// <summary>
        /// 编辑带有SKU的产品
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listSku"></param>
        /// <returns></returns>
        public int Edit(ProductEntity entity, List<ProductSkuEntity> listSku)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (!entity.CateNum.IsEmpty())
                {
                    ProductCategoryEntity category = new ProductCategoryProvider(this.CompanyID).GetSingle(entity.CateNum);
                    entity.CateName = category != null ? category.CateName : "";
                }
                if (!entity.UnitNum.IsEmpty())
                {
                    MeasureEntity meaure = new MeasureProvider(this.CompanyID).GetMeasure(entity.UnitNum);
                    entity.UnitName = meaure != null ? meaure.MeasureName : "";
                }
                entity.Include(a => new { a.ProductName, a.BarCode, a.FactoryNum, a.InCode, a.UnitNum, a.UnitName, a.CateNum, a.CateName, a.StorageNum, a.DefaultLocal, a.CusNum, a.CusName, a.SupNum, a.SupName, a.Description, a.Display, a.Remark });
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID);

                int line = this.Product.Update(entity);

                if (!listSku.IsNullOrEmpty())
                {
                    ProductSkuEntity SkuEntity = new ProductSkuEntity();
                    SkuEntity.IncludeAll();
                    SkuEntity.Where(a => a.ProductNum == entity.SnNum)
                        .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                        .And(a => a.CompanyID == this.CompanyID)
                        ;
                    List<ProductSkuEntity> listSource = this.ProductSku.GetList(SkuEntity);
                    listSource = listSource.IsNull() ? new List<ProductSkuEntity>() : listSource;

                    listSku = listSku.IsNull() ? new List<ProductSkuEntity>() : listSku;

                    foreach (ProductSkuEntity item in listSku)
                    {
                        item.SnNum = item.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : item.SnNum;
                        if (listSource.Exists(a => a.SnNum == item.SnNum))
                        {
                            item.Include(b => new { b.Size, b.Color, b.NetWeight, b.GrossWeight, b.InPrice, b.AvgPrice, b.OutPrice });
                            item.Where(b => b.SnNum == item.SnNum).And(b => b.CompanyID == this.CompanyID);
                            line += this.ProductSku.Update(item);
                        }
                        else
                        {
                            item.IsDelete = (int)EIsDelete.NotDelete;
                            item.CreateTime = DateTime.Now;
                            item.CompanyID = this.CompanyID;
                            item.IncludeAll();
                            line += this.ProductSku.Add(item);
                        }
                    }

                    foreach (ProductSkuEntity item in listSource)
                    {
                        if (!listSku.Exists(a => a.SnNum == item.SnNum))
                        {
                            item.IsDelete = (int)EIsDelete.Deleted;
                            item.IncludeIsDelete(true);
                            item.Where(b => b.SnNum == item.SnNum).And(b => b.CompanyID == this.CompanyID);
                            line += this.ProductSku.Update(item);
                        }
                    }
                }
                ts.Complete();
                return line;
            }
        }

        /// <summary>
        /// 编辑产品信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int EditProduct(ProductEntity entity)
        {
            if (!entity.CateNum.IsEmpty())
            {
                ProductCategoryEntity category = new ProductCategoryProvider(this.CompanyID).GetSingle(entity.CateNum);
                entity.CateName = category != null ? category.CateName : "";
            }
            if (!entity.UnitNum.IsEmpty())
            {
                MeasureEntity meaure = new MeasureProvider(this.CompanyID).GetMeasure(entity.UnitNum);
                entity.UnitName = meaure != null ? meaure.MeasureName : "";
            }
            entity.Include(a => new { a.ProductName, a.BarCode, a.FactoryNum, a.InCode, a.UnitNum, a.UnitName, a.CateNum, a.CateName, a.StorageNum, a.DefaultLocal, a.CusNum, a.CusName, a.SupNum, a.SupName, a.Description, a.Display, a.Remark });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID);

            int line = this.Product.Update(entity);

            return line;
        }

        /// <summary>
        /// 编辑产品SKU
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int EditSku(ProductSkuEntity entity)
        {
            entity.Include(b => new { b.Size, b.Color, b.NetWeight, b.GrossWeight, b.InPrice, b.AvgPrice, b.OutPrice });
            entity.Where(b => b.SnNum == entity.SnNum).And(b => b.CompanyID == this.CompanyID);
            int line = this.ProductSku.Update(entity);

            return line;
        }

        /// <summary>
        /// 根据产品唯一编号删除产品
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public int DeleteProduct(string SnNum)
        {
            ProductEntity entity = new ProductEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            int line = this.Product.Update(entity);

            ProductSkuEntity skuEntity = new ProductSkuEntity();
            skuEntity.IsDelete = (int)EIsDelete.Deleted;
            skuEntity.IncludeIsDelete(true);
            skuEntity.Where(item => item.ProductNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            line += this.ProductSku.Update(skuEntity);

            return line;
        }

        /// <summary>
        /// 根据SKU唯一编号删除SKU
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public int DeleteSku(string SnNum)
        {
            ProductSkuEntity skuEntity = new ProductSkuEntity();
            skuEntity.IsDelete = (int)EIsDelete.Deleted;
            skuEntity.IncludeIsDelete(true);
            skuEntity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            int line = this.ProductSku.Update(skuEntity);

            return line;
        }

        /// <summary>
        /// 根据产品的唯一编号查询SKU
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public ProductSkuEntity GetSku(string SnNum)
        {
            ProductSkuEntity skuEntity = new ProductSkuEntity();
            skuEntity.IncludeAll();
            skuEntity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            skuEntity = this.ProductSku.GetSingle(skuEntity);

            return skuEntity;
        }

        /// <summary>
        /// 根据产品的唯一编号查询SKU
        /// </summary>
        /// <param name="ProductNum"></param>
        /// <returns></returns>
        public List<ProductSkuEntity> GetSkuList(string ProductNum)
        {
            ProductSkuEntity skuEntity = new ProductSkuEntity();
            skuEntity.IncludeAll();
            skuEntity.Where(item => item.ProductNum == ProductNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            List<ProductSkuEntity> listResult = this.ProductSku.GetList(skuEntity);

            return listResult;
        }

        /// <summary>
        /// 批量删除产品SKU
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int DeleteSku(List<string> list)
        {
            string[] items = null;
            if (!list.IsNullOrEmpty())
            {
                items = list.ToArray();
            }
            if (items.IsNullOrEmpty())
            {
                items = new string[] { "" };
            }
            ProductSkuEntity skuEntity = new ProductSkuEntity();
            skuEntity.IsDelete = (int)EIsDelete.Deleted;
            skuEntity.IncludeIsDelete(true);
            skuEntity.Where("SnNum", ECondition.In, items);
            skuEntity.And(item => item.CompanyID == this.CompanyID);

            int line = this.ProductSku.Update(skuEntity);

            return line;
        }

        /// <summary>
        /// 批量删除产品
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int DeleteProduct(List<string> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                foreach (string item in list)
                {
                    line+=DeleteProduct(item);
                }

                ts.Complete();
                return line;
            }
        }

        /// <summary>
        /// 根据产品SKU唯一编号查询产品信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public ProductEntity GetProductSku(string SnNum)
        {
            V_SkuEntity entity = new V_SkuEntity();
            entity.IncludeAll();
            entity.Where(item => item.SnNum==SnNum)
                .And(item => item.IsDelete == (int)EIsDelete.NotDelete)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            ProductEntity result = this.V_Sku.GetSingle<ProductEntity>(entity);

            return result;
        }

        /// <summary>
        /// 根据产品的SKU的BarCode查询产品信息
        /// </summary>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public ProductEntity GetSkuBarCode(string BarCode)
        {
            V_SkuEntity entity = new V_SkuEntity();
            entity.IncludeAll();
            entity.Where(item => item.BarCode == BarCode)
                .And(item => item.IsDelete == (int)EIsDelete.NotDelete)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            ProductEntity result = this.V_Sku.GetSingle<ProductEntity>(entity);

            return result;
        }

        /// <summary>
        /// 查询SKU产品分页信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<ProductEntity> GetSkuList(ProductEntity entity, ref PageInfo pageInfo)
        {
            V_SkuEntity product = new V_SkuEntity();
            product.IncludeAll();
            product.OrderBy(item => item.ID, EOrderBy.DESC);
            product.Where(item=>item.CompanyID==this.CompanyID)
                .And(item=>item.IsDelete==(int)EIsDelete.NotDelete)
                ;

            if (entity.BarCode.IsNotEmpty())
            {
                product.And("BarCode", ECondition.In, "%" + entity.BarCode + "%");
            }
            if (entity.FactoryNum.IsNotEmpty())
            {
                product.And("FactoryNum", ECondition.In, "%" + entity.FactoryNum + "%");
            }
            if (entity.InCode.IsNotEmpty())
            {
                product.And("InCode", ECondition.In, "%" + entity.InCode + "%");
            }
            if (entity.ProductName.IsNotEmpty())
            {
                product.And("ProductName", ECondition.In, "%" + entity.ProductName + "%");
            }
            if (entity.CateNum.IsNotEmpty())
            {
                product.And(item=>item.CateNum==entity.CateNum);
            }
            if (entity.UnitNum.IsNotEmpty())
            {
                product.And(item=>item.UnitNum==entity.UnitNum);
            }
            if (entity.Color.IsNotEmpty())
            {
                product.And("Color", ECondition.In, "%" + entity.Color + "%");
            }
            if (entity.Size.IsNotEmpty())
            {
                product.And("Size", ECondition.In, "%" + entity.Size + "%");
            }
            if (entity.CusNum.IsNotEmpty())
            {
                product.And(item => item.CusNum == entity.CusNum);
            }
            if (entity.CusName.IsNotEmpty())
            {
                product.And("CusName", ECondition.In, "%" + entity.CusName + "%");
            }
            if (entity.SupNum.IsNotEmpty())
            {
                product.And(item => item.SupNum == entity.SupNum);
            }
            if (entity.SupName.IsNotEmpty())
            {
                product.And("SupName", ECondition.In, "%" + entity.SupName + "%");
            }
            int rowCount = 0;
            List<ProductEntity> listResult = this.V_Sku.GetList<ProductEntity>(product, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;

            return listResult;
        }
    }
}
