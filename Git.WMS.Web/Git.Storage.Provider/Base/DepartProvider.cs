/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2013-09-26 21:04:18
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2013-09-26 21:04:18       情缘
*********************************************************************************/

using Git.Framework.Cache;
using Git.Framework.Log;
using Git.Framework.Resource;
using Git.Storage.Entity.Base;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Storage.Common;
using Git.Storage.Entity.Sys;
using Git.Storage.Common.Enum;
using System.Transactions;

namespace Git.Storage.Provider.Base
{
    public partial class DepartProvider : DataFactory
    {
        public DepartProvider(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 查询根节点
        /// </summary>
        /// <returns></returns>
        private SysDepartEntity GetRoot()
        {
            string Key = string.Format(CacheKey.JOOSHOW_SYSDEPART_CACHE, this.CompanyID);
            List<SysDepartEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                SysDepartEntity entity = new SysDepartEntity();
                entity.SnNum = ConvertHelper.NewGuid();
                entity.DepartNum = new TNumProvider(this.CompanyID).GetSwiftNum(typeof(SysDepartEntity), 5);
                entity.IsDelete = (int)EIsDelete.NotDelete;
                entity.CreateTime = DateTime.Now;
                entity.DepartName = "Root";
                entity.ParentNum = "";
                entity.CompanyID = this.CompanyID;
                entity.Left = 1;
                entity.Right = 2;
                entity.IncludeAll();
                int line = this.SysDepart.Add(entity);
                if (line > 0)
                {
                    CacheHelper.Remove(Key);
                }
            }
            listSource = GetList();

            SysDepartEntity result = listSource.First(item => item.ParentNum.IsEmpty());

            return result;
        }

        /// <summary>
        /// 获得所有的部门信息
        /// </summary>
        /// <returns></returns>
        public List<SysDepartEntity> GetList()
        {
            string key = string.Format(CacheKey.JOOSHOW_SYSDEPART_CACHE, this.CompanyID);
            List<SysDepartEntity> listResult = CacheHelper.Get(key) as List<SysDepartEntity>;
            if (!listResult.IsNullOrEmpty())
            {
                return listResult;
            }
            SysDepartEntity temp = new SysDepartEntity();
            temp.IncludeAll();
            temp.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(item=>item.CompanyID==this.CompanyID);
            listResult = this.SysDepart.GetList(temp);
            if (!listResult.IsNullOrEmpty())
            {
                foreach (SysDepartEntity item in listResult)
                {
                    int depth = listResult.Where(a => a.Left <= item.Left && a.Right >= item.Right).Count();
                    item.Depth = depth;
                }
                foreach (SysDepartEntity entity in listResult.Where(itemParent => !string.IsNullOrEmpty(itemParent.ParentNum)))
                {
                    SysDepartEntity tempEntity = listResult.SingleOrDefault(item => item.SnNum == entity.ParentNum);
                    if (!tempEntity.IsNull())
                    {
                        entity.ParentName = tempEntity.DepartName;
                    }
                }
                CacheHelper.Insert(key, listResult, null, DateTime.Now.AddDays(1));
            }
            return listResult;
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <returns></returns>
        public int Add(SysDepartEntity entity)
        {
            string Key = string.Format(CacheKey.JOOSHOW_SYSDEPART_CACHE, this.CompanyID);
            if (entity.ParentNum.IsEmpty())
            {
                SysDepartEntity parent = GetRoot();
                entity.ParentNum = GetRoot().SnNum;
            }
            List<SysDepartEntity> listSource = GetList();
            listSource = listSource.IsNull() ? new List<SysDepartEntity>() : listSource;
            using (TransactionScope ts = new TransactionScope())
            {
                //查询新增节点的上一个节点
                SysDepartEntity parent = listSource.FirstOrDefault(item => item.SnNum == entity.ParentNum);
                int rightValue = parent.Right;

                List<SysDepartEntity> listNodes = listSource.Where(item => item.Right >= rightValue).ToList();
                if (!listNodes.IsNullOrEmpty())
                {
                    foreach (SysDepartEntity item in listNodes)
                    {
                        item.Right += 2;
                        item.IncludeRight(true);
                        item.Where(b => b.SnNum == item.SnNum).And(b => item.CompanyID == this.CompanyID);
                        this.SysDepart.Update(item);
                    }
                }

                listNodes = listSource.Where(item => item.Left >= rightValue).ToList();
                if (!listNodes.IsNullOrEmpty())
                {
                    foreach (SysDepartEntity item in listNodes)
                    {
                        item.Left += 2;
                        item.IncludeLeft(true);
                        item.Where(b => b.SnNum == item.SnNum).And(b => item.CompanyID == this.CompanyID);
                        this.SysDepart.Update(item);
                    }
                }

                entity.SnNum = ConvertHelper.NewGuid();
                entity.DepartNum = entity.DepartNum.IsEmpty() ? new TNumProvider(this.CompanyID).GetSwiftNum(typeof(SysDepartEntity), 5) : string.Empty;
                entity.CompanyID = this.CompanyID;
                entity.IsDelete = (int)EIsDelete.NotDelete;
                entity.CreateTime = DateTime.Now;
                entity.Left = rightValue;
                entity.Right = rightValue + 1;
                entity.IncludeAll();

                int line = this.SysDepart.Add(entity);
                if (line > 0)
                {
                    CacheHelper.Remove(Key);
                }
                ts.Complete();
                return line;
            }
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(SysDepartEntity entity)
        {
            string key = string.Format(CacheKey.JOOSHOW_SYSDEPART_CACHE, this.CompanyID);
            entity.Include(a => new { a.DepartName});

            int line = 0;
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(item=>item.CompanyID==this.CompanyID);
            line = this.SysDepart.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<string> list)
        {
            string key = string.Format(CacheKey.JOOSHOW_SYSDEPART_CACHE, this.CompanyID);

            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;

                List<SysDepartEntity> listSource = GetList();
                listSource = listSource.IsNull() ? new List<SysDepartEntity>() : listSource;

                foreach (string item in list)
                {
                    SysDepartEntity parent = listSource.FirstOrDefault(a => a.SnNum == item);
                    int rightValue = parent.Right;
                    int leftValue = parent.Left;

                    SysDepartEntity delDepart = new SysDepartEntity();
                    delDepart.IsDelete = (int)EIsDelete.Deleted;
                    delDepart.IncludeIsDelete(true);
                    delDepart.Where(a => a.Left >= leftValue)
                        .And(a => a.Right <= rightValue)
                        .And(a => a.CompanyID == this.CompanyID)
                        ;
                    line += this.SysDepart.Update(delDepart);

                    List<SysDepartEntity> listNodes = listSource.Where(a => a.Left > leftValue).ToList();
                    if (!listNodes.IsNullOrEmpty())
                    {
                        foreach (SysDepartEntity depart in listNodes)
                        {
                            depart.Left = depart.Left - (rightValue - leftValue + 1);
                            depart.IncludeLeft(true);
                            depart.Where(a => a.SnNum == depart.SnNum).And(a => a.CompanyID == this.CompanyID);
                            line += this.SysDepart.Update(depart);
                        }
                    }

                    listNodes = listSource.Where(a => a.Right > rightValue).ToList();
                    if (!listNodes.IsNullOrEmpty())
                    {
                        foreach (SysDepartEntity depart in listNodes)
                        {
                            depart.Right = depart.Right - (rightValue - leftValue + 1);
                            depart.IncludeRight(true);
                            depart.Where(a => a.SnNum == depart.SnNum).And(a => a.CompanyID == this.CompanyID);
                            line += this.SysDepart.Update(depart);
                        }
                    }
                }
                ts.Complete();
                if (line > 0)
                {
                    CacheHelper.Remove(key);
                }
                return line;
            }

        }

        /// <summary>
        /// 部门查看分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<SysDepartEntity> GetList(SysDepartEntity entity, ref PageInfo pageInfo)
        {
            List<SysDepartEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return listSource;
            }
            listSource = listSource.Where(item => item.ParentNum.IsNotEmpty()).ToList();
            if (!entity.DepartName.IsEmpty())
            {
                listSource = listSource.Where(a => a.DepartName.Contains(entity.DepartName)).ToList();
            }
            if (!entity.DepartNum.IsEmpty())
            {
                listSource = listSource.Where(a => a.DepartNum == entity.DepartNum).ToList();
            }
            int rowCount = listSource.Count;
            pageInfo.RowCount = rowCount;
            List<SysDepartEntity> listResult= listSource.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
            if (!listResult.IsNullOrEmpty())
            {
                listResult.ForEach(item => 
                {
                    if (item.ParentName == "Root")
                    {
                        item.ParentName = "";
                    }
                });
            }
            return listResult;
        }

        /// <summary>
        /// 根据编号查询部门信息
        /// </summary>
        /// <param name="DepartNum"></param>
        /// <returns></returns>
        public SysDepartEntity GetSingle(string DepartNum)
        {
            List<SysDepartEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return null;
            }
            listSource = listSource.Where(item => item.ParentNum.IsNotEmpty()).ToList();
            SysDepartEntity depart = listSource.FirstOrDefault(item => item.SnNum == DepartNum);

            return depart;
        }

        /// <summary>
        /// 查询所有的子类
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public List<SysDepartEntity> GetChildList(string SnNum)
        {
            SysDepartEntity node = GetSingle(SnNum);
            List<SysDepartEntity> listSource = GetList();

            if (node != null && listSource != null)
            {
                listSource = listSource.Where(item => item.ParentNum.IsNotEmpty()).ToList();
                List<SysDepartEntity> listResult = listSource.Where(item => item.Left >= node.Left && item.Right <= node.Right).ToList();
                if (!listResult.IsNullOrEmpty())
                {
                    listResult.ForEach(item =>
                    {
                        if (item.ParentName == "Root")
                        {
                            item.ParentName = "";
                        }
                    });
                }
                return listResult;
            }
            return null;
        }

        /// <summary>
        /// 查询族谱路径
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public List<SysDepartEntity> GetParentList(string SnNum)
        {
            SysDepartEntity node = GetSingle(SnNum);
            List<SysDepartEntity> listSource = GetList();

            if (node != null && listSource != null)
            {
                listSource = listSource.Where(item => item.ParentNum.IsNotEmpty()).ToList();
                return listSource.Where(item => item.Left <= node.Left && item.Right >= node.Right).ToList();
            }
            return null;
        }
    }
}
