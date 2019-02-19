
var SaleLocation={
    TabGrid:undefined,
	Server: function () {
        var config = (function () {
            var URL_GetList = "/Report/StoreAjax/GetProductStoreList";
            var URL_ToExcel = "/Report/StoreAjax/ToProductStoreExcel";
            return {
                URL_GetList: URL_GetList,
                URL_ToExcel:URL_ToExcel
            };
        })();

        //数据操作服务
        var StoreReport = (function ($, config) {
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
        return StoreReport;
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=SaleLocation.Server();
        var search=SaleLocation.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            SaleLocation.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable:function(result){

        var cols=[
            {title:'产品名称', name:'ProductName', width: 200, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'产品编号', name:'BarCode', width: 75, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'产品规格', name:'Size', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'类别', name:'CateName', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'库存', name:'Num', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'单位', name:'UnitName', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
        ];
        
        if(this.TabGrid==undefined){
            this.TabGrid=$("#tabList").mmGrid({
                cols:cols,
                items:result.Result,
                checkCol:false,
                nowrap:true,
                height:450
            });
        }else{
            this.TabGrid.load(result.Result);
        }

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: SaleLocation.PageClick });
        }
    },
    GetSearch:function(){
        var ProductName=$("input[name='ProductName']").val();
        var BarCode=$("input[name='BarCode']").val();
        
        var search={};
        search["BarCode"]=BarCode;
        search["ProductName"]=ProductName;

        return search;
    },
    Init:function(){

    	//初始化查询
    	var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            SaleLocation.PageClick(1,10);
        });

        //导出Excel
        $(".toolbar").find("a[data-command='Excel']").click(function(){
        	var Server=SaleLocation.Server();
            var search=SaleLocation.GetSearch();
            Server.ToExcel(search,function(result){
                if(result.Code==1000){
                    var path = unescape(result.Message);
                    window.location.href = path;
                }else{
                    $.jBox.info(result.Message, "提示");
                }
            });
        });
    	SaleLocation.PageClick(1,10);
    }
}