(function(){
    addDetailButtonListener();
    addCloseButtonListener();
    addtoCartButtonListener();
    addSubmitFormListener();
}());

function addtoCartButtonListener(){
    $('.add-cart-button').click(function(event){
        var id=  $(this).attr('data-id');
        $('.productId').val(id)
        $('.modal-layer').addClass('modal-layer--opened');
        $('.form-dialog').addClass('popup-dialog--opened');
    });
}

function addSubmitFormListener(){
    $('.form-dialog button').click(function(event){
        event.preventDefault();
        var id = $('.productId').val();
        var dto = {
            productId : id,
            quantity : $('.quantity').val(),
            shipmentId : $('.shipper').val()
        }
       
        $.ajax({
            method: 'POST',
            url:'/shop/AddToCart',
            data: JSON.stringify(dto),
            contentType : 'application/json',
            success : function(response){
                console.log("nognol bener");
                location.reload();
            },
            error: function({status ,responseJSON}){
                if(status === 422){
                    writeValidationMessage(responseJSON);
                    console.log("nongol gagal")
                }
            }
        });
    });
}

function addDetailButtonListener(){
    $('.info-button').click(function(event){
        var id = $(this).attr('data-id');
        $.ajax({
            url:`/Shop/Detail/${id}`,
            success: function(response){
                 populateDetail(response);
                 $('.modal-layer').addClass('modal-layer--opened');
                 $('.detail-dialog').addClass('popup-dialog--opened');
            }
        });
    });
}
function populateDetail({name,category,description,price,sellerName}){
    $('.detail-dialog .name').text(name);
    $('.detail-dialog .category').text(category);
    $('.detail-dialog .description').text(description);
    $('.detail-dialog .price').text(price);
    $('.detail-dialog .sellerName').text(sellerName);
}

function addCloseButtonListener(){
    $('.close-button').click(function(event){
       $('.modal-layer').removeClass('modal-layer--opened');
       $('.popup-dialog').removeClass('popup-dialog--opened');
       $('.form-dialog .validation-message').text("");
       $('.form-dialog input').val("");
    });
}

function writeValidationMessage(errorMessages){
    for (let error of errorMessages){
        let {field,message} = error;
        $(`.form-dialog [data-for=${field}]`).text(message);
    }
}