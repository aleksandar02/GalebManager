$(function () {

    function renderStores() {
        const storesData = JSON.parse(getStores());
        const stores = $('#stores');

        $.each(storesData, (index, store) => {
            stores.append(`<option value='${store.Id}'>${store.Name}</option>`);
        });
    }

    function renderSuppliers() {
        const suppliersData = JSON.parse(getSuppliers());
        const suppliers = $('#supplier');

        $.each(suppliersData, (index, supplier) => {
            suppliers.append(`<option value='${supplier.Id}'>${supplier.Name}</option>`);
        });
    }

    function getStores() {
        var output = "";
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
        var output = "";
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

    renderStores();
    renderSuppliers();
});