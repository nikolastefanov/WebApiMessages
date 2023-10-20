$(document).ready(function(){
    $("#first").click(function(){
      $("p").hide(1000);
      alert("skrivam se!")
    });
  });


  $(document).ready(function(){
    $("#second").click(function(){
      $("p").show(1000);
    });
  });


  $("#tirth").click(function(){
    $.get("https://www.youtube.com/watch?v=pLcaXieNlDY", function(data, status){
      alert("Data: " + data + "\nStatus: " + status);
    });
  })

  open.window("https://www.youtube.com/watch?v=pLcaXieNlDY")