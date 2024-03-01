function dataSearch(){
	var searchData = document.getElementById("searchData").value;
	var selected = $("input[name='flexRadioDefault']:checked").val();
	if (selected == 'users') {
		$.ajax({
			url: '/Search/UserSearch',
			type: 'GET',
			dataType: 'json',
			data: { searchData: searchData },
			success: function (url) {
				if (url == "No user found") {
					alert(url);
				} else {
					window.location.href = url;
				}
				
			},
			error: function (error) {
				console.log(error);
			}
		});
	}
	if (selected == 'terms') {
		$.ajax({
			url: '/Search/TermSearch',
			type: 'GET',
			dataType: 'json',
			data: { searchData: searchData },
			success: function (url) {
				if (url == "No timetable found") {
					alert(url)
				} else {
					window.location.href = url;
				}
			}
		});
	}
}
