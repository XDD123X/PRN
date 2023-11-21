// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    $(document).ready(function () {
        let distanceX = 0;
        let startX = 0;

        $("#photo").on("touchstart mousedown", function (e) {
            startX = e.clientX || e.originalEvent.touches[0].clientX;
        });

        $("#photo").on("touchmove mousemove", function (e) {
            const currentX = e.clientX || e.originalEvent.touches[0].clientX;
            distanceX = currentX - startX;
        });

        $("#photo").on("touchend mouseup", function () {
            const threshold = 50;

            if (distanceX > threshold) {
                // Quẹt sang phải
                document.getElementById('photo').classList.add('swiped-right');
                //$("#photo").addClass("swiped-right");
                // Thêm logic xử lý khi quẹt sang phải ở đây
            } else if (distanceX < -threshold) {
                // Quẹt sang trái
                document.getElementById('photo').classList.add('swiped-left');
                //$("#photo").addClass("swiped-left");
                // Thêm logic xử lý khi quẹt sang trái ở đây
            }

            // Đặt lại các giá trị và loại bỏ hiệu ứng
            distanceX = 0;
            startX = 0;

            // Thời gian timeout để loại bỏ lớp hiệu ứng sau khi hoàn thành
            setTimeout(function () {
                $("#photo").removeClass("swiped-left swiped-right");
            }, 300);
        });


        $('#no').click(function () {
            no();
        });
        $('#heart').click(function () {
            like();
        });
    });

});
function no() {
    let id = $('#no').attr('data-iduser');
    let skip = $('#no').attr('data-skip');
    console.log(id + '   ' + skip);
    $.ajax({
        url: '/Users/Nope?id=' + id + '&skip=' + skip,
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        //data: JSON.stringify({ id: id, skip: skip }),
        async: true,
        processData: false,
        cache: false,
        success: function (response) {
            console.log(response);
            var user = '';
            let count = 1;
            response.data.forEach(p => {
                if (count == 1) {
                    user += `<div class="carousel-item h-100 active">
                        <img src="${p}" class="d-block w-100 user" alt="photo">
                    </div>`;
                } else {
                    user += `<div class="carousel-item h-100">
                        <img src="${p}" class="d-block w-100 user" alt="photo">
                    </div>`;
                }
                count++;
            });
            $('#no').attr('data-iduser', response.userid); $('#no').attr('data-skip', response.skip);
            $('.star').data('iduser', response.userid);
            $('.star').data('skip', response.skip);
            $('#heart').data('iduser', response.userid);
            $('#heart').data('skip', response.skip);
            $('#photo').html(user);
            $('.profile').html(`<div class="name">${response.name} <span>${response.age}</span></div>
							<div class="local">
								<i class="fas fa-map-marker-alt"></i>
								<span>${response.des}</span>
							</div>`);
        },
        error: function (xhr) {
            alert('error');
        }
    });
}

function like() {
    let id = $('#no').attr('data-iduser');
    let skip = $('#no').attr('data-skip');
    $.ajax({
        url: '/Users/Like?id=' + id + '&skip=' + skip,
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        //data: JSON.stringify({ id: id, skip: skip }),
        async: true,
        processData: false,
        cache: false,
        success: function (response) {
            if (!response.status) {
                const alert = document.querySelector('#expired');
                alert.classList.remove('d-none');
                setTimeout(() => {
                    alert.classList.add('d-none');
                }, 2000);
            } else {
                var user = '';
                let count = 1;
                response.data.forEach(p => {
                    if (count == 1) {
                        user += `<div class="carousel-item h-100 active">
                        <img src="${p}" class="d-block w-100 user" alt="photo">
                    </div>`;
                    }
                    else {
                        user += `<div class="carousel-item h-100">
                        <img src="${p}" class="d-block w-100 user" alt="photo">
                    </div>`;
                    }
                    count++;
                })
                if (response.isMatch) {
                    const alert = document.querySelector('#match');
                    alert.style.display = "block";
                    alert.classList.remove('d-none');
                    $('#list-match').append(`
                  <div class="col-3">
                    <img src="${response.userMatch.avt}" alt="avt" style="width: 70px; height: 70px;"/>
                    <p>${response.userMatch.name}</p> 
                  </div>
                `);
                    $('#nav-profile').append(`
                  <div class="messages">
					<div class="avatar">
						<img src="${response.userMatch.avt}" alt="" />
					</div>
					<div class="message">
						<div class="user">${response.userMatch.name}</div>
						<div class="text">Hi!</div>
					</div>
				</div>
                `);

                    setTimeout(() => {
                        alert.classList.add('d-none');
                    }, 2000);
                }
                $('#no').attr('data-iduser', response.userid);
                $('#no').attr('data-skip', response.skip);
                $('.star').attr('data-iduser', response.userid);
                $('.star').attr('data-skip', response.skip);
                $('#heart').attr('data-iduser', response.userid);
                $('#heart').attr('data-skip', response.skip);
                $('#photo').html(user);
                $('.profile').html(`<div class="name">${response.name} <span>${response.age}</span></div>
							<div class="local">
								<i class="fas fa-map-marker-alt"></i>
								<span>${response.des}</span>
							</div>`);
            }
    },
        error: function (xhr) {
            alert('error');
        }
    });
}