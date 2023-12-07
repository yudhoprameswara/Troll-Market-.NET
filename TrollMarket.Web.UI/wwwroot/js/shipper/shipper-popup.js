(function(){
    addCloseButtonListener();
    addInsertButtonListener();
    addSubmitFormListener();
    addEditButtonListener();
   
    // deleteButtonListener();
}())

function deleteButtonListener(){
    $('.delete-category').click(function(event){
        event.preventDefault();
        let category = $(this).attr('data-id');
        $.ajax({
            url:`/api/book/delete/category/${category}`,
            method:'DELETE',
            success: function(response){
                
            }
            ,
            error: function(dependencies){
                console.log(dependencies)
                $('.dependencies').text(dependencies.responseText)
                $('.modal-layer').addClass('modal-layer--opened');
                $('.delete-dialog').addClass('popup-dialog--opened');
            }
        });
    })
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

function addInsertButtonListener(){
    $('.create-container .create-button').click(function(event){
       event.preventDefault();
       $('.service-input').removeClass('hidden')
       $('.modal-layer').addClass('modal-layer--opened');
       $('.form-dialog').addClass('popup-dialog--opened');
    });
}

function addEditButtonListener(){

    $('.shipper-button .edit-button').click(function(event){
        event.preventDefault();
        // $('.service-input').remove();
        $('.service-input').addClass('hidden')
        let id = $(this).attr('data-id');
        $.ajax({
            url:`/shipper/${id}`,
            success: function(response){
                 populateInputForm(response);
                 $('.modal-layer').addClass('modal-layer--opened');
                 $('.form-dialog').addClass('popup-dialog--opened');
            }
        });
    });
}

function populateInputForm({id,name,price,service}){
    $('.form-dialog .shipperId').val(id);
    $('.form-dialog .name').val(name);
    $('.form-dialog .price').val(price);
    if(service == 'No'){
        $('.form-dialog .service').removeAttr('checked')
    }
}

function addSubmitFormListener(){
    $('.form-dialog button').click(function(event){
        event.preventDefault();
        let dto = collectInputForm();
        $.ajax({
            method: 'POST',
            url:'/shipper/upsert',
            data: JSON.stringify(dto),
            contentType : 'application/json',
            success : function(response){
                location.reload();
            },
            error: function({status ,responseJSON}){
                if(status === 422){
                writeValidationMessage(responseJSON);
                }
            }
        });
    });
    
}


function collectInputForm(){
   let id =$('.form-dialog .shipperId').val();
   let dto = {
        id: (id === "")? null : id,
        name : $('.form-dialog .name').val(),
        price : $('.form-dialog .price').val(),
        service: $('.form-dialog .service').prop('checked')?'Yes':'No'
        };
   return dto;
}

function writeValidationMessage(errorMessages){
    for (let error of errorMessages){
        let {field,message} = error;
        $(`.form-dialog [data-for=${field}]`).text(message);
    }
}
