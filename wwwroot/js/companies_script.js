//onload function 
document.addEventListener("DOMContentLoaded", function () {
    function_to_check_checkboxes();
});


function function_to_check_checkboxes() {
    var isCustomer = document.getElementById('isCustomer');
    if (isCustomer.checked !== true) {
        var buttonLeft = document.getElementById('btnLeftCustomer');
        buttonLeft.disabled = true;
        var buttonRight = document.getElementById('btnRightCustomer');
        buttonRight.disabled = true;
        var selOptsCustomer = document.getElementById('selectedOptionsCustomer');
        selOptsCustomer.disabled = true;
        var availOptsCustomer = document.getElementById('availOptionsCustomer');
        availOptsCustomer.disabled = true;
    }

    var isVendor = document.getElementById('isVendor');
    if (isVendor.checked !== true) {
        var buttonLeftV = document.getElementById('btnLeftVendor');
        buttonLeftV.disabled = true;
        var buttonRightV = document.getElementById('btnRightVendor');
        buttonRightV.disabled = true;
        var selOptsVendor = document.getElementById('selectedOptionsVendor');
        selOptsVendor.disabled = true;
        var availOptsVendor = document.getElementById('availOptionsVendor');
        availOptsVendor.disabled = true;
    }

    var isContractor = document.getElementById('isContractor');
    if (isContractor.checked !== true) {
        var buttonLeftC = document.getElementById('btnLeftContractor');
        buttonLeftC.disabled = true;
        var buttonRightC = document.getElementById('btnRightContractor');
        buttonRightC.disabled = true;
        var selOptsContractor = document.getElementById('selectedOptionsContractor');
        selOptsContractor.disabled = true;
        var availOptsContractor = document.getElementById('availOptionsContractor');
        availOptsContractor.disabled = true;
    }
}

//Customer Checkbox
document.getElementById('isCustomer').onclick = function () {

    if (this.checked) {
        var buttonLeft = document.getElementById('btnLeftCustomer');
        buttonLeft.disabled = false;
        var buttonRight = document.getElementById('btnRightCustomer');
        buttonRight.disabled = false;
        var selOptsCustomer = document.getElementById('selectedOptionsCustomer');
        selOptsCustomer.disabled = false;
        var availOptsCustomer = document.getElementById('availOptionsCustomer');
        availOptsCustomer.disabled = false;
    }
    else {
        var buttonLeft = document.getElementById('btnLeftCustomer');
        buttonLeft.disabled = true;
        var buttonRight = document.getElementById('btnRightCustomer');
        buttonRight.disabled = true;
        var selOptsCustomer = document.getElementById('selectedOptionsCustomer');
        selOptsCustomer.disabled = true;
        var availOptsCustomer = document.getElementById('availOptionsCustomer');
        availOptsCustomer.disabled = true;
    }
};

//Vendor Checkbox
document.getElementById('isVendor').onclick = function () {

    if (this.checked) {
        var buttonLeft = document.getElementById('btnLeftVendor');
        buttonLeft.disabled = false;
        var buttonRight = document.getElementById('btnRightVendor');
        buttonRight.disabled = false;
        var selOptsVendor = document.getElementById('selectedOptionsVendor');
        selOptsVendor.disabled = false;
        var availOptsVendor = document.getElementById('availOptionsVendor');
        availOptsVendor.disabled = false;
    }
    else {
        var buttonLeft = document.getElementById('btnLeftVendor');
        buttonLeft.disabled = true;
        var buttonRight = document.getElementById('btnRightVendor');
        buttonRight.disabled = true;
        var selOptsVendor = document.getElementById('selectedOptionsVendor');
        selOptsVendor.disabled = true;
        var availOptsVendor = document.getElementById('availOptionsVendor');
        availOptsVendor.disabled = true;
    }
};

//Contractor Checkbox
document.getElementById('isContractor').onclick = function () {

    if (this.checked) {
        var buttonLeft = document.getElementById('btnLeftContractor');
        buttonLeft.disabled = false;
        var buttonRight = document.getElementById('btnRightContractor');
        buttonRight.disabled = false;
        var selOptsVendor = document.getElementById('selectedOptionsContractor');
        selOptsVendor.disabled = false;
        var availOptsVendor = document.getElementById('availOptionsContractor');
        availOptsVendor.disabled = false;
    }
    else {
        var buttonLeft = document.getElementById('btnLeftContractor');
        buttonLeft.disabled = true;
        var buttonRight = document.getElementById('btnRightContractor');
        buttonRight.disabled = true;
        var selOptsVendor = document.getElementById('selectedOptionsContractor');
        selOptsVendor.disabled = true;
        var availOptsVendor = document.getElementById('availOptionsContractor');
        availOptsVendor.disabled = true;
    }
};

//Customer
$('#btnRightCustomer').click(function (e) {
    var selectedOptsCustomer = $('#selectedOptionsCustomer option:selected');
    if (selectedOptsCustomer.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availOptionsCustomer').append($(selectedOptsCustomer).clone());
    $(selectedOptsCustomer).remove();
    e.preventDefault();
});

$('#btnLeftCustomer').click(function (e) {
    var selectedOptsCustomer = $('#availOptionsCustomer option:selected');
    if (selectedOptsCustomer.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedOptionsCustomer').append($(selectedOptsCustomer).clone());
    $(selectedOptsCustomer).remove();
    e.preventDefault();
});

//Vendor
$('#btnRightVendor').click(function (e) {
    var selectedOptsVendor = $('#selectedOptionsVendor option:selected');
    if (selectedOptsVendor.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availOptionsVendor').append($(selectedOptsVendor).clone());
    $(selectedOptsVendor).remove();
    e.preventDefault();
});

$('#btnLeftVendor').click(function (e) {
    var selectedOptsVendor = $('#availOptionsVendor option:selected');
    if (selectedOptsVendor.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedOptionsVendor').append($(selectedOptsVendor).clone());
    $(selectedOptsVendor).remove();
    e.preventDefault();
});

//Contractor
$('#btnRightContractor').click(function (e) {
    var selectedOptsContractor = $('#selectedOptionsContractor option:selected');
    if (selectedOptsContractor.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availOptionsContractor').append($(selectedOptsContractor).clone());
    $(selectedOptsContractor).remove();
    e.preventDefault();
});

$('#btnLeftContractor').click(function (e) {
    var selectedOptsContractor = $('#availOptionsContractor option:selected');
    if (selectedOptsContractor.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedOptionsContractor').append($(selectedOptsContractor).clone());
    $(selectedOptsContractor).remove();
    e.preventDefault();
});

function hidden_inputs() {
    var isCustomer = document.getElementById('isCustomer');
    if (isCustomer.checked) {
        document.getElementById("inputCustomer").value = "Customer";
    }

    var isVendor = document.getElementById('isVendor');
    if (isVendor.checked) {
        document.getElementById("inputVendor").value = "Vendor";
    }

    var isContractor = document.getElementById('isContractor');
    if (isContractor.checked) {
        document.getElementById("inputContractor").value = "Contractor";
    }
}

$('#btnSubmit').click(function (e) {
    hidden_inputs();
    $('#selectedOptionsCustomer option').prop('selected', true);
    $('#selectedOptionsVendor option').prop('selected', true);
    $('#selectedOptionsContractor option').prop('selected', true);
});

