const baseCustomer = "https://localhost:5001/api/User";
var userId = localStorage.getItem("TidePharmacy-User").replace(/^"(.+(?="$))"$/, '$1');
var myCustomer = {};

function handleOnLoad() {
    getUserInfo();
}
function getUserInfo() {
    const getCustomerApiUrl = baseCustomer + "/" + userId
    fetch(getCustomerApiUrl).then(function(response) {
        return response.json();
    }).then(function(json) {
        console.log(json);
        myCustomer = json[0];
        console.log(myCustomer);
		populateForm(myCustomer);
    }).catch(function(error) {
        console.log(error);
    });
}

function putUser() {
    const putCustomerApiUrl = baseCustomer + "/" + userId
    const sendCustomer = {
        id: parseInt(userId),
        fname: document.getElementById("FirstName").value,
        lname: document.getElementById("LastName").value,
        dob: document.getElementById("DOB").value,
        gender: document.getElementById("Gender").value,
        city: document.getElementById("City").value,
        state: document.getElementById("State").value,
        zipcode: parseInt(document.getElementById("Zipcode").value),
        street: document.getElementById("Street").value
    }
    fetch(putCustomerApiUrl, {
        method: "PUT",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json',
        },
        body: JSON.stringify(sendCustomer)
    })
    .then((response)=>{
        myCustomer = sendCustomer;
        populateList();
        blankFields();
    });
}

function populateForm(myCustomer) {
    document.getElementById("FirstName").value = myCustomer.fname;
    document.getElementById("LastName").value = myCustomer.lname;
    document.getElementById("Email").value = myCustomer.email;
    document.getElementById("DOB").value = myCustomer.dob;
    document.getElementById("Gender").value = myCustomer.gender;
    document.getElementById("City").value = myCustomer.city;
    document.getElementById("State").value = myCustomer.state;
    document.getElementById("Zipcode").value = myCustomer.zipcode;
    document.getElementById("Street").value = myCustomer.street;
}


function handleEditClick() {
    makeEditable();
    hideButtons();
    var buttonHtml = "<button class=\"btn btn-primary btn-lg\" onclick=\"handleEditSave("+userId+")\">Save</button>"
    buttonHtml += "<button class=\"btn btn-warning btn-lg btn-cancle\" onclick=\"handleCancelSave()\">Cancel</button>"
    document.getElementById("saveButton").innerHTML = buttonHtml;
    document.getElementById("saveButton").style.display="inline-block";
}
function handleEditSave(userId) {
    putUser(userId);
    makeReadOnly();
    showButtons();
}
function handleCancelSave() {
    populateForm();
    makeReadOnly();
    showButtons();
}



function makeReadOnly() {
    document.getElementById("FirstName").readOnly = true;
    document.getElementById("LastName").readOnly = true;
    document.getElementById("Gender").readOnly = true;
    document.getElementById("DOB").readOnly = true;
    document.getElementById("City").readOnly = true;
    document.getElementById("State").readOnly = true;
    document.getElementById("Zipcode").readOnly = true;
    document.getElementById("Street").readOnly = true;
}
function makeEditable() {
    document.getElementById("FirstName").readOnly = false;
    document.getElementById("LastName").readOnly = false;
    document.getElementById("Gender").readOnly = false;
    document.getElementById("DOB").readOnly = false;
    document.getElementById("City").readOnly = false;
    document.getElementById("State").readOnly = false;
    document.getElementById("Zipcode").readOnly = false;
    document.getElementById("Street").readOnly = false;
}

function hideButtons() {
    document.getElementById("editButton").style.display = "none";
    document.getElementById("backButton").style.display = "none";
}
function showButtons() {
    document.getElementById("editButton").style.display = "inline-block";
    document.getElementById("backButton").style.display = "inline-block";
    document.getElementById("saveButton").style.display = "none";
}