requirejs.config({
    "baseUrl": "/content/bornaandishe/assets/js",
    paths: {
        "jquery": "../libs/jquery/dist/jquery",
        "bootstrap": "../libs/bootstrap/dist/js/bootstrap.min",
        "owlCarousel": "../libs/owl.carousel/dist/owl.carousel.min",
    },
    shim: {
        "bootstrap": {
            deps: ["jquery"],
            exports: "$.fn.popover"
        },
        "owlCarousel": {
            deps: ["jquery"],
            exports: '$'
        },
    },
    enforceDefine: true
});


define([
    "bootstrap",
    "owlCarousel",

], function(
    bootstrap,
    owlCarousel
) {

    (function($) {

        $('.navbar').each(function() {
            var menu = $('.nav > li'),
                menuNested = $('.nav > li > ul li');
            menu.each(function() {
                menu.has('ul').addClass('submenu');
            });
            menuNested.each(function() {
                menuNested.has('ul').addClass('submenu2');
            });
        });

        $('.panel .panel-body').each(function() {
                panelNested = $('ul > li');
            panelNested.each(function() {
                panelNested.has('ul').addClass('submenu');
            });
        });

        $('.side-menu-button').on('click', function(){
           $('.side-menu-hidden').toggleClass('open');
        });


        $('.header_topbar__search .btn-search').on('click', function(){
          $('form').toggleClass('open');
        });

        $('.user-login').on('click', function(){
           $('.login-form').toggleClass('open');
        });

        $('#ColorBox li').on('click', function() {
            var selectedClass = $(this).attr('class');

            $('#showoption').
            removeClass('theme-color-purple theme-color-default theme-color-red theme-color-orange theme-color-green theme-color-blue theme-color-lightblue theme-color-darkblue').
            addClass(selectedClass);

         });

        $('#comment').owlCarousel({
            autoplay: true,
            autoplayTimeout: 3000,
            rtl: true,
            loop: true,
            margin: 50,
            nav: false,
            dots: true,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 1
                }
            }
        });

    })(jQuery);

});
