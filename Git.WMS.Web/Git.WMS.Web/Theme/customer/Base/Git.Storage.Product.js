
/**
*
*产品管理功能
*
**/

var ProductManager = {
    TabProduct:undefined,
    Server: function () {
        var config = (function () {
            var URL_GetList = "/Storage/ProductAjax/GetList";
            var URL_Delete = "/Storage/ProductAjax/Delete";
            var URL_Add = "/Storage/ProductAjax/Add";
            var URL_ToExcel = "/Storage/ProductAjax/ToExcel";

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
                var SnNum=h.find('input[name="SnNum"]').val();
                var BarCode=h.find('input[name="BarCode"]').val();
                var ProductName=h.find('input[name="ProductName"]').val();
                var FactoryNum=h.find('input[name="FactoryNum"]').val();
                var InCode=h.find('input[name="InCode"]').val();
                var UnitNum=h.find('select[name="UnitNum"]').val();
                var CateNum=h.find('select[name="CateNum"]').val();
                var Size=h.find('input[name="Size"]').val();
                var AvgPrice=h.find('input[name="AvgPrice"]').val();
                var DefaultLocal=h.find('input[name="DefaultLocal"]').val();
                var StorageName=h.find('input[name="StorageName"]').val();
                var LocalName=h.find('input[name="LocalName"]').val();
                var StorageNum=h.find('select[name="StorageNum"]').val();
                var SupName=h.find('input[name="SupName"]').val();
                var SupNum=h.find('input[name="SupNum"]').val();
                var CusName=h.find('input[name="CusName"]').val();
                var CusNum=h.find('input[name="CusNum"]').val();
                var Display=h.find('input[name="Display"]').val();
                var Remark=h.find('input[name="Remark"]').val();
                var MinNum=h.find('input[name="MinNum"]').val();
                var MaxNum=h.find('input[name="MaxNum"]').val();

                if(git.IsEmpty(ProductName)){
                    $.jBox.tip("请输入产品名称","warn");
                    return false;
                }
                if(git.IsEmpty(UnitNum)){
                    $.jBox.tip("请选择产品单位","warn");
                    return false;
                }
                if(git.IsEmpty(CateNum)){
                    $.jBox.tip("请选择产品类别","warn");
                    return false;
                }
                
                var param={};
                param["SnNum"]=SnNum;
                param["BarCode"]=BarCode;
                param["ProductName"]=ProductName;
                param["FactoryNum"]=FactoryNum;
                param["InCode"]=InCode;
                param["UnitNum"]=UnitNum;
                param["CateNum"]=CateNum;
                param["Size"]=Size;
                param["AvgPrice"]=AvgPrice;
                param["DefaultLocal"]=DefaultLocal;
                param["StorageName"]=StorageName;
                param["LocalName"]=LocalName;
                param["StorageNum"]=StorageNum;
                param["SupName"]=SupName;
                param["SupNum"]=SupNum;
                param["CusName"]=CusName;
                param["CusNum"]=CusNum;
                param["Display"]=Display;
                param["Remark"]=Remark;
                param["MinNum"]=MinNum;
                param["MaxNum"]=MaxNum;

                var entity={};
                entity["Entity"]=JSON.stringify(param);
                var Server=ProductManager.Server();
                Server.Add(entity,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        var pageIndex=$("#mypager").pager("GetCurrent");
                        if(Command=="Add"){
                            ProductManager.PageClick(1,pageSize);
                        }else if(Command=="Edit"){
                            ProductManager.Refresh();
                        }
                        $.jBox.close();
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });
            }
        }

        if(Command=="Add"){
            $.jBox.open("get:/Storage/Product/Add", "新增产品", 650, 500, { buttons: { "确定": true, "关闭": false } ,submit:submit,loaded:function(h){
                ProductManager.SelectLocation(h);
                ProductManager.SelectCustomer(h);
                ProductManager.SelectSupplier(h);
            }});
        }else if(Command=="Edit"){
            $.jBox.open("get:/Storage/Product/Add?SnNum="+SN, "编辑产品", 650, 500, { buttons: { "确定": true, "关闭": false } ,submit:submit,loaded:function(h){
                ProductManager.SelectLocation(h);
                ProductManager.SelectCustomer(h);
                ProductManager.SelectSupplier(h);
            }});
        }
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=ProductManager.Server();
        var search=ProductManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            ProductManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=ProductManager.Server();
        var search=ProductManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            ProductManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable:function(result){

        var cols=[
            {title:'产品编号', name:'BarCode', width: 65, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'产品名称', name:'ProductName', width: 150, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'厂商编码', name:'FactoryNum', width: 65, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'内部编码', name:'InCode', width: 65, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'规格', name:'Size', width: 85, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'类别', name:'CateName', width: 100, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'存储单位', name:'UnitName', width: 60, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'预警(下)', name:'MinNum', width: 50, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'预警(上)', name:'MaxNum', width: 50, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'包装类型', name:'IsSingle', width: 60, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return git.GetEnumDesc(EProductPackage,val);
            }},
            {title:'价格', name:'AvgPrice', width: 50, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'重量', name:'NetWeight', width: 50, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'显示名', name:'Display', width: 100, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'默认供应商', name:'SupName', width: 100, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'默认客户', name:'CusName', width: 100, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'备注', name:'Remark', width: 100, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'操作', name:'ID', width: 100, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                var html="";
                html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;&nbsp;';
                html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                return html;
            }}
        ];
        
        if(this.TabProduct==undefined){
            this.TabProduct=$("#tabList").mmGrid({
                cols:cols,
                items:result.Result,
                checkCol:true,
                nowrap:true,
                height:450
            });
            ProductManager.BindEvent();
        }else{
            this.TabProduct.load(result.Result);
        }

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: ProductManager.PageClick });
        }
    },
    BindEvent:function(){

        this.TabProduct.off("cellSelected").on("cellSelected",function(e, item, rowIndex, colIndex){
            
            if($(e.target).is("a.edit")){
                var SN=item.SnNum;
                ProductManager.Dialog(SN,"Edit")
            }else if($(e.target).is("a.delete")){
                var SN=item.SnNum;
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=[];
                        list.push(SN);
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=ProductManager.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            ProductManager.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);
            }
        });
    },
    GetSelect:function(){
        var list=[];
        
        if(this.TabProduct!=undefined){
            var rows=this.TabProduct.selectedRows();
            if(rows!=undefined && rows.length>0){
                for(var i=0;i<rows.length;i++){
                    list.push(rows[i].SnNum);
                }
            }
        }
        return list;
    },
    GetSearch:function(){
        var searchBar=$("div[data-condition='search']");
        var BarCode=searchBar.find("input[name='BarCode']").val();
        var ProductName=searchBar.find("input[name='ProductName']").val();
        var CateNum=searchBar.find("select[name='CateNum']").val();
        var search={};
        search["BarCode"]=BarCode;
        search["ProductName"]=ProductName;
        search["CateNum"]=CateNum;
        return search;
    },
    SelectLocation:function(h){
        //选择库位对话框
        h.find("input[name='LocalName']").LocalDialog({Mult:false,StorageSearch:true,callBack:function(result){
            if(result!=undefined){
                h.find("input[name='LocalName']").val(result.LocalName);
                h.find("input[name='DefaultLocal']").val(result.LocalNum);
                h.find("select[name='StorageNum']").find("option[value='"+result.StorageNum+"']").attr("selected",true);
            }
        }});
    },
    SelectCustomer:function(h){
        //选择库位对话框
        h.find("input[name='CusName']").CustomerDialog({Mult:false,callBack:function(result){
            if(result!=undefined){
                h.find("input[name='CusNum']").val(result.CustomerSN);
                h.find("input[name='CusName']").val(result.CusName);
            }
        }});
    },
    SelectSupplier:function(h){
        //选择库位对话框
        h.find("input[name='SupName']").SupplierDialog({Mult:false,callBack:function(result){
            if(result!=undefined){
                h.find("input[name='SupName']").val(result.SupName);
                h.find("input[name='SupNum']").val(result.SnNum);
            }
        }});
    },
    ToolBar:function(){

        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                ProductManager.Dialog(undefined,command);
            }else if(command=="Edit"){
                var list=ProductManager.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var SN=list[0];
                ProductManager.Dialog(SN,command);
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=ProductManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=ProductManager.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            ProductManager.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=ProductManager.Server();
                var search=ProductManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                ProductManager.Refresh();
            }

        });

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            ProductManager.PageClick(1,10);
        });
        
        //监听回车事件,用于扫描
        $("input[name='BarCode']").keydown(function(event){
            if (event.keyCode == 13) {    
                var value=$(this).val();
                if(!git.IsEmpty(value)){
                    ProductManager.PageClick(1,10);
                    setTimeout(function(){
                        $("input[name='BarCode']").val("");
                        $("input[name='BarCode']").focus();
                    },300);
                }
            }    
        });

        $("input[name='BarCode']").focus();
        
        //加载默认数据
        ProductManager.PageClick(1,10);
    }
}

