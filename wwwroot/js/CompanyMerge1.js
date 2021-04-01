function collect_checkboxes() {
    var checkboxes = document.getElementsByName('select');
    var input = document.getElementById("select");
    var arrayOfValues = [];
    for (var checkbox of checkboxes) {
        if (checkbox.checked) {
            arrayOfValues.push(checkbox.value);
        }
    }
    input.value = arrayOfValues;
}

$('#btnMerge').click(function (e) {
    collect_checkboxes();
});


}