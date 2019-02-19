using Git.Framework.Controller;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Newtonsoft.Json;
using Git.Storage.Entity.Report;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Framework.Io;


namespace Git.WMS.Web.Areas.Report.Controllers
{
    public class SingleAjaxController : AjaxPage
    {
        /// <summary>
        /// 新增报表参数
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult AddParam()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string ParamName = WebUtil.GetFormValue<string>("ParamName");
            string ShowName = WebUtil.GetFormValue<string>("ShowName");
            string ParamType = WebUtil.GetFormValue<string>("ParamType");
            string ParamData = WebUtil.GetFormValue<string>("ParamData");
            string DefaultValue = WebUtil.GetFormValue<string>("DefaultValue");
            string ParamElement = WebUtil.GetFormValue<string>("ParamElement");
            string Remark = WebUtil.GetFormValue<string>("Remark");

            List<ReportParamsEntity> listSource = Session[SessionKey.SESSION_REPORT_DETAIL] as List<ReportParamsEntity>;
            listSource = listSource.IsNull() ? new List<ReportParamsEntity>() : listSource;
            if (SnNum.IsNotEmpty() && listSource.Exists(a => a.SnNum == SnNum))
            {
                ReportParamsEntity entity = listSource.First(a => a.SnNum == SnNum);
                entity.ParamName = ParamName;
                entity.ShowName = ShowName;
                entity.ParamType = ParamType;
                entity.ParamData = ParamData;
                entity.DefaultValue = DefaultValue;
                entity.ParamElement = ParamElement;
                entity.Remark = Remark;
                entity.CompanyID = this.CompanyID;
            }
            else
            {
                ReportParamsEntity entity = new ReportParamsEntity();
                entity.SnNum = ConvertHelper.NewGuid();
                entity.ParamName = ParamName;
                entity.ShowName = ShowName;
                entity.ParamType = ParamType;
                entity.ParamData = ParamData;
                entity.DefaultValue = DefaultValue;
                entity.ParamElement = ParamElement;
                entity.Remark = Remark;
                entity.CompanyID = this.CompanyID;
                listSource.Add(entity);
            }
            Session[SessionKey.SESSION_REPORT_DETAIL] = listSource;
            DataResult dataResult = new DataResult() { Code=(int)EResponseCode.Success,Message="新增成功" };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除报表参数
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult Delete()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);
            List<ReportParamsEntity> listSource = Session[SessionKey.SESSION_REPORT_DETAIL] as List<ReportParamsEntity>;
            listSource = listSource.IsNull() ? new List<ReportParamsEntity>() : listSource;
            if (listSource.Exists(a => a.SnNum==SnNum))
            {
                listSource.Remove(a => a.SnNum == SnNum);
            }
            Session[SessionKey.SESSION_REPORT_DETAIL] = listSource;
            DataResult dataResult = new DataResult() { Code = (int)EResponseCode.Success, Message = "操作成功" };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 加载报表参数
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult LoadParam()
        {
            List<ReportParamsEntity> listSource = Session[SessionKey.SESSION_REPORT_DETAIL] as List<ReportParamsEntity>;
            listSource = listSource.IsNull() ? new List<ReportParamsEntity>() : listSource;
            DataResult<List<ReportParamsEntity>> dataResult = new DataResult<List<ReportParamsEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Result = listSource;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 加载存储过程参数
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult LoadProcParam()
        {
            string ProcName = WebUtil.GetFormValue<string>("ProcName");
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", this.CompanyID);
            dic.Add("ProceName", ProcName);

            string result = client.Execute(ReportApiName.ReportApiName_GetProcParameter,dic);
            DataResult<List<ReportParamsEntity>> dataResult = JsonConvert.DeserializeObject<DataResult<List<ReportParamsEntity>>>(result);
            List<ReportParamsEntity> listResult = dataResult.Result;
            if (!listResult.IsNullOrEmpty())
            {
                foreach (ReportParamsEntity item in listResult)
                {
                    item.SnNum =item.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : item.SnNum;
                }
            }
            Session[SessionKey.SESSION_REPORT_DETAIL] = listResult;
            DataResult Result = new DataResult() { Code = (int)EResponseCode.Success, Message = "加载成功" };
            return Content(JsonHelper.SerializeObject(Result));
        }

        /// <summary>
        /// 保存报表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Save()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string ReportNum = WebUtil.GetFormValue<string>("ReportNum");
            string ReportName = WebUtil.GetFormValue<string>("ReportName");
            int ReportType = WebUtil.GetFormValue<int>("ReportType");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string DataSource = WebUtil.GetFormValue<string>("DataSource");
            int DsType = WebUtil.GetFormValue<int>("DsType");
            string FileName = WebUtil.GetFormValue<string>("FileName");
            int Status = (int)EBool.No;

            List<ReportParamsEntity> listSource = Session[SessionKey.SESSION_REPORT_DETAIL] as List<ReportParamsEntity>;
            listSource = listSource.IsNull() ? new List<ReportParamsEntity>() : listSource;

            ReportsEntity entity = new ReportsEntity();
            entity.SnNum = SnNum;
            entity.ReportNum = ReportNum;
            entity.ReportName = ReportName;
            entity.ReportType = ReportType;
            entity.Remark = Remark;
            entity.DataSource = DataSource;
            entity.DsType = DsType;
            entity.FileName = FileName;
            entity.Status = Status;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = this.CompanyID;

            ITopClient client = new TopClientDefault();
            ReportsEntity oldEntity = null;
            if (!SnNum.IsEmpty())
            {
                Dictionary<string, string> dicReport = new Dictionary<string, string>();
                dicReport.Add("CompanyID", this.CompanyID);
                dicReport.Add("SnNum", SnNum);
                string reportResult = client.Execute(ReportApiName.ReportApiName_GetSingle, dicReport);
                DataResult<ReportsEntity> dataResult = JsonConvert.DeserializeObject<DataResult<ReportsEntity>>(reportResult);
                oldEntity = dataResult.Result;
            }
            if (oldEntity != null)
            {
                if (oldEntity.FileName != entity.FileName && entity.FileName.IsNotEmpty())
                {
                    string FileRealPath = Server.MapPath("~" + oldEntity.FileName);
                    string FileTempPath = Server.MapPath("~" + entity.FileName);
                    FileManager.DeleteFile(FileRealPath);
                    System.IO.File.Copy(FileTempPath, FileRealPath, true);
                    entity.FileName = oldEntity.FileName;
                }
            }
            else
            {
                if (entity.FileName.IsNotEmpty())
                {
                    //判断上传的文件存在
                    if (FileManager.FileExists(Server.MapPath("~" + entity.FileName)))
                    {
                        Git.Framework.Io.FileItem fileItem = FileManager.GetItemInfo(Server.MapPath("~" + entity.FileName));
                        string FileRealPath = Server.MapPath("~/Theme/content/report/" + fileItem.Name);
                        FileManager.MoveFile(Server.MapPath("~" + entity.FileName), FileRealPath);
                        entity.FileName = "/Theme/content/report/" + fileItem.Name;
                    }
                    else
                    {
                        //如果文件不存在,复制默认的文件
                        string fileName = string.Format("/Theme/content/report/{0}_{1}_{2}.frx", ReportType, ReportName, DateTime.Now.ToString("yyMMddHHmmssfff"));
                        string FileRealPath = Server.MapPath("~"+fileName);
                        string FileTempPath = Server.MapPath("~/Theme/content/temp/Report.frx");
                        FileManager.MoveFile(FileTempPath, FileRealPath);
                        entity.FileName = fileName;
                    }
                }
                else
                {
                    //如果文件不存在,复制默认的文件
                    string fileName = string.Format("/Theme/content/report/{0}_{1}_{2}.frx", ReportType, ReportName, DateTime.Now.ToString("yyMMddHHmmssfff"));
                    string FileRealPath = Server.MapPath("~" + fileName);
                    string FileTempPath = Server.MapPath("~/Theme/content/temp/Report.frx");
                    FileManager.MoveFile(FileTempPath, FileRealPath);
                    entity.FileName = fileName;
                }
            }
            
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", this.CompanyID);
            dic.Add("Entity", JsonConvert.SerializeObject(entity));
            dic.Add("List", JsonConvert.SerializeObject(listSource));

            string ApiName = ReportApiName.ReportApiName_Add;
            if (!SnNum.IsEmpty())
            {
                ApiName = ReportApiName.ReportApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }
    }
}
