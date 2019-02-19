using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Entity.Sys;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Storage.Common.Enum;

namespace Git.Storage.Provider.Sys
{
    public partial class CarrierProvider:DataFactory
    {
        public CarrierProvider(string _CompanyID) 
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(CarrierEntity entity)
        {
            entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.SnNum;
            entity.CarrierNum =entity.CarrierNum.IsEmpty() ? new TNumProvider(this.CompanyID).GetSwiftNum(typeof(CarrierEntity),5):entity.CarrierNum;
            entity.CompanyID = this.CompanyID;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.IncludeAll();
            int line = this.Carrier.Add(entity);

            return line;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Edit(CarrierEntity entity)
        {
            entity.IncludeCarrierName(true).IncludeRemark(true);
            entity.Where(async => async.SnNum == entity.SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;

            int line = this.Carrier.Update(entity);

            return line;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public int Delete(string SnNum)
        {
            CarrierEntity entity = new CarrierEntity();
            entity.Where(async => async.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            int line = this.Carrier.Delete(entity);

            return line;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public CarrierEntity GetSingle(string SnNum)
        {
            CarrierEntity entity = new CarrierEntity();
            entity.IncludeAll();
            entity.Where(async => async.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;

            entity = this.Carrier.GetSingle(entity);

            return entity;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<CarrierEntity> GetList(CarrierEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(item => item.ID, EOrderBy.DESC);
            entity.Where(async => async.CompanyID == this.CompanyID)
                ;
            if (!entity.CarrierNum.IsEmpty())
            {
                entity.And("CarrierNum", ECondition.Like, "%" + entity.CarrierNum + "%");
            }
            if (!entity.CarrierName.IsEmpty())
            {
                entity.And("CarrierName", ECondition.Like, "%" + entity.CarrierName + "%");
            }

            int rowCount = 0;
            List<CarrierEntity> listResult = this.Carrier.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;

            return listResult;
        }
    }
}
