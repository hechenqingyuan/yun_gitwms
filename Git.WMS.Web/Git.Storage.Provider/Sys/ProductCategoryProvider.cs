/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-27 9:16:31
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-27 9:16:31       情缘
*********************************************************************************/

using Git.Framework.Cache;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Framework.ORM;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Git.Storage.Provider.Sys
{
    public partial class ProductCategoryProvider : DataFactory
    {
        public ProductCategoryProvider(string CompanyID) 
        {
            this.CompanyID = CompanyID;
        }

        /// <summary>
        /// 查询根节点
        /// </summary>
        /// <returns></returns>
        private ProductCategoryEntity GetRoot()
        {
            string Key = string.Format(CacheKey.JOOSHOW_PRODUCTCATEGORY_CACHE, this.CompanyID);
            List<ProductCategoryEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                ProductCategoryEntity entity = new ProductCategoryEntity();
                entity.SnNum = ConvertHelper.NewGuid();
                entity.CateNum = new TNumProvider(this.CompanyID).GetSwiftNum(typeof(ProductCategoryEntity), 5);
                entity.IsDelete = (int)EIsDelete.NotDelete;
                entity.CreateTime = DateTime.Now;
                entity.CateName = "Root";
                entity.ParentNum = "";
                entity.CompanyID = this.CompanyID;
                entity.Left = 1;
                entity.Right = 2;
                entity.IncludeAll();
                int line = this.ProductCategory.Add(entity);
                if (line > 0)
                {
                    CacheHelper.Remove(Key);
                }
            }
            listSource = GetList();

            ProductCategoryEntity result = listSource.First(item => item.ParentNum.IsEmpty());

            return result;
        }

        /// <summary>
        /// 添加产品类别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(ProductCategoryEntity entity)
        {
            string Key = string.Format(CacheKey.JOOSHOW_PRODUCTCATEGORY_CACHE,this.CompanyID);
            List<ProductCategoryEntity> listSource = GetList();
            listSource = listSource.IsNull() ? new List<ProductCategoryEntity>() : listSource;
            if (entity.ParentNum.IsEmpty())
            {
                ProductCategoryEntity parent = GetRoot();
                entity.ParentNum = GetRoot().SnNum;
            }
            listSource = GetList();
            using (TransactionScope ts = new TransactionScope())
            {
                //查询新增节点的上一个节点
                ProductCategoryEntity parent = listSource.FirstOrDefault(item => item.SnNum == entity.ParentNum);
                int rightValue = parent.Right;

                List<ProductCategoryEntity> listNodes = listSource.Where(item => item.Right >= rightValue).ToList();
                if (!listNodes.IsNullOrEmpty())
                {
                    foreach (ProductCategoryEntity item in listNodes)
                    {
                        item.Right += 2;
                        item.IncludeRight(true);
                        item.Where(b => b.SnNum == item.SnNum).And(b => item.CompanyID == this.CompanyID);
                        this.ProductCategory.Update(item);
                    }
                }

                listNodes = listSource.Where(item => item.Left >= rightValue).ToList();
                if (!listNodes.IsNullOrEmpty())
                {
                    foreach (ProductCategoryEntity item in listNodes)
                    {
                        item.Left += 2;
                        item.IncludeLeft(true);
                        item.Where(b => b.SnNum == item.SnNum).And(b => item.CompanyID == this.CompanyID);
                        this.ProductCategory.Update(item);
                    }
                }

                entity.SnNum = entity.SnNum.IsEmpty()? ConvertHelper.NewGuid():entity.SnNum;
                entity.CateNum = entity.CateNum.IsEmpty() ? new TNumProvider(CompanyID).GetSwiftNum(typeof(ProductCategoryEntity), 5) : entity.CateNum;

                entity.CompanyID = this.CompanyID;
                entity.IsDelete = (int)EIsDelete.NotDelete;
                entity.CreateTime = DateTime.Now;
                entity.Left = rightValue;
                entity.Right = rightValue + 1;
                entity.IncludeAll();

                int line = this.ProductCategory.Add(entity);
                if (line > 0)
                {
                    CacheHelper.Remove(Key);
                }
                ts.Complete();
                return line;
            }
        }

        /// <summary>
        /// 根据产品类别删除删除
        /// </summary>
        /// <param name="cateNum"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<string> list)
        {
            string Key = string.Format(CacheKey.JOOSHOW_PRODUCTCATEGORY_CACHE, this.CompanyID);
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;

                List<ProductCategoryEntity> listSource = GetList();
                listSource = listSource.IsNull() ? new List<ProductCategoryEntity>() : listSource;

                foreach (string item in list)
                {
                    ProductCategoryEntity parent = listSource.FirstOrDefault(a => a.SnNum == item);
                    int rightValue = parent.Right;
                    int leftValue = parent.Left;

                    ProductCategoryEntity delCategory = new ProductCategoryEntity();
                    delCategory.IsDelete = (int)EIsDelete.Deleted;
                    delCategory.IncludeIsDelete(true);
                    delCategory.Where(a => a.Left >= leftValue)
                        .And(a => a.Right <= rightValue)
                        .And(a => a.CompanyID == this.CompanyID)
                        ;
                    line+=this.ProductCategory.Update(delCategory);

                    List<ProductCategoryEntity> listNodes = listSource.Where(a => a.Left > leftValue).ToList();
                    if (!listNodes.IsNullOrEmpty())
                    {
                        foreach (ProductCategoryEntity cate in listNodes)
                        {
                            cate.Left = cate.Left - (rightValue - leftValue + 1);
                            cate.IncludeLeft(true);
                            cate.Where(a => a.SnNum == cate.SnNum).And(a => a.CompanyID == this.CompanyID);
                            line += this.ProductCategory.Update(cate);
                        }
                    }

                    listNodes = listSource.Where(a => a.Right > rightValue).ToList();
                    if (!listNodes.IsNullOrEmpty())
                    {
                        foreach (ProductCategoryEntity cate in listNodes)
                        {
                            cate.Right = cate.Right - (rightValue - leftValue + 1);
                            cate.IncludeRight(true);
                            cate.Where(a => a.SnNum == cate.SnNum).And(a => a.CompanyID == this.CompanyID);
                            line += this.ProductCategory.Update(cate);
                        }
                    }
                }
                if (line > 0)
                {
                    CacheHelper.Remove(Key);
                }
                ts.Complete();
                return line;
            }
        }

        /// <summary>
        /// 修改产品类别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(ProductCategoryEntity entity)
        {
            string Key = string.Format(CacheKey.JOOSHOW_PRODUCTCATEGORY_CACHE, this.CompanyID);
            entity.IncludeCateName(true).IncludeRemark(true)
                .Where(a => a.SnNum == entity.SnNum)
                ;
            int line = this.ProductCategory.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 根据产品类别编号查询
        /// </summary>
        /// <param name="cateNum"></param>
        /// <returns></returns>
        public ProductCategoryEntity GetSingle(string SnNum)
        {
            List<ProductCategoryEntity> list = GetList();
            if (!list.IsNullOrEmpty())
            {
                return list.FirstOrDefault(a => a.SnNum == SnNum);
            }
            return null;
        }

        /// <summary>
        /// 查询所有的产品类别
        /// </summary>
        /// <returns></returns>
        public List<ProductCategoryEntity> GetList()
        {
            string Key = string.Format(CacheKey.JOOSHOW_PRODUCTCATEGORY_CACHE, this.CompanyID);

            List<ProductCategoryEntity> list = CacheHelper.Get(Key) as List<ProductCategoryEntity>;
            if (!list.IsNullOrEmpty())
            {
                return list;
            }
            ProductCategoryEntity entity = new ProductCategoryEntity();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID);
            list = this.ProductCategory.GetList(entity);
            if (!list.IsNullOrEmpty())
            {
                foreach (ProductCategoryEntity item in list)
                {
                    int depth = list.Where(a => a.Left <= item.Left && a.Right >= item.Right).Count();
                    item.Depth = depth;

                    if (item.ParentNum.IsNotEmpty())
                    {
                        ProductCategoryEntity parent = list.FirstOrDefault(a => a.SnNum == item.ParentNum);
                        item.ParentName = item != null && parent.CateName!="Root" ? parent.CateName : string.Empty;
                    }
                }
                CacheHelper.Insert(Key, list);
            }
            return list;
        }

        /// <summary>
        /// 查询商品种类分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<ProductCategoryEntity> GetList(ProductCategoryEntity entity, ref PageInfo pageInfo)
        {
            List<ProductCategoryEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return listSource;
            }
            
            List<ProductCategoryEntity> listResult = listSource.Where(item=>item.IsDelete==(int)EIsDelete.NotDelete).ToList();

            if(entity.CateNum.IsNotEmpty())
            {
                listResult = listResult.Where(item => item.CateNum.Contains(entity.CateNum)).ToList();
            }
            if (entity.CateName.IsNotEmpty())
            {
                listResult = listResult.Where(item => item.CateName.Contains(entity.CateName)).ToList();
            }
            if (entity.ParentNum.IsNotEmpty())
            {
                listResult = listResult.Where(item => item.ParentNum == entity.ParentNum).ToList();
            }
            int rowCount = listResult.Count();
            pageInfo.RowCount = rowCount;
            return listResult.OrderBy(item=>item.Depth).OrderBy(item=>item.ID).Where(item=>item.CateName!="Root").Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
        }

        /// <summary>
        /// 查询所有的子类
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public List<ProductCategoryEntity> GetChildList(string SnNum)
        {
            ProductCategoryEntity node = GetSingle(SnNum);
            List<ProductCategoryEntity> listSource = GetList();

            if (node != null && listSource != null)
            {
                return listSource.Where(item => item.Left >= node.Left && item.Right <= node.Right).ToList();
            }
            return null;
        }

        /// <summary>
        /// 查询族谱路径
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public List<ProductCategoryEntity> GetParentList(string SnNum)
        {
            ProductCategoryEntity node = GetSingle(SnNum);
            List<ProductCategoryEntity> listSource = GetList();

            if (node != null && listSource != null)
            {
                return listSource.Where(item => item.Left <= node.Left && item.Right >= node.Right).ToList();
            }
            return null;
        }
    }
}
