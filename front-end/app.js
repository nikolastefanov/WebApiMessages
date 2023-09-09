const appUrl='https://localhost:44311/api/'

let currentUsername=null

const Url=appUrl+'messages/all'

console.log(Url)


/*
function loadMessages(){
    $.get({
        url:Url,
        succes:function succes(data){
            console.log(Url)
            console.log(data);
            renderMessages(data);
        },
        error:function error(error){
            console.log(error)
        }
    })
}

*/



function loadMessages(){
   // $('#messages').empty()

fetch(Url)
.then((data)=>data.json())
.then(data=>renderMessages(data))
.catch(e=>console.log(e))

/*
    $.get(Url, function(data){
        // console.log(Url)
        //console.log(data);
        renderMessages(data);
      });

      */
}


function renderMessages(data){

    $('#messages').empty()

    for(let message of data){

        //console.log(message)

       $('#messages').append('<div class="message d-flex justify-content-start"><strong>'
       + message.user
       + '</strong>: '
       + message.content
       +'</div>')
    }
}


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


function createMessage(){
    let username=currentUsername
    let message=$('#message').val()

    if (username==null) {
        alert('You cannot send a message befor choosing an username!')
        return
    }

    if (message.length===0) {
        alert('You cannot send emty message!')
        return
    }


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
}

$('#reset-data').hide()


setInterval(loadMessages,1000)