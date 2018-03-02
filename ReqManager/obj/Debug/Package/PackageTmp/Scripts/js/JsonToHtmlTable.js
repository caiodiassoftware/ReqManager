function CreateTableView(objArray, id, enableHeader) {

    if (enableHeader === undefined) {
        enableHeader = true; //default enable headers
    }

    // If the returned data is an object do nothing, else try to parse
    var array = typeof objArray != 'object' ? JSON.parse(objArray) : new Array(objArray);
    var keys = Object.keys(array[0]);

    var str = '<table id="' + id + '">';

    // table head
    if (enableHeader) {
        str += '<thead><tr>';
        for (var index in keys) {
            str += '<th scope="col">' + keys[index] + '</th>';
        }
        str += '</tr></thead>';
    }

    // table body
    str += '<tbody>';
    for (var i = 0; i < array.length; i++) {
        str += (i % 2 == 0) ? '<tr class="alt">' : '<tr>';
        for (var index in keys) {
            var objValue = array[i][keys[index]];

            // Support for Nested Tables
            if (typeof objValue === 'object' && objValue !== null) {
                if (Array.isArray(objValue)) {
                    str += '<td>';
                    for (var aindex in objValue) {
                        str += CreateTableView(objValue[aindex], id, true);
                    }
                    str += '</td>';
                } else {
                    str += '<td>' + CreateTableView(objValue, id, true) + '</td>';
                }
            } else {
                if (objValue === null)
                    str += '<td></td>';
                else
                    str += '<td>' + objValue + '</td>';
            }

        }
        str += '</tr>';
    }
    str += '</tbody>'
    str += '</table>';

    return str;
}