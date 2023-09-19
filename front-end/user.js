const APP_SERVICE_URL=require('app.js')

function login(){
    let username=$('#username-login').val()
    let password=$('#password-login').val()
    let requestBody={
        username:username,
        password:password
    }
    

    console.log(requestBody);
    $('#guest-navbar').hide();
    $('#caption').text('Welcome to Chat-Inc!');
    hideLoginAndRegisterAndShowLoggedInData();


    
    
        // $.post({
        //     url: APP_SERVICE_URL + 'users/login',
        //     data: JSON.stringify(requestBody),
        //     success: function (data) {
        //         // CHANGE CAPTION TO 'Welcome to Chat-Inc!'
        //         // Save token to localStorage using saveToken()
        //         // EXTRACT FROM JWT TOKEN currently logged in user's username
        //         // Logged-in-data visualize
        //         // Hide Guest Navbar
        //     },
        //     error: function (error) {
        //         console.error(error);
        //     }
        // });
}


function register() {
    let username = $('#username-register').val();
    let password = $('#password-password').val();

    $('#username-register').val('');
    $('#password-password').val('');

    let requestBody = {
        username: username,
        password: password
    };

    $.post({
        url: APP_SERVICE_URL + 'users/register',
        data: JSON.stringify(requestBody),
        success: function (data) {
            // toggleLogin();
        },
        error: function (error) {
            console.error(error);
        }
    });
}




function togleLogin(){
    $('#login-data').show()
    $('#register-data').hide()
}


function hideLoginAndRegisterAndShowLoggedInData() {
    $('#login-data').hide();
    $('#register-data').hide();

    $('#logged-in-data').show();
}


function showLoginAndHideLoggedInData() {
    toggleLogin();

    $('#logged-in-data').hide();
}


function togleRegister(){
    $('#login-data').hide()()
    $('#register-data').show()
}


function logout(){

    //TODO: copy functionality described in the Exercise


    $('#logged-in-data').hide()

    togleLogin()
}