Cart = {
    _properties: {
        getCartViewLink: "",
        addToCartLink: "",
        decrementLink: "",
        removeFromCartLink: ""
    },

    init: function (properties) {
        $.extend(Cart._properties, properties);

        Cart.initEvents();
    },

    initEvents: function () {
        $(".add-to-cart").click(Cart.addToCart);
        $(".cart_quantity_up").click(Cart.incrementItem);
        $(".cart_quantity_down").click(Cart.decrementItem);
        $(".cart_quantity_delete").click(Cart.removeFromCart);
    },

    addToCart: function (event) {
        event.preventDefault();

        var button = $(this);
        var id = button.data("id");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function () {
                Cart.showToolTip(button);
                Cart.refreshCartView();
            })
            .fail(function () {
                console.log("addToCart fail");
            });
    },

    incrementItem: function (event) {
        event.preventDefault();

        var button = $(this);
        var id = button.data("id");

        var container = button.closest("tr");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function () {
                var quantity = parseInt($(".cart_quantity_input", container).val());
                $(".cart_quantity_input", container).val(quantity + 1);
                Cart.refreshPrice(container);
                Cart.refreshTotalPrice();
                Cart.refreshCartView();
            })
            .fail(function () {
                console.log("incrementItem fail");
            });
    },

    refreshPrice: function (container) {
        var quantity = parseInt($(".cart_quantity_input", container).val());
        var price = parseFloat($(".cart_price", container).data("price"));
        var total_price = quantity * price;
        var value = total_price.toLocaleString("ru-RU", { style: "currency", currency: "RUB" });

        $(".cart_total_price", container).data("price", total_price);
        $(".cart_total_price", container).html(value);

        Cart.refreshTotalPrice();
    },

    decrementItem: function (event) {
        event.preventDefault();

        var button = $(this);
        var id = button.data("id");

        var container = button.closest("tr");

        $.get(Cart._properties.decrementLink + "/" + id)
            .done(function () {
                var quantity = parseInt($(".cart_quantity_input", container).val());
                if (quantity > 1) {
                    $(".cart_quantity_input", container).val(quantity - 1);
                    Cart.refreshPrice(container);
                }
                else {
                    container.remove();
                }
                Cart.refreshTotalPrice();
                Cart.refreshCartView();
            })
            .fail(function () {
                console.log("decrementItem fail");
            });
    },

    removeFromCart: function (event) {
        event.preventDefault();

        var button = $(this);
        var id = button.data("id");

        $.get(Cart._properties.removeFromCartLink + "/" + id)
            .done(function () {
                button.closest("tr").remove();
                Cart.refreshTotalPrice();
                Cart.refreshCartView();
            })
            .fail(function () {
                console.log("removeFromCart fail");
            });
    },

    refreshTotalPrice: function () {
        var sum = 0;

        $(".cart_total_price").each(function ()
        {
            var price = parseFloat($(this).data("price"));
            sum += price;
        });

        var value_sum = sum.toLocaleString("ru-RU", { style: "currency", currency: "RUB" });
        $("#order-sum").html(value_sum);

        var total = sum - parseFloat($("#order-discount").data("discount"));

        var value_total = total.toLocaleString("ru-RU", { style: "currency", currency: "RUB" });
        $("#total-order-sum").html(value_total);
    },

    showToolTip: function (button) {
        button.tooltip({ title: "Добавлено в корзину" }).tooltip("show");
        setTimeout(function () {
            button.tooltip("destroy");
        }, 500);
    },

    refreshCartView: function () {
        var container = $("#cart-container");
        $.get(Cart._properties.getCartViewLink)
            .done(function (result) {
                container.html(result);
            })
            .fail(function () { console.log("refreshCartView fail"); });
    }
}