//onload function 
document.addEventListener("DOMContentLoaded", function () {
    function_to_check_checkboxes();
    document.getElementById("selection2").style.display = "none";
    document.getElementById("selection1").style.display = "flex";
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

    var isCustomer2 = document.getElementById('isCustomer2');
    if (isCustomer2.checked !== true) {
        var buttonLeft2 = document.getElementById('btnLeftCustomer2');
        buttonLeft2.disabled = true;
        var buttonRight2 = document.getElementById('btnRightCustomer2');
        buttonRight2.disabled = true;
        var selOptsCustomer2 = document.getElementById('selectedOptionsCustomer2');
        selOptsCustomer2.disabled = true;
        var availOptsCustomer2 = document.getElementById('availOptionsCustomer2');
        availOptsCustomer2.disabled = true;
    }

    var isVendor2 = document.getElementById('isVendor2');
    if (isVendor2.checked !== true) {
        var buttonLeftV2 = document.getElementById('btnLeftVendor2');
        buttonLeftV2.disabled = true;
        var buttonRightV2 = document.getElementById('btnRightVendor2');
        buttonRightV2.disabled = true;
        var selOptsVendor2 = document.getElementById('selectedOptionsVendor2');
        selOptsVendor2.disabled = true;
        var availOptsVendor2 = document.getElementById('availOptionsVendor2');
        availOptsVendor2.disabled = true;
    }

    var isContractor2 = document.getElementById('isContractor2');
    if (isContractor2.checked !== true) {
        var buttonLeftC2 = document.getElementById('btnLeftContractor2');
        buttonLeftC2.disabled = true;
        var buttonRightC2 = document.getElementById('btnRightContractor2');
        buttonRightC2.disabled = true;
        var selOptsContractor2 = document.getElementById('selectedOptionsContractor2');
        selOptsContractor2.disabled = true;
        var availOptsContractor2 = document.getElementById('availOptionsContractor2');
        availOptsContractor2.disabled = true;
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

document.getElementById('isCustomer2').onclick = function () {

    if (this.checked) {
        var buttonLeft = document.getElementById('btnLeftCustomer2');
        buttonLeft.disabled = false;
        var buttonRight = document.getElementById('btnRightCustomer2');
        buttonRight.disabled = false;
        var selOptsCustomer = document.getElementById('selectedOptionsCustomer2');
        selOptsCustomer.disabled = false;
        var availOptsCustomer = document.getElementById('availOptionsCustomer2');
        availOptsCustomer.disabled = false;
    }
    else {
        var buttonLeft = document.getElementById('btnLeftCustomer2');
        buttonLeft.disabled = true;
        var buttonRight = document.getElementById('btnRightCustomer2');
        buttonRight.disabled = true;
        var selOptsCustomer = document.getElementById('selectedOptionsCustomer2');
        selOptsCustomer.disabled = true;
        var availOptsCustomer = document.getElementById('availOptionsCustomer2');
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

document.getElementById('isVendor2').onclick = function () {

    if (this.checked) {
        var buttonLeft = document.getElementById('btnLeftVendor2');
        buttonLeft.disabled = false;
        var buttonRight = document.getElementById('btnRightVendor2');
        buttonRight.disabled = false;
        var selOptsVendor = document.getElementById('selectedOptionsVendor2');
        selOptsVendor.disabled = false;
        var availOptsVendor = document.getElementById('availOptionsVendor2');
        availOptsVendor.disabled = false;
    }
    else {
        var buttonLeft = document.getElementById('btnLeftVendor2');
        buttonLeft.disabled = true;
        var buttonRight = document.getElementById('btnRightVendor2');
        buttonRight.disabled = true;
        var selOptsVendor = document.getElementById('selectedOptionsVendor2');
        selOptsVendor.disabled = true;
        var availOptsVendor = document.getElementById('availOptionsVendor2');
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

document.getElementById('isContractor2').onclick = function () {

    if (this.checked) {
        var buttonLeft = document.getElementById('btnLeftContractor2');
        buttonLeft.disabled = false;
        var buttonRight = document.getElementById('btnRightContractor2');
        buttonRight.disabled = false;
        var selOptsVendor = document.getElementById('selectedOptionsContractor2');
        selOptsVendor.disabled = false;
        var availOptsVendor = document.getElementById('availOptionsContractor2');
        availOptsVendor.disabled = false;
    }
    else {
        var buttonLeft = document.getElementById('btnLeftContractor2');
        buttonLeft.disabled = true;
        var buttonRight = document.getElementById('btnRightContractor2');
        buttonRight.disabled = true;
        var selOptsVendor = document.getElementById('selectedOptionsContractor2');
        selOptsVendor.disabled = true;
        var availOptsVendor = document.getElementById('availOptionsContractor2');
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

$('#btnRightCustomer2').click(function (e) {
    var selectedOptsCustomer = $('#selectedOptionsCustomer2 option:selected');
    if (selectedOptsCustomer.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availOptionsCustomer2').append($(selectedOptsCustomer).clone());
    $(selectedOptsCustomer).remove();
    e.preventDefault();
});

$('#btnLeftCustomer2').click(function (e) {
    var selectedOptsCustomer = $('#availOptionsCustomer2 option:selected');
    if (selectedOptsCustomer.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedOptionsCustomer2').append($(selectedOptsCustomer).clone());
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

$('#btnRightVendor2').click(function (e) {
    var selectedOptsVendor = $('#selectedOptionsVendor2 option:selected');
    if (selectedOptsVendor.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availOptionsVendor2').append($(selectedOptsVendor).clone());
    $(selectedOptsVendor).remove();
    e.preventDefault();
});

$('#btnLeftVendor2').click(function (e) {
    var selectedOptsVendor = $('#availOptionsVendor2 option:selected');
    if (selectedOptsVendor.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedOptionsVendor2').append($(selectedOptsVendor).clone());
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

$('#btnRightContractor2').click(function (e) {
    var selectedOptsContractor = $('#selectedOptionsContractor2 option:selected');
    if (selectedOptsContractor.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availOptionsContractor2').append($(selectedOptsContractor).clone());
    $(selectedOptsContractor).remove();
    e.preventDefault();
});

$('#btnLeftContractor2').click(function (e) {
    var selectedOptsContractor = $('#availOptionsContractor2 option:selected');
    if (selectedOptsContractor.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedOptionsContractor2').append($(selectedOptsContractor).clone());
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

function hidden_inputs2() {
    var isCustomer = document.getElementById('isCustomer2');
    if (isCustomer.checked) {
        document.getElementById("inputCustomer").value = "Customer";
    }

    var isVendor = document.getElementById('isVendor2');
    if (isVendor.checked) {
        document.getElementById("inputVendor").value = "Vendor";
    }

    var isContractor = document.getElementById('isContractor2');
    if (isContractor.checked) {
        document.getElementById("inputContractor").value = "Contractor";
    }
}

$('#btnSubmit').click(function (e) {
    hidden_inputs();

    var x = document.getElementById("selection1");
    if (x.style.display === "none") {
        hidden_inputs2();
        $('#selectedOptionsCustomer2 option').prop('selected', true);
        $('#selectedOptionsVendor2 option').prop('selected', true);
        $('#selectedOptionsContractor2 option').prop('selected', true);
    } else {
        hidden_inputs();
        $('#selectedOptionsCustomer option').prop('selected', true);
        $('#selectedOptionsVendor option').prop('selected', true);
        $('#selectedOptionsContractor option').prop('selected', true);
    }

    
});

