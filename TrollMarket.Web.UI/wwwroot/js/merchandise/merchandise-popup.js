(function(){
    addDetailButtonListener();
    addCloseButtonListener();
}());

function addDetailButtonListener(){
    $('.info-button').click(function(event){
        var id = $(this).attr('data-id');
        $.ajax({
            url:`/merchandise/detail/${id}`,
            success: function(response){
                 $('.modal-layer').addClass('modal-layer--opened');
                 $('.detail-dialog').addClass('popup-dialog--opened');
                 populateDetail(response);
            }
        });
    });
}
function populateDetail({name,category,description,price,discontinue}){
    $('.detail-dialog .name').text(name);
    $('.detail-dialog .category').text(category);
    $('.detail-dialog .description').text(description);
    $('.detail-dialog .price').text(price);
    var continuity = "";
    if(discontinue == true){
        continuity = "Yes"
    }
    else continuity = "No"
    $('.detail-dialog .discontinue').text(continuity);
}

function addCloseButtonListener(){
    $('.close-button').click(function(event){
       $('.modal-layer').removeClass('modal-layer--opened');
       $('.popup-dialog').removeClass('popup-dialog--opened');
    });
}