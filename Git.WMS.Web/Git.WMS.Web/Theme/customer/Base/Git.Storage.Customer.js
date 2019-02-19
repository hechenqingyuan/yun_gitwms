
var CustomerManager = {
    CusGrid:undefined,
    Server: function () {
        var config = (function () {
            var URL_GetList = "/Client/CustomerAjax/GetList";
            var URL_Delete = "/Client/CustomerAjax/Delete";
            var URL_Add = "/Client/CustomerAjax/Add";
            var URL_ToExcel = "/Client/CustomerAjax/ToExcel";
            var URL_GetAddList = "/Client/CustomerAjax/GetAddList";
            var URL_DelAdd = "/Client/CustomerAjax/DelAdd";
            var URL_AddAddress = "/Client/CustomerAjax/AddAddress";

            return {
                URL_GetList: URL_GetList,
                URL_Delete: URL_Delete,
                URL_Add: URL_Add,
                URL_ToExcel: URL_ToExcel,
                URL_GetAddList: URL_GetAddList,
                URL_DelAdd: URL_DelAdd,
                URL_AddAddress: URL_AddAddress,
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

            var GetAddList=function(data,callback){
                $.gitAjax({
                    url: config.URL_GetAddList,
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

            var DelAdd=function(data,callback){
                $.gitAjax({
                    url: config.URL_DelAdd,
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

            var AddAddress=function(data,callback){
                $.gitAjax({
                    url: config.URL_AddAddress,
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
                ToExcel: ToExcel,
                GetAddList: GetAddList,
                DelAdd: DelAdd,
                AddAddress: AddAddress,
            }

        })($, config);
        return dataServer;
    },
    Dialog:function(SnNum,Command){
        var submit=function(v, h, f){
            if(v==2){
                var CusNum=h.find('input[name="CusNum"]').val();
                var SnNum=h.find('input[name="SnNum"]').val();
                var CusName=h.find('input[name="CusName"]').val();
                var Remark=h.find('input[name="Remark"]').val();
                var Fax=h.find('input[name="Fax"]').val();
                var Email=h.find('input[name="Email"]').val();
                var Phone=h.find('input[name="Phone"]').val();
                
                if(git.IsEmpty(CusName)){
                    $.jBox.tip("请输入客户名","warn");
                    return false;
                }

                var param={};
                var entity={};

                entity["CusNum"]=CusNum;
                entity["SnNum"]=SnNum;
                entity["CusName"]=CusName;
                entity["Remark"]=Remark;
                entity["Phone"]=Phone;
                entity["Fax"]=Fax;
                entity["Email"]=Email;
                entity["Phone"]=Phone;
                param["Entity"]=JSON.stringify(entity);

                var Server=CustomerManager.Server();
                Server.Add(param,function(result){
                    var pageSize=$("#mypager").pager("GetPageSize");
                    var pageIndex=$("#mypager").pager("GetCurrent");
                    if(Command=="Add"){
                        CustomerManager.PageClick(1,pageSize);
                    }else if(Command=="Edit"){
                        CustomerManager.Refresh();
                    }
                    $.jBox.tip(result.Message,"success");
                });
            }else if(v==1){
                var item=new AddressManager(h,SnNum);
                item.Add(undefined,"Add")
                return false;
            }else if(v==3){
                return true;
            }
        }

        if(Command=="Add"){
            $.jBox.open("get:/Client/Customer/Add", "新增客户", 750, 450, { buttons: {"新增地址":1, "确定": 2, "关闭": 3 } ,submit:submit,loaded:function(h){
                var item=new AddressManager(h,SnNum);
                item.Load();
            }});
        }else if(Command=="Edit"){
            $.jBox.open("get:/Client/Customer/Add?SnNum="+SnNum, "编辑客户", 750, 450, { buttons: { "新增地址":1, "确定": 2, "关闭": 3 } ,submit:submit,loaded:function(h){
                var item=new AddressManager(h,SnNum);
                item.Load();
            }});
        }
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=CustomerManager.Server();
        var search=CustomerManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            $.jBox.closeTip();
            if(result.Code==1){
                CustomerManager.SetTable(result);
            }else{
                $.jBox.tip(result.Message,"warn");
            }
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=CustomerManager.Server();
        var search=CustomerManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            $.jBox.closeTip();
            if(result.Code==1){
                CustomerManager.SetTable(result);
            }else{
                $.jBox.tip(result.Message,"warn");
            }
        });
    },
    SetTable:function(result){
        var cols=[
            {title:'客户编号', name:'CusNum', width: 70, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'客户名称', name:'CusName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'地址', name:'Address', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'联系人', name:'Contact', width: 70, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'电话', name:'Phone', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'备注', name:'Remark', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'邮箱', name:'Email', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'传真', name:'Fax', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'创建时间', name:'CreateTime', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.JsonToDateTime(data);
            }},
            {title:'操作', name:'ID', width: 120, align: 'left',lockWidth:false,  renderer: function(data,item,rowIndex){
                var html="";
                html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;&nbsp;';
                html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                return html;
            }},
        ];

        if(this.CusGrid==undefined){
            this.CusGrid=$("#tabList").mmGrid({
                cols:cols,
                items:result.Result,
                checkCol:true,
                nowrap:true,
                height:380
            });
            //绑定事件
            CustomerManager.BindEvent();
        }else{
            this.CusGrid.load(result.Result);
        }

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: CustomerManager.PageClick });
        }
    },
    BindEvent:function(){
        this.CusGrid.off("cellSelected").on("cellSelected",function(e, item, rowIndex, colIndex){
            if($(e.target).is("a.edit")){
                var CusNum=item.CustomerSN;
                CustomerManager.Dialog(CusNum,"Edit");
            }else if($(e.target).is("a.delete")){
                var CusNum=item.CustomerSN;
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=[];
                        list.push(CusNum);
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=CustomerManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                CustomerManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"warn");
                            }
                        });
                    }
                }
                $.jBox.confirm("此操作将删除所有关联地址,确定要删除吗？", "提示", submit);
            }
        });
    },
    GetSelect:function(){
        var list=[];
        if(this.CusGrid!=undefined){
            var rows=this.CusGrid.selectedRows();
            if(rows!=undefined && rows.length>0){
                for(var i=0;i<rows.length;i++){
                    list.push(rows[i].CustomerSN);
                }
            }
        }
        return list;
    },
    GetSearch:function(){
        var searchBar=$("div[data-condition='search']");
        var CusNum=searchBar.find("input[name='CusNum']").val();
        var CusName=searchBar.find("input[name='CusName']").val();
        var Phone=searchBar.find("input[name='Phone']").val();
        var search={};
        search["CusNum"]=CusNum;
        search["CusName"]=CusName;
        search["Phone"]=Phone;
        return search;
    },
    ToolBar:function(){

        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                CustomerManager.Dialog(undefined,command);
            }else if(command=="Edit"){
                var list=CustomerManager.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var SnNum=list[0];
                CustomerManager.Dialog(SnNum,command);
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=CustomerManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=CustomerManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                CustomerManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"warn");
                            }
                        });
                    }
                }
                $.jBox.confirm("此操作将删除所有关联地址,确定要删除吗？", "提示", submit);
            }else if(command=="Excel"){
                var Server=CustomerManager.Server();
                var search=CustomerManager.GetSearch();
                Server.ToExcel(search,function(result){
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                CustomerManager.Refresh();
            }

        });

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            CustomerManager.PageClick(1,10);
        });
        
        //加载默认数据
        CustomerManager.PageClick(1,10);
    }
};

var AddressManager=function(parent,CustomerSN){
    this.Parent=parent;
    this.CustomerSN=CustomerSN;
    
    function load(){
        var Server=CustomerManager.Server();
        var param={};
        param["CustomerSN"]=this.CustomerSN;
        Server.GetAddList(param,function(result){
            setTable(result);
        });
    }

    this.Load=load;

    function setTable(result){
        parent.find("#tabDetail").DataTable({
            destroy: true,
            data:result.Result==undefined ? []:result.Result,
            paging:false,
            searching:false,
            scrollX: false,
            scrollY: "200px",
            bAutoWidth:true,
            bInfo:false,
            ordering:false,
            columns: [
                { data: 'Contact'},
                { data: 'Phone'},
                { data: 'Address'},
                { data: 'Remark'},
                {data:"SnNum", render:function(data,type,full,meta){
                    var html="";
                    html+='<a class="edit" href="javascript:void(0)" data-value="'+data+'">编辑</a>&nbsp;&nbsp;';
                    html+='<a class="delete" href="javascript:void(0)" data-value="'+data+'">删除</a>&nbsp;';
                    return html;
                }}
            ],
            oLanguage:{
                sEmptyTable:"没有查询到任何数据"
            },
            initComplete:function(settings, json){
                bindEvent();
            }
        });

        function bindEvent(){
            parent.find("#tabDetail").find('a.edit').click(function(event) {
                var SnNum=$(this).attr("data-value");
                add(SnNum,"Edit");
            });

            parent.find("#tabDetail").find('a.delete').click(function(event) {
                var SnNum=$(this).attr("data-value");
                var param={};
                param["SnNum"]=SnNum;
                alert(param);
                var Server=CustomerManager.Server();
                Server.DelAdd(param,function(result){
                    if(result.Code==1){
                        load();
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });
            });
        }
    }

    function add(SnNum,Command){
        var submit=function(v, h, f){
            if(v){
                var SnNum=h.find("input[name='SnNum']").val();
                var CustomerSN=h.find("input[name='CustomerSN']").val();
                var Contact=h.find("input[name='Contact']").val();
                var Address=h.find("input[name='Address']").val();
                var Phone=h.find("input[name='Phone']").val();
                var Remark=h.find("input[name='Remark']").val();

                var param={};
                param["SnNum"]=SnNum;
                param["CustomerSN"]=CustomerSN;
                param["Contact"]=Contact;
                param["Address"]=Address;
                param["Phone"]=Phone;
                param["Remark"]=Remark;

                var Server=CustomerManager.Server();
                Server.AddAddress(param,function(result){
                    if(result.Code==1){
                        load();
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });

            }
        }
        if(Command=="Add"){
            $.jBox.open("get:/Client/Customer/Address", "新增地址", 400, 300, { buttons: {"确定": true, "关闭": false } ,submit:submit});
        }else if(Command=="Edit"){
            $.jBox.open("get:/Client/Customer/Address?SnNum="+SnNum, "编辑地址", 400, 300, { buttons: {"确定": true, "关闭": false } ,submit:submit});
        }
    }

    this.Add=add;
}