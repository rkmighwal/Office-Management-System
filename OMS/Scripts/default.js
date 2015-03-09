/* Credit: http://www.templatemo.com */
    
$(document).ready( function() {        

	// sidebar menu click
	$('.templatemo-sidebar-menu li.sub a').click(function(){
		if($(this).parent().hasClass('open')) {
			$(this).parent().removeClass('open');
		} else {
			$(this).parent().addClass('open');
		}
	});  // sidebar menu click

    // Model Form Functions

	jQuery.ajaxSetup({ cache: false });

	jQuery("a[data-modal]").on("click", function (e) {

	    // hide dropdown if any
	    jQuery(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');


	    jQuery('#modelContent').load(this.href, function () {
	        jQuery('#modalPlaceHolder').modal({
	            /*backdrop: 'static',*/
	            keyboard: true
	        }, 'show');

	        bindForm(this);
	    });

	    return false;
	});

}); // document.ready

function bindForm(dialog) {

    jQuery('form', dialog).submit(function () {
        jQuery.ajax({
            url: this.action,
            type: this.method,
            data: jQuery(this).serialize(),
            success: function (result) {
                if (result.message)
                    alert(result.message);

                if (result.success) {
                    jQuery('#modalPlaceHolder').modal('hide');
                    //Refresh
                    location.reload();
                } else {
                    jQuery('#modelContent').html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}
