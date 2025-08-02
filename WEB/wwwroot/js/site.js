// Kategori seçilince Alt Kategorileri getirir ve "subCategories" select'ini doldurur.
// Ardından (varsa) ürün listesini sıfırlar.
function loadSubCategoriesByCategoryId(categoryId, selectedSubCategoryId = null, selectedProductId = null) {
    var $subCategories = $("#subCategories");
    var $productDetail = $("#productDetail");
    var $productIdHidden = $("#productId");

    if (!categoryId || categoryId === "0") {
        // Kategori seçilmemişse alt listeyi ve ürün listesini kilitle
        $subCategories.empty().append('<option selected disabled value="0">Önce kategori seçiniz!</option>').prop('disabled', true);
        $productDetail.empty().append('<option selected disabled value="">Önce alt kategori seçiniz</option>').prop('disabled', true);
        $productIdHidden.val("");
        return;
    }

    $.ajax({
        url: '/Request/SubCategories/GetByCategoryId', // <- kendi endpoint'ine göre değiştir
        type: 'GET',
        data: { categoryId: categoryId },
        success: function (data) {
            $subCategories.empty();

            if (!data || data.length === 0) {
                $subCategories.append('<option disabled selected value="0">Bu kategoride alt kategori yok!</option>')
                    .prop('disabled', true);
                // Ürünleri de sıfırla
                $productDetail.empty().append('<option selected disabled value="">Önce alt kategori seçiniz</option>')
                    .prop('disabled', true);
                $productIdHidden.val("");
                return;
            }

            $subCategories.append('<option disabled selected value="0">Lütfen alt kategori seçiniz!</option>')
                .prop('disabled', false);

            $.each(data, function (i, sc) {
                // Beklenen şekil: { id: 'guid', name: 'Alt Kategori Adı' }
                var selected = (selectedSubCategoryId && sc.id === selectedSubCategoryId) ? 'selected' : '';
                $subCategories.append(`<option value="${sc.id}" ${selected}>${sc.name}</option>`);
            });

            // Eğer bir alt kategori önceden seçilmesi isteniyorsa:
            if (selectedSubCategoryId) {
                loadProductsBySubCategoryId(selectedSubCategoryId, selectedProductId);
            } else {
                // Ürün listesini resetle
                $productDetail.empty().append('<option selected disabled value="">Önce alt kategori seçiniz</option>')
                    .prop('disabled', true);
                $productIdHidden.val("");
            }
        },
        error: function (err) {
            console.error(err);
            // Hata durumunda listeleri güvenli şekilde kapat
            $subCategories.empty().append('<option disabled selected value="0">Alt kategoriler yüklenemedi</option>')
                .prop('disabled', true);
            $productDetail.empty().append('<option selected disabled value="">Ürünler yüklenemedi</option>')
                .prop('disabled', true);
            $productIdHidden.val("");
        }
    });
}

// Alt Kategori seçilince ürünleri getirir ve "productDetail" select'ini doldurur.
// Ürün seçildiğinde gizli "productId" alanını set eder.
function loadProductsBySubCategoryId(subCategoryId, selectedProductId = null) {
    var $productDetail = $("#productDetail");
    var $productIdHidden = $("#productId");

    if (!subCategoryId || subCategoryId === "0") {
        $productDetail.empty().append('<option selected disabled value="">Önce alt kategori seçiniz</option>')
            .prop('disabled', true);
        $productIdHidden.val("");
        return;
    }

    $.ajax({
        url: '/Request/Products/GetBySubCategoryId', // <- kendi endpoint'ine göre değiştir
        type: 'GET',
        data: { subCategoryId: subCategoryId },
        success: function (data) {
            $productDetail.empty();

            if (!data || data.length === 0) {
                $productDetail.append('<option disabled selected value="">Bu alt kategoride ürün yok!</option>')
                    .prop('disabled', true);
                $productIdHidden.val("");
                return;
            }

            $productDetail.append('<option disabled selected value="">Lütfen ürün seçiniz!</option>')
                .prop('disabled', false);

            $.each(data, function (i, p) {
                // Beklenen şekil: { id: 'guid', name: 'Ürün Adı', ... }
                var selected = (selectedProductId && p.id === selectedProductId) ? 'selected' : '';
                $productDetail.append(`<option value="${p.id}" ${selected}>${p.name}</option>`);
            });

            // Eğer önceden seçilecek bir ürün verilmişse, gizli ProductId alanını da set et
            if (selectedProductId) {
                $productIdHidden.val(selectedProductId);
            }
        },
        error: function (err) {
            console.error(err);
            $productDetail.empty().append('<option disabled selected value="">Ürünler yüklenemedi</option>')
                .prop('disabled', true);
            $productIdHidden.val("");
        }
    });
}

// Ürün seçimi değiştiğinde gizli ProductId alanını güncelle
$(document).on('change', '#productDetail', function () {
    $("#productId").val($(this).val() || "");
});
