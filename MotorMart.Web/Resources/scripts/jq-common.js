$(document).ready(function () {

    bind_form_events();

    $(".postcode").DefaultValue("A post code please");

    /*------Cascading Dropdown lists-------*/
    $("#makeid").change(function () {
        $.ajax({
            url: '/ajax/getmakemodels',
            data: { makeid: $(this).val() },
            dataType: 'json',
            success: function (data) {
                $('#modelid').html('');
                $.each(data, function (index, item) {
                    $('#modelid').append("<option value=\"" + item.Value + "\">" + item.Text + "</option>");
                });
            }
        });
    });

    /*------QTip applications---------*/
    $(".postcode").qtip({
        content:
        {
            text: $('div.tooltip-content')
        },
        position: {
            my: 'center left',
            at: 'center right'
        },
        //show: 'click',
        //        hide: {
        //            event: 'unfocus'
        //        },
        style: {
            tip: true,
            classes: 'ui-tooltip-light'
        }
    });


    /*---------------//Character count script!--------------*/
    // using id and custom settings
    $('.sms-message').jqEasyCounter({
        'maxChars': 160,
        'maxCharsWarning': 140,
        'msgFontSize': '12px',
        'msgFontColor': '#555555',
        'msgFontFamily': 'Lucida Sans Unicode',
        'msgTextAlign': 'right',
        'msgWarningColor': '#F00',
        'msgAppendMethod': 'insertAfter'
    });

});

function bind_form_events() {

    /* SET FORM FOCUS ENTER SUBMIT OF FOCUSED INPUT  */
    var focussed_form;
    $('input').focus(focus_form);
    $('input').blur(unfocus_form);
    $('select').focus(focus_form);
    $('select').blur(unfocus_form);
    $('a.btn').focus(focus_form);
    $('a.btn').blur(unfocus_form);
    $('.select_bg').focus(focus_form);

    $(document).keypress(function (e) {
        if (e.keyCode == 13) {
            submit_form();
        }
    });

    $('a.preventDefault').click(function (e) {
        e.preventDefault();
    });

    $('a.btn.forinput').click(function (e) {
        e.preventDefault();
        $(this).closest('form').trigger('submit');
    });

}

function focus_form() {
    focussed_form = $(this).closest('form');
}

function unfocus_form() {
    focussed_form = null;
}

function submit_form() {
    $(focussed_form).trigger('submit');
}

/*----------------Default Value Function--------------*/

jQuery.fn.DefaultValue = function (text) {
    return this.each(function () {

        if (this.type == 'text' || this.type == 'password' || this.type == 'textarea') {

            var currentElement = this;
            if (this.value == '') { this.value = text } else { return; }

            $(this).focus(function () {
                if (this.value == text || this.value == '') {
                    this.value = '';
                }
            });

            $(this).blur(function () {
                if (this.value == text || this.value == '') {
                    this.value = text;
                }
            });

            $(this).parents("form").each(function () {
                $(this).submit(function () {
                    if (currentElement.value == text) {
                        currentElement.value = '';
                    }
                });
            });

        }
    });
};

// Helper for selecting a tab on page load
function SelectTab(tabsId, tabIndex) {
    $(document).ready(function () {
        var $tabs = $(tabsId).tabs();
        $tabs.tabs('select', tabIndex);
    });
}