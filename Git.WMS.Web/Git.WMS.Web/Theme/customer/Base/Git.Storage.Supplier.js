
var SupplierManager = {
    SupGrid:undefined,
    Server: function () {
        var config = (function () {
            var URL_GetList = "/Client/SupplierAjax/GetList";
            var URL_Delete = "/Client/SupplierAjax/Delete";
            var URL_Add = "/Client/SupplierAjax/Add";
            var URL_ToExcel = "/Client/SupplierAjax/ToExcel";

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
    Dialog:function(SnNum,Command){
        var submit=function(v, h, f){
            if(v){
                var SupNum=h.find('input[name="SupNum"]').val();
                var SnNum=h.find('input[name="SnNum"]').val();
                var SupName=h.find('input[name="SupName"]').val();
                var SupType=h.find('select[name="SupType"]').val();
                var Phone=h.find('input[name="Phone"]').val();
                var Fax=h.find('input[name="Fax"]').val();
                var Email=h.find('input[name="Email"]').val();
                var ContactName=h.find('input[name="ContactName"]').val();
                var Address=h.find('input[name="Address"]').val();
                var Description=h.find('input[name="Description"]').val();
                
                if(git.IsEmpty(SupName)){
                    $.jBox.tip("请输入供应商名","warn");
                    return false;
                }

                if(git.IsEmpty(SupType)){
                    $.jBox.tip("请选择供应商类型","warn");
                    return false;
                }
                var param={};
                var entity={};

                entity["SupNum"]=SupNum;
                entity["SnNum"]=SnNum;
                entity["SupName"]=SupName;
                entity["SupType"]=SupType;
                entity["Phone"]=Phone;
                entity["Fax"]=Fax;
                entity["Email"]=Email;
                entity["ContactName"]=ContactName;
                entity["Address"]=Address;
                entity["Description"]=Description;

                param["Entity"]=JSON.stringify(entity);

                var Server=SupplierManager.Server();
                Server.Add(param,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        var pageIndex=$("#mypager").pager("GetCurrent");
                        if(Command=="Add"){
                            SupplierManager.PageClick(1,pageSize);
                        }else if(Command=="Edit"){
                            SupplierManager.Refresh();
                        }
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });
            }
        }

        if(Command=="Add"){
            $.jBox.open("get:/Client/Supplier/Add", "新增供应商", 650, 350, { buttons: { "确定": true, "关闭": false } ,submit:submit});
        }else if(Command=="Edit"){
            $.jBox.open("get:/Client/Supplier/Add?SnNum="+SnNum, "编辑供应商", 650, 350, { buttons: { "确定": true, "关闭": false } ,submit:submit});
        }
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=SupplierManager.Server();
        var search=SupplierManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            SupplierManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=SupplierManager.Server();
        var search=SupplierManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            SupplierManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable:function(result){
        
        var cols=[
            {title:'供应商编号', name:'SupNum', width: 70, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'供应商名称', name:'SupName', width: 130, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'类型', name:'SupType', width: 70, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.GetEnumDesc(ESupType,data);
            }},
            {title:'电话', name:'Phone', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'传真', name:'Fax', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'Email', name:'Email', width: 90, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'联系人', name:'ContactName', width: 90, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'地址', name:'Address', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'创建时间', name:'CreateTime', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.JsonToDateTime(data);
            }},
            {title:'描述', name:'Description', width: 130, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'操作', name:'ID', width: 120, align: 'left',lockWidth:false,  renderer: function(data,item,rowIndex){
                var html="";
                html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;&nbsp;';
                html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                return html;
            }},
        ];

        if(this.SupGrid==undefined){
            this.SupGrid=$("#tabList").mmGrid({
                cols:cols,
                items:result.Result,
                checkCol:true,
                nowrap:true,
                height:380
            });
            //绑定事件
            SupplierManager.BindEvent();
        }else{
            this.SupGrid.load(result.Result);
        }

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: SupplierManager.PageClick });
        }
    },
    BindEvent:function(){
        this.SupGrid.off("cellSelected").on("cellSelected",function(e, item, rowIndex, colIndex){
            if($(e.target).is("a.edit")){
                var SnNum=item.SnNum;
                SupplierManager.Dialog(SnNum,"Edit");
            }else if($(e.target).is("a.delete")){
                var SnNum=item.SnNum;
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=[];
                        list.push(SnNum);
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=SupplierManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                SupplierManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"warn");
                            }
                        });
                    }
                }
                $.jBox.confirm("确定要删除该供应商吗？", "提示", submit);
            }
        });
    },
    GetSelect:function(){
        var list=[];
        if(this.SupGrid!=undefined){
            var rows=this.SupGrid.selectedRows();
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
        var SupNum=searchBar.find("input[name='SupNum']").val();
        var SupName=searchBar.find("input[name='SupName']").val();
        var Phone=searchBar.find("input[name='Phone']").val();
        var SupType=searchBar.find("select[name='SupType']").val();
        var search={};
        search["SupNum"]=SupNum;
        search["SupName"]=SupName;
        search["Phone"]=Phone;
        search["SupType"]=SupType;
        return search;
    },
    ToolBar:function(){

        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                SupplierManager.Dialog(undefined,command);
            }else if(command=="Edit"){
                var list=SupplierManager.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var SupNum=list[0];
                SupplierManager.Dialog(SupNum,command);
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=SupplierManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=SupplierManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                SupplierManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"warn");
                            }
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=SupplierManager.Server();
                var search=SupplierManager.GetSearch();
                Server.ToExcel(search,function(result){
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                SupplierManager.Refresh();
            }

        });

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            SupplierManager.PageClick(1,10);
        });
        
        //加载默认数据
        SupplierManager.PageClick(1,10);
    }
};