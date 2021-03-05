$('#btnRight').click(function (e) {
    var selectedOpts = $('#selectedOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
});

$('#btnLeft').click(function (e) {
    var selectedOpts = $('#availOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
});

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

$('#btnSubmit').click(function (e) {
    $('#selectedOptions option').prop('selected', true);
});