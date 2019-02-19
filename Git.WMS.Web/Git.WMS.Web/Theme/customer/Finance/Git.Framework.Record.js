
/**
*
*财务管理-应付实付
*
**/

var FinanceRecordManager = {
    TabGrid:undefined,
    Server: function () {
        var config = (function () {
            var URL_GetList = "/Finance/RecordAjax/GetList";
            var URL_ToExcel = "/Finance/RecordAjax/ToExcel";

            return {
                URL_GetList: URL_GetList,
                URL_ToExcel: URL_ToExcel,
            };
        })();

        //数据操作服务
        var dataServer = (function ($, config) {

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

            return {
                GetList: GetList,
                ToExcel: ToExcel,
            }

        })($, config);
        return dataServer;
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=FinanceRecordManager.Server();
        var search=FinanceRecordManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            FinanceRecordManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=FinanceRecordManager.Server();
        var search=FinanceRecordManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            FinanceRecordManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable:function(result){

        var cols=[
            {title:'流水号', name:'PayNum', width: 65, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'名称', name:'Title', width: 160, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'分类', name:'CateName', width: 80, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'收款方/付款方', name:'SourceObject', width: 150, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'类型', name:'BillType', width: 85, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return git.GetEnumDesc(EFinanceType,val);
            }},
            {title:'付款金额', name:'Amount', width: 100, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'付款时间', name:'PayTime', width: 85, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return git.JsonToDateTime(val);
            }},
            {title:'付款方式', name:'PayType', width: 90, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return git.GetEnumDesc(EPayType,val);
            }},
            {title:'付款机构', name:'BankName', width: 100, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
        ];
        
        if(this.TabGrid==undefined){
            this.TabGrid=$("#tabList").mmGrid({
                cols:cols,
                items:result.Result,
                checkCol:true,
                nowrap:true,
                height:450
            });
        }else{
            this.TabGrid.load(result.Result);
        }

        /*$("#tabList").DataTable({
            destroy: true,
            data:result.Result,
            paging:false,
            searching:false,
            scrollX: false,
            bAutoWidth:false,
            bInfo:false,
            ordering:false,
            columns: [
                { data: 'PayNum'},
                { data: 'Title'},
                { data: 'CateName'},
                { data: 'SourceObject'},
                { data: 'BillType',render:function(data,type,full,meta){
                    return git.GetEnumDesc(EFinanceType,data);
                }},
                { data: 'Amount',render:function(data,type,full,meta){
                    return git.ToDecimal(data,2);
                }},
                { data: 'PayTime',render:function(data,type,full,meta){
                    return git.JsonToDateTime(data);
                }},
                { data: 'PayType',render:function(data,type,full,meta){
                    return git.GetEnumDesc(EPayType,data);
                }},
                { data: 'BankName'},
            ],
            aoColumnDefs:[
                
            ],
            oLanguage:{
                sEmptyTable:"没有查询到任何数据"
            }
        });*/

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: FinanceRecordManager.PageClick });
        }
    },
    GetSearch:function(){
        var searchBar=$("div[data-condition='search']");

        var BillNum=searchBar.find("input[name='BillNum']").val();
        var CateNum=searchBar.find("select[name='CateNum']").val();
        var Title=searchBar.find("input[name='Title']").val();
        var BeginTime=searchBar.find("input[name='BeginTime']").val();
        var EndTime=searchBar.find("input[name='EndTime']").val();
        
        var search={};
        search["BillNum"]=BillNum;
        search["CateNum"]=CateNum;
        search["Title"]=Title;
        search["BeginTime"]=BeginTime;
        search["EndTime"]=EndTime;

        return search;
    },
    ToolBar:function(){
        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");
            if(command=="Add"){
            }else if(command=="Edit"){
            }else if(command=="Delete"){
            }else if(command=="Excel"){
                var Server=FinanceRecordManager.Server();
                var search=FinanceRecordManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                FinanceRecordManager.Refresh();
            }
        });

        //搜索 高级搜索按钮
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            FinanceRecordManager.PageClick(1,10);
        });
        searchBar.find("button[data-command='Advanced']").click(function(){
            var display=$("div.formsearch").css("display");
            if(display=="block"){
                $("div.formsearch").hide(300);
            }else{
                $("div.formsearch").show(300);
            }
        });
        searchBar.find("button[data-command='Clear']").click(function(){
            //清空搜索条件
            searchBar.find("input[name='BillNum']").val("");
            searchBar.find("select[name='CateNum']").val("");
            searchBar.find("input[name='ToName']").val("");
            searchBar.find("input[name='Title']").val("");
            searchBar.find("input[name='ContractNum']").val("");
            searchBar.find("input[name='BeginTime']").val("");
            searchBar.find("input[name='EndTime']").val("");
        });
        
        //加载默认数据
        FinanceRecordManager.PageClick(1,10);
    }
}


