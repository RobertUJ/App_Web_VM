function formPrint() {
    window.print();
    return true;
}

function noCopyMouse(e) {
    return (e.button == 0);
}


function noCopyKey(e) {
    var forbiddenKeys = new Array('c', 'x', 'v');
    var keyCode = (e.keyCode) ? e.keyCode : e.which;
    var isCtrl;
    var result = true;

    if (window.event)
        isCtrl = e.ctrlKey
    else
        isCtrl = (window.Event) ? ((e.modifiers & Event.CTRL_MASK) == Event.CTRL_MASK) : false;


    if (isCtrl) {
        for (i = 0; i < forbiddenKeys.length; i++) {
            if (forbiddenKeys[i] == String.fromCharCode(keyCode).toLowerCase()) {
                result = false;
            }
        }
    }
    return result;
}

function InsuranceValidation() {
    var result = true;
    var message = "";
    var txtClient_Name = $("#txtClient_Name").val(); if (txtClient_Name == "") { message += "Nombre Asegurado requerido\n"; result = false; }
    var txtDriver_Name = $("#txtDriver_Name").val(); if (txtDriver_Name == "") { message += "Nombre del Conductor requerido\n"; result = false; }
    var txtDrivers_Licence_Number = $("#txtDrivers_Licence_Number").val(); if (txtDrivers_Licence_Number == "") { message += "No Licencia requerido\n"; result = false; }
    var rdpBirth_Date = $("#rdpBirth_Date").val(); if (rdpBirth_Date == "") { message += "Fecha Nacimiento\n"; result = false; }
    var txtJob_Position = $("#txtJob_Position").val(); if (txtJob_Position == "") { message += "Ocupación requerido\n"; result = false; }
    var txtClient_Address_Street = $("#txtClient_Address_Street").val(); if (txtClient_Address_Street == "") { message += "Calle requerido\n"; result = false; }
    var txtClient_Address_Street_Number = $("#txtClient_Address_Street_Number").val(); if (txtClient_Address_Street_Number == "") { message += "Calle Number requerido\n"; result = false; }
    var txtClient_Address = $("#txtClient_Address").val(); if (txtClient_Address == "") { message += "Colonia requerido\n"; result = false; }
    var txtClient_Zip = $("#txtClient_Zip").val(); if (txtClient_Zip == "") { message += "CP requerido\n"; result = false; }
    var txtClient_Phone = $("#txtClient_Phone").val(); if (txtClient_Phone == "") { message += "Teléfono requerido\n"; result = false; }
    var txtClient_Email = $("#txtClient_Email").val(); if (txtClient_Email == "") { message += "Email requerido\n"; result = false; }
    var txtModel = $("#txtModel").val(); if (txtModel == "") { message += "Modelo requerido\n"; result = false; }
    var txtVIN = $("#txtVIN").val(); if (txtVIN == "") { message += "Número de Serie  requerido\n"; result = false; }
    var txtPlates = $("#txtPlates").val(); if (txtPlates == "") { message += "Placas requerido\n"; result = false; }
    var rdpEffective_Date = $("#rdpEffective_Date").val(); if (rdpEffective_Date == "") { message += "Fecha Efectiva requerido\n"; result = false; }
    var txtInsurance_City = $("#txtInsurance_City").val(); if (txtInsurance_City == "") { message += "Municipio requerido\n"; result = false; }

    if (message != "") {
        alert(message);
    }

    return(result);
}

function loadDays() {   
    var ddlEntrance_State = $("#ddlEntrance_State").find("option:selected").text();
    var Entry_State = ddlEntrance_State.split("|");
    var ddlCountry_Plates = $("#ddlCountry_Plates").find("option:selected").text();
    $("#ddlProduct_Days").load("/Get_Info.aspx", { "IP_Entry_State": Entry_State[0], "IP_Country_Plates": ddlCountry_Plates });
}

function daysChange() {  
    var Days_Value = $(this).find("option:selected").val();
    var Insurance_Days = Days_Value.split("|");
    $("#lblTotal_Insurance").text("$" + (parseFloat(Insurance_Days[1]) + parseFloat(Insurance_Days[2])) + ".00");

}

function Clear_Insurance() {
    $("#txtClient_Name").val("");
    $("#txtDrivers_Licence_Number").val("");
    $("#txtDriver_Name").val("");
    $("#rdpBirth_Date").val("");
    $("#txtJob_Position").val("");
    $("#txtClient_Address_Street").val("");
    $("#txtClient_Address_Street_Number").val("");
    $("#txtClient_Address").val("");
    $("#txtClient_Zip").val("");
    $("#txtClient_Phone").val("");
    $("#txtClient_Email").val("");
    $("#txtModel").val("");
    $("#txtVIN").val("");
    $("#txtPlates").val("");
    $("#rdpEffective_Date").val("");
    $("#txtInsurance_City").val("JUAREZ");
}

function start() {
  
    $("#ddlEntrance_State").on("change", loadDays);
    $("#ddlCountry_Plates").on("change", loadDays);
    $("#ddlProduct_Days").on("change", daysChange);


    $("#txtPhone").on("cut copy paste", function (e) { e.preventDefault(); });
    $("#txtConfirmation").on("cut copy paste", function (e) { e.preventDefault(); });  
    //$("#pnlTicket").on("ready", formPrint);
    //$("#pnlTicket_Service").on("ready", formPrint);

}

$(document).on("ready", start);