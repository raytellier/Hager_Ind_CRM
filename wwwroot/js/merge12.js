document.addEventListener("DOMContentLoaded", function () {
    if (document.getElementById("creditCheck1").textContent.indexOf("true") >= 0) {
        document.getElementById("input_CreditCheck").value = "true";
    }
    else {
        document.getElementById("input_CreditCheck").value = "false";
    }
    //alert("it runs when onloads");

    //if (document.getElementById("creditCheck1").text().indexOf("true") > -1) {
    //    document.getElementById("input_CreditCheck").value = "true";
    //    alert("true");
    //}
    //else {
    //    document.getElementById("input_CreditCheck").value = "false";
    //    alert("false");
    //}


    if (document.getElementById("active1").textContent.indexOf("true") >= 0) {
        document.getElementById("input_Active").value = "true";
    }
    else {
        document.getElementById("input_Active").value = "false";
    }
});


document.getElementById("toggle_Name").onclick = function () {
    
    if (!this.checked) {
        var left = document.getElementById("name1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("name2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_Name").value = left.textContent;

    }
    else {
        var left = document.getElementById("name1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("name2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_Name").value = right.textContent;
    }
};

document.getElementById("toggle_Location").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("location1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("location2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_Location").value = left.textContent;

    }
    else {
        var left = document.getElementById("location1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("location2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_Location").value = right.textContent;
    }
};

document.getElementById("toggle_BillingTerms").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("billingTerms1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("billingTerms2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_BillingTermsID").value = document.getElementById("billingTerms1ID").textContent;

    }
    else {
        var left = document.getElementById("billingTerms1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("billingTerms2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_BillingTermsID").value = document.getElementById("billingTerms2ID").textContent;
    }
};

document.getElementById("toggle_Currency").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("currency1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("currency2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_CurrencyID").value = document.getElementById("currency1ID").textContent;

    }
    else {
        var left = document.getElementById("currency1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("currency2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_CurrencyID").value = document.getElementById("currency2ID").textContent;
    }
};

document.getElementById("toggle_Phone").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("phone1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("phone2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_Phone").value = left.textContent;

    }
    else {
        var left = document.getElementById("phone1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("phone2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_Phone").value = right.textContent;
    }
};

document.getElementById("toggle_Website").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("website1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("website2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_Website").value = left.textContent;

    }
    else {
        var left = document.getElementById("website1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("website2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_Website").value = right.textContent;
    }
};

document.getElementById("toggle_Notes").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("notes1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("notes2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_Notes").value = left.textContent;

    }
    else {
        var left = document.getElementById("notes1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("notes2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_Notes").value = right.textContent;
    }
};

document.getElementById("toggle_BillingAddress1").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("billingAddress1_company1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("billingAddress1_company2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_BillingAddress1").value = left.textContent;

    }
    else {
        var left = document.getElementById("billingAddress1_company1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("billingAddress1_company2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_BillingAddress1").value = right.textContent;
    }
};

document.getElementById("toggle_BillingAddress2").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("billingAddress2_company1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("billingAddress2_company2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_BillingAddress2").value = left.textContent;

    }
    else {
        var left = document.getElementById("billingAddress2_company1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("billingAddress2_company2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_BillingAddress2").value = right.textContent;
    }
};

document.getElementById("toggle_BillingCountry").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("billingCountry1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("billingCountry2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_BillingCountryID").value = document.getElementById("billingCountry1ID").textContent;

    }
    else {
        var left = document.getElementById("billingCountry1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("billingCountry2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_BillingCountryID").value = document.getElementById("billingCountry2ID").textContent;
    }
};

document.getElementById("toggle_BillingProvince").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("billingProvince1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("billingProvince2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_BillingProvinceID").value = document.getElementById("billingProvince1ID").textContent;

    }
    else {
        var left = document.getElementById("billingProvince1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("billingProvince2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_BillingProvinceID").value = document.getElementById("billingProvince2ID").textContent;
    }
};

document.getElementById("toggle_BillingPostal").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("billingPostal1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("billingPostal2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_BillingPostal").value = left.textContent;

    }
    else {
        var left = document.getElementById("billingPostal1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("billingPostal2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_BillingPostal").value = right.textContent;
    }
};

document.getElementById("toggle_ShippingAddress1").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("shippingAddress1_company1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("shippingAddress1_company2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_ShippingAddress1").value = left.textContent;

    }
    else {
        var left = document.getElementById("shippingAddress1_company1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("shippingAddress1_company2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_ShippingAddress1").value = right.textContent;
    }
};

document.getElementById("toggle_ShippingAddress2").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("shippingAddress2_company1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("shippingAddress2_company2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_ShippingAddress2").value = left.textContent;

    }
    else {
        var left = document.getElementById("shippingAddress2_company1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("shippingAddress2_company2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_ShippingAddress2").value = right.textContent;
    }
};

document.getElementById("toggle_ShippingCountry").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("shippingCountry1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("shippingCountry2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_ShippingCountryID").value = document.getElementById("shippingCountry1ID").textContent;

    }
    else {
        var left = document.getElementById("shippingCountry1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("shippingCountry2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_ShippingCountryID").value = document.getElementById("shippingCountry2ID").textContent;
    }
};

document.getElementById("toggle_ShippingProvince").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("shippingProvince1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("shippingProvince2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_ShippingProvinceID").value = document.getElementById("ShippingProvince1ID").textContent;

    }
    else {
        var left = document.getElementById("shippingProvince1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("shippingProvince2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_ShippingProvinceID").value = document.getElementById("shippingProvince2ID").textContent;
    }
};

document.getElementById("toggle_ShippingPostal").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("shippingPostal1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold"); 

        var right = document.getElementById("shippingPostal2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold"); 

        document.getElementById("input_ShippingPostal").value = left.textContent;

    }
    else {
        var left = document.getElementById("shippingPostal1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold"); 

        var right = document.getElementById("shippingPostal2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold"); 

        document.getElementById("input_ShippingPostal").value = right.textContent;
    }
};

document.getElementById("toggle_Active").onclick = function () {

    if (!this.checked) {

        document.getElementById("input_Active").value = document.getElementById("active1").textContent;

    }
    else {

        document.getElementById("input_Active").value = document.getElementById("active2").textContent;
    }
};

document.getElementById("toggle_CreditCheck").onclick = function () {

    if (!this.checked) {

        document.getElementById("input_CreditCheck").value = document.getElementById("creditCheck1").textContent;

    }
    else {

        document.getElementById("input_CreditCheck").value = document.getElementById("creditCheck2").textContent;
    }
};



document.getElementById("toggle_Types").onclick = function () {

    if (!this.checked) {
        var left = document.getElementById("types1");
        left.classList.remove("text-secondary");
        left.classList.add("text-primary"); left.classList.add("font-weight-bold");

        var right = document.getElementById("types2");
        right.classList.remove("text-primary");
        right.classList.add("text-secondary"); right.classList.remove("font-weight-bold");

        document.getElementById("selection2").style.display = "none";
        document.getElementById("selection1").style.display = "flex";
    }
    else {
        var left = document.getElementById("types1");
        left.classList.remove("text-primary");
        left.classList.add("text-secondary"); left.classList.remove("font-weight-bold");

        var right = document.getElementById("types2");
        right.classList.remove("text-secondary");
        right.classList.add("text-primary"); right.classList.add("font-weight-bold");

        document.getElementById("selection1").style.display = "none";
        document.getElementById("selection2").style.display = "flex";
    }
};