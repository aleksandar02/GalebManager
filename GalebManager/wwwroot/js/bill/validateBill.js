$('#updateBillForm').validate({
    rules: {
        updateStores: {
            required: true
        },
        updateSuppliers: {
            required: true
        },
        updateBillNumber: {
            required: true
        },
        updateBillDate: {
            required: true
        },
        updateBillSum: {
            required: true
        }
    },
    messages: {
        updateStores: {
            required: 'Obavezno polje!'
        },
        updateSuppliers: {
            required: 'Obavezno polje!'
        },
        updateBillNumber: {
            required: "Obavezno polje!"
        },
        updateBillDate: {
            required: 'Obavezno polje!'
        },
        updateBillSum: {
            required: 'Obavezno polje!'
        }
    }

    //submitHandler: function (form) {
    //    form.submit();
    //}
});