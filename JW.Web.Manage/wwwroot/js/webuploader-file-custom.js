function setFile(i, fileName, fileGuid) {
    var file_id = 'WU_FILE_' + (LIMIT_FILE_NUM - i);
    var file_s = "'" + file_id + "'";
    var $li = '<div id=' + file_s + '>' + fileName + '<span class="cancel fileRed" onclick="removeFile(' + file_s + ')">删除</span></div>';
    $("#fileList").append($li);
    $("#upload_file_info").append("<input type='hidden' name='hidFile' id='hidFile_" + file_id + "' value='" + fileGuid + "' />");
}
$(function () {
    var $list = $('#fileList'),
            // Web Uploader实例
            uploader;
    if (!WebUploader.Uploader.support()) {
        $.modalAlert('Web Uploader 不支持您的浏览器！如果你使用的是IE浏览器，请尝试升级 flash 播放器');
        throw new Error('WebUploader does not support the browser you are using.');
    }
    // 初始化Web Uploader
    uploader = WebUploader.create({
        // 自动上传。
        auto: true,
        //去重复
        duplicate:true,
        //上传限制
        fileNumLimit: LIMIT_FILE_NUM,
        //上传单个文件大小
        fileSingleSizeLimit: UPLOAD_FILE_SINGLE_SIZE_LIMIT,
        // swf文件路径
        swf: '/Content/res/webuploader/Uploader.swf',
        // 文件接收服务端。
        server: '/Uploader/UploadFile?key=' + UPLOAD_KEY + '&t=' + Math.random(),
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: '#filePicker',
        // 只允许选择文件，可选。
        accept: {
            title: 'Files',
            extensions: UPLOAD_FILE_EXT,
            mimeTypes: 'file/*'
        }
    });
    // 当有文件添加进来的时候
    uploader.on('fileQueued', function (file) {
        var has_num = $("#upload_file_info").children("input").length;
        if (has_num >= LIMIT_FILE_NUM) {
            $.modalAlert("最多只允许上传" + LIMIT_FILE_NUM + "个文件");
            uploader.removeFile(file.id, true);
            return false;
        }
        var file_s = "'" + file.id + "'";
        var $li = $('<div id=' + file_s + '>' + file.name + '<span class="cancel fileRed" onclick="if(removeFile(' + file_s + ')){uploader.removeFile(' + file_s + ', true);}">删除</span></div>');
        $list.append($li);
        $li.on('click', '.cancel', function () {
            uploader.removeFile(file.id, true);
        });
    });
    // 文件上传过程中创建进度条实时显示。
    uploader.on('uploadProgress', function (file, percentage) {
        var $li = $('#' + file.id),
                $percent = $li.find('.progress span');
        //避免重复创建
        if (!$percent.length) {
            $percent = $('<p class="progress"><span></span></p>')
                    .appendTo($li)
                    .find('span');
        }
        $percent.css('width', percentage * 100 + '%');
    });
    // 文件上传成功，给item添加成功class, 用样式标记上传成功
    uploader.on('uploadSuccess', function (file, respone) {
        if (respone.Success == false) {
            alert(respone.Msg);
            $("#" + file.id).remove();
            uploader.removeFile(file.id, true);
            return false;
        }
        setValue(file.id, respone);
    });
    // 文件上传失败，现实上传出错。
    uploader.on('uploadError', function (file) {
        var $li = $('#' + file.id),
                $error = $li.find('div.error');
        // 避免重复创建
        if (!$error.length) {
            $error = $('<div class="error"></div>').appendTo($li);
        }
        $error.text('上传失败');
    });
    // 完成上传完了，成功或者失败，先删除进度条。
    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').remove();
    });
    uploader.on('error', function (handler) {
        if (handler == "Q_EXCEED_NUM_LIMIT") {
            $.modalAlert("最多只允许上传" + LIMIT_FILE_NUM + "个文件");
        }
        if (handler == "F_DUPLICATE") {
            $.modalAlert("上传文件不能重复");
        }
        if (handler == "F_EXCEED_SIZE") {
            $.modalAlert("上传文件大小不能超过" + UPLOAD_FILE_SINGLE_SIZE_LIMIT_TEXT);
        }
        if (handler == "Q_TYPE_DENIED") {
            $.modalAlert("上传文件扩展名只能为" + UPLOAD_FILE_EXT);
        }
    })
});
function animates(id, type) {
    var that = $('#' + id).find('.file-panel');
    if (type === 1) {
        that.stop().animate({ height: 30 });
    } else {
        that.stop().animate({ height: 0 });
    }
}
function setValue(id, respone) {
    var name = "hidFile_" + id;
    $("#upload_file_info").append("<input type='hidden' name='hidFile' id='" + name + "' value='" + respone.Data + "' />");
}
function removeFile(id) {
    var name = "hidFile_" + id;
    var aid = $('#upload_file_info input[id="' + name + '"]').val();
    $.post('/Uploader/RemoveFile', { 'aid': aid, 'key': UPLOAD_KEY, 't': Math.random() }, function (respone) {
        if (respone.Success == true) {
            //删除
            $('#upload_file_info input[id="' + name + '"]').remove();
            //删除文件
            $("#" + id).remove();
            return true;
        } else {
            $.modalMsg(respone.Msg, respone.Success);
            return false;
        }
    }, 'json')
}