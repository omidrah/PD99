function showConfirm(msg) {
    var self = this;
    self.dfd = $.Deferred();
    var n = noty({
        text: msg,
        type: 'confirm',
        dismissQueue: false,
        layout: 'center',
        theme: 'defaultTheme'
        , modal: true
        , buttons:
            [{
                addClass: 'btn btn-primary', text: 'بلی', onClick: function ($noty) {
                    $noty.close();
                    self.dfd.resolve(true);
                }
            },
            {
                addClass: 'btn btn-danger', text: 'خیر', onClick: function ($noty) {
                    $noty.close();
                    self.dfd.resolve(false);
                }
            }]
    })

    return self.dfd.promise();
}


function showNotification(txt,type,layout,timeout,modal) {
    var n = noty({
        text: txt,
        type: type,
        dismissQueue: true,
        timeout: timeout || 2000,
        modal:modal || false,
        closeWith: ['click'],
        layout: layout || 'topLeft',
        theme: 'relax',
        maxVisible: 1
    });    
}