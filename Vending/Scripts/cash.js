$('.coin').click(function () {
    var price = $(this).find('img').attr('data');
    $.ajax({
        type: "POST",
        dataType: "json",
        cache: false,
        url: "/Cash/Increase",
        dataType: 'json',
        data: { size: price },

        success: function (response) {
            if (response != null && response.success) {
                $("#cash").html(response.cash);
                $("#cashout_btn").css('display', 'block');
                refreshDrinks();
            } else {
                alert(response.success + " fail " + response.cash);
            }
        },
        error: function (response) {
            alert(response.success + " fail2 " + response.cash);
        }
    });
});

$('#cashout').click(function () {
    $.ajax({
        type: "POST",
        dataType: "json",
        data: '',
        cache: false,
        url: "/Cash/Cashout",
        dataType: 'json',

        success: function (response) {
            if (response != null && response.success) {
                $("#cash").html(response.cash);
                $("#cashout_btn").css('display', 'none');
                refreshDrinks();
            }
            alert(response.message);
        },
        error: function (response) {
            alert(response.success + " fail2 " + response.cash);
        }
    });
});
