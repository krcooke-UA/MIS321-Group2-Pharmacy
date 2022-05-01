const validateUrl = "https://localhost:5001/api/Auth/Validate-Token";

const validateLogin = async() => {
    const authToken = JSON.parse(localStorage.getItem("TidePharmacy-Token"));
    if(authToken == "" || authToken == null) {
        alert("Not logged in!");
        return;
    }
    try {
        const response = await fetch(validateUrl, {
            method: "POST",
            headers: {
                "Accept": 'application/json',
                "Content-Type": 'application/json',
            },
            body: JSON.stringify(authToken)
        });
        const data = await response.json();
        console.log(data);
        if(data) {
            console.log("yay");
            return;
        }
        window.location.href = "login.html";
    } catch (error) {
        
    }
}

validateLogin();