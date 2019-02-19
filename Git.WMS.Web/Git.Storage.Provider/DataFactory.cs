/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-04 10:27:19
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-04 10:27:19       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider
{
    public partial class DataFactory
    {
        protected string CompanyID { get; set; }

        /************************************************* Sys 空间 ****************************************************/
        public Git.Storage.IDataAccess.Sys.ICompany Company { get { return new Git.Storage.DataAccess.Sys.CompanyDataAccess(); } }

        public Git.Storage.IDataAccess.Sys.ITNum TNum { get { return new Git.Storage.DataAccess.Sys.TNumDataAccess(); } }

        public Git.Storage.IDataAccess.Sys.IAdmin Admin { get { return new Git.Storage.DataAccess.Sys.AdminDataAccess(); } }

        public Git.Storage.IDataAccess.Sys.ISequence Sequence { get { return new Git.Storage.DataAccess.Sys.SequenceDataAccess(); } }

        public Git.Storage.IDataAccess.Sys.ISysDepart SysDepart { get { return new Git.Storage.DataAccess.Sys.SysDepartDataAccess(); } }

        public Git.Storage.IDataAccess.Sys.ISysRole SysRole { get { return new Git.Storage.DataAccess.Sys.SysRoleDataAccess(); } }

        public Git.Storage.IDataAccess.Sys.ISysResource SysResource { get { return new Git.Storage.DataAccess.Sys.SysResourceDataAccess(); } }

        public Git.Storage.IDataAccess.Sys.ISysRelation SysRelation { get { return new Git.Storage.DataAccess.Sys.SysRelationDataAccess(); } }

        public Git.Storage.IDataAccess.Sys.ICarrier Carrier { get { return new Git.Storage.DataAccess.Sys.CarrierDataAccess(); } }

        /************************************************* Base 空间 ****************************************************/
        public Git.Storage.IDataAccess.Base.IProc_SwiftNum Proc_SwiftNum { get { return new Git.Storage.DataAccess.Base.Proc_SwiftNumDataAccess(); } }


        /************************************************* Storage 空间 ****************************************************/
        public Git.Storage.IDataAccess.Storage.IMeasure Measure { get { return new Git.Storage.DataAccess.Storage.MeasureDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.IMeasureRel MeasureRel { get { return new Git.Storage.DataAccess.Storage.MeasureRelDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.ICusAddress CusAddress { get { return new Git.Storage.DataAccess.Storage.CusAddressDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.ICustomer Customer { get { return new Git.Storage.DataAccess.Storage.CustomerDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.IEquipment Equipment { get { return new Git.Storage.DataAccess.Storage.EquipmentDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.ILocation Location { get { return new Git.Storage.DataAccess.Storage.LocationDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.IProduct Product { get { return new Git.Storage.DataAccess.Storage.ProductDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.IProductCategory ProductCategory { get { return new Git.Storage.DataAccess.Storage.ProductCategoryDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.IStorage Storage { get { return new Git.Storage.DataAccess.Storage.StorageDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.ISupplier Supplier { get { return new Git.Storage.DataAccess.Storage.SupplierDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.ILocalProduct LocalProduct { get { return new Git.Storage.DataAccess.Storage.LocalProductDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.IInventoryBook InventoryBook { get { return new Git.Storage.DataAccess.Storage.InventoryBookDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.ICar Car { get { return new Git.Storage.DataAccess.Storage.CarDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.IV_LocalProduct V_LocalProduct { get { return new Git.Storage.DataAccess.Storage.V_LocalProductDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.IV_StorageProduct V_StorageProduct { get { return new Git.Storage.DataAccess.Storage.V_StorageProductDataAccess(); } }

        public Git.Storage.IDataAccess.Storage.IV_LocalCapacity V_LocalCapacity { get { return new Git.Storage.DataAccess.Storage.V_LocalCapacityDataAccess(); } }

        /************************************************* InStorage 空间 ****************************************************/

        public Git.Storage.IDataAccess.InStorage.IInStorage InStorage { get { return new Git.Storage.DataAccess.InStorage.InStorageDataAccess(); } }

        public Git.Storage.IDataAccess.InStorage.IInStorDetail InStorDetail { get { return new Git.Storage.DataAccess.InStorage.InStorDetailDataAccess(); } }

        public Git.Storage.IDataAccess.InStorage.IProc_AuditeInStorage Proc_AuditeInStorage { get { return new Git.Storage.DataAccess.InStorage.Proc_AuditeInStorageDataAccess(); } }


        /************************************************* Report 空间 ****************************************************/
        public Git.Storage.IDataAccess.Report.IReports Reports { get { return new Git.Storage.DataAccess.Report.ReportsDataAccess(); } }

        public Git.Storage.IDataAccess.Report.IReportParams ReportParams { get { return new Git.Storage.DataAccess.Report.ReportParamsDataAccess(); } }

        public Git.Storage.IDataAccess.Report.IBalanceBook BalanceBook { get { return new Git.Storage.DataAccess.Report.BalanceBookDataAccess(); } }


        /************************************************* OutStorage 空间 ****************************************************/

        public Git.Storage.IDataAccess.OutStorage.IOutStorage OutStorage { get { return new Git.Storage.DataAccess.OutStorage.OutStorageDataAccess(); } }

        public Git.Storage.IDataAccess.OutStorage.IOutStoDetail OutStoDetail { get { return new Git.Storage.DataAccess.OutStorage.OutStoDetailDataAccess(); } }

        public Git.Storage.IDataAccess.OutStorage.IProc_AuditeOutStorage Proc_AuditeOutStorage { get { return new Git.Storage.DataAccess.OutStorage.Proc_AuditeOutStorageDataAccess(); } }


        /************************************************* Move 空间 ****************************************************/

        public Git.Storage.IDataAccess.Move.IMoveOrder MoveOrder { get { return new Git.Storage.DataAccess.Move.MoveOrderDataAccess(); } }

        public Git.Storage.IDataAccess.Move.IMoveOrderDetail MoveOrderDetail { get { return new Git.Storage.DataAccess.Move.MoveOrderDetailDataAccess(); } }

        public Git.Storage.IDataAccess.Move.IProc_AuditeMove Proc_AuditeMove { get { return new Git.Storage.DataAccess.Move.Proc_AuditeMoveDataAccess(); } }


        /************************************************* Bad 空间 ****************************************************/

        public Git.Storage.IDataAccess.Bad.IBadReport BadReport { get { return new Git.Storage.DataAccess.Bad.BadReportDataAccess(); } }

        public Git.Storage.IDataAccess.Bad.IBadReportDetail BadReportDetail { get { return new Git.Storage.DataAccess.Bad.BadReportDetailDataAccess(); } }

        public Git.Storage.IDataAccess.Bad.IProc_AuditeBadReport Proc_AuditeBadReport { get { return new Git.Storage.DataAccess.Bad.Proc_AuditeBadReportDataAccess(); } }


        /************************************************* Allocate 空间 ****************************************************/
        public Git.Storage.IDataAccess.Allocate.IAllocateOrder AllocateOrder { get { return new Git.Storage.DataAccess.Allocate.AllocateOrderDataAccess(); } }

        public Git.Storage.IDataAccess.Allocate.IAllocateDetail AllocateDetail { get { return new Git.Storage.DataAccess.Allocate.AllocateDetailDataAccess(); } }

        public Git.Storage.IDataAccess.Allocate.IProc_AuditeAllocate Proc_AuditeAllocate { get { return new Git.Storage.DataAccess.Allocate.Proc_AuditeAllocateDataAccess(); } }

        /************************************************* Finance 空间 ****************************************************/

        public Git.Storage.IDataAccess.Finance.IFinanceCate FinanceCate { get { return new Git.Storage.DataAccess.Finance.FinanceCateDataAccess(); } }

        public Git.Storage.IDataAccess.Finance.IFinanceBill FinanceBill { get { return new Git.Storage.DataAccess.Finance.FinanceBillDataAccess(); } }

        public Git.Storage.IDataAccess.Finance.IFinancePay FinancePay { get { return new Git.Storage.DataAccess.Finance.FinancePayDataAccess(); } }


        /************************************************* Biz 空间 ****************************************************/

        public Git.Storage.IDataAccess.Biz.ISaleOrder SaleOrder { get { return new Git.Storage.DataAccess.Biz.SaleOrderDataAccess(); } }

        public Git.Storage.IDataAccess.Biz.ISaleDetail SaleDetail { get { return new Git.Storage.DataAccess.Biz.SaleDetailDataAccess(); } }

        public Git.Storage.IDataAccess.Biz.IPurchase Purchase { get { return new Git.Storage.DataAccess.Biz.PurchaseDataAccess(); } }

        public Git.Storage.IDataAccess.Biz.IPurchaseDetail PurchaseDetail { get { return new Git.Storage.DataAccess.Biz.PurchaseDetailDataAccess(); } }


        public Git.Storage.IDataAccess.Biz.ISaleReturn SaleReturn { get { return new Git.Storage.DataAccess.Biz.SaleReturnDataAccess(); } }

        public Git.Storage.IDataAccess.Biz.ISaleReturnDetail SaleReturnDetail { get { return new Git.Storage.DataAccess.Biz.SaleReturnDetailDataAccess(); } }

        public Git.Storage.IDataAccess.Biz.IPurchaseReturn PurchaseReturn { get { return new Git.Storage.DataAccess.Biz.PurchaseReturnDataAccess(); } }

        public Git.Storage.IDataAccess.Biz.IPurchaseReturnDetail PurchaseReturnDetail { get { return new Git.Storage.DataAccess.Biz.PurchaseReturnDetailDataAccess(); } }

        /************************************************* Check 空间 ****************************************************/

        public Git.Storage.IDataAccess.Check.IInventoryOrder InventoryOrder { get { return new Git.Storage.DataAccess.Check.InventoryOrderDataAccess(); } }

        public Git.Storage.IDataAccess.Check.IInventoryDetail InventoryDetail { get { return new Git.Storage.DataAccess.Check.InventoryDetailDataAccess(); } }

        public Git.Storage.IDataAccess.Check.IInventoryDif InventoryDif { get { return new Git.Storage.DataAccess.Check.InventoryDifDataAccess(); } }

        public Git.Storage.IDataAccess.Check.ICloneTemp CloneTemp { get { return new Git.Storage.DataAccess.Check.CloneTempDataAccess(); } }

        public Git.Storage.IDataAccess.Check.ICloneHistory CloneHistory { get { return new Git.Storage.DataAccess.Check.CloneHistoryDataAccess(); } }

        public Git.Storage.IDataAccess.Check.IProc_CreateCheck Proc_CreateCheck { get { return new Git.Storage.DataAccess.Check.Proc_CreateCheckDataAccess(); } }

        public Git.Storage.IDataAccess.Check.IProc_AuditeCheck Proc_AuditeCheck { get { return new Git.Storage.DataAccess.Check.Proc_AuditeCheckDataAccess(); } }


        /************************************************* Pick 空间 ****************************************************/

        public Git.Storage.IDataAccess.Pick.IProc_PickProduct Proc_PickProduct { get { return new Git.Storage.DataAccess.Pick.Proc_PickProductDataAccess(); } }


        /************************************************* Sku 空间 ****************************************************/

        public Git.Storage.IDataAccess.Sku.IProductSku ProductSku { get { return new Git.Storage.DataAccess.Sku.ProductSkuDataAccess(); } }

        public Git.Storage.IDataAccess.Sku.IV_Sku V_Sku { get { return new Git.Storage.DataAccess.Sku.V_SkuDataAccess(); } }

    }
}
