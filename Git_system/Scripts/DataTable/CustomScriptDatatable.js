$.extend($.fn.dataTable.defaults, {
    "displayLength": 30,//number entity in table
    "ordering": false,
    "info": false
});

$(document).ready(function () {
    $('.dataTable').dataTable({
        "dom": '<"row"<"col-sm-6" T><"col-sm-6" f>>rtip',//l is entity
        "tableTools": {
            "sSwfPath": "/Scripts/DataTable/TableTool/swf/copy_csv_xls_pdf.swf",
            "aButtons": [
                            {
                                "sExtends": "copy",
                                "sButtonClass": "hidden"
                            },
                            {
                                "sExtends": "xls",
                                "oSelectorOpts": { filter: 'applied', order: 'current' }
                            },
                            {
                                "sExtends": "pdf",
                                "sButtonClass": "hidden"
                            },
                            "print"
            ]
        }
    });
});