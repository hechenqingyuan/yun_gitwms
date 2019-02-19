
var BalanceBook={
    TabGrid:undefined,
	Server: function () {
        var config = (function () {
            var URL_GetList = "/Report/BalanceBookAjax/GetList";
            var URL_ToExcel = "/Report/BalanceBookAjax/ToExcel";
            return {
                URL_GetList: URL_GetList,
                URL_ToExcel:URL_ToExcel
            };
        })();

        //数据操作服务
        var BalanceBook = (function ($, config) {
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
        return BalanceBook;
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=BalanceBook.Server();
        var search=BalanceBook.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            BalanceBook.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable:function(result){

        var cols=[
            {title:'日期', name:'Day', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'产品名称', name:'ProductName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'产品编号', name:'BarCode', width: 75, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'产品规格', name:'Size', width: 130, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'产品批次', name:'BatchNum', width: 75, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'单位', name:'UnitName', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'仓库', name:'StorageName', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'期初', name:'BeginNum', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'入库', name:'InNum', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'出库', name:'OutNum', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'期末', name:'EndNum', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
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
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: BalanceBook.PageClick });
        }
    },
    GetSearch:function(){
        var ProductName=$("input[name='ProductName']").val();
        var BarCode=$("input[name='BarCode']").val();
        var BatchNum=$("input[name='BatchNum']").val();
        var StorageNum=$("select[name='StorageNum']").val();
        var Day=$("input[name='Day']").val();

        var search={};
        search["BarCode"]=BarCode;
        search["ProductName"]=ProductName;
        search["BatchNum"]=BatchNum;
        search["StorageNum"]=StorageNum;
        search["Day"]=Day;

        return search;
    },
    Init:function(){

    	//初始化查询
    	var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            BalanceBook.PageClick(1,10);
        });

        //导出Excel
        $(".toolbar").find("a[data-command='Excel']").click(function(){
        	var Server=BalanceBook.Server();
            var search=BalanceBook.GetSearch();
            Server.ToExcel(search,function(result){
                if(result.Code==1000){
                    var path = unescape(result.Message);
                    window.location.href = path;
                }else{
                    $.jBox.info(result.Message, "提示");
                }
            });
        });
    	BalanceBook.PageClick(1,10);
    }
}