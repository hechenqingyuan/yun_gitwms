
/**
*
*库位管理功能
*
**/

var LocationManager = {
    Server: function () {
        var config = (function () {
            var URL_GetList = "/Storage/LocationAjax/GetList";
            var URL_Delete = "/Storage/LocationAjax/Delete";
            var URL_Add = "/Storage/LocationAjax/Add";
            var URL_ToExcel = "/Storage/LocationAjax/ToExcel";
            var URL_SetDefault = "/Storage/LocationAjax/SetDefault";
            var URL_SetForbid = "/Storage/LocationAjax/SetForbid";

            return {
                URL_GetList: URL_GetList,
                URL_Delete: URL_Delete,
                URL_Add: URL_Add,
                URL_ToExcel: URL_ToExcel,
                URL_SetDefault: URL_SetDefault,
                URL_SetForbid: URL_SetForbid,
            };
        })();

        //数据操作服务
        var dataServer = (function ($, config) {

            //查询分页列表
            var GetList=function(data,callback){
                $.gitAjax({
                    url: config.URL_GetList,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            var Delete=function(data,callback){
                $.gitAjax({
                    url: config.URL_Delete,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            var Add=function(data,callback){
                $.gitAjax({
                    url: config.URL_Add,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            var ToExcel=function(data,callback){
                $.gitAjax({
                    url: config.URL_ToExcel,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            var SetDefault=function(data,callback){
                $.gitAjax({
                    url: config.URL_SetDefault,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            var SetForbid=function(data,callback){
                $.gitAjax({
                    url: config.URL_SetForbid,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            return {
                GetList: GetList,
                Delete: Delete,
                Add: Add,
                ToExcel: ToExcel,
                SetDefault: SetDefault,
                SetForbid: SetForbid,
            }

        })($, config);
        return dataServer;
    },
    Dialog:function(SN,Command){
        var submit=function(v, h, f){
            if(v){
                var LocalNum=h.find('input[name="LocalNum"]').val();
                var LocalBarCode=h.find('input[name="LocalBarCode"]').val();
                var LocalName=h.find('input[name="LocalName"]').val();
                var LocalType=h.find('select[name="LocalType"]').val();
                var StorageNum=h.find('select[name="StorageNum"]').val();
                var IsDefault=0;
                if(h.find('input[name="IsDefault"]').attr("checked")){
                    IsDefault=1;
                }
                var IsForbid=0;
                if(h.find('input[name="IsForbid"]').attr("checked")){
                    IsForbid=1;
                }
                var Remark=h.find('input[name="Remark"]').val();
                
                var param={};
                param["LocalNum"]=LocalNum;
                param["LocalBarCode"]=LocalBarCode;
                param["LocalName"]=LocalName;
                param["LocalType"]=LocalType;
                param["StorageNum"]=StorageNum;
                param["IsForbid"]=IsForbid;
                param["IsDefault"]=IsDefault;
                param["IsForbid"]=IsForbid;
                param["Remark"]=Remark;

                var Server=LocationManager.Server();
                Server.Add(param,function(result){
                    var pageSize=$("#mypager").pager("GetPageSize");
                    var pageIndex=$("#mypager").pager("GetCurrent");
                    if(Command=="Add"){
                        LocationManager.PageClick(1,pageSize);
                    }else if(Command=="Edit"){
                        LocationManager.Refresh();
                    }
                    $.jBox.tip(result.Message,"success");
                });
            }
        }
        var StorageNum=$('select[name="StorageNum"]').val();
        if(Command=="Add"){
            $.jBox.open("get:/Storage/Location/Add?StorageNum="+StorageNum, "新增库位", 650, 300, { buttons: { "确定": true, "关闭": false } ,submit:submit});
        }else if(Command=="Edit"){
            $.jBox.open("get:/Storage/Location/Add?SnNum="+SN, "编辑库位", 650, 300, { buttons: { "确定": true, "关闭": false } ,submit:submit});
        }
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=LocationManager.Server();
        var search=LocationManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            LocationManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=LocationManager.Server();
        var search=LocationManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            LocationManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable:function(result){
        $("#tabList").DataTable({
            destroy: true,
            data:result.Result,
            paging:false,
            searching:false,
            scrollX: false,
            bAutoWidth:true,
            bInfo:false,
            ordering:false,
            columns: [
                { data: 'LocalNum' ,render:function(data, type, full, meta){
                    return "<input type='checkbox' name='location_item' value='"+data+"'/>";
                }},
                { data: 'LocalBarCode'},
                { data: 'LocalName'},
                { data: 'LocalType',render:function(data,type,full,meta){
                    return git.GetEnumDesc(ELocalType,data); 
                }},
                { data: 'StorageName'},
                { data: 'IsForbid',render:function(data,type,full,meta){
                    if(data==EBoolJson.Yes){
                        return "<input type='checkbox' checked='checked' name='IsForbid_item'/>";
                    }else{
                        return "<input type='checkbox' name='IsForbid_item'/>";
                    }
                }},
                { data: 'IsDefault',render:function(data,type,full,meta){
                    if(data==EBoolJson.Yes){
                        return "<input type='checkbox' checked='checked' name='IsDefault_item'/>";
                    }else{
                        return "<input type='checkbox' name='IsDefault_item'/>";
                    }
                }},
                { data: 'CreateTime',render:function(data,type,full,meta){
                    return git.JsonToDateTime(data); 
                }},
                { data: 'Remark'},
                {data:"ID",render:function(data,type,full,meta){
                    var html="";
                    html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;&nbsp;';
                    html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                    return html;
                }}
            ],
            aoColumnDefs:[
                { "sWidth": "15px",  "aTargets": [0] }
            ],
            oLanguage:{
                sEmptyTable:"没有查询到任何数据"
            }
        });

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: LocationManager.PageClick });
        }

        //绑定编辑 删除事件
        LocationManager.BindEvent();
    },
    BindEvent:function(){
        $("#tabList").find("a.edit").click(function(){
            var SN=$(this).parent().parent().find("input[name='location_item']").val();
            LocationManager.Dialog(SN,"Edit")
        });

        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='location_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var list=[];
                    list.push(SN);
                    var param={};
                    param["list"]=JSON.stringify(list);
                    var Server=LocationManager.Server();
                    Server.Delete(param,function(result){
                        $.jBox.tip(result.Message,"success");
                        var pageSize=$("#mypager").pager("GetPageSize");
                        LocationManager.PageClick(1,pageSize);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });

        $("#tabList").find("input[name='IsForbid_item']").click(function(){
            var SnNum=$(this).parent().parent().find("input[name='location_item']").val();
            var IsForbid=EBoolJson.No;
            if($(this).attr("checked")){
                IsForbid=EBoolJson.Yes;
            }
            var param={};
            param["LocalNum"]=SnNum;
            param["IsForbid"]=IsForbid;
            var Server=LocationManager.Server();
            Server.SetForbid(param,function(result){
                LocationManager.Refresh();
            });
        });

        $("#tabList").find("input[name='IsDefault_item']").click(function(){
            var SnNum=$(this).parent().parent().find("input[name='location_item']").val();
            var IsDefault=EBoolJson.No;
            if($(this).attr("checked")){
                IsDefault=EBoolJson.Yes;
            }
            var param={};
            param["LocalNum"]=SnNum;
            param["IsDefault"]=IsDefault;
            var Server=LocationManager.Server();
            Server.SetDefault(param,function(result){
                LocationManager.Refresh();
            });
        });
    },
    SelectAll:function(item){
        var flag=$(item).attr("checked");
        if(flag){
            $("#tabList").find("input[name='location_item']").attr("checked",true);
        }else{
            $("#tabList").find("input[name='location_item']").attr("checked",false);
        }
    },
    GetSelect:function(){
        var list=[];
        $("#tabList").find("input[name='location_item']").each(function(i,item){
            var flag=$(item).attr("checked");
            if(flag){
                var value=$(item).val();
                list.push(value);
            }
        });
        return list;
    },
    GetSearch:function(){
        var searchBar=$("div[data-condition='search']");
        var LocalBarCode=searchBar.find("input[name='LocalBarCode']").val();
        var LocalName=searchBar.find("input[name='LocalName']").val();
        var StorageNum=searchBar.find("select[name='StorageNum']").val();
        var search={};
        search["LocalBarCode"]=LocalBarCode;
        search["LocalName"]=LocalName;
        search["StorageNum"]=StorageNum;
        search["ListLocalType"]=JSON.stringify([]);
        return search;
    },
    ToolBar:function(){

        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                LocationManager.Dialog(undefined,command);
            }else if(command=="Edit"){
                var list=LocationManager.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var SN=list[0];
                LocationManager.Dialog(SN,command);
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=LocationManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=LocationManager.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            LocationManager.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=LocationManager.Server();
                var search=LocationManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                LocationManager.Refresh();
            }

        });

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            LocationManager.PageClick(1,10);
        });
        
        //加载默认数据
        LocationManager.PageClick(1,10);
    }
}