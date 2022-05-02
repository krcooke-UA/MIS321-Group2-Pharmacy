function handleAccount() {
  var userType = localStorage.getItem("TidePharmacy-Type").replace(/^"(.+(?="$))"$/, '$1');
  if(userType == "Customer") {
    window.location.href = "CustomerPortal.html";
  }
  else {
    window.location.href = "AdminPortal.html";
  }
}
  