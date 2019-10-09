$(function () {
    setTimeout(function () {
        refreshDrinks();
    }, 500);
});

function refreshDrinks() {
    $.ajax({
        type: "POST",
        dataType: "json",
        data: '',
        cache: false,
        url: "/Home/Available",
        dataType: 'json',

        success: function (response) {
            if (response.length > 0) {
                var result_html = '';
                var offl = '';
                response.forEach(function (item) {
                    if (item.avail) {
                        result_html += '<a class="drink">';
                    }
                    offl = '';
                    result_html += '<div class="p-2 drink_item" data="' + item.id + '">';
                    result_html += '<img src="' + item.pic_url + '" class="drinks_pic';
                    if (!item.avail) {
                        result_html += ' drinks_pic_offline';
                        offl = ' drinks_text_offline';
                    }
                    result_html += '" />'
                    result_html += '<div class="drink_price' + offl + '"> ' + item.price + '</div>';
                    result_html += '<div class="drink_name' + offl + '">' + item.name + '</div>';
                    result_html += '</div>';
                    if (item.avail) {
                        result_html += '</a>';
                    }
                });
                $("#main").html(result_html);

                $('.drink').click(function () {
                    var id = $(this).find('.drink_item').attr('data');
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        cache: false,
                        url: "/Home/Purchase",
                        dataType: 'json',
                        data: { drink_id: id },

                        success: function (response) {
                            if (response != null && response.success) {
                                console.log('Товар (id:' + id + ') куплен');
                                alert('Товар (id:' + id + ') куплен');
                                $("#cash").html(response.cash);
                                if (response.cash > 0) {
                                    $("#cashout_btn").css('display', 'block');
                                }
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

            }
        },
        error: function (response) {
            console.log(response.success + " fail2 " + response.cash);
        }
    });
}

