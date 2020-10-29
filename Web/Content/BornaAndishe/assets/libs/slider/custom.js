//Theme Options
var themeElements = {
    siteWrap: '.site-wrap',
    footerWrap: '.footer-wrap',
    mainMenu: '.header-navigation',
    selectMenu: '.select-menu',
    ratingForm: '.rating-form',
    themexSlider: '.themex-slider',
    parallaxSliderClass: 'parallax-slider',
    toolTip: '.tooltip2',
    toolTipWrap: '.tooltip-wrap',
    tooltipSwitch: '.switch-button',
    button: '.button2',
    submitButton: '.submit-button',
    printButton: '.print-button',
    facebookButton: '.facebook-button',
    toggleTitle: '.toggle-title',
    toggleContent: '.toggle-content',
    toggleContainer: '.toggle-container',
    accordionContainer: '.accordion',
    tabsContainer: '.tabs-container',
    tabsTitles: '.tabs',
    tabsPane: '.pane',
    playerContainer: '.jp-container',
    playerSource: '.jp-source a',
    player: '.jp-jplayer',
    playerFullscreen: '.jp-screen-option',
    placeholderFields: '.popup-form input',
    userImageUploader: '.user-image-uploader',
    popupContainer: '.popup-container',
    googleMap: '.google-map-container',
    woocommercePrice: '.product-price',
    woocommerceTotal: 'tr.total',
    widgetTitle: '.widget-title',
    ajaxForm: '.ajax-form',
};

//DOM Loaded
jQuery(document).ready(function($) {

    //Sliders
    $('.themex-slider').each(function() {
        var sliderOptions= {
            speed: parseInt($(this).find('.slider-speed').val()),
            pause: parseInt($(this).find('.slider-pause').val()),
            effect: $(this).find('.slider-effect').val()
        };

        $(this).themexSlider(sliderOptions);
    });


});
