

var CapacityManager={
	TabGrid:undefined,
	Server: function () {
        var config = (function () {
            var URL_GetList = "/Report/CapacityAjax/GetList";
            var URL_ToExcel = "/Report/CapacityAjax/ToExcel";
            return {
                URL_GetList: URL_GetList,
                URL_ToExcel:URL_ToExcel
            };
        })();

        //数据操作服务
        var Manager = (function ($, config) {
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
        return Manager;
    },
    /**
     * 获得查询的参数
     * [GetSearch description]
     */
    GetSearch:function(){
    	var ProductName=$("input[name='ProductName']").val();
    	var BarCode=$("input[name='BarCode']").val();
    	var StorageNum=$("select[name='StorageNum']").val();
    	var SupNum=$("select[name='SupNum']").val();

    	var param={};
    	param["ProductName"]=ProductName;
    	param["BarCode"]=BarCode;
    	param["StorageNum"]=StorageNum;
    	param["SupNum"]=SupNum;

    	return param
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=CapacityManager.Server();
        var search=CapacityManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            $.jBox.closeTip();
            if(result.Code==1){
                CapacityManager.SetTable(result);
            }else{
                $.jBox.tip(result.Message,"warn");
            }
        });
    },
    SetTable:function(result){
    	var cols=[
            {title:'库位编号', name:'LocalBarCode', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'库位名称', name:'LocalName', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'库位类型', name:'LocalType', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.GetEnumDesc(ELocalType,data); 
            }},
            {title:'仓库', name:'StorageName', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'库存数', name:'Num', width: 70, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                if(val==0){
                    return '<span class="label label-success">空</span>';
                }
                return val;
            }},
            // {title:'体积', name:'Volume', width: 70, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
            //     if(item.Num==0){
            //         return '';
            //     }
            //     return git.ToDecimal(val,2);
            // }},
            // {title:'重量', name:'NetWeight', width: 70, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
            //     if(item.Num==0){
            //         return '';
            //     }
            //     return git.ToDecimal(val,2);
            // }},
            // {title:'托盘', name:'PalletNum', width: 70, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
            //     if(item.Num==0){
            //         return '';
            //     }
            //     return val;
            // }},
    	];

    	if(CapacityManager.TabGrid==undefined){
    		CapacityManager.TabGrid=$('#tabList').mmGrid({
    			cols:cols
    			, items: result.Result
                , fullWidthRows: false
                , height:410
                , checkCol:true
                , multiSelect:true
                , nowrap:true
    		});
    	}else{
    		CapacityManager.TabGrid.load(result.Result);
    	}
        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: CapacityManager.PageClick });
        }
    },
    Init:function(){

    	//初始化查询
    	var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            CapacityManager.PageClick(1,10);
        });

        //导出Excel
        $(".tools").find("a[data-command='Excel']").click(function(){
        	var Server=CapacityManager.Server();
            var search=CapacityManager.GetSearch();
            Server.ToExcel(search,function(result){
                if(result.Code==1000){
                    var path = unescape(result.Message);
                    window.location.href = path;
                }else{
                    $.jBox.info(result.Message, "提示");
                }
            });
        });
    	CapacityManager.PageClick(1,10);
    }
}
