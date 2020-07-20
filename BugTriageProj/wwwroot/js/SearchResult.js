jQuery.fn.createTable = function (config) {
    var elem = $(this[0])
    var tableSchema = new DataTableSchema(elem, config);
    return elem.DataTable(tableSchema.config);

};
function createSchema(elem,config) {
    return new DataTableSchema(elem, config);
}

function DataTableSchema(elem, config) {
    var rows = (elem).children('thead').children('tr').children('th');
    var schema = (config)? config.schema:  [];
    var data = (config)? config.data:  [];
   
    var columns = [];
    $.each(rows, function (key, value) {
        value = $(value).data('col');
        var object = { data: value };
        for (var i = 0; i < schema.length; i++) {
            if (value in schema[i]) {
                object.render = schema[i][value];
                break;
            }
        }
        columns.push(object);
       
       
    });
   
    this.config = {
        // Design Assets
        dom: 'Bfrtip',
        buttons: [
            'excel'
        ],
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
        data: data || [],
        // Columns Setups
        columns: columns,
       
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
    }
       // this.table = elem.DataTable(config);
        
    }


