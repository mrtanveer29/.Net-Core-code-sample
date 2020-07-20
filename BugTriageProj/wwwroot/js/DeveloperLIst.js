function deleteDeveopers(params) {
    var isConfirmed = confirm("Are You sure to delete all the developers!");
    if (isConfirmed) {
        $.ajax({
            url: "/developer/DeleteDeveloper",
            type: "DELETE",
            data: { "type": params.type },
            success: function (res) {
                alert(res.msg);
                $('#developers').DataTable().clear();
                $('#developers').DataTable().ajax.reload();
            }
        });
    }
}
function onDelete(id) {
    var isConfirmed = confirm("Are You sure to delete the developer!");
    if (isConfirmed) {
        $.ajax({
            url: "/developer/DeleteDeveloper",
            type: "DELETE",
            data: { "id":id },
            success: function (res) {
                alert(res.msg);
                $('#developers').DataTable().clear();
                $('#developers').DataTable().ajax.reload();
            }
        });
    }
}
function GenerateDeveloperList(type) {
  
    $("#developers").DataTable({
        // Design Assets
        stateSave: true,
        autoWidth: true,
        // ServerSide Setups
        processing: false,
        serverSide: false,
        // Paging Setups
        paging: true,
        // Searching Setups
        searching: { regex: true },
        // Ajax Filter
        ajax: {
            url: "/developer/GetDevelopers",
            data: { type: type },
            type: "POST",
            contentType: "application/x-www-form-urlencoded",
            dataType: "json",
            "dataSrc": ""
        },
        // Columns Setups
        columns: [
            { data: "developerName" },
            { data: "developerTypeName" },


            {
                data: "developerId",
                render: function (data, type, dataItem) {
                    // data in this case is developerId
                    // type can be display, filter etc
                    // dataItem is the current row
                    var html = "";

                    html += "<a style='margin:0 2px;' class='btn btn-danger pull-right' onClick='onDelete(" + data + ")' >Delete Profile</a>"
                    html += "<a  class='btn btn-default pull-right' href='/developer/profile/" + data + "' >View Profile</a> "
                    return html;
                }
            }
        ],
        // Column Definitions
        columnDefs: [
            { targets: "no-sort", orderable: false },
            { targets: "no-search", searchable: false },
            { "orderable": false, "targets": -1 },
            {
                targets: "trim",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        data = strtrunc(data, 10);
                    }

                    return data;
                }
            },
            { targets: "date-type", type: "date-eu" }
        ]
    });
}
$(() => {
    //$.fn.dataTable.moment("DD/MM/YYYY HH:mm:ss");
    //$.fn.dataTable.moment("DD/MM/YYYY");
   
});