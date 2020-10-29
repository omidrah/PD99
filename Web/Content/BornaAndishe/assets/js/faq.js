requirejs.config({
    "baseUrl": "/content/BornaAndishe/assets/js",

  paths: {
        "jquery": "../libs/jquery/dist/jquery",
        "common": "module/_common"
    },
    shim: {

    },
    enforceDefine: true
});

define([
    "common",
],
function(
    common
) {

   (function($) {

      var $active = $('#accordion .panel-collapse.in').prev().addClass('active');
      $active.find('a').prepend('<i class="fa fa-minus-circle"></i>');
      $('#accordion .panel-heading').not($active).find('a').prepend('<i class="fa fa-plus-circle"></i>');
      $('#accordion').on('show.bs.collapse', function (e) {
          $('#accordion .panel-heading.active').removeClass('active').find('.fa').toggleClass('fa-plus-circle fa-minus-circle');
          $(e.target).prev().addClass('active').find('.fa').toggleClass('fa-plus-circle fa-minus-circle');
      });


   })(jQuery);

  function initialize() {
    var mapProp = {
      center: new google.maps.LatLng(35.723415, 51.419142),
      zoom: 17,
      scrollwheel: false,
      zoomControl: false,
      panControl: false,
      draggable: false,

      mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("gmap"), mapProp);
  }
  google.maps.event.addDomListener(window, 'load', initialize);

});
