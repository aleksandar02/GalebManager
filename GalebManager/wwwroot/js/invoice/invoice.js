$(function() {
  $('#invoiceTable').DataTable({
    processing: true,
    language: {
      search: '',
      zeroRecords: 'Nema rezultata!'
    },
    pageLength: 10,
    lengthChange: false,
    info: false,
    bFilter: false
  });

  $('#dateFrom').datepicker('setDate', '-30d');
  $('#dateTo').datepicker('setDate', 'now');
});

// Search
$('#searchForm').submit(function(e) {
  e.preventDefault();

  let invoiceFilter = createInvoiceFilter();

  let invoices = '';

  invoices = getRequest('/Invoice/SearchInvoices', invoiceFilter);

  let container = $('#invoiceTableData');
  container.html('');

  renderTableData(container, invoices);
});

function createInvoiceFilter() {
  const store = $('#stores option')
    .filter(':selected')
    .val();
  const supplier = $('#supplier option')
    .filter(':selected')
    .val();
  const factureStatus = $('#factured').val();
  const dateFrom = $('#dateFrom').val();
  const dateTo = $('#dateTo').val();

  let invoiceFilter = {
    storeId: store,
    supplierId: supplier,
    factureStatus: factureStatus,
    dateFrom: dateFrom,
    dateTo: dateTo
  };

  return invoiceFilter;
}

// Ajax Requests
function getRequest(url, data) {
  let output = '';

  $.ajax({
    url: url,
    type: 'GET',
    dataType: 'json',
    contentType: 'application/json',
    async: false,
    data: {
      json: JSON.stringify(data)
    },
    error: function(e) {
      console.log('Neuspeh!');
    },
    success: function(response) {
      output = response;
    }
  });

  return output;
}

function getByIdRequest(url) {
  let output = '';

  $.ajax({
    url: url,
    type: 'GET',
    dataType: 'json',
    contentType: 'application/json',
    async: false,
    error: function(e) {
      console.log('Neuspeh!');
    },
    success: function(response) {
      output = response;
    }
  });

  return output;
}

function postRequest(url, data) {
  let output = false;

  $.ajax({
    url: url,
    type: 'POST',
    dataType: 'json',
    async: false,
    data: {
      json: JSON.stringify(data)
    },
    error: function(e) {
      return false;
    },
    success: function(response) {
      output = response;
    }
  });

  return output;
}

// Close modals
$('#editInvoiceModal').on('hidden.bs.modal', () => {
  let modalContent = $('#editInvoiceModal .modal-content');

  modalContent.html('');
});

$('#addBillModal').on('hidden.bs.modal', () => {
  let modalContent = $('#addBillModal .modal-content');

  modalContent.html('');
});

// Show Modals
function showUpdateInvoiceModal(el) {
  const modal = $('#editInvoiceModal');

  const url = '/Invoice/Details/' + el.id;
  let invoice = '';

  invoice = JSON.parse(getByIdRequest(url));

  renderUpdateModalData(invoice);
  handleUpdateInvoice();

  const formattedDate = moment(invoice.Date).format('MM/DD/YYYY');

  renderStores();
  renderSuppliers();

  $('#updateStores').val(invoice.StoreId);
  $('#updateSuppliers').val(invoice.SupplierId);

  $('#updateInvoiceDate')
    .datepicker('setDate', formattedDate)
    .datepicker();

  modal.modal('show');
}

function showAddBillModal(el) {
  const modal = $('#addBillModal');

  const url = '/Invoice/Details/' + el.id;
  let invoice = '';

  invoice = JSON.parse(getByIdRequest(url));

  renderAddBillModal(invoice);
  $('#billDate')
    .datepicker('setDate', 'now')
    .datepicker();

  handleAddBill();

  modal.modal('show');
}

// Render Content
function renderUpdateModalData(invoice) {
  let modalContent = $('#editInvoiceModal .modal-content');

  modalContent.append(`<form actiob="" id="updateInvoiceForm">
  
                      <div class="modal-header">
                          <h4 class="modal-title">Izmena fakture</h4>
                          <button type="button" class="close" data-dismiss="modal">&times;</button>
                      </div>
  
                      <div class="modal-body">
                         <div class="row">
  
                                      <div class="col-md-6">
                                          <div class="form-group">
                                              <label for="updateInvoiceStores" class="control-label">Prodavnica</label>
                                              <select name="updateStores" class="form-control" id="updateStores" required>
                                                  <option value="" disabled selected>Izaberi</option>
                                              </select>
                                          </div>
                                          <div class="form-group">
                                              <label for="updateInvoiceNumber" class="control-label">Broj fakture</label>
                                              <input name="updateInvoiceNumber" id="updateInvoiceNumber" value="${invoice.Number}" class="form-control" required />
                                          </div>
  
                                          <div class="form-group">
                                              <label for="updateInvoiceDate" class="control-label">Datum</label>
                                              <input type="text"  id="updateInvoiceDate" class="form-control" required />
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
                                              <label for="updateBillNumber" class="control-label">Broj racuna</label>
                                              <input name="updateBillNumber" id="updateBillNumber" value="${invoice.BillNumber}" class="form-control" />
                                          </div>
  
                                          <div class="form-group">
                                              <label for="updateInvoiceSum" class="control-label">Iznos</label>
                                              <input name="updateInvoiceSum" id="updateInvoiceSum" value="${invoice.Sum}" class="form-control" required />
                                          </div>
  
                                          <input type="hidden" id="invoiceId" value="${invoice.Id}" />
                                      </div>
                                  </div>
  
                      </div>
  
                      <div class="modal-footer">
                          <button type="submit" class="btn btn-general btn--primary shadow-md">Izmena</button>
                          <button type="button" class="btn btn-general btn--danger" data-dismiss="modal">Close</button>
                      </div>
                  </form>`);
}

function renderTableData(container, invoices) {
  $.each(invoices, (index, invoice) => {
    const formattedDate = moment(invoice.date).format('MM/DD/YYYY');

      const isFactured = invoice.billNumber === '' ? false : true;

    if (isFactured) {
      container.append(`<tr>
                      <td>
                          ${invoice.storeName}
                      </td>
                      <td>
                          ${invoice.number}
                      </td>
                      <td>
                          ${invoice.billNumber}
                      </td>
                      <td>
                          ${invoice.supplierName}
                      </td>
                      <td>
                          ${formattedDate}
                      </td>
                      <td>
                          ${invoice.sum}
                      </td>
                      <td class="text-right">
                          <a href="#" class="action-link edit-link" id="${invoice.id}" onclick="showUpdateInvoiceModal(this);"><i class="fas fa-pen fa-fw"></i>&nbsp;Izmena</a>
                          <a href="#" class="action-link details-link"><i class="fas fa-info fa-fw"></i>&nbsp;Detalji</a>
                      </td>
                  </tr>`);
    } else {
      container.append(`<tr>
                      <td>
                          ${invoice.storeName}
                      </td>
                      <td>
                          ${invoice.number}
                      </td>
                      <td>
                          ${invoice.billNumber}
                      </td>
                      <td>
                          ${invoice.supplierName}
                      </td>
                      <td>
                          ${formattedDate}
                      </td>
                      <td>
                          ${invoice.sum}
                      </td>
                      <td class="text-right">
                          
                          <a href="#" class="action-link facture-link" id="${invoice.id}" onclick="showAddBillModal(this);"><i class="fas fa-plus fa-fw"></i>&nbsp;Racun</a>
                          <a href="#" class="action-link edit-link" id="${invoice.id}" onclick="showUpdateInvoiceModal(this);"><i class="fas fa-pen fa-fw"></i>&nbsp;Izmena</a>
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

  setTimeout(function() {
    $('#alert').fadeOut(700, function() {
      $(this).remove();
    });
  }, 3000);
}

function renderAddBillModal(invoice) {
  let modalContent = $('#addBillModal .modal-content');

  modalContent.append(`<form id="addBill">
  
                      <div class="modal-header">
                          <h4 class="modal-title">Dodaj racun</h4>
                          <button type="button" class="close" data-dismiss="modal">&times;</button>
                      </div>
  
                      <div class="modal-body">
                         <div class="row">
  
                                      <div class="col-md-6">
                                          <div class="form-group">
                                              <label for="addBillNumber" class="control-label">Broj racuna</label>
                                              <input name="addBillNumber" id="addBillNumber" value="" class="form-control" required />
                                          </div>
                                          <div class="form-group">
                                              <label for="billSum" class="control-label">Iznos</label>
                                              <input name="billSum" id="billSum" value="" class="form-control" required />
                                          </div>
                                      </div>
                                      <div class="col-md-6">
                                          <div class="form-group">
                                              <label for="billDate" class="control-label">Datum</label>
                                              <input type="text" name="billDate" id="billDate" value="" class="form-control" required />
                                          </div>
  
                                          <input type="hidden" id="invoiceId" value="${invoice.Id}" />
                                      </div>
                                  </div>
                              <input type="hidden" id="storeId" name="storeId" value="${invoice.StoreId}" />
                              <input type="hidden" id="supplierId" name="supplierId" value="${invoice.SupplierId}" />
                              <input type="hidden" id="invoiceNumber" name="invoiceNumber" value="${invoice.Number}" />
  
  
                      </div>
  
                      <div class="modal-footer">
                          <button type="submit" class="btn btn-general btn--primary shadow-md">Dodaj</button>
                          <button type="button" class="btn btn-general btn--danger" data-dismiss="modal">Close</button>
                      </div>
                  </form>`);
}

// Handle Forms
function handleUpdateInvoice() {
  $('#updateInvoiceForm').on('submit', function(e) {
    e.preventDefault();
    e.stopImmediatePropagation();

    const isValid = $('#updateInvoiceForm').valid();

    if (isValid === true) {
      // TODO: Get value and send them to server
      const id = $('#invoiceId').val();
      const store = $('#updateStores option')
        .filter(':selected')
        .val();
      const supplier = $('#updateSuppliers option')
        .filter(':selected')
        .val();
      const number = $('#updateInvoiceNumber').val();
      const billNumber = $('#updateBillNumber').val();
      const sum = $('#updateInvoiceSum').val();
      const date = $('#updateInvoiceDate').val();

      let invoice = {
        Id: id,
        StoreId: store,
        SupplierId: supplier,
        Number: number,
        BillNumber: billNumber,
        Sum: sum,
        Date: date
      };

      const url = '/Invoice/UpdateInvoice/' + id;
      const response = JSON.parse(postRequest(url, invoice));

      if (response) {
        $('#editInvoiceModal .close').click();

        renderAlert('success', 'Uspesno izmenjena faktura!');
        let invoiceFilter = createInvoiceFilter();

        let invoices = '';

          invoices = getRequest('/Invoice/SearchInvoices', invoiceFilter);

        let container = $('#invoiceTableData');
        container.html('');

        renderTableData(container, invoices);
      } else {
        renderAlert('danger', 'Doslo je do greske!');
      }
    } else {
      console.log('Invalid from!');
    }
  });
}

function handleAddBill() {
  $('#addBill').on('submit', function(e) {
    e.preventDefault();
    e.stopImmediatePropagation();

    const isValid = $('#addBill').valid();

    if (isValid) {
        const invoiceId = $('#invoiceId').val();
      const storeId = $('#storeId').val();
      const supplierId = $('#supplierId').val();
      const number = $('#addBillNumber').val();
      const factureNumber = $('#invoiceNumber').val();
      const date = $('#billDate').val();
      const sum = $('#billSum').val();

      let bill = {
        StoreId: storeId,
        SupplierId: supplierId,
        FactureNumber: factureNumber,
        Number: number,
        Date: date,
        Sum: sum
      };

      console.log(bill);

      const url = '/Invoice/AddBill/' + invoiceId;
      const response = JSON.parse(postRequest(url, bill));

      if (response) {
        $('#addBillModal .close').click();

        renderAlert('success', 'Racun je dodan!');
        let invoiceFilter = createInvoiceFilter();

        let invoices = '';

        invoices = getRequest('/Invoice/SearchInvoices', invoiceFilter);

        let container = $('#invoiceTableData');
        container.html('');

        renderTableData(container, invoices);
      } else {
        renderAlert('danger', 'Doslo je do greske!');
      }
    } else {
      console.log('Invalid from!');
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
    suppliers.append(
      `<option value='${supplier.Id}'>${supplier.Name}</option>`
    );
  });
}

function getStores() {
  let output = '';
  $.ajax({
    url: '/Meta/GetStores',
    type: 'GET',
    async: false,
    dataType: 'json',
    error: function(e) {
      console.log('Neuspeh!');
    },
    success: function(response) {
      output = response;
    }
  });

  return output;
}

function getSuppliers() {
  let output = '';
  $.ajax({
    url: '/Meta/GetSuppliers',
    type: 'GET',
    async: false,
    dataType: 'json',
    error: function(e) {
      console.log('Neuspeh!');
    },
    success: function(response) {
      output = response;
    }
  });

  return output;
}
