
/**
*
*财务管理-应付实付
*
**/

var FinancePayManager = {
    TabGrid:undefined,
    Server: function () {
        var config = (function () {
            var URL_Add = "/Finance/PayAjax/AddRec";
            var URL_GetList = "/Finance/PayManagerAjax/GetList";
            var URL_GetDetail = "/Finance/PayManagerAjax/GetDetail";
            var URL_Delete = "/Finance/PayManagerAjax/Delete";
            var URL_Cancel = "/Finance/PayManagerAjax/Cancel";
            var URL_Audite = "/Finance/PayManagerAjax/Audite";
            var URL_ToExcel = "/Finance/PayManagerAjax/ToExcel";

            var URL_AddPay = "/Finance/PayAjax/AddPay";
            var URL_PayDetail = "/Finance/BillAjax/AddRec";

            return {
                URL_GetList: URL_GetList,
                URL_Add: URL_Add,
                URL_Delete: URL_Delete,
                URL_Cancel: URL_Cancel,
                URL_Audite: URL_Audite,
                URL_ToExcel: URL_ToExcel,

                URL_AddPay: URL_AddPay,
                URL_PayDetail: URL_PayDetail,
            };
        })();

        //数据操作服务
        var dataServer = (function ($, config) {

            //查询分页列表
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

            var Cancel=function(data,callback){
                $.gitAjax({
                    url: config.URL_Cancel,
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

            var Audite=function(data,callback){
                $.gitAjax({
                    url: config.URL_Audite,
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

            var AddPay=function(data,callback){
                $.gitAjax({
                    url: config.URL_AddPay,
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
                Add: Add,
                GetList: GetList,
                Delete: Delete,
                Cancel: Cancel,
                Audite: Audite,
                ToExcel: ToExcel,
                AddPay: AddPay,
            }

        })($, config);
        return dataServer;
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=FinancePayManager.Server();
        var search=FinancePayManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            FinancePayManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=FinancePayManager.Server();
        var search=FinancePayManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            FinancePayManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable:function(result){
        var cols=[
            {title:'流水号', name:'BillNum', width: 65, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'名称', name:'Title', width: 160, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'分类', name:'CateName', width: 80, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'收款方', name:'ToName', width: 150, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'应付金额', name:'Amount', width: 85, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'实付金额', name:'RealPayAmount', width: 100, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'剩余金额', name:'LeavAmount', width: 60, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'状态', name:'Status', width: 70, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                if(val==EFinanceStatusJson.Pass){
                    return '<span class="label label-success">'+git.GetEnumDesc(EFinanceStatus,val)+'</span>';
                }else if(val==EFinanceStatusJson.NotPass){
                    return '<span class="label label-warning">'+git.GetEnumDesc(EFinanceStatus,val)+'</span>';
                }else{
                    return git.GetEnumDesc(EFinanceStatus,val);
                }
            }},
            {title:'创建时间', name:'CreateTime', width: 85, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return git.JsonToDateTime(val);
            }},
            {title:'操作', name:'ID', width: 150, align: 'left',lockWidth:false,  renderer: function(val,item,rowIndex){
                var html="";
                if(item.Status==EFinanceStatusJson.Wait || item.Status==EFinanceStatusJson.NotPass){
                    html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;';
                }
                if(item.Status==EFinanceStatusJson.Wait || item.Status==EFinanceStatusJson.NotPass){
                    html+='<a class="audite" href="javascript:void(0)">审核</a>&nbsp;';
                }
                html+='<a class="view" href="javascript:void(0)">查看</a>&nbsp;';

                if(item.Status==EFinanceStatusJson.Wait || item.Status==EFinanceStatusJson.NotPass){
                    html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                }
                
                if(item.Status==EFinanceStatusJson.Pass || item.Status==EFinanceStatusJson.PayPart){
                 html+='<a class="pay" href="javascript:void(0)">收款</a>&nbsp;';
                }
                return html;
            }}
        ];
        
        if(this.TabGrid==undefined){
            this.TabGrid=$("#tabList").mmGrid({
                cols:cols,
                items:result.Result,
                checkCol:true,
                nowrap:true,
                height:450
            });
            //绑定编辑 删除事件
            FinancePayManager.BindEvent();
        }else{
            this.TabGrid.load(result.Result);
        }

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: FinancePayManager.PageClick });
        }
    },
    BindEvent:function(){

        this.TabGrid.off("cellSelected").on("cellSelected",function(e, item, rowIndex, colIndex){
            if($(e.target).is("a.edit")){
                var SN=item.SnNum;
                FinancePayManager.Detail(SN,"Edit");
            }else if($(e.target).is("a.audite")){
                var SN=item.SnNum;
                FinancePayManager.Detail(SN,"Audite");
            }else if($(e.target).is("a.view")){
                var SN=item.SnNum;
                FinancePayManager.Detail(SN,"View");
            }else if($(e.target).is("a.pay")){
                var SN=item.SnNum;
                FinancePayManager.Pay("",SN,"Add");
            }else if($(e.target).is("a.delete")){
                var SN=item.SnNum;
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=[];
                        list.push(SN);
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=FinancePayManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                FinancePayManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"warn");
                            }
                        });
                    }
                }
                $.jBox.confirm("该操作不可撤销,确定要删除吗？", "提示", submit);
            }
        });
    },
    GetSelect:function(){
        var list=[];
        if(this.TabGrid!=undefined){
            var rows=this.TabGrid.selectedRows();
            if(rows!=undefined && rows.length>0){
                for(var i=0;i<rows.length;i++){
                    list.push(rows[i].SnNum);
                }
            }
        }
        return list;
    },
    GetRows:function(){
        var list=[];
        if(this.TabGrid!=undefined){
            var rows=this.TabGrid.selectedRows();
            if(rows!=undefined && rows.length>0){
                for(var i=0;i<rows.length;i++){
                    list.push(rows[i]);
                }
            }
        }
        return list;
    },
    GetSearch:function(){
        var searchBar=$("div[data-condition='search']");

        var PayNum=searchBar.find("input[name='PayNum']").val();
        var CateNum=searchBar.find("select[name='CateNum']").val();
        var ToName=searchBar.find("input[name='ToName']").val();
        var Title=searchBar.find("input[name='Title']").val();
        var ContractNum=searchBar.find("input[name='ContractNum']").val();
        var Status=$("div[data-group='Status']").find("button.disabled").attr("value");
        var BeginTime=searchBar.find("input[name='BeginTime']").val();
        var EndTime=searchBar.find("input[name='EndTime']").val();

        var search={};
        search["PayNum"]=PayNum;
        search["CateNum"]=CateNum;
        search["ToName"]=ToName;
        search["Title"]=Title;
        search["ContractNum"]=ContractNum;
        search["Status"]=Status;
        search["BeginTime"]=BeginTime;
        search["EndTime"]=EndTime;

        return search;
    },
    Detail:function(SnNum,Command){
    	var currentSnNum=SnNum;
        var submit=function(v,h,f){
            if(v=="1"){
                var PayNum=h.find('input[name="PayNum"]').val();
                var SnNum=h.find('input[name="SnNum"]').val();
                var CateNum=h.find('select[name="CateNum"]').val();
                var FromName=h.find('input[name="FromName"]').val();
                var ToName=h.find('input[name="ToName"]').val();
                var Amount=h.find('input[name="Amount"]').val();
                var PrePayCount=h.find('input[name="PrePayCount"]').val();
                var Remark=h.find('input[name="Remark"]').val();
                var Title=h.find('input[name="Title"]').val();
                var Remark=h.find('input[name="Remark"]').val();
                var LastTime=h.find('input[name="LastTime"]').val();
                var Title=h.find('input[name="Title"]').val();

                var param={};
                param["PayNum"]=PayNum;
                param["SnNum"]=SnNum;
                param["CateNum"]=CateNum;
                param["FromName"]=FromName;
                param["ToName"]=ToName;
                param["Amount"]=Amount;
                param["PrePayCount"]=PrePayCount;
                param["Status"]=1; //审核通过
                param["Remark"]=Remark;
                param["LastTime"]=LastTime;
                param["Title"]=Title;

                var Server=FinancePayManager.Server();
                Server.Add(param,function(result){
                    $.jBox.tip(result.Message,"success");
                    var pageSize=$("#mypager").pager("GetPageSize");
                    FinancePayManager.PageClick(1,pageSize);
                }); 
            }else if(v=="2"){
                
            }else if(v=="3"){
                var param={};
                param["SnNum"]=currentSnNum;
                param["Status"]=2; //审核通过
                var Server=FinancePayManager.Server();
                Server.Audite(param,function(result){
                    $.jBox.tip(result.Message,"success");
                    var pageSize=$("#mypager").pager("GetPageSize");
                    FinancePayManager.PageClick(1,pageSize);
                }); 
            }else if(v=="4"){
            	var param={};
                param["SnNum"]=currentSnNum;
                param["Status"]=3; //审核通过
                var Server=FinancePayManager.Server();
                Server.Audite(param,function(result){
                    $.jBox.tip(result.Message,"success");
                    var pageSize=$("#mypager").pager("GetPageSize");
                    FinancePayManager.PageClick(1,pageSize);
                }); 
            }
        }
        
        // 1确定 2 关闭  3 审核通过 4 审核不通过
        if(Command=="View"){
            $.jBox.open("get:/Finance/Pay/AddRec?SnNum="+SnNum, "查看应付", 670, 350, { buttons: {"关闭":2 } ,submit:submit,loaded:function(h){
                h.find('input,select').attr('disabled',true);
            }}); 
        }else if(Command=="Audite"){
            $.jBox.open("get:/Finance/Pay/AddRec?SnNum="+SnNum, "审核应付", 670, 350, { buttons: {"审核通过": 3, "审核不通过": 4,"关闭":2 } ,submit:submit,loaded:function(h){
                h.find('input,select').attr('disabled',true);
            }}); 
        }else if(Command=="Add"){
        	$.jBox.open("get:/Finance/Pay/AddRec", "新增应付", 670, 350, { buttons: {"确定": 1, "关闭":2 } ,submit:submit,loaded:function(h){
                
            }}); 
        }else if(Command=="Edit"){
        	$.jBox.open("get:/Finance/Pay/AddRec?SnNum="+SnNum, "编辑应付", 670, 350, { buttons: {"确定": 1, "关闭":2 } ,submit:submit,loaded:function(h){
                
            }}); 
        }
    },
    Pay:function(SnNum,BillSnNum,Command){
        var currentSnNum=SnNum;
        var submit=function(v,h,f){
            if(v=="1"){
                var PayNum=h.find('input[name="PayNum"]').val();
                var SnNum=h.find('input[name="SnNum"]').val();
                var PayType=h.find('select[name="PayType"]').val();
                var BankName=h.find('input[name="BankName"]').val();
                var Amount=h.find('input[name="Amount"]').val();
                var PayTime=h.find('input[name="PayTime"]').val();
                var Remark=h.find('input[name="Remark"]').val();
                var BillSnNum=h.find('input[name="BillSnNum"]').val();

                var param={};
                param["PayNum"]=PayNum;
                param["SnNum"]=SnNum;
                param["PayType"]=PayType;
                param["BankName"]=BankName;
                param["Amount"]=Amount;
                param["PayTime"]=PayTime;
                param["Remark"]=Remark;
                param["BillSnNum"]=BillSnNum;

                var Server=FinancePayManager.Server();
                Server.AddPay(param,function(result){
                    $.jBox.tip(result.Message,"success");
                    var pageSize=$("#mypager").pager("GetPageSize");
                    FinancePayManager.PageClick(1,pageSize);
                }); 
            }else if(v=="2"){
                
            }
        }
        
        // 1确定 2 关闭
        if(Command=="Add"){
            $.jBox.open("get:/Finance/Pay/AddPay?BillSnNum="+BillSnNum, "新增实付", 670, 300, { buttons: {"确定": 1, "关闭":2 } ,submit:submit,loaded:function(h){
                
            }}); 
        }else if(Command=="Edit"){
            $.jBox.open("get:/Finance/Pay/AddPay?BillSnNum="+BillSnNum+"&SnNum="+SnNum, "编辑实付", 670, 300, { buttons: {"确定": 1, "关闭":2 } ,submit:submit,loaded:function(h){
                
            }}); 
        }
    },
    ToolBar:function(){
        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                FinancePayManager.Detail(undefined,"Add");
            }else if(command=="Edit"){
                var list=FinancePayManager.GetRows();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var item=list[0];
                if(item.Status==EFinanceStatusJson.Wait || item.Status==EFinanceStatusJson.NotPass){
                    var SN=list[0].SnNum;
                    FinancePayManager.Detail(SN,"Edit");
                }else{
                    $.jBox.tip("不能编辑该项","warn");
                }
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=FinancePayManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=FinancePayManager.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            FinancePayManager.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=FinancePayManager.Server();
                var search=FinancePayManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                FinancePayManager.Refresh();
            }

        });

        //搜索 高级搜索按钮
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            FinancePayManager.PageClick(1,10);
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
            searchBar.find("input[name='PayNum']").val("");
            searchBar.find("select[name='CateNum']").val("");
            searchBar.find("input[name='ToName']").val("");
            searchBar.find("input[name='Title']").val("");
            searchBar.find("input[name='ContractNum']").val("");
            searchBar.find("input[name='BeginTime']").val("");
            searchBar.find("input[name='EndTime']").val("");
        });


        //状态按钮处理
        $("div[data-group='Status']").find("button").click(function(){
            $("div[data-group='Status']").find("button").removeClass("disabled");
            $(this).addClass("disabled");
            FinancePayManager.PageClick(1,10);
        });
        
        //加载默认数据
        FinancePayManager.PageClick(1,10);
    }
}


