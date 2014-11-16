String.prototype.contains = function (it) { return this.indexOf(it) != -1; };

function BrowseServer(startupPath, functionData) {
    // You can use the "CKFinder" class to render CKFinder in a page:
    var finder = new CKFinder();

    // The path for the installation of CKFinder (default = "/ckfinder/").
    finder.BasePath = '/Content/scripts/plugins/ckfinder/';

    //Startup path in a form: "Type:/path/to/directory/"
    finder.StartupPath = startupPath;

    // Name of a function which is called when a file is selected in CKFinder.
    finder.SelectFunction = SetFileField;

    // Additional data to be passed to the SelectFunction in a second argument.
    // We'll use this feature to pass the Id of a field that will be updated.
    finder.SelectFunctionData = functionData;

    // Launch CKFinder
    finder.Popup();
}

// This is a sample function which is called when a file is selected in CKFinder.
function SetFileField(fileUrl, data) {
    document.getElementById(data["selectFunctionData"]).value = fileUrl;
}

// Stop the indents after opening tag
CKEDITOR.on('instanceReady', function (ev) {
    var tags = ['p', 'ol', 'ul', 'li', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6'];

    for (var key in tags) {
        ev.editor.dataProcessor.writer.setRules(tags[key],
                {
                    indent: false,
                    breakBeforeOpen: true,
                    breakAfterOpen: false,
                    breakBeforeClose: false,
                    breakAfterClose: true
                });
    }
});

$(document).ready(function () {

//    $(document).bind("contextmenu", function (e) {
//        return false;

//    });
    // CK Editor
    var config = {
        toolbar:
		    [
			    ['Source', '-', 'NewPage', 'Preview', '-'],
                ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
                ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
                '/',
                ['Format', 'Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
                ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent'],
                ['JustifyLeft', 'JustifyCenter', 'JustifyRight'],
                ['Link', 'Unlink', 'Anchor'],
                ['Image', 'Flash', 'Table', 'HorizontalRule', 'SpecialChar', 'PageBreak'],
                ['Maximize', 'ShowBlocks', '-', 'About']
		    ],
        stylesCombo_stylesSet: "",
        height: "450px",
        htmlEncodeOutput: true,
        filebrowserBrowseUrl: '/Content/scripts/plugins/ckfinder/ckfinder.html',
        filebrowserImageBrowseUrl: '/Content/scripts/plugins/ckfinder/ckfinder.html?type=Images',
        filebrowserFlashBrowseUrl: '/Content/scripts/plugins/ckfinder/ckfinder.html?type=Flash',
        filebrowserUploadUrl: '/Content/scripts/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',
        filebrowserImageUploadUrl: '/Content/scripts/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images',
        filebrowserFlashUploadUrl: '/Content/scripts/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'
    };

    $('textarea.ckeditor').ckeditor(config);

    var basicConfig = {
        toolbar:
		    [
			    ['Source', '-'],
                ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord'],
                ['Undo', 'Redo', '-', 'RemoveFormat'],
        //'/',
                ['Bold', 'Italic', 'Underline'],
                ['NumberedList', 'BulletedList'],
                ['JustifyLeft', 'JustifyCenter', 'JustifyRight'],
                ['Link', 'Unlink', 'Anchor']
		    ],
        stylesCombo_stylesSet: "",
        height: "150px",
        htmlEncodeOutput: true
    };

    $('textarea.basic-ckeditor').ckeditor(basicConfig);


    // Tree view
    $("#sitemap-treeview").treeview({
        animated: "fast",
        collapsed: true,
        unique: true,
        persist: "cookie",
        cookieId: "treeview-sitemap"
    });

    // Tree view
    $("#mailer-treeview").treeview({
        animated: "fast",
        collapsed: true,
        unique: false,
        persist: "cookie",
        cookieId: "treeview-mailer"
    });

    // Tree view
    $("#treeview").treeview({
        animated: "fast",
        collapsed: false,
        unique: false,
        persist: "cookie",
        cookieId: "treeview"
    });

    InitUI();

    // Statistics tables
    $("table.stats-table a.show-next-row").click(function (e) {
        e.preventDefault();
        $(this).closest('tr').next('tr').toggle();
        $(this).find('span.ui-icon').toggleClass('ui-icon-plus').toggleClass('ui-icon-minus');
    });

    InitRssItemPreviewContent();
    InitDeleteDialogs();
});

function InitUI() {
    $('.ui-tabs').tabs({ cookie: { expires: 30} });

    $('.ajax-ui-tabs').tabs({
        cookie: {
            expires: 30
        },
        cache: false,
        ajaxOptions: {
            cache: false,
            success: function (html) {
                Sys.Mvc.FormContext._Application_Load();
                InitUI();
                InitRssItemPreviewContent();
            },
            error: function (xhr, status, index, anchor) {
                $(anchor.hash).html("Couldn't load this tab.");
            }
        }
    });

    // Submit Buttons
    $("input:submit, input:button").button();

    // Date pickers
    $(".date-picker").datepicker({ dateFormat: "dd/mm/yy", showOn: 'both', buttonImage: '/content/images/icons/calendar-icon.png', buttonImageOnly: true });


    $("a.show-full-detail").click(function (e) { e.preventDefault(); $(this).next('.full-detail').toggle(); });

    $(".page table a.delete-btn").click(function () { return confirm("Are you sure you want to delete?"); });

    $("select.auto-submit").change(function (e) { $(this).closest('form').trigger('submit'); });

    $(".search-filters, .page-actions").addClass("fg-toolbar ui-widget-header ui-corner-all ui-helper-clearfix ui-state-default");

    //all hover and click logic for buttons
    $(".fg-button:not(.ui-state-disabled):not(.no-ui-state-hover)")
         .hover(
         function () {
             $(this).addClass("ui-state-hover");
         },
         function () {
             $(this).removeClass("ui-state-hover");
         }
    )
     .mousedown(function () {
         $(this).parents('.fg-buttonset-single:first').find(".fg-button.ui-state-active").removeClass("ui-state-active");
         if ($(this).is('.ui-state-active.fg-button-toggleable, .fg-buttonset-multi .ui-state-active')) { $(this).removeClass("ui-state-active"); }
         else { $(this).addClass("ui-state-active"); }
     })
     .mouseup(function () {
         if (!$(this).is('.fg-button-toggleable, .fg-buttonset-single .fg-button, .fg-buttonset-multi .fg-button')) {
             $(this).removeClass("ui-state-active");
         }
     })
     .click(function (e) {
         //e.preventDefault();
     });
 }


 function InitRssItemPreviewContent() {
     $("a.rss-item-content-preview-btn").click(function (e) {
         e.preventDefault();
         var $btn = $(this);
         show_ajax_loader('body');

         $.get($btn.attr('href') + "?rand=" + Math.random(), null, function (data) {
             hide_ajax_loader('body');
             $('body').append(data);
             BindFormEvents();
             InitUI();                          
             $('#dialog-preview').dialog({
                 autoOpen: true,
                 width: 600,
                 modal: true,
                 close: function (event, ui) { $(this).dialog().remove(); },
                 buttons: {                     
                     "Close": function () {
                         $(this).dialog("close");
                     }
                 }
             });             
         });
     });
 }


 function InitPageTemplateEnabledToggle() {

     $(".page-template-content-actions .toggle-enable").click(function (e) {
         e.preventDefault();
         var $btn = $(this);
         $.post("/ajax/EnablePageTemplateContent?pagetemplatecontentid=" + $(this).attr('rel') + "&rand=" + Math.random(), null,
            function (data) {
                var result = GetJsonResult(data);
                if (result.Successful) {
                    $btn.addClass('ui-state-disabled');
                    $btn.closest('.page-actions').find('.toggle-disable').removeClass('ui-state-disabled');
                }
            });
     });

     $(".page-template-content-actions .toggle-disable").click(function (e) {
         e.preventDefault();
         var $btn = $(this);
         $.post("/ajax/DisablePageTemplateContent?pagetemplatecontentid=" + $(this).attr('rel') + "&rand=" + Math.random(), null,
            function (data) {
                var result = GetJsonResult(data);
                if (result.Successful) {
                    $btn.addClass('ui-state-disabled');
                    $btn.closest('.page-actions').find('.toggle-enable').removeClass('ui-state-disabled');
                }
            });
     });
 }

 /*--------------------- Attributes -------------------------------*/

 function GetAttributeQueryString() {
     var contenttypeid = $(AttributeSourceButton).closest('.page-actions').find('.contenttypeid').val();
     var contentitemid = $(AttributeSourceButton).closest('.page-actions').find('.contentitemid').val();
     var pagetemplatecontentid = $(AttributeSourceButton).closest('.page-actions').find('.pagetemplatecontentid').val();

     var query = "contenttypeid=" + contenttypeid + "&contentitemid=" + contentitemid + "&pagetemplatecontentid=" + pagetemplatecontentid;
     return query;
 }

 var AttributeSourceButton;

 function InitAttributesDialog() {

     $('.show-attribute-dialog').click(function (e) {
         e.preventDefault();
         show_ajax_loader('body');
         AttributeSourceButton = $(this);         
         $.get("/ajax/AttributeDialog?" + GetAttributeQueryString() + "&rand=" + Math.random(), null, function (data) {
             hide_ajax_loader('body');
             $('body').append(data);
             BindFormEvents();
             InitUI();
             UpdateTemplateContentAttributeFormList();
             $('#dialog-attributes').dialog({
                 autoOpen: true,
                 width: 600,
                 modal: true,
                 close: function (event, ui) { $(this).dialog().remove(); },
                 buttons: {
                     "Close": function () {
                         $(this).dialog("close");
                     }
                 }
             });

             $('#faddtemplatecontent').submit(function () {
                 show_ajax_loader("#dialog-attributes");
                 $.post("/ajax/AddTemplateContentAttribute", $(this).serialize(),
	                function (data) {
	                    hide_ajax_loader("#dialog-attributes");
	                    var result = GetJsonResult(data);
	                    if (!result.Successful) {
	                        $("#error-messages").hide();
	                        ProcessResult(data);
	                        $("#error-messages").slideDown();
	                    }
	                    else {
	                        $("#error-messages").hide();
	                        ProcessResult(data);
	                        $("#error-messages").slideDown();
	                        UpdateTemplateContentAttributeFormList();
	                    }
	                });
                 return false;
             });
         });
     });
 } 

 function UpdateTemplateContentAttributeFormList() {
     $.get("/ajax/AttributeAjaxFormList?" + GetAttributeQueryString() + "&rand=" + Math.random(), {}, function (data) {
         $("#attribute-form-list").html(data); //.fadeOut('slow', function () {
         //$(this).html(data).fadeIn();
         BindFormEvents();
         InitUI();
         $('#fedittemplatecontent').submit(function () {
             show_ajax_loader("#dialog-attributes");
             $.post("/ajax/EditTemplateContentAttributes", $(this).serialize(),
	                function (data) {
	                    hide_ajax_loader("#dialog-attributes");
	                    var result = GetJsonResult(data);
	                    if (!result.Successful) {
	                        $("#error-messages").hide();
	                        ProcessResult(data);
	                        $("#error-messages").slideDown();
	                    }
	                    else {
	                        $("#error-messages").hide();
	                        ProcessResult(data);
	                        $("#error-messages").slideDown();
	                        UpdateTemplateContentAttributeFormList();
	                    }
	                });
             return false;
         });
         $('#fedittemplatecontent .delete-btn').click(function (e) {
             if (!confirm("Are you sure you want to delete?")) return false;
             show_ajax_loader("#dialog-attributes");             
             $.post($(this).attr('href'), null,
	                function (data) {
	                    hide_ajax_loader("#dialog-attributes");
	                    var result = GetJsonResult(data);
	                    if (!result.Successful) {
	                        $("#error-messages").hide();
	                        ProcessResult(data);
	                        $("#error-messages").slideDown();
	                    }
	                    else {
	                        $("#error-messages").hide();
	                        ProcessResult(data);
	                        $("#error-messages").slideDown();
	                        UpdateTemplateContentAttributeFormList();
	                    }
	                });
             return false;
         });
         //})
     });
 }

 /*---------------------------------- DELETE DIALOG ------------------------------*/
 function InitDeleteDialogs() {
     //InitDeleteDialog();
     InitPageTemplateEnabledToggle();
     InitAttributesDialog();
     InitApplicationSettingDeleteDialog();
     InitSitemapDeleteDialog();
     InitDealerDeleteDialog();
     InitVehicleDeleteDialog();
     InitBodyTypeDeleteDialog();
     InitFuelTypeDeleteDialog();
     InitTransmissionDeleteDialog();
     InitVehicleColorDeleteDialog();
     InitVehicleMakeDeleteDialog();
     InitVehicleModelDeleteDialog();
 }

 function InitDeleteDialog(event, obj, url) {
     event.preventDefault();
     show_ajax_loader('body');

     var targetUrl = url.contains('?') ? url : url + "?";

     $.get(targetUrl + $(obj).attr('rel') + "&rand=" + Math.random(), null, function (data) {
         hide_ajax_loader('body');
         $('body').append(data);
         BindFormEvents();
         InitUI();
         $('#dialog-delete').dialog({
             autoOpen: true,
             width: 600,
             modal: true,
             close: function (event, ui) { $(this).dialog().remove(); },
             buttons: {
                 "Ok": function () {
                     $(this).find("form").trigger('submit');
                 },
                 "Cancel": function () {
                     $(this).dialog("close");
                 }
             }
         });
         $('#dialog-cancel').dialog({
             autoOpen: true,
             width: 600,
             modal: true,
             close: function (event, ui) { $(this).dialog().remove(); },
             buttons: {                 
                 "Close": function () {
                     $(this).dialog("close");
                 }
             }
         });         
     });
 }

 /*------------------------------------ SITEMAP -----------------------------------*/
 function InitSitemapDeleteDialog() {

     $('#btn-delete-sitemap').click(function (e) {
         InitDeleteDialog(e, $(this), "/sitemap/deletedialog?sitemapid=");
     });
 }

 /*------------------------------------ BODY TYPE -----------------------------------*/
 function InitBodyTypeDeleteDialog() {

     $('#btn-delete-bodytype').click(function (e) {
         InitDeleteDialog(e, $(this), "/misc/bodytype/deletedialog?bodytypeid=");
     });
 }

 /*------------------------------------ FUEL TYPE -----------------------------------*/
 function InitFuelTypeDeleteDialog() {

     $('#btn-delete-fueltype').click(function (e) {
         InitDeleteDialog(e, $(this), "/misc/fueltype/deletedialog?fueltypeid=");
     });
 }

 /*------------------------------------ TRANSMISSION -----------------------------------*/
 function InitTransmissionDeleteDialog() {

     $('#btn-delete-transmission').click(function (e) {
         InitDeleteDialog(e, $(this), "/misc/transmission/deletedialog?transmissionid=");
     });
 }

 /*------------------------------------ VEHICLE COLOR -----------------------------------*/
 function InitVehicleColorDeleteDialog() {

     $('#btn-delete-vehiclecolor').click(function (e) {
         InitDeleteDialog(e, $(this), "/misc/vehiclecolor/deletedialog?colorid=");
     });
 }

 /*------------------------------------ VEHICLE MAKE -----------------------------------*/
 function InitVehicleMakeDeleteDialog() {

     $('#btn-delete-vehiclemake').click(function (e) {
         InitDeleteDialog(e, $(this), "/misc/vehiclemake/deletedialog?makeid=");
     });
 }

 /*------------------------------------ VEHICLE MODEL -----------------------------------*/
 function InitVehicleModelDeleteDialog() {

     $('#btn-delete-vehiclemodel').click(function (e) {
         InitDeleteDialog(e, $(this), "/misc/vehiclemodel/deletedialog?modelid=");
     });
 }

 /*------------------------------------ VEHICLE -----------------------------------*/
 function InitVehicleDeleteDialog() {

     $('#btn-delete-vehicle').click(function (e) {
         InitDeleteDialog(e, $(this), "/vehicle/deletedialog?vehicleid=");
     });
 }

 /*------------------------------------ DEALER -----------------------------------*/
 function InitDealerDeleteDialog() {

     $('#btn-delete-dealer').click(function (e) {
         InitDeleteDialog(e, $(this), "/misc/dealer/deletedialog?dealerid=");
     });
 }

 /*------------------------------------ APPLICATION SETTING -----------------------------------*/
 function InitApplicationSettingDeleteDialog() {

     $('#btn-delete-applicationsetting').click(function (e) {
         InitDeleteDialog(e, $(this), "/misc/applicationsetting/deletedialog?applicationsettingid=");
     });
 }

 /*--------------------------------------- DIALOG---------------------------------*/

function handleActiveDialog(dialog) {

    LoadControls();
    BindFormEvents();

    $(dialog).hide().css({ "z-index": "9999" });

    var winH = $(window).height();
    var winW = $(window).width();
    var scrollTop = $(window).scrollTop();

    //Set the popup window to center
    $(dialog).css('top', winH / 2 - $(dialog).height() / 2 + scrollTop);
    $(dialog).css('left', winW / 2 - $(dialog).width() / 2);

    // Display pop up and reinit javascript goodiness
    $("#overlay").fadeTo("fast", 0.5).fadeIn("fast", function () { $(dialog).fadeIn(500); });

    $(dialog).find("a.close-btn-trigger").click(function (e) {
        e.preventDefault();
        hide_confirmationpopup(dialog);
    });
    $(dialog).click(function () {
        //hide_confirmationpopup(dialog);
    });
    $("#overlay").click(function () {
        hide_confirmationpopup(dialog);
    });
    $(document).keyup(function (e) {
        if (e.keyCode == 27) {
            $(document).unbind('keyup');
            $(document).unbind('keypress');
            hide_confirmationpopup(dialog);
        }
    });
}

function hide_confirmationpopup(dialog) {
    $(dialog).fadeOut('fast', function (d) {
        $(this).remove();
        $("#overlay").fadeOut('fast', function (e) {
            $(this).remove();
        });
    });
}

/*------------------------------------------------ AJAX LOADER -----------------------------------------------------*/
function show_ajax_loader(container) {
    $(container).append('<div class="ajax-loader"></div>');
}

function hide_ajax_loader(container) {
    $(container).find('.ajax-loader').remove();
}

/*---------------------------------------- STANDARD FORM METHODS-----------------------------------------------*/
function BindFormEvents() {

    var focussed_form;
    $('input').focus(focus_form);
    $('input').blur(unfocus_form);
    /*$('textarea').focus(focus_form);
    $('textarea').blur(unfocus_form);*/
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

/*------------------------------------------- MVC MODELSTATE HANDLER ------------------------------------------------*/
function GetJsonResult(content) {
    //var json = content.get_response().get_object();    
    //return eval(json);
    return content;
}

function ProcessResult(content) {

    var result = GetJsonResult(content);
    $("#error-messages > span").empty();
    $("#error-messages > ul").empty();
    $(':input').removeClass('input-validation-error');
    if (result.Successful) {
        $('#error-messages').append('<span>' + result.Message + '</span>').removeClass('error').addClass('success');
    }
    else {

        $('#error-messages').append('<span>' + result.Message + '</span>').removeClass('success').addClass('error validation-summary');

        $('#error-messages').append('<ul></ul>');
        for (var err in result.Errors) {
            var propertyName = result.Errors[err].PropertyName;
            var errorMessage = result.Errors[err].Error;
            var message = errorMessage;

            propertyName = propertyName.replace(".", "_");

            $('#' + propertyName).addClass('input-validation-error');
            $('#error-messages > ul').append('<li>' + message + '</li>');
        }
    }
}

// Helper for selecting a tab on page load
function SelectTab(tabsId, tabIndex) {
    $(document).ready(function () {
        var $tabs = $(tabsId).tabs();
        $tabs.tabs('select', tabIndex);
    });
}