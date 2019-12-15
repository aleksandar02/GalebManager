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

});

// Search
$('#searchForm').submit(function (e) {
    e.preventDefault();

    let billFilter = createBillFilter();

    let bills = "";

    bills = getRequest("/Bill/SearchBills", billFilter);

    let container = $('#billTableData');
    container.html("");

    renderTableData(container, bills);
});

function createBillFilter() {
    const store = $("#stores option").filter(":selected").val();
    const supplier = $("#supplier option").filter(":selected").val();
    const factureStatus = $('#factured').val();
    const dateFrom = $('#dateFrom').val();
    const dateTo = $('#dateTo').val();

    let billFilter = {
        storeId: store,
        supplierId: supplier,
        factureStatus: factureStatus,
        dateFrom: dateFrom,
        dateTo: dateTo
    };

    return billFilter;
}

// Render Content
function renderFacturedUpdateModalData(bill) {
    let modalContent = $('#editBillModal .modal-content');

    modalContent.append(`<form actiob="" id="updateBillForm">

                    <div class="modal-header">
                        <h4 class="modal-title">Izmena racuna</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>

                    <div class="modal-body">
                       <div class="row">

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="updateBillStores" class="control-label">Prodavnica</label>
                                            <select name="updateStores" class="form-control" id="updateStores" required>
                                                <option value="" disabled selected>Izaberi</option>
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label for="updateBillNumber" class="control-label">Broj racuna</label>
                                            <input name="updateBillNumber" id="updateBillNumber" value="${bill.Number}" class="form-control" required />
                                        </div>

                                        <div class="form-group">
                                            <label for="updateBillDate" class="control-label">Datum</label>
                                            <input type="text"  id="updateBillDate" class="form-control" required />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="updateSuppliers" class="control-label">Dobavljac</label>
                                            <select name="updateSuppliers" class="form-control" id="updateSuppliers" required>
                                                <option value="" disabled selected>Izaberi</option>
                                            </select>
                                        </div>

                                        <div class="form-group">
                                            <label for="updateFactureNumber" class="control-label">Broj fakture</label>
                                            <input name="updateFactureNumber" id="updateFactureNumber" value="${bill.FactureNumber}" class="form-control" />
                                        </div>

                                        <div class="form-group">
                                            <label for="updateBillSum" class="control-label">Iznos</label>
                                            <input name="updateBillSum" id="updateBillSum" value="${bill.Sum}" class="form-control" required />
                                        </div>

                                        <input type="hidden" id="billId" value="${bill.Id}" />
                                    </div>
                                </div>

                    </div>

                    <div class="modal-footer">
                        <button type="submit" class="btn btn-general btn--primary shadow-md">Izmena</button>
                        <button type="button" class="btn btn-general btn--danger" data-dismiss="modal">Close</button>
                    </div>
                </form>`);
}

function renderTableData(container, bills) {
    $.each(bills, (index, bill) => {
        const formattedDate = moment(bill.date).format('MM/DD/YYYY');

        const isFactured = bill.factureNumber === "" ? false : true;

        if (isFactured) {
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
                    <td class="text-right">
                        <a href="#" class="action-link edit-link" id="${bill.id}" onclick="showUpdateBillModal(this);"><i class="fas fa-pen fa-fw"></i>&nbsp;Izmena</a>
                        <a href="#" class="action-link details-link"><i class="fas fa-info fa-fw"></i>&nbsp;Detalji</a>
                    </td>
                </tr>`);
        } else {
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
                    <td class="text-right">
                        
                        <a href="#" class="action-link facture-link" id="${bill.id}" onclick="showAddFactureModal(this);"><i class="fas fa-plus fa-fw"></i>&nbsp;Faktura</a>
                        <a href="#" class="action-link edit-link" id="${bill.id}" onclick="showUpdateBillModal(this);"><i class="fas fa-pen fa-fw"></i>&nbsp;Izmena</a>
                        <a href="#" class="action-link details-link"><i class="fas fa-info fa-fw"></i>&nbsp;Detalji</a>
                    </td>
                </tr>`);
        }
    });
}

function renderAlert(type, message) {
    $('#content-wrapper').append(`<div id="alert" class="_alert alert--${type}">
        <p><i class="fas fa-check"></i><span>${message}</span></p>
    </div>`);

    setTimeout(function () {
        $('#alert').fadeOut(500, function () {
            $(this).remove();
        });
    }, 2500);
}

function renderAddFactureModal(bill) {
    let modalContent = $('#addFactureModal .modal-content');

    modalContent.append(`<form id="addFacture">

                    <div class="modal-header">
                        <h4 class="modal-title">Dodaj fakturu</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>

                    <div class="modal-body">
                       <div class="row">

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="addFactureNumber" class="control-label">Broj racuna</label>
                                            <input name="addFactureNumber" id="addFactureNumber" value="" class="form-control" required />
                                        </div>
                                        <div class="form-group">
                                            <label for="factureSum" class="control-label">Broj racuna</label>
                                            <input name="factureSum" id="factureSum" value="" class="form-control" required />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="factureDate" class="control-label">Broj racuna</label>
                                            <input name="factureDate" id="factureDate" value="" class="form-control" required />
                                        </div>

                                        <input type="hidden" id="billId" value="${bill.Id}" />
                                    </div>
                                </div>

                    </div>

                    <div class="modal-footer">
                        <button type="submit" class="btn btn-general btn--primary shadow-md">Dodaj</button>
                        <button type="button" class="btn btn-general btn--danger" data-dismiss="modal">Close</button>
                    </div>
                </form>`);
}

// Ajax Requests
function getRequest(url, data) {
    let output = "";

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
            output = response;
        }
    });

    return output;
}

function getByIdRequest(url) {
    let output = "";

    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        error: function (e) {
            console.log('Neuspeh!');
        },
        success: function (response) {
            output = response;
        }
    });

    return output;
}

function updateBillRequest(url, data) {
    let output = false;

    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        async: false,
        data: {
            json: JSON.stringify(data)
        },
        error: function (e) {
            return false;
        },
        success: function (response) {
            output = response;
        }
    });

    return output;
}

$('#editBillModal').on('hidden.bs.modal', () => {
    let modalContent = $('#editBillModal .modal-content');

    modalContent.html('');
});

$('#addFactureModal').on('hidden.bs.modal', () => {
    let modalContent = $('#addFactureModal .modal-content');

    modalContent.html('');
});

// Show Modals

function showUpdateBillModal(el) {
    const modal = $('#editBillModal');

    const url = '/Bill/Details/' + el.id;
    let bill = "";

    bill = JSON.parse(getByIdRequest(url));

    renderFacturedUpdateModalData(bill);
    handleFacturedUpdateBill();

    const formattedDate = moment(bill.Date).format('MM/DD/YYYY');

    renderStores();
    renderSuppliers();

    $('#updateStores').val(bill.StoreId);
    $('#updateSuppliers').val(bill.SupplierId);

    $('#updateBillDate').datepicker(
        'setDate', formattedDate
    ).datepicker();

    modal.modal('show');
}

function showAddFactureModal(el) {
    const modal = $('#addFactureModal');

    const url = '/Bill/Details/' + el.id;
    let bill = "";

    bill = JSON.parse(getByIdRequest(url));

    renderAddFactureModal(bill);
    handleAddFacture();

    modal.modal('show');
}

// Handle Forms
function handleFacturedUpdateBill() {
    $('#updateBillForm').on('submit', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        const isValid = $('#updateBillForm').valid();

        if (isValid === true) {

            // TODO: Get value and send them to server
            const id = $('#billId').val();
            const store = $("#updateStores option").filter(":selected").val();
            const supplier = $("#updateSuppliers option").filter(":selected").val();
            const number = $('#updateBillNumber').val();
            const factureNumber = $('#updateFactureNumber').val();
            const sum = $('#updateBillSum').val();
            const date = $('#updateBillDate').val();

            let bill = {
                "Id": id,
                "StoreId": store,
                "SupplierId": supplier,
                "Number": number,
                "FactureNumber": factureNumber,
                "Sum": sum,
                "Date": date
            };

            const url = "/Bill/UpdateFacturedBill/" + id;
            const response = JSON.parse(updateBillRequest(url, bill));

            if (response) {
                renderAlert('success', 'Uspesno izmenjen racun!');
                let billFilter = createBillFilter();

                let bills = "";

                bills = getRequest("/Bill/SearchBills", billFilter);

                let container = $('#billTableData');
                container.html("");

                renderTableData(container, bills);
            } else {
                renderAlert('danger', 'Doslo je do greske!');
            }

            $("#editBillModal .close").click();


        } else {
            console.log('Invalid from!');
        }
    });
}
function handleAddFacture() {
    $('#addFacture').on('submit', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        const isValid = $('#addFacture').valid();

        if (isValid) {
            console.log('Valid facture!');

            // TODO: Insert in Db
        } else {
            console.log('Invalid facture data!');
        }

    });
}






/* From create bill */
function renderStores() {
    const storesData = JSON.parse(getStores());
    const stores = $('#updateStores');

    $.each(storesData, (index, store) => {
        stores.append(`<option value='${store.Id}'>${store.Name}</option>`);
    });
}

function renderSuppliers() {
    const suppliersData = JSON.parse(getSuppliers());
    const suppliers = $('#updateSuppliers');

    $.each(suppliersData, (index, supplier) => {
        suppliers.append(`<option value='${supplier.Id}'>${supplier.Name}</option>`);
    });
}

function getStores() {
    let output = "";
    $.ajax({
        url: '/Meta/GetStores',
        type: 'GET',
        async: false,
        dataType: 'json',
        error: function (e) {
            console.log('Neuspeh!');
        },
        success: function (response) {
            output = response;
        }
    });

    return output;
}

function getSuppliers() {
    let output = "";
    $.ajax({
        url: '/Meta/GetSuppliers',
        type: 'GET',
        async: false,
        dataType: 'json',
        error: function (e) {
            console.log('Neuspeh!');
        },
        success: function (response) {
            output = response;
        }
    });

    return output;
}

