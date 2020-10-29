requirejs.config({
   "baseUrl": "/content/BornaAndishe/assets/js",
   paths: {
       "jquery": "../libs/jquery/dist/jquery",
       "bootstrap": "../libs/bootstrap/dist/js/bootstrap.min",
       "owlCarousel": "../libs/owl.carousel/dist/owl.carousel.min",
       "raty": "../libs/raty/lib/jquery.raty",
       "common": "module/_common",
      // "jquery": "~/content/BornaAndishe/assets/libs/jquery/dist/jquery",

      // "bootstrap": "~/content/BornaAndishe/assets/libs/bootstrap/dist/js/bootstrap.min",
      // "owlCarousel": "~/content/BornaAndishe/assets/libs/owl.carousel/dist/owl.carousel.min",
      // "raty": "~/content/BornaAndishe/assets/libs/raty/lib/jquery.raty",
      //"common": "module/_common",
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
      "raty": {
        deps: ["jquery"],
        exports: "$"
      },

   },
   enforceDefine: true
});


define([
   "jquery",
   "bootstrap",
   "common",
   "owlCarousel",
   "raty"


], function(
   jquery,
   bootstrap,
   common,
   owlCarousel,
   raty
) {

   (function($) {


      // $('#da-slider').cslider({
		// 	autoplay	: true,
		// 	bgincrement	: 450
		// });

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

      $('#courses-row').owlCarousel({
             autoplay:true,
             autoplayTimeout:10000,
             rtl:true,
             loop:true,
             margin:10,
             nav:true,
             dots:false,
             navText: [
                '<div class="btn"><i class="fa fa-caret-right fa-3x"></i></div>',
                '<div class="btn"><i class="fa fa-caret-left fa-3x"></i></div>'
             ],
             responsive:{
                 0:{
                     items:1
                 },
                 700:{
                     items:2
                 },
                 1000:{
                     items:4
                 }
             }
      });

      $('#courses1, #courses2, #courses3, #courses4').owlCarousel({
             autoplay:true,
             autoplayTimeout:10000,
             rtl:true,
             loop:true,
             margin:10,
             nav:true,
             dots:false,
             navText: [
                '<div class="btn"><i class="fa fa-fast-forward fa-lg"></i></div>',
                '<div class="btn"><i class="fa fa-backward fa-lg"></i></div>'
             ],
             responsive:{
                 0:{
                     items:1
                 },
                 700:{
                     items:1
                 },
                 1000:{
                     items:1
                 }
             }
      });


   })(jQuery);

});
