requirejs.config({
    "baseUrl": "/content/BornaAndishe/assets/js",
   paths: {
      "jquery": "../libs/jquery/dist/jquery",
      "common": "module/_common",
      "colorbox": "../libs/jquery-colorbox/jquery.colorbox-min",
   },
   shim: {
      "colorbox": {
         deps: ["jquery"],
         exports: '$'
      },
   },
   enforceDefine: true
});


define([
   "jquery",
   "common",
   "colorbox",

], function(
   jquery,
   common,
   colorbox
) {

   (function($) {

      $('.colorbox').colorbox({ rel: 'colorbox' });

   })(jQuery);

});
