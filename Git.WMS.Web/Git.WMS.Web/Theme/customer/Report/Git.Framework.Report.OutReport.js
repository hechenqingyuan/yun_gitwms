
var OutReport={
	Server: function () {
        var config = (function () {
            var URL_GetList = "/Report/StoreAjax/OutReportList";
            var URL_ToExcel = "/Report/StoreAjax/ToOutReportListExcel";
            return {
                URL_GetList: URL_GetList,
                URL_ToExcel:URL_ToExcel
            };
        })();

        //数据操作服务
        var OutReport = (function ($, config) {
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

            //导出Excel
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
                ToExcel:ToExcel
            }

        })($, config);
        return OutReport;
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=OutReport.Server();
        var BarCode=$("input[name='BarCode']").val();
        var ProductName=$("input[name='ProductName']").val();
        var CusNum=$("input[name='CusNum']").val();
        var CusName=$("input[name='CusName']").val();
        var BeginTime=$("input[name='BeginTime']").val();
        var EndTime=$("input[name='EndTime']").val();

        var search={};
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        search["BarCode"]=BarCode;
        search["ProductName"]=ProductName;
        search["CusNum"]=CusNum;
        search["CusName"]=CusName;
        search["BeginTime"]=BeginTime;
        search["EndTime"]=EndTime;
        Server.GetList(search,function(result){
            OutReport.SetTable(result);
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
                { data: 'ProductName'},
                { data: 'BarCode'},
                { data: 'Size'},
                { data: 'BatchNum'},
                { data: 'OrderNum'},
                { data: 'Num',render:function(data,type,full,meta){
                    return git.ToDecimal(data,2);
                }},
                { data: 'OutPrice',render:function(data,type,full,meta){
                    return git.ToDecimal(data,2);
                }},
                { data: 'Amount',render:function(data,type,full,meta){
                    return git.ToDecimal(data,2);
                }},
                { data: 'CusName'},
                { data: 'CreateTime',render:function(data,type,full,meta){
                    return git.JsonToDateTime(data);
                }},
            ],
            oLanguage:{
                sEmptyTable:"没有查询到任何数据"
            }
        });
        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: OutReport.PageClick });
        }
    },
    Init:function(){

    	//初始化查询
    	var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            OutReport.PageClick(1,10);
        });

        //导出Excel
        $(".tools").find("a.icon-download[data-command='Excel']").click(function(){
        	var Server=OutReport.Server();
            var BarCode=$("input[name='BarCode']").val();
            var ProductName=$("input[name='ProductName']").val();
            var CusNum=$("input[name='CusNum']").val();
            var CusName=$("input[name='CusName']").val();
            var BeginTime=$("input[name='BeginTime']").val();
            var EndTime=$("input[name='EndTime']").val();

            var search={};
            search["BarCode"]=BarCode;
            search["ProductName"]=ProductName;
            search["CusNum"]=CusNum;
            search["CusName"]=CusName;
            search["BeginTime"]=BeginTime;
            search["EndTime"]=EndTime;

            Server.ToExcel(search,function(result){
                if(result.Code==1000){
                    var path = unescape(result.Message);
                    window.location.href = path;
                }else{
                    $.jBox.info(result.Message, "提示");
                }
            });
        });
    	OutReport.PageClick(1,10);
    }
}