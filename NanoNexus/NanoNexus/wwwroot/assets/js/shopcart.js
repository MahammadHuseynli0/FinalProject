$(document).ready(function () {
    // Handle remove item button click
    $('.remove-item-btn').click(function (e) {
        e.preventDefault();

        var $item = $(this).closest('.basket-item');
        var productId = $item.data('product-id');

        $.ajax({
            url: '@Url.Action("RemoveItem", "Shop")',
            type: 'POST',
            data: { productId: productId },
            success: function () {
                $item.remove();
                updateCartTotals();
            },
            error: function (xhr, status, error) {
                console.error('Error removing item:', error);
                alert('There was an error removing the item. Please try again.');
            }
        });
    });

    // Handle quantity increase button click
    $('.btn-up').click(function () {
        var $item = $(this).closest('.basket-item');
        var productId = $item.data('product-id');

        $.ajax({
            url: '@Url.Action("IncreaseItem", "Shop")',
            type: 'POST',
            data: { productId: productId },
            success: function () {
                var $input = $item.find('input[name="number"]');
                var newVal = parseInt($input.val()) + 1;
                $input.val(newVal);
                updateItemTotal($item);
                updateCartTotals();
            },
            error: function (xhr, status, error) {
                console.error('Error increasing item quantity:', error);
                alert('There was an error updating the quantity. Please try again.');
            }
        });
    });

    // Handle quantity decrease button click
    $('.btn-down').click(function () {
        var $item = $(this).closest('.basket-item');
        var productId = $item.data('product-id');

        $.ajax({
            url: '@Url.Action("DecreaseItem", "Shop")',
            type: 'POST',
            data: { productId: productId },
            success: function () {
                var $input = $item.find('input[name="number"]');
                var newVal = parseInt($input.val()) - 1;
                if (newVal >= 1) {
                    $input.val(newVal);
                    updateItemTotal($item);
                    updateCartTotals();
                }
            },
            error: function (xhr, status, error) {
                console.error('Error decreasing item quantity:', error);
                alert('There was an error updating the quantity. Please try again.');
            }
        });
    });

    // Update item total
    function updateItemTotal($item) {
        var price = parseFloat($item.find('.price').text().replace('$', ''));
        var quantity = parseInt($item.find('input[name="number"]').val());
        var total = price * quantity;

        $item.find('.total').text('$' + total.toFixed(2));
    }

    // Update cart totals
    function updateCartTotals() {
        var subtotal = 0;
        $('.basket-item').each(function () {
            var total = parseFloat($(this).find('.total').text().replace('$', ''));
            subtotal += total;
        });

        var shipping = parseFloat($('input[name="radio-flat-rate"]:checked').next('label').find('span').text().replace('$', '')) || 0;
        var total = subtotal + shipping;

        $('.subtotal').text('$' + subtotal.toFixed(2));
        $('.price-total').text('$' + total.toFixed(2));
    }
});