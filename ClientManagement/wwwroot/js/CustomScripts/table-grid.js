



function AddTableRow(tableBodyElement, rowClass, rowId, rowName, rows, heading="") {
    if (rows !== null && rows !== "") {
        var rowHtml = '<tr class="' + rowClass + '"id="' + rowId + '"name="' + rowName + '">';
        rows.forEach(function (v, i) {
            rows[i].heading = rows[i].heading !== undefined ? rows[i].heading : "";
            rowHtml += AddTableRowElements(rows[i].element, rows[i].type, rows[i].id, rows[i].class, rows[i].name, rows[i].style, rows[i].value, rows[i].heading);
        });
        rowHtml += '</tr>';

        $(tableBodyElement).prepend(rowHtml);
    }
}

function AddTableRowElements(element, elementType, elementId, elementClass, elementName, elementStyle, elementValue, elementHeading) {
    if (element !== "" && element !== null) {
        var elementHtml = "";
        if (element === "input") {
            elementHtml += AddInputElement(elementType, elementId, elementClass, elementName, elementStyle, elementValue, elementHeading);
        }
        if (element === "display") {
            elementHtml += AddDisplayElement(elementType, elementId, elementClass, elementName, elementStyle, elementValue, elementHeading);
        }
        if (element === "a") {
            elementHtml += AddAnchorElement(elementType, elementId, elementClass, elementName, elementStyle, elementValue, elementHeading);
        }
        if (element === "dropdown") {
            elementHtml += AddSelectElement(elementType, elementId, elementClass, elementName, elementStyle, elementValue, elementHeading)
        }
        return elementHtml;
    }
}

function AddAnchorElement(elementType, elementId, elementClass, elementName, elementStyle, elementValue, elementHeading) {
    var elementStart = "";
    var elementLength = elementValue.length;
    if (elementValue !== null && elementLength > 0) {
        elementStart = '<td style = "' + elementStyle + '">' + elementHeading;
        elementValue.forEach(function (v, i) {
            var separator = i > 1 || i < elementLength-1 ? "|" : "";
            elementStart += '<a class = "' + elementValue[i].class + '" id = "' + elementValue[i].id + '" type = "' + elementValue[i].type + '" name = "' + elementValue[i].name + '" data-toggle = "' + elementValue[i].dataToggle + '" title = "' + elementValue[i].title + '" style = "' + elementValue[i].style + '" href = "' + elementValue[i].href + '">' + elementValue[i].value + ' </a>' + separator;
        });
        elementStart += "</td>";
    }
    else {
        elementStart = '<td><a class = "' + elementClass + '" id = "' + elementId + '" type = "' + elementType + '" name = "' + elementName + '" value = "' + elementValue + '"/> </td>';
    }
    return elementStart;
}

function AddDisplayElement(elementType, elementId, elementClass, elementName, elementStyle, elementValue, elementHeading) {
    var elementStart = "";
    if (elementValue !== null && elementValue !== "") {
        elementStart = '<td class = "' + elementClass + '" id = "' + elementId + '" type = "' + elementType + '" name = "' + elementName + '" style = "' + elementStyle + '" value = ""> ' + elementHeading + elementValue + '</td>';
    }
    return elementStart;
}

function AddInputElement(elementType, elementId, elementClass, elementName, elementStyle, elementValue, elementHeading) {
    var elementStart = "";
    if (elementType !== null && elementType !== "" && elementType !== "hidden") {
        if (elementType == 'file') {
            elementStart = '<td style= "display: none;">' + elementHeading + ' <input class="' + elementClass + '" id = "' + elementId + '" type = "' + elementType + '" name = "' + elementName + '" style = "' + elementStyle + '" value="' + elementValue + '"/> </td>';
        } else {
            elementStart = '<td>' + elementHeading  + ' <input class="' + elementClass + '" id = "' + elementId + '" type = "' + elementType + '" name = "' + elementName + '" style = "' + elementStyle + '" value="' + elementValue + '"/> </td>';
        }
    }
    else 
        elementStart = '<input class="' + elementClass + '" id = "' + elementId + '" type = "' + elementType + '" name = "' + elementName + '"  style = "' + elementStyle + '" value ="' + elementValue + '"/>';
    return elementStart;
}

function RemoveTableRow(tableId, rowClass) {
    $(tableId).find("tbody").children("tr." + rowClass).each(function (ind, val) {
        var rowId = $(val).attr("id");

        if (rowId !== undefined && rowId !== null && rowId !== "") {
            ChangeRowElementValues("id", rowId, ind, val, '-', ']');
        }

        RenameRowElements(val, ind);
    });
}


function RenameRowElements(val, ind) {

    $(val).children().each(function (i, v) {
        var name = $(v).attr("name");
        if (name !== undefined && name !== null && name !== "") {
            ChangeRowElementValues("name", name, ind, v, '[', ']');
        }
        RenameRowElements(v, ind);
    });
}

function ChangeRowElementValues(property, propertyValue, index, element, startingChar, endingChar) {

    var indexStarting = propertyValue.lastIndexOf(startingChar);
    var indexEnding = propertyValue.lastIndexOf(endingChar);
    var newValue = "";
    if (indexStarting > 0 && indexEnding > 0) {
        newValue = newValue.concat(propertyValue.substring(0, [indexStarting + 1]), index, propertyValue.substring([indexEnding]));
    }
    else if(indexStarting > 0){
        newValue = newValue.concat(propertyValue.substring(0, [indexStarting + 1]), index);
    }
    $(element).attr(property, newValue);

}
