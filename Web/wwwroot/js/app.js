$(".mobile-menuli").click(function(){
    var btnindex = $(this).index()
    $(".mobile-menuul").eq(btnindex).stop().slideToggle(500)
})

$(".menubar i").click(function(){
    $(".mobile-menu").stop().slideToggle(500)
})

window.onload=function(){
    document.querySelector(".preloader").remove()
}

window.onscroll = function() {scrollFunction()};
function scrollFunction(){
    if(document.body.scrollTop > 80 ||
        document.documentElement.scrollTop > 80){
            document.querySelector(".transparent-bar").style.position="fixed";
        } else{
            document.querySelector(".transparent-bar").style.position="relative";
        }
} 

// My Account

$(".panel-heading").click(function(){
    $(this).next().stop().slideToggle(500)
})

// My Account

// Details

$(document).ready(function () {

    var quantitiy = 0;
    $('.quantity-right-plus').click(function (e) {
        e.preventDefault();
        var quantity = parseInt($('#quantity').val());
        $('#quantity').val(quantity + 1);
    });

    $('.quantity-left-minus').click(function (e) {
        e.preventDefault();
        var quantity = parseInt($('#quantity').val());
        if (quantity > 0) {
            $('#quantity').val(quantity - 1);
        }
    });

});