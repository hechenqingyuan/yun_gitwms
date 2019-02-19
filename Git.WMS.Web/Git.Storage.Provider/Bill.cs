/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-30 17:21:32
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-30 17:21:32       情缘
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider
{
    public abstract partial class Bill<T, V> : DataFactory
        where T : BaseEntity
        where V : BaseEntity
    {

        /// <summary>
        /// 创建单据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public abstract string Create(T entity, List<V> list);

        /// <summary>
        /// 取消单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract string Cancel(T entity);

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract string Delete(T entity);

        /// <summary>
        /// 批量删除单据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public abstract string Delete(IEnumerable<string> list);

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract string Audite(T entity);

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract string Print(T entity);

        /// <summary>
        /// 查询单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract T GetOrder(T entity);

        /// <summary>
        /// 获得单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract List<V> GetOrderDetail(V entity);

        /// <summary>
        /// 查询单据分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public abstract List<T> GetList(T entity, ref PageInfo pageInfo);

        /// <summary>
        /// 查询单据详细数据分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public abstract List<V> GetDetailList(V entity, ref PageInfo pageInfo);

        /// <summary>
        /// 编辑单据信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract string EditOrder(T entity);

        /// <summary>
        /// 查询单据详细信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public abstract V GetDetail(string SnNum);

        /// <summary>
        /// 编辑单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract string EditDetail(V entity);

        /// <summary>
        /// 编辑入库单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public abstract string EditOrder(T entity, List<V> list);

        /// <summary>
        /// 获得订单数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract int GetCount(T entity);

        /// <summary>
        /// 获得打印单据的数据源
        /// </summary>
        /// <param name="argOrderNum"></param>
        /// <returns></returns>
        public abstract DataSet GetPrint(string argOrderNum);
    }
}
