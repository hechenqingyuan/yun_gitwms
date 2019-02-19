
/**
*
*单位管理功能
*
**/

var MeasureManager = {
    Server: function () {
        var config = (function () {
            var URL_GetList = "/Storage/MeasureAjax/GetList";
            var URL_Delete = "/Storage/MeasureAjax/Delete";
            var URL_Add = "/Storage/MeasureAjax/Add";
            var URL_ToExcel = "/Storage/MeasureAjax/ToExcel";

            return {
                URL_GetList: URL_GetList,
                URL_Delete: URL_Delete,
                URL_Add: URL_Add,
                URL_ToExcel: URL_ToExcel
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

            return {
                GetList: GetList,
                Delete: Delete,
                Add: Add,
                ToExcel: ToExcel
            }

        })($, config);
        return dataServer;
    },
    Dialog:function(SN,Command){
        var submit=function(v, h, f){
            if(v){
                var MeasureNum=h.find('input[name="MeasureNum"]').val();
                var SN=h.find('input[name="SN"]').val();
                var MeasureName=h.find('input[name="MeasureName"]').val();

                if(git.IsEmpty(MeasureName)){
                    $.jBox.tip("请输入单位名","warn");
                    return false;
                }
                var param={};
                param["MeasureNum"]=MeasureNum;
                param["SN"]=SN;
                param["MeasureName"]=MeasureName;

                var Server=MeasureManager.Server();
                Server.Add(param,function(result){
                    var pageSize=$("#mypager").pager("GetPageSize");
                    var pageIndex=$("#mypager").pager("GetCurrent");
                    if(Command=="Add"){
                        MeasureManager.PageClick(1,pageSize);
                    }else if(Command=="Edit"){
                        MeasureManager.Refresh();
                    }
                    $.jBox.tip(result.Message,"success");
                });
            }
        }

        if(Command=="Add"){
            $.jBox.open("get:/Storage/Measure/Add", "新增单位", 350, 200, { buttons: { "确定": true, "关闭": false } ,submit:submit});
        }else if(Command=="Edit"){
            $.jBox.open("get:/Storage/Measure/Add?SN="+SN, "编辑单位", 350, 200, { buttons: { "确定": true, "关闭": false } ,submit:submit});
        }
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=MeasureManager.Server();
        var search=MeasureManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            MeasureManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=MeasureManager.Server();
        var search=MeasureManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            MeasureManager.SetTable(result);
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
                { data: 'SN' ,render:function(data, type, full, meta){
                    return "<input type='checkbox' name='measure_item' value='"+data+"'/>";
                }},
                { data: 'MeasureNum'},
                { data: 'MeasureName'},
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
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: MeasureManager.PageClick });
        }

        $("#tabList").find('input[type="checkbox"]').attr("checked",false);
        //绑定编辑 删除事件
        MeasureManager.BindEvent();
    },
    BindEvent:function(){
        $("#tabList").find("a.edit").click(function(){
            var SN=$(this).parent().parent().find("input[name='measure_item']").val();
            MeasureManager.Dialog(SN,"Edit")
        });

        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='measure_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var list=[];
                    list.push(SN);
                    var param={};
                    param["list"]=JSON.stringify(list);
                    var Server=MeasureManager.Server();
                    Server.Delete(param,function(result){
                        $.jBox.tip(result.Message,"success");
                        var pageSize=$("#mypager").pager("GetPageSize");
                        MeasureManager.PageClick(1,pageSize);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });
    },
    SelectAll:function(item){
        var flag=$(item).attr("checked");
        if(flag){
            $("#tabList").find("input[name='measure_item']").attr("checked",true);
        }else{
            $("#tabList").find("input[name='measure_item']").attr("checked",false);
        }
    },
    GetSelect:function(){
        var list=[];
        $("#tabList").find("input[name='measure_item']").each(function(i,item){
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
        var MeasureNum=searchBar.find("input[name='MeasureNum']").val();
        var MeasureName=searchBar.find("input[name='MeasureName']").val();
        var search={};
        search["MeasureNum"]=MeasureNum;
        search["MeasureName"]=MeasureName;
        return search;
    },
    ToolBar:function(){

        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                MeasureManager.Dialog(undefined,command);
            }else if(command=="Edit"){
                var list=MeasureManager.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var SN=list[0];
                MeasureManager.Dialog(SN,command);
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=MeasureManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=MeasureManager.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            MeasureManager.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=MeasureManager.Server();
                var search=MeasureManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                MeasureManager.Refresh();
            }

        });

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            MeasureManager.PageClick(1,10);
        });
        
        //加载默认数据
        MeasureManager.PageClick(1,10);
    }
}