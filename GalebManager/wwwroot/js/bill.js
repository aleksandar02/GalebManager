$(function () {
    $('#billTable').DataTable({
        "processing": true,
        "language": {
            "search": "",
            "zeroRecords": "Nema rezultata!"
        },
        "pageLength": 10,
        "lengthChange": false,
        "info": false,
        "bFilter": false
    });

    $('#dateFrom').datepicker(
        'setDate', '-30d'
    );
    $('#dateTo').datepicker(
        'setDate', 'now'
    );

    $('#searchForm').submit(function (e) {
        e.preventDefault();
        
        const store = $("#stores option").filter(":selected").val();
        const supplier = $("#supplier option").filter(":selected").val();
        const factureStatus = $('#factured').val();
        const dateFrom = $('#dateFrom').val();
        const dateTo = $('#dateTo').val();

        let bills = "";

        let billFilter = {
            storeId: store,
            supplierId: supplier,
            factureStatus: factureStatus,
            dateFrom: dateFrom,
            dateTo: dateTo
        };

        bills = getRequest("/Bill/SearchBills", billFilter);

        console.log(bills);

        let container = $('#billTableData');
        container.html("");

        renderTableData(container, bills);
    });



    function getRequest(url, data) {
        let result = "";

        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            data: {
                json: JSON.stringify(data)
            },
            error: function (e) {
                console.log('Neuspeh!');
            },
            success: function (response) {
                result = response;
            }
        });

        return result;
    }

    function renderTableData(container, bills) {
        $.each(bills, (index, bill) => {
            const formattedDate = moment(bill.date).format('MM/DD/YYYY');

            container.append(`<tr>
                    <td>
                        ${bill.storeName}
                    </td>
                    <td>
                        ${bill.number}
                    </td>
                    <td>
                        ${bill.factureNumber}
                    </td>
                    <td>
                        ${bill.supplierName}
                    </td>
                    <td>
                        ${formattedDate}
                    </td>
                    <td>
                        ${bill.sum}
                    </td>
                    <td>
                        <a href="#" class="action-link edit-link" data-toggle="modal" data-target="#editBillModal"><i class="fas fa-pen fa-fw"></i>&nbsp;Izmeni</a>
                        <a href="#" class="action-link details-link"><i class="fas fa-info fa-fw"></i>&nbsp;Detalji</a>
                    </td>
                </tr>`);
        });
    }


});