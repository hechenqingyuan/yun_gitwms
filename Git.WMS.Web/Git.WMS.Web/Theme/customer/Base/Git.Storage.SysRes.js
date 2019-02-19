
/**
*
*资源管理
*
**/

var SysResManager = {
    Server: function () {
    	var config = (function () {
            var URL_GetList = "/SysResAjax/GetList";
            var URL_ToExcel = "/SysResAjax/ToExcel";
            var URL_Edit = "/SysResAjax/Edit";

            var URL_Save = "/SysResAjax/Save";

            return {
                URL_GetList: URL_GetList,
                URL_Edit: URL_Edit,
                URL_Save: URL_Save,
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

            var Edit=function(data,callback){
                $.gitAjax({
                    url: config.URL_Edit,
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

            var Save=function(data,callback){
                $.gitAjax({
                    url: config.URL_Save,
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
                Edit: Edit,
                Save: Save,
                ToExcel: ToExcel
            }

        })($, config);
        return dataServer;
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
    	var Server=SysResManager.Server();
        var search=SysResManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            SysResManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=SysResManager.Server();
        var search=SysResManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            SysResManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable:function(result){
        var SysResStr=$("input[name='SysResStr']").val();

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
                { data: 'ResNum',createdCell:function(td, cellData, rowData, row, col){
                    var tr=$(td).parent();
                    $(tr).attr("data-value",JSON.stringify(rowData));
                }},
                { data: 'ResName'},
                { data: 'ParentName'},
                { data: 'ResType',render:function(data,type,full,meta){
                    return git.GetEnumDesc(EResourceType,data);
                }},
                { data: 'CssName'},
                { data: 'Sort'},
                { data: 'Url'},
                { data: 'CreateTime',render:function(data,type,full,meta){
                    return git.JsonToDateTime(data);
                }}
            ],
            aoColumnDefs:[
            ],
            oLanguage:{
                sEmptyTable:"没有查询到任何数据"
            }
        });

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: SysResManager.PageClick });
        }

        //初始化类型
        $("#tabList").find("select").each(function(i,item){
            var value=$(item).attr("data-value");
            $(item).find("option[value='"+value+"']").attr("selected",true);
        });

        //绑定编辑 删除事件
        SysResManager.BindEvent();
    },
    BindEvent:function(){
        $("#tabList").find("a.save").click(function(){
            var tr=$(this).parent().parent();
            var item=JSON.parse($(tr).attr("data-value"));

            var FirstType=$(tr).find("select[name='FirstType']").val();
            var FirstRule=$(tr).find("input[name='FirstRule']").val();
            var FirstLength=$(tr).find("input[name='FirstLength']").val();
            var JoinChar=$(tr).find("input[name='JoinChar']").val();
            var SecondType=$(tr).find("select[name='SecondType']").val();
            var SecondRule=$(tr).find("input[name='SecondRule']").val();
            var SecondLength=$(tr).find("input[name='SecondLength']").val();

            FirstType=git.IsEmpty(FirstType) ? "0":FirstType;
            FirstLength=git.IsEmpty(FirstLength) ? "0":FirstLength;
            SecondType=git.IsEmpty(SecondType) ? "0":SecondType;
            SecondLength=git.IsEmpty(SecondLength) ? "0":SecondLength;
            

            item["FirstType"]=FirstType;
            item["FirstRule"]=FirstRule;
            item["FirstLength"]=FirstLength;
            item["JoinChar"]=JoinChar;
            item["SecondType"]=SecondType;
            item["SecondRule"]=SecondRule;
            item["SecondLength"]=SecondLength;

            var param={};
            param["entity"]=JSON.stringify(item);
            var Server=SysResManager.Server();
            Server.Edit(param,function(result){
                $.jBox.tip(result.Message,"info");
            });

        });
    },
    GetSearch:function(){
        var searchBar=$("div[data-condition='search']");
        var ResNum=searchBar.find("input[name='ResNum']").val();
        var ResName=searchBar.find("input[name='ResName']").val();
        var search={};
        search["ResNum"]=ResNum;
        search["ResName"]=ResName;

        return search;
    },
    ToolBar:function(){
    	//工具栏按钮点击事件
    	$("div.toolbar").find("a.btn").click(function(){
    		var command=$(this).attr("data-command");
            if(command=="Excel"){
    			var Server=SysResManager.Server();
    			var search=SysResManager.GetSearch();
                Server.ToExcel(search,function(result){
    				if(result.Code==1000){
    					var path = unescape(result.Message);
                    	window.location.href = path;
    				}else{
    					$.jBox.info(result.Message, "提示");
    				}
    			});
    		}else if(command=="Refresh"){
                SysResManager.Refresh();
            }

    	});

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            SysResManager.PageClick(1,10);
        });
        
    	//加载默认数据
    	SysResManager.PageClick(1,10);
    },
    SavePower:function(){
        var list=[];
        $("input[type='checkbox']").each(function(i,item){
            if($(item).attr("checked")){
                var value=$(item).val();
                list.push(value);
            }
        });
        console.log(JSON.stringify(list));
        var RoleNum=$("input[name='RoleNum']").val();
        var param={};
        param["List"]=JSON.stringify(list);
        param["RoleNum"]=RoleNum;
        var Server=SysResManager.Server();
        Server.Save(param,function(result){
            $.jBox.tip(result.Message,"info");
        });
    }
}
