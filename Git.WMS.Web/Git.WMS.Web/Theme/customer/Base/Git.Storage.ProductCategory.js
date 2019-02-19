
/**
*
*产品类别管理功能
*
**/

var ProductCategoryManager = {
    Server: function () {
        var config = (function () {
            var URL_GetList = "/Storage/ProductCategoryAjax/GetList";
            var URL_Delete = "/Storage/ProductCategoryAjax/Delete";
            var URL_Add = "/Storage/ProductCategoryAjax/Add";
            var URL_ToExcel = "/Storage/ProductCategoryAjax/ToExcel";

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
                var CateNum=h.find('input[name="CateNum"]').val();
                var SnNum=h.find('input[name="SnNum"]').val();
                var CateName=h.find('input[name="CateName"]').val();
                var Remark=h.find('input[name="Remark"]').val();
                var ParentNum=h.find('select[name="ParentNum"]').val();

                if(git.IsEmpty(CateName)){
                    $.jBox.tip("请输入产品类别名","warn");
                    return false;
                }
                var param={};
                param["CateNum"]=CateNum;
                param["SnNum"]=SnNum;
                param["CateName"]=CateName;
                param["Remark"]=Remark;
                param["ParentNum"]=ParentNum;

                var Server=ProductCategoryManager.Server();
                Server.Add(param,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        var pageIndex=$("#mypager").pager("GetCurrent");
                        if(Command=="Add"){
                            ProductCategoryManager.PageClick(1,pageSize);
                        }else if(Command=="Edit"){
                            ProductCategoryManager.Refresh();
                        }
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });
            }
        }

        if(Command=="Add"){
            $.jBox.open("get:/Storage/ProductCategory/Add", "新增产品类别", 350, 310, { buttons: { "确定": true, "关闭": false } ,submit:submit});
        }else if(Command=="Edit"){
            $.jBox.open("get:/Storage/ProductCategory/Add?SnNum="+SN, "编辑产品类别", 350, 310, { buttons: { "确定": true, "关闭": false } ,submit:submit});
        }
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=ProductCategoryManager.Server();
        var search=ProductCategoryManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            ProductCategoryManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=ProductCategoryManager.Server();
        var search=ProductCategoryManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            ProductCategoryManager.SetTable(result);
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
                { data: 'SnNum' ,render:function(data, type, full, meta){
                    return "<input type='checkbox' name='category_item' value='"+data+"'/>";
                }},
                { data: 'CateNum'},
                { data: 'CateName'},
                { data: 'ParentName'},
                { data: 'CreateTime',render:function(data,type,full,meta){
                    return git.JsonToDateTime(data,"YYYY-MM-DD");
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
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: ProductCategoryManager.PageClick });
        }

        //绑定编辑 删除事件
        ProductCategoryManager.BindEvent();
    },
    BindEvent:function(){
        $("#tabList").find("a.edit").click(function(){
            var SN=$(this).parent().parent().find("input[name='category_item']").val();
            ProductCategoryManager.Dialog(SN,"Edit")
        });

        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='category_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var list=[];
                    list.push(SN);
                    var param={};
                    param["list"]=JSON.stringify(list);
                    var Server=ProductCategoryManager.Server();
                    Server.Delete(param,function(result){
                        $.jBox.tip(result.Message,"success");
                        var pageSize=$("#mypager").pager("GetPageSize");
                        ProductCategoryManager.PageClick(1,pageSize);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });
    },
    SelectAll:function(item){
        var flag=$(item).attr("checked");
        if(flag){
            $("#tabList").find("input[name='category_item']").attr("checked",true);
        }else{
            $("#tabList").find("input[name='category_item']").attr("checked",false);
        }
    },
    GetSelect:function(){
        var list=[];
        $("#tabList").find("input[name='category_item']").each(function(i,item){
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
        var CateName=searchBar.find("input[name='CateName']").val();
        var CateNum=searchBar.find("input[name='CateNum']").val();
        var search={};
        search["CateName"]=CateName;
        search["CateNum"]=CateNum;
        return search;
    },
    ToolBar:function(){

        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                ProductCategoryManager.Dialog(undefined,command);
            }else if(command=="Edit"){
                var list=ProductCategoryManager.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var SN=list[0];
                ProductCategoryManager.Dialog(SN,command);
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=ProductCategoryManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=ProductCategoryManager.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            ProductCategoryManager.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=ProductCategoryManager.Server();
                var search=ProductCategoryManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                ProductCategoryManager.Refresh();
            }

        });

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            ProductCategoryManager.PageClick(1,10);
        });
        
        //加载默认数据
        ProductCategoryManager.PageClick(1,10);
    }
}