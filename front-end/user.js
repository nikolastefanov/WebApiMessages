//const APP_SERVICE_URL=require('app.js')

function login(){

    
    let username=$('#username-login').val()
    let password=$('#password-login').val()

    $('#username-login').val('')
    $('#password-login').val('')

    let requestBody={
        username:username,
        password:password
    }
    

    console.log(requestBody);
    $('#guest-navbar').hide();
    $('#caption').text('Welcome to Chat-Inc!');
    hideLoginAndRegisterAndShowLoggedInData();


    
    
         $.post({
             url: APP_SERVICE_URL+'users/login',
             data: JSON.stringify(requestBody),
             success: function (data) {

                $('#guest-navbar').removeClass('d-flex')
                $('#guest-navbar').addClass('d-none')

              hideGuestNavbar()

                $('#caption').text('Welcome to Chat-Jue!')

                console.log(data)

                hideGuestNavbar()

                let token=data.rawHeader
                   +'.'
                   +data.rawPayload
                   +'.'
                   +data.rawSignature;

                    // console.log(token)

                   saveToken(token)

                   $('#username-logged-in').text(getUser())

                   hideLoginAndRegisterAndShowLoggedInData()


        //         // CHANGE CAPTION TO 'Welcome to Chat-Inc!'
        //         // Save token to localStorage using saveToken()
        //         // EXTRACT FROM JWT TOKEN currently logged in user's username
        //         // Logged-in-data visualize
        //         // Hide Guest Navbar
             },
             error: function (error) {
                 console.error(error);
            }
         });
}


function register() {
    let username = $('#username-register').val();
    let password = $('#password-register').val();

    $('#username-register').val('');
    $('#password-password').val('');

    let requestBody = {
        username: username,
        password: password
    };

 console.log(requestBody)

/*
 $.post({
    url:appUrl+'messages/create',
    headers:{
        'Content-Type':'application/json'
    },
    data:JSON.stringify({
        content:message,
        user:username
    }),
    succes:function(data){
        loadMessages()
    },
    error: function(error){
        console.log(error)
    }
})
*/

    $.post({
        url: APP_SERVICE_URL + 'users/register',
        headers:{'Content-Type':'application/json'},
        data: JSON.stringify({requestBody}),
        success: function (data) {
            console.log(...data)
             togleLogin();
        },
        error: function (error) {
            console.error(error);
        }
    });
}

/*
fetch(APP_SERVICE_URL + 'users/register', {
      method: 'POST',
      body: JSON.stringify({
        requestBody
      }),
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(promise => {
      console.log(promise)
      return promise.json()
    }).then(response => {
      //console.log(...data)
    })
}
*/

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
    togleLogin();

    $('#logged-in-data').hide();
}


function togleRegister(){
    $('#login-data').hide()
    $('#register-data').show()
}


function logout(){

    //TODO: copy functionality described in the Exercise

    $('#caption').text('Choose your username to begin chatting!');

    showGuestNavbar()

    showLoginAndHideLoggedInData();
    
    $('#logged-in-data').hide()

    togleLogin()
}

function saveToken(token) {
    localStorage.setItem('auth_token', token);
}

function evictToken() {
    localStorage.removeItem('auth_token', token);
}

function getUser() {
    let token = localStorage.getItem('auth_token');

    let claims = token.split('.')[1];
    let decodedClaims = atob(claims);
    let parsedClaims = JSON.parse(decodedClaims);

    return parsedClaims.nameid;
}

function isLoggedIn() {
    return localStorage.getItem('auth_token') != null;
}


$('#logged-in-data').hide();
togleLogin();



function hideGuestNavbar(){
    $('#guest-navbar')
        .removeClass('d-block')
        .addClass('d-none')
}


function showGuestNavbar(){
    $('#guest-navbar')
        .removeClass('d-none')
        .addClass('d-block')
}


$('#logged-in-data').hide();
togleLogin()