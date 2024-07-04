<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

$(document).ready(function () {
    function getFilterValues() {
        var search = $('#search').val();
        var minPrice = $('#minPrice').val();
        var maxPrice = $('#maxPrice').val();
        var selectedBrands = [];
        var selectedColors = [];
        var selectedCategories = [];

        $('input[name="selectedBrands"]:checked').each(function () {
            selectedBrands.push($(this).val());
        });

        $('input[name="selectedColors"]:checked').each(function () {
            selectedColors.push($(this).val());
        });
        $('input[name="selectedCategory"]:checked').each(function () {
            selectedCategories.push($(this).val());
        });

        return {
            search: search,
            minPrice: minPrice,
            maxPrice: maxPrice,
            selectedBrands: selectedBrands,
            selectedColors: selectedColors,
            selectedCategories: selectedCategories
        };
    }

    function filterProducts() {
        var filters = getFilterValues();

        $.ajax({
            url: '/Controller/Filter',
            type: 'GET',
            data: filters,
            success: function (data) {
                $('#productList').html(data);
            },
            error: function (xhr, status, error) {
                console.error('Error filtering products:', error);
            }
        });
    }

    $('#filterButton').on('click', function () {
        filterProducts();
    });

    // Optionally, you can trigger filtering on change of any filter input
    $('#search, #minPrice, #maxPrice').on('input', function () {
        filterProducts();
    });

    $('input[name="selectedBrands"], input[name="selectedColors"],input[name="selectedCategories').on('change', function () {
        filterProducts();
    });
});