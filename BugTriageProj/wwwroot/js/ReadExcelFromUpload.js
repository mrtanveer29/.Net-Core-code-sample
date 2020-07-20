function onLoadExcel(file,data, callback) {
   
    var fileExtension = ['xls', 'xlsx'];
    var filename = file.val();
    //--- Validation for excel file---  
    if (filename.length == 0) {
        alert("Please select a file.");
        return false;
    }
    else {
        var extension = filename.replace(/^.*\./, '');
        if ($.inArray(extension, fileExtension) == -1) {
            alert("Please select only excel files.");
            return false;
        }
    }
    var filedata = new FormData();
    // file uploads
    var fileUpload = file.get(0);
    var files = fileUpload.files;
    filedata.append(files[0].name, files[0]);
    // extra data
    for (var key in data) {
        if (data.hasOwnProperty(key)) {
            filedata.append(key, data[key])
        }
    }
    callback(filedata);

}

function remote(conf) {
    conf.beforeSend = function (xhr) {
        xhr.setRequestHeader("XSRF-TOKEN",
            $('input:hidden[name="__RequestVerificationToken"]').val());
    };
    conf.contentType = false;
    conf.processData = false;
    $.ajax(conf)
}