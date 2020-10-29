requirejs.config({
    "baseUrl": "/content/BornaAndishe/assets/js",
   paths: {
      "jquery": "../libs/jquery/dist/jquery",
      "common": "module/_common",
      "owlCarousel": "../libs/owl.carousel/dist/owl.carousel.min",
      "accordion": "../libs/woco-accordion/woco.accordion.min",
   },
   shim: {
      "owlCarousel": {
         deps: ["jquery"],
         exports: '$'
      },
      "accordion": {
         deps: ["jquery"],
         exports: '$'
      },

   },
   enforceDefine: true
});


define([
   "jquery",
   "common",
   "owlCarousel",
   "accordion",

], function(
   jquery,
   common,
   owlCarousel,
   accordion
) {

   (function($) {


      $(".accordion").accordion();


      $('#boxSlider').owlCarousel({
          autoplay:true,
          autoplayTimeout:10000,
          rtl:true,
          loop:true,
          margin:15,
          nav:false,
          dots:false,
          responsive:{
              0:{
                  items:1
              },
              600:{
                  items:1
              },
              1000:{
                  items:1
              }
          }
      });

      $('#logoSlider').owlCarousel({
          autoplay:true,
          autoplayTimeout:3000,
          rtl:true,
          loop:true,
          margin:50,
          nav:false,
          dots:true,
          responsive:{
              0:{
                  items:1
              },
              600:{
                  items:1
              },
              1000:{
                  items:6
              }
          }
      });

   })(jQuery);

});
