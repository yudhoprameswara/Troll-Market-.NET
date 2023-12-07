(function(){
    addTopUpButtonListener();
    addCloseButtonListener();
    addSubmitFormListener();
}());

function addTopUpButtonListener(){
    $('.top-up-button').click(function(event){
        $('.modal-layer').addClass('modal-layer--opened');
        $('.form-dialog').addClass('popup-dialog--opened');
    });
}

function addCloseButtonListener(){
    $('.close-button').click(function(event){
       $('.modal-layer').removeClass('modal-layer--opened');
       $('.popup-dialog').removeClass('popup-dialog--opened');
       $('.form-dialog input').val("");
       $('.form-dialog textarea').val("");
       $('.form-dialog .validation-message').text("");
    });
}

function addSubmitFormListener(){
    $('.form-dialog button').click(function(event){
        event.preventDefault();

        let topUp = $('.top-up-number-box').val();
        let update = {
            TopUp: topUp
        }
        $.ajax({
            method: 'POST',
            url:`/Profile/TopUp`,
            data: JSON.stringify(update),
            contentType : 'application/json',
            success : function(response){
                console.log(response)
                location.reload();

            }
            ,
            error: function ({responseText}){
                $(`.form-dialog [data-for=topUp]`).text(responseText);

             //   if(status === 422){
             ///*   writeValidationMessage(responseJSON);*/
             //   }
            }
        });
    });
}

function writeValidationMessage(errorMessages){
    for (let error of errorMessages){
        let {field,message} = error;
        $(`.form-dialog [data-for=${field}]`).text(message);
    }
}