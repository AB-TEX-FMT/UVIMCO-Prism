//Construct all columns for a table
function constructTableColumns(response, columns) {
    //columns for DataTables
    $.each(JSON.parse(response.columns), function (i, val) {
        if (val["format"] == "Currency") { var col = { data: val["data"], title: val["name"], render: $.fn.dataTable.render.number(',', '.', 2, '$') }; }
        else if (val["format"] == "Decimal") { var col = { data: val["data"], title: val["name"], render: $.fn.dataTable.render.number(',', '.', 2, '') }; }
        else { var col = { data: val["data"], title: val["name"] }; }
        columns.push(col);
    });
    return [columns];
}
