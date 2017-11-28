// Validator defaults
$.validator.setDefaults({ ignore: '' }); // Don't ignore hidden fields
    
// ready
$(function () {

    // Messaging
    if (!window.messageTemplate) {
        var source = $("#msg-template").html();
        window.messageTemplate = Handlebars.compile(source);                
    }

    // Data Tables
    $('.data-table').DataTable({
        bLengthChange: false,
        bFilter: false,
        paging: false,
        bInfo: false
    });

    // Delete Conf and Api Call
    $('[data-id]').click(function () {
        var type = $(this).data('type');
        var id = $(this).data('id');

        var callback = function () {
            app.state.working();
            $.ajax({
                url: window.root + '/api/' + type + '/' + id,
                method: 'delete',
                success: function (data) {
                    if (data.success) {
                        $('[data-id=' + id + ']').parents('tr').remove();
                        app.msg('success', 'Yay!', 'The ' + type + ' has been deleted sucessfully', true);
                        if (!$('tbody tr').length) {
                            window.location.reload();
                        } else {
                            app.state.ready();
                        }
                    }
                },
                error: function () {
                    app.state.ready();
                    app.error();
                }
            });
        };
        app.confirm('Delete this ' + type + '?', 'Are you sure you want to delete this ' + type + '?', 'Delete ' + type, callback);
    });    
});

var app = (function () {
    "use strict";
    return {
        // Manage the state of the application
        state: (function () {
            return {
                ready: function () { $('#standby.modal').modal('hide'); },
                working: function () {
                    $('#standby.modal').modal({ backdrop: 'static' });
                    $('#standby.modal').show();
                }
            };
        }()),

        // Shows an alert modal
        alert: function (text, caption, hideCallback) {
            if ((text && text.trim().length === 0)) {
                return;
            }
            app.state.ready();
            $('#modal.modal .modal-footer button').remove();
            $('#modal.modal .modal-footer').append('<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>');
            $('#modal.modal .modal-title').text(caption || "Alert");
            $('#modal.modal .modal-body p').text(text);
            $('#modal.modal').modal({ backdrop: 'static' }).on('hide.bs.modal', hideCallback);
        },

        // Shows an error modal
        error: function (message) {
            app.state.ready();
            app.alert(message || "We're sorry, an unexpected error has occurred.", "Error");
        },

        // Messages
        msg: function (type, caption, message, autoClose) {
            var message = { caption: caption, message: message };
            message.class = type === "Error" ? "danger" : type === "Warn" ? "warning" : type === "Info" ? "info" : "success";
            message.icon = (type === "Error" || type === "Warning") ? "exclamation-sign" : type === "Info" ? "info-sign" : "ok-sign";
            $('#messages').append(window.messageTemplate(message));
        },

        // Shows a confirm modal
        confirm: function (caption, message, buttonText, actionCallback) {
            app.state.ready();
            $('#modal.modal .modal-footer button').remove();
            $('#modal.modal .modal-footer').append('<button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>');
            $('#modal.modal .modal-footer').append('<button type="button" class="btn-action btn btn-danger" data-dismiss="modal">' + buttonText + '</button>');
            $('#modal.modal .modal-title').text(caption);
            $('#modal.modal .modal-body p').text(message);
            $('#modal.modal').modal({ backdrop: 'static' });

            if (actionCallback != null) {
                $('#modal.modal .modal-footer button.btn-action').on('click', actionCallback);
            }
        }
    }
}());