$(function () {
    $('#billTable').DataTable({
        "language": {
            "search": "",
            "zeroRecords": "Nema rezultata!"
        },
        "pageLength": 10,
        "lengthChange": false,
        "info": false
    });

    $('#billTable_filter label input').addClass('form--input').attr('placeholder', 'Pretraga');
    $('#billTable_wrapper .row:first-child .col-sm-12:first-child').html(`<a href="/Bill/Create" class="btn btn-general btn--green shadow-md mb-2">Novi racun</a>`);

});