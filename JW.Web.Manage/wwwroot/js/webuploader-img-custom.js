function setPic(i, picUrl, picGuid) {
    var file_id = 'WU_FILE_' + (LIMIT_NUM - i);
    var file_s = "'" + file_id + "'";
    var $li = '<div id=' + file_s + ' class="file-item" onmouseover="animates(' + file_s + ',1)" onmouseout="animates(' + file_s + ',2)" >' +
            '<img src="' + picUrl + '"  width="110px" height="110px">' +
            '<div class="info">已上传的图片</div>' +
            '<div class="file-panel">' +
            '<span class="cancel" onclick="removePic(' + file_s + ')">删除</span>' +
            '</div></div>';
    $("#picList").append($li);
    $("#upload_pic_info").append("<input type='hidden' name='hidPic' id='hidPic_" + file_id + "' value='" + picUrl + "' guid='" + picGuid + "' />");
}
$(function () {

    var $list = $('#picList'),
            // 优化retina, 在retina下这个值是2
            ratio = window.devicePixelRatio || 1,
            // 缩略图大小
            thumbnailWidth = 100 * ratio,
            thumbnailHeight = 100 * ratio,
            // Web Uploader实例
            uploader;
    if (!WebUploader.Uploader.support()) {
        $.modalAlert('Web Uploader 不支持您的浏览器！如果你使用的是IE浏览器，请尝试升级 flash 播放器');
        throw new Error('WebUploader does not support the browser you are using.');
    } 
  
    // 初始化Web Uploader
    uploader = WebUploader.create({
        //是否上传前压缩
        compress:false,
        // 自动上传。
        auto: true,
        //去重复
        duplicate: true,
        //上传限制
        fileNumLimit: LIMIT_NUM,
        //上传单个文件大小
        fileSingleSizeLimit: UPLOAD_SINGLE_SIZE_LIMIT,
        // swf文件路径
        swf: '/js/plugins/webuploader/Uploader.swf',
        // 文件接收服务端。
        server: '/Uploader/UploadImg?nodedir=' + UPLOAD_DIR + '&key=' + UPLOAD_KEY + '&t=' + Math.random(),
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: '#picPicker',
        // 只允许选择文件，可选。
        accept: {
            title: 'Images',
            extensions: UPLOAD_EXT,
            mimeTypes: 'image/*'
        }
    });
    // 当有文件添加进来的时候
    uploader.on('fileQueued', function (file) {    
        var has_num = $("#upload_pic_info").children("input").length;
        if (has_num >= LIMIT_NUM) {         
            $.modalAlert("最多只允许上传" + LIMIT_NUM + "张图片");
            uploader.removeFile(file.id, true);
            return false;
        } 
        var file_s = "'" + file.id + "'";
        
        var $li = $(
                '<div id=' + file_s + ' class="file-item "  onmouseover="animates(' + file_s + ',1)" onmouseout="animates(' + file_s + ',2)">' +
                '<img width="110px" height="110px">' +
                '<div class="info">' + file.name + '</div>' +
                '</div>'
                ),
                $img = $li.find('img');
        $btns = $('<div class="file-panel"><span class="cancel" onclick="if(removePic(' + file_s + ')){uploader.removeFile(' + file_s + ', true);}">删除</span></div>').appendTo($li),
        $list.append($li);
        // 创建缩略图
        uploader.makeThumb(file, function (error, src) {
            if (error) {
                $img.replaceWith('<span>不能预览</span>');
                return;
            }
            $img.attr('src', src);
        }, thumbnailWidth, thumbnailHeight);
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
            $("#" + file.id).remove();
            uploader.removeFile(file.id, true);
            return false;
        }
        setValue(file.id, respone);
        $('#' + file.id).addClass('upload-state-done');
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
            $.modalAlert("最多只允许上传" + LIMIT_NUM + "个文件");
        }
        if (handler == "F_DUPLICATE") {
            $.modalAlert("上传文件不能重复");
        }
        if (handler == "F_EXCEED_SIZE") {
            $.modalAlert("上传文件大小不能超过" + UPLOAD_SINGLE_SIZE_LIMIT_TEXT);
        }
        if (handler == "Q_TYPE_DENIED") {
            $.modalAlert("上传文件扩展名只能为" + UPLOAD_EXT);
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
    var name = "hidPic_" + id;
    $("#upload_pic_info").append("<input type='hidden' name='hidPic' id='" + name + "' value='" + respone.Data.Url + "' guid='" + respone.Data.Guid + "' />");
  
}
//删除文件
function removePic(id) { 
    var name = "hidPic_" + id;    
    var url = $('#upload_pic_info input[id="' + name + '"]').attr("guid");   
    $.post('/Uploader/DelImg', { 'url': url, 'key': UPLOAD_KEY, 't': Math.random() }, function (respone) {     
        if (respone.Success == true) {
            //删除
            $('#upload_pic_info input[id="' + name + '"]').remove();
            //删除图片
            $("#" + id).remove();
            return true;
        } else {
            $.modalMsg(respone.Msg, respone.Success);
            return false;
        }
    }, 'json')
}