var Login = function () {


    return {

        init: function () {

            jQuery('#forget-password').click(function () {
                jQuery('#loginform').hide();
                jQuery('#forgotform').show(200);
            });

            jQuery('#forget-btn').click(function () {

                jQuery('#loginform').slideDown(200);
                jQuery('#forgotform').slideUp(200);
            });


            jQuery('#login-btn').click(function () {

                var CompanyNum=$("input[name='CompanyNum']").val();
                var UserName=$("input[name='UserName']").val();
                var PassWord=$("input[name='PassWord']").val();

                if(git.IsEmpty(CompanyNum)){
                    $.jBox.tip("请输入公司编号","warn");
                    return false;
                }

                if(git.IsEmpty(UserName)){
                    $.jBox.tip("请输入用户名","warn");
                    return false;
                }

                if(git.IsEmpty(PassWord)){
                    $.jBox.tip("请输入密码","warn");
                    return false;
                }

                var param={};
                param["CompanyNum"]=CompanyNum;
                param["UserName"]=UserName;
                param["PassWord"]=PassWord;

                $.ajax({
                    url: "/UserAjax/Login?t="+Math.random(),
                    data: param,
                    type: "post",
                    dataType: "json",
                    success: function (result) {

                        if (result.Code == "1") {
                            var reutrnUrl = $("#hdReturnUrl").val();
                            if (git.IsEmpty(reutrnUrl)) {
                                window.location.href = "/Home/Desktop";
                            } else {
                                window.location.href = reutrnUrl;
                            }
                            
                        }else{
                            $.jBox.tip(result.Message,"warn");
                        }
                    }
                });
                
            });

        }
    };

}();