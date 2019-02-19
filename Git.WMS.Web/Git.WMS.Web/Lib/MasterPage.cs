/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2013-09-01 12:23:27
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2013-09-01 12:23:27       情缘
*********************************************************************************/

using Git.Framework.Resource;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Git.Framework.Io;
using Git.Storage.Entity.Base;
using System.Text;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Sdk;
using Newtonsoft.Json;
using Git.Storage.Entity.Sys;
using Git.Storage.Common;
using Git.Framework.Cache;

namespace Git.WMS.Web.Lib
{
    public partial class MasterPage : MainPage
    {
        /// <summary>
        /// 调用父类初始化方法
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            //LoadResource();

            SetMenu();

            //SetStorage();

            SetNav();

            //SetVersion();

            //设置左侧菜单是否展示
            string menuStatus = "open";
            if (Session[SessionKey.SESSION_MENU_STATUS] != null)
            {
                menuStatus = Session[SessionKey.SESSION_MENU_STATUS] as string;
            }
            menuStatus = menuStatus == "close" ? "sidebar-closed" : "";
            ViewBag.MenuStatus = menuStatus;
        }

        public string Title { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string CssFile { get; set; }
        public string JsFile { get; set; }


        ///// <summary>
        ///// 加载资源文件包括SEO文件关键字 CSS JS
        ///// </summary>
        //private void LoadResource()
        //{
        //    PageEntity pageEntity = ResourceManager.GetPageEntityByPath(this.RequestPath);
        //    if (!pageEntity.IsNull())
        //    {
        //        if (!pageEntity.SeoEntity.IsNull())
        //        {
        //            Title = pageEntity.SeoEntity.Title;
        //            Keyword = pageEntity.SeoEntity.KeyWords;
        //            Description = pageEntity.SeoEntity.Description;
        //        }
        //        string cssFile = string.Empty;
        //        string jsFile = string.Empty; ;
        //        if (!pageEntity.FileGroupList.IsNullOrEmpty())
        //        {
        //            foreach (FileGroup fileGroup in pageEntity.FileGroupList)
        //            {
        //                if (!fileGroup.IsNull() && !fileGroup.FileList.IsNullOrEmpty())
        //                {
        //                    foreach (FileEntity fileEntity in fileGroup.FileList)
        //                    {
        //                        if (fileEntity.FileType == EFileType.Css)
        //                        {
        //                            if (fileEntity.Browser.IsEmpty())
        //                            {
        //                                cssFile += "<link href=\"" + fileEntity.FilePath + "\" rel=\"stylesheet\" type=\"text/css\" />";
        //                            }
        //                            else
        //                            {
        //                                if (fileEntity.Browser.ToLower() == "ie6")
        //                                {
        //                                    cssFile += "<!--[if IE 6]><link rel=\"stylesheet\" href=\"" + fileEntity.FilePath + "\" /><![endif]-->";
        //                                }
        //                                else if (fileEntity.Browser.ToLower() == "ie7")
        //                                {
        //                                    cssFile += "<!--[if IE 7]><link rel=\"stylesheet\" href=\"" + fileEntity.FilePath + "\" /><![endif]-->";
        //                                }
        //                                else if (fileEntity.Browser.ToLower() == "ie8")
        //                                {
        //                                    cssFile += "<!--[if IE 8]><link rel=\"stylesheet\" href=\"" + fileEntity.FilePath + "\" /><![endif]-->";
        //                                }
        //                                else if (fileEntity.Browser.ToLower() == "ie9")
        //                                {
        //                                    cssFile += "<!--[if IE 9]><link rel=\"stylesheet\" href=\"" + fileEntity.FilePath + "\" /><![endif]-->";
        //                                }
        //                            }
        //                        }
        //                        if (fileEntity.FileType == EFileType.Js)
        //                        {
        //                            jsFile += "<script type=\"text/javascript\" src=\"" + fileEntity.FilePath + "?t="+Guid.NewGuid().ToString()+"\"></script>";
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        CssFile = cssFile;
        //        JsFile = jsFile;
        //    }

        //    ViewBag.CssFile = CssFile;
        //    ViewBag.JsFile = JsFile;
        //    ViewBag.Title = Title;
        //    ViewBag.Keyword = Keyword;
        //    ViewBag.Description = Description;
        //    ViewBag.CurrentLoginUser = this.LoginUser;
        //}


        /// <summary>
        /// 根据登录用的角色加载菜单信息
        /// </summary>
        private void SetMenu()
        {
            StringBuilder sb = new StringBuilder();
            if (IsLogin() && !this.LoginUser.RoleNum.IsEmpty())
            {
                List<SysResourceEntity> listSource = CacheHelper.Get(SessionKey.SESSION_MENU_RESOURCE + this.LoginUser.RoleNum) as List<SysResourceEntity>;
                listSource = null;
                if (listSource.IsNullOrEmpty())
                {
                    string ApiName = UserApiName.UserApiName_GetPower;
                    ITopClient client = new TopClientDefault();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("CompanyID", this.CompanyID);
                    dic.Add("RoleNum", this.LoginUser.RoleNum);
                    string result = client.Execute(ApiName, dic);
                    DataResult<List<SysResourceEntity>> dataResult = JsonConvert.DeserializeObject<DataResult<List<SysResourceEntity>>>(result);
                    listSource = dataResult.Result;

                    if (!listSource.IsNullOrEmpty())
                    {
                        CacheHelper.Insert(SessionKey.SESSION_MENU_RESOURCE + this.LoginUser.RoleNum,listSource,null,DateTime.Now.AddMinutes(30));
                    }
                }
                
                if (!listSource.IsNullOrEmpty())
                {
                    string path = this.Path.ToLower();
                    SysResourceEntity current = listSource.FirstOrDefault(a => a.Url.ToLower() == path);
                    SysResourceEntity source = null;
                    if (current != null)
                    {
                        source = listSource.FirstOrDefault(a => a.ResNum == current.ParentNum);
                    }
                    foreach (SysResourceEntity parent in listSource.Where(a => a.ParentNum.IsEmpty()))
                    {
                        bool isExists = false;

                        StringBuilder sbChild = new StringBuilder();
                        if (listSource.Exists(a => a.ParentNum == parent.ParentNum))
                        {
                            StringBuilder sbLi = new StringBuilder();
                            foreach (SysResourceEntity child in listSource.Where(a => a.ParentNum == parent.ResNum))
                            {
                                if (child.Url.ToLower() == path || (source != null && child.Url.ToLower()==source.Url.ToLower()))
                                {
                                    isExists = true;
                                }
                                sbLi.AppendFormat("<li {2}><a href=\"{0}\">{1}</a></li>", child.Url, child.ResName, (child.Url.ToLower() == path || (source != null && child.Url.ToLower() == source.Url.ToLower())) ? "class='active'" : "");
                            }
                            sbChild.AppendFormat("<ul class=\"sub\"{0}>", isExists ? "style='display: block;'" : "");
                            sbChild.Append(sbLi.ToString());
                            sbChild.Append("</ul>");
                        }

                        StringBuilder sbContent = new StringBuilder();
                        sbContent.Append("<a href=\"javascript:void(0)\">");
                        sbContent.AppendFormat("<i class=\"{0}\"></i>" , parent.CssName.IsEmpty () ? "icon-bookmark-empty":parent.CssName);
                        sbContent.AppendFormat("<span class=\"title\">{0}</span>", parent.ResName);
                        sbContent.AppendFormat("<span class=\"arrow {0}\"></span>", isExists ? "open" : "");
                        sbContent.Append("</a>");

                        sbContent.Append(sbChild.ToString());

                        sb.AppendFormat("<li class=\"has-sub {0}\">", isExists ? "open active" : "");
                        sb.Append(sbContent.ToString());
                        sb.Append("</li>");
                    }
                }
            }
            ViewBag.MenuItems = sb.ToString();
        }

        ///// <summary>
        ///// 设置导航信息
        ///// </summary>
        private void SetNav()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<ul class=\"breadcrumb\">");
            //sb.Append("<li>");
            //sb.Append("<i class=\"icon-home\"></i>");
            //sb.Append("<a href=\"/Home/Welcome\">首页</a>");
            //sb.Append("<i class=\"icon-angle-right\"></i>");
            //sb.Append("</li>");
            //if (IsLogin() && !this.LoginUser.RoleNum.IsEmpty())
            //{
            //    PowerProvider provider = new PowerProvider();
            //    SysResourceProvider SysResourceProvider = new SysResourceProvider();
            //    List<SysResourceEntity> listSource = SysResourceProvider.GetList();
            //    List<SysResourceEntity> list = provider.GetRoleResource(this.LoginUser.RoleNum);
            //    if (!list.IsNullOrEmpty())
            //    {
            //        SysResourceEntity item = list.SingleOrDefault(a => a.Url.ToLower() == this.Path.ToLower());
            //        List<SysResourceEntity> listResult = new List<SysResourceEntity>();
            //        while (item != null)
            //        {
            //            listResult.Insert(0, item);

            //            if (item.ParentNum.IsEmpty())
            //            {
            //                break;
            //            }
            //            else
            //            {
            //                if (listSource.Exists(a => a.ResNum == item.ParentNum))
            //                {
            //                    item = listSource.First(a => a.ResNum == item.ParentNum);
            //                }
            //                else
            //                {
            //                    break;
            //                }
            //            }
            //        }
            //        for (int i = 0; i < listResult.Count; i++)
            //        {
            //            if (i != listResult.Count - 1)
            //            {
            //                sb.Append("<li>");
            //                sb.AppendFormat("<a href=\"{0}\">{1}</a>", listResult[i].Url.IsEmpty() ? "javascript:void(0)" : listResult[i].Url, listResult[i].ResName);
            //                sb.Append("<i class=\"icon-angle-right\"></i>");
            //                sb.Append("</li>");
            //            }
            //            else
            //            {
            //                sb.Append("<li>");
            //                sb.AppendFormat("<a href=\"javascript:void(0)\">{0}</a>", listResult[i].ResName);
            //                sb.Append("</li>");
            //            }
            //        }
            //    }
            //}
            //sb.Append("</ul>");


            StringBuilder sb = new StringBuilder();
            if (IsLogin() && !this.LoginUser.RoleNum.IsEmpty())
            {
                List<SysResourceEntity> listSource = CacheHelper.Get(SessionKey.SESSION_MENU_RESOURCE + this.LoginUser.RoleNum) as List<SysResourceEntity>;
                if (listSource.IsNullOrEmpty())
                {
                    string ApiName = UserApiName.UserApiName_GetPower;
                    ITopClient client = new TopClientDefault();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("CompanyID", this.CompanyID);
                    dic.Add("RoleNum", this.LoginUser.RoleNum);
                    string result = client.Execute(ApiName, dic);
                    DataResult<List<SysResourceEntity>> dataResult = JsonConvert.DeserializeObject<DataResult<List<SysResourceEntity>>>(result);
                    listSource = dataResult.Result;

                    if (!listSource.IsNullOrEmpty())
                    {
                        CacheHelper.Insert(SessionKey.SESSION_MENU_RESOURCE + this.LoginUser.RoleNum, listSource, null, DateTime.Now.AddMinutes(30));
                    }
                }

                sb.Append("<ul class='breadcrumb'>");
                    sb.Append("<li>");
                        sb.Append("<i class='icon-home'></i>");
                        sb.Append("<a href='/Home/Desktop'>首页</a>");
                        sb.Append("<i class='icon-angle-right'></i>");
                    sb.Append("</li>");
                

                if (!listSource.IsNullOrEmpty())
                {
                    string path = this.Path.ToLower();

                    SysResourceEntity item = listSource.SingleOrDefault(a => a.Url.ToLower() == path);
                    List<SysResourceEntity> listResult = new List<SysResourceEntity>();
                    while (item != null)
                    {
                        listResult.Insert(0, item);
                        if (item.ParentNum.IsEmpty())
                        {
                            break;
                        }
                        else
                        {
                            if (listSource.Exists(a => a.ResNum == item.ParentNum))
                            {
                                item = listSource.First(a => a.ResNum == item.ParentNum);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    for (int i = 0; i < listResult.Count; i++)
                    {
                        if (i != listResult.Count - 1)
                        {
                            sb.Append("<li>");
                                sb.AppendFormat("<a href=\"{0}\">{1}</a>", listResult[i].Url.IsEmpty() ? "javascript:void(0)" : listResult[i].Url, listResult[i].ResName);
                                sb.Append("<i class=\"icon-angle-right\"></i>");
                            sb.Append("</li>");
                        }
                        else
                        {
                            sb.Append("<li>");
                            sb.AppendFormat("<a href=\"javascript:void(0)\">{0}</a>", listResult[i].ResName);
                            sb.Append("</li>");
                        }
                    }
                }
                sb.Append("</ul>");
            }
            ViewBag.NavMenu = sb.ToString();
        }

        ///// <summary>
        ///// 设置系统的编译版本
        ///// </summary>
        //private void SetVersion()
        //{
        //    string version = Git.Framework.Cache.CacheHelper.Get<string>(Git.Storage.Provider.CacheKey.CACHE_SYS_VERSION);
        //    ViewBag.DebugVersion = version;
        //}
    }
}