
function getTextElementsUnder(parentSelector) {
    var elems = []
    $(parentSelector).find(':input').each(function () {
        var type = $(this).attr('type')
        if (type == "text") elems.push($(this));
        if (type == "submit") elems.push($(this));
    })

    return elems;
}
function toggleEnableState(option) {
    var singleEntryForm = getTextElementsUnder("#frmCreateDeveloper")
    if (option == "multiple") {
        $.each(singleEntryForm, function (i, element) {
            $(element).attr("disabled", true);
        });
    } else {
        $.each(singleEntryForm, function (i, element) {
            $(element).removeAttr("disabled");
        });
    }

}
function Get_All_Forms_Data(Element) {
    Element = Element || '';
    var All_Page_Data = {};
    var All_Forms_Data_Temp = {};
    if (!Element) {
        Element = 'body';
    }

    $(Element).find('input,select,textarea').each(function (i) {
        All_Forms_Data_Temp[i] = $(this);
    });

    $.each(All_Forms_Data_Temp, function () {
        var input = $(this);
        var Element_Name;
        var Element_Value;

        if ((input.attr('type') == 'submit') || (input.attr('type') == 'button')) {
            return true;
        }

        if ((input.attr('name') !== undefined) && (input.attr('name') != '')) {
            Element_Name = input.attr('name').trim();
        }
        else if ((input.attr('id') !== undefined) && (input.attr('id') != '')) {
            Element_Name = input.attr('id').trim();
        }
        else if ((input.attr('class') !== undefined) && (input.attr('class') != '')) {
            Element_Name = input.attr('class').trim();
        }

        if (input.val() !== undefined) {
            if (input.attr('type') == 'checkbox') {
                Element_Value = input.parent().find('input[name="' + Element_Name + '"]:checked').val();
            }
            else if ((input.attr('type') == 'radio')) {
                Element_Value = $('input[name="' + Element_Name + '"]:checked', Element).val();
            }
            else {
                Element_Value = input.val();
            }
        }
        else if (input.text() != undefined) {
            Element_Value = input.text();
        }

        if (Element_Value === undefined) {
            Element_Value = '';
        }

        if (Element_Name !== undefined) {
            var Element_Array = new Array();
            if (Element_Name.indexOf(' ') !== -1) {
                Element_Array = Element_Name.split(/(\s+)/);
            }
            else {
                Element_Array.push(Element_Name);
            }

            $.each(Element_Array, function (index, Name) {
                Name = Name.trim();
                if (Name != '') {
                    All_Page_Data[Name] = Element_Value;
                }
            });
        }
    });
    return All_Page_Data;
}