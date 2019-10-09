$(function () {

    $("#coin_add").click(function (e) {
        e.preventDefault();
        var cc = $("#coin_count").html();
        var sz = $("#size").val();
        var model_id = $("#model_id").val();
        if (sz < 1) { return; }
        cc = cc - sz;
        if (cc < 1) { return; }
        $("#coin_count").html(cc);
        $.ajax({
            type: "POST",
            dataType: "json",
            cache: false,
            url: "/Admin/IncreaseCash",
            dataType: 'json',
            data: {
                coin_id: model_id, size: sz
            },

            success: function (response) {
                if (response != null && response.success) {
                    //
                } else {
                    console.log(response.success + " fail " + response.cash);
                }
            },
            error: function (response) {
                console.log(response.success + " fail2 " + response.cash);
            }
        });
    });

    $("#coin_del").click(function (e) {
        e.preventDefault();
        var cc = $("#coin_count").html();
        var sz = $("#size").val();
        var model_id = $("#model_id").val();

        if (sz < 1) { return; }
        cc = cc - sz;
        if (cc < 1) { return; }
        $("#coin_count").html(cc);
        $.ajax({
            type: "POST",
            dataType: "json",
            cache: false,
            url: "/Admin/IncreaseCash",
            dataType: 'json',
            data: {
                coin_id: model_id, size: -1 * sz
            },
            success: function (response) {
                if (response != null && response.success) {
                    //
                } else {
                    console.log(response.success + " fail " + response.cash);
                }
            },
            error: function (response) {
                console.log(response.success + " fail2 " + response.cash);
            }
        });
    });

    $("#blocked").click(function (e) {
        var sz = 0;
        var model_id = $("#model_id").val();

        if ($('#blocked').is(":checked")) {
            sz = 1
        }
        $.ajax({
            type: "POST",
            dataType: "json",
            cache: false,
            url: "/Admin/BlockCoin",
            dataType: 'json',
            data: {
                coin_id: model_id, blk: sz
            },
            success: function (response) {
                if (response != null && response.success) {
                    //
                } else {
                    console.log(response.success + " fail " + response.cash);
                }
            },
            error: function (response) {
                console.log(response.success + " fail2 " + response.cash);
            }
        });
    });

});
