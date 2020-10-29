requirejs.config({
    "baseUrl": "/content/BornaAndishe/assets/js",
   paths: {
      "jquery": "../libs/jquery/dist/jquery",
      "bootstrap": "../libs/bootstrap/dist/js/bootstrap.min",
      "colorbox": "../libs/jquery-colorbox/jquery.colorbox-min",
      "owlCarousel": "../libs/owl.carousel/dist/owl.carousel.min",
      // "parallaxSlider": "../libs/Parallax-content-slider/js/jquery.cslider",
      "slider": "../libs/slider/slider",
      "modernizr": "../libs/Parallax-content-slider/js/modernizr.custom",
      "raty": "../libs/raty/lib/jquery.raty",
      "waypoints": "http://cdnjs.cloudflare.com/ajax/libs/waypoints/2.0.3/waypoints.min",
      "counterup": "../libs/Counter-Up/jquery.counterup.min",
      "common": "module/_common",
   },
   shim: {
      "bootstrap": {
        deps: ["jquery"],
        exports: "$.fn.popover"
      },
      "modernizr": {
         deps: ["jquery"],
         exports: '$'
      },
      // "parallaxSlider": {
      //    deps: ["jquery"],
      //    exports: '$'
      // },
      "slider": {
         deps: ["jquery"],
         exports: '$'
      },
      "owlCarousel": {
         deps: ["jquery"],
         exports: '$'
      },
      "colorbox": {
         deps: ["jquery"],
         exports: '$'
      },
      "raty": {
        deps: ["jquery"],
        exports: "$"
      },
      "waypoints": {
        deps: ["jquery"],
        exports: "$"
      },
      "counterup": {
        deps: ["waypoints"],
        exports: "$"
      }

   },
   enforceDefine: true
});


define([
   "jquery",
   "bootstrap",
   "modernizr",
   "slider",
   // "parallaxSlider",
   "owlCarousel",
   "colorbox",
   "raty",
   "counterup",
   "common"

], function(
   jquery,
   slider,
   bootstrap,
   modernizr,
   // parallaxSlider,
   owlCarousel,
   colorbox,
   raty,
   counterup,
   common
) {

   $(document).ready(function(){
      $('.themex-slider').each(function() {
          var sliderOptions= {
             speed: parseInt($(this).find('.slider-speed').val()),
             pause: parseInt($(this).find('.slider-pause').val()),
             effect: $(this).find('.slider-effect').val()
          };

          $(this).themexSlider(sliderOptions);
      });
   });

   (function($) {

      // $('#da-slider').cslider({
		// 	autoplay	: false,
		// 	bgincrement	: 200
		// });

      $('.counter').counterUp({
        delay: 10,
        time: 1000
      });

      var $active = $('#accordion .panel-collapse.in').prev().addClass('active');
      $active.find('a').prepend('<i class="fa fa-caret-down"></i>');
      $('#accordion .panel-heading').not($active).find('a').prepend('<i class="fa fa-caret-left"></i>');
      $('#accordion').on('show.bs.collapse', function (e) {
          $('#accordion .panel-heading.active').removeClass('active').find('.fa').toggleClass('fa-caret-left fa-caret-down');
          $(e.target).prev().addClass('active').find('.fa').toggleClass('fa-caret-left fa-caret-down');
      });

      $('.stars').raty({
         starType: 'i',
         score: function()
   		{
   			return $(this).attr('data-score');
   		}
      });

      $('a.colorbox').colorbox({rel:'colorbox'});

      $('#courses').owlCarousel({
             autoplay:true,
             autoplayTimeout:10000,
             rtl:true,
             loop:true,
             margin:10,
             nav:true,
             dots:false,
             navText: [
                '<div class="btn btn-default"><i class="fa fa-chevron-right"></i></div>',
                '<div class="btn btn-default"><i class="fa fa-chevron-left"></i></div>'
             ],
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

      $('#imageGallery').owlCarousel({
          autoplay:false,
          autoplayTimeout:10000,
          rtl:true,
          loop:true,
          margin:5,
          nav:true,
          navText: [
             '<div class="btn btn-default"><i class="fa fa-chevron-right"></i></div>',
             '<div class="btn btn-default"><i class="fa fa-chevron-left"></i></div>'
          ],
          dots:false,
          responsive:{
              0:{
                  items:1
              },
              600:{
                  items:1
              },
              1000:{
                  items:3
              }
          }
      });

   })(jQuery);

});
