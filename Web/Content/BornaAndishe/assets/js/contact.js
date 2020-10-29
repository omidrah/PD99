requirejs.config({
    "baseUrl": "/content/BornaAndishe/assets/js",

  paths: {
      //   "jquery": "../libs/jquery/dist/jquery",
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
  function initialize() {
    var mapProp = {
      center: new google.maps.LatLng(35.723415, 51.419142),
      zoom: 19,
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
