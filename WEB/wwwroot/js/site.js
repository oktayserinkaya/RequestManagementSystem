function loadSubCategoriesByCategoryId(categoryId, selectedSubCategoryId = null, selectedProductId = null) {
    if (categoryId) {
        $.ajax({
            url: '/Request/SubCategories/GetByCategoryId',
            type: 'GET',
            data: { categoryId: categoryId },
            success: function (data) {
                var subCategories = $("#subCategorySelect");
                var products = $("#productSelect");

                subCategories.empty().attr('disabled', false);
                products.empty().append('<option disabled selected value="0">Lütfen alt kategori seçiniz!</option>').attr('disabled', true);

                if (data.length === 0) {
                    subCategories.append('<option disabled selected value="0">Bu kategoride alt kategori yok!</option>');
                    subCategories.attr('disabled', true);
                    return;
                }

                subCategories.append('<option disabled selected value="0">Lütfen alt kategori seçiniz!</option>');

                $.each(data, function (index, subCategory) {
                    const selected = subCategory.id === selectedSubCategoryId ? 'selected' : '';
                    // SUBCATEGORYNAME kullanılıyor!
                    subCategories.append(`<option value="${subCategory.id}" ${selected}>${subCategory.subCategoryName}</option>`);
                });

                if (selectedSubCategoryId) {
                    loadProductsBySubCategoryId(selectedSubCategoryId, selectedProductId);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}


function loadProductsBySubCategoryId(subCategoryId, selectedProductId = null) {
    if (subCategoryId) {
        $.ajax({
            url: '/Request/Products/GetBySubCategoryId',
            type: 'GET',
            data: { subCategoryId: subCategoryId },
            success: function (data) {
                var products = $("#productSelect");
                products.empty().attr('disabled', false);

                if (data.length === 0) {
                    products.append('<option disabled selected value="0">Bu alt kategoride ürün yok!</option>');
                    products.attr('disabled', true);
                    return;
                }

                products.append('<option disabled selected value="0">Lütfen ürün seçiniz!</option>');

                $.each(data, function (index, product) {
                    const selected = product.id === selectedProductId ? 'selected' : '';
                    // PRODUCTNAME kullanılıyor!
                    products.append(`<option value="${product.id}" ${selected}>${product.productName}</option>`);
                });
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}

