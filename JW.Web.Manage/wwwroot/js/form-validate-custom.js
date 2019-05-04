//以下为修改jQuery Validation插件兼容Bootstrap的方法，没有直接写在插件中是为了便于插件升级
        $.validator.setDefaults({
            highlight: function (element) {
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            },
            success: function (element) {
                element.closest('.form-group').removeClass('has-error').addClass('has-success');
            },
            errorElement: "span",
            errorPlacement: function (error, element) {
                if (element.is(":radio") || element.is(":checkbox")) {
                    error.appendTo(element.parent().parent().parent());
                } else {
                    error.appendTo(element.parent());
                }
            },
            errorClass: "help-block m-b-none",
            validClass: "help-block m-b-none"
        });
        // 邮政编码验证   
        jQuery.validator.addMethod("isZipCode", function (value, element) {
            var exp = /^[0-9]{6}$/;
            return this.optional(element) || (exp.test(value));
        }, "请正确填写您的邮政编码");
        // 密码验证   
        jQuery.validator.addMethod("isPWD", function (value, element) {
            var exp = /^[A-Za-z0-9_!#$%^&*.~]{6,20}$/;
            return this.optional(element) || (exp.test(value));
        }, "密码只能由数字、26个英文字母、下划线或者特殊字符(!#$%^&*.~)组成的字符串");
        // 用户名验证   
        jQuery.validator.addMethod("isUserName", function (value, element) {
            var exp = /^[a-zA-Z0-9_\u4E00-\u9FA5\uF900-\uFA2D]+$/;
            return this.optional(element) || (exp.test(value));
        }, "账号只能由中文名、数字、26个英文字母或者下划线组成的字符串");
        // 手机号验证   
        jQuery.validator.addMethod("isMobile", function (value, element) {
            var exp = /^((13|15|17|18)[0-9]|14[57])\d{8}$/;
            return this.optional(element) || (exp.test(value));
        }, "请输入正确的手机号码");
