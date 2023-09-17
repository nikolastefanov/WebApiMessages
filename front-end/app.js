const appUrl='https://localhost:44311/api/'

let currentUsername=null

const Url=appUrl+'messages/all'

//console.log(Url)





function chooseUsername(){
    let username=$('#username').val()

    if (username.length===0) {
        alert('You cannot choose an empty username!')
        return
    }
     currentUsername=username
    console.log(currentUsername)
    $('#username-choose').text(currentUsername)
    $('#choose-data').hide()
    $('#reset-data').show()
}

function resetUsername(){
    $('#choose-data').show()
    $('#reset-data').hide()

}



/*
$('#reset-data').hide()


setInterval(loadMessages,1000)
*/