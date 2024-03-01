var selectedSubjects = [];
function recommender(userName) {
	$.ajax({
		url: '/Timetable/Recommender',
		type: 'GET',
		dataType: 'json',
		data: { userName: userName },
		success: function (recommendedSubjects) {
			console.log(recommendedSubjects)
			displayRecommendations(recommendedSubjects);
		},
		error: function (error) {
			console.log(error);
		}
	});
}
function subjectSearch() {
	var subjectId = document.getElementById("Subject").value;
	$.ajax({
		url: '/TimeTable/SubjectSearch',
		type: 'GET',
		dataType: 'json',
		data: { subjectId: subjectId },
		success: function (classData) {
			if (classData == "No subject found") {
				alert(classData);
			}
			else if (selectedSubjects.indexOf(classData.subjectId) == -1) {
				selectedSubjects.push(classData.subjectId);
				displaySubjects(classData);
			}
		},
		error: function (error) {
			console.log('Error fetching class data:', error);
		}
	});
}
function displayRecommendations(recommendedSubjects) {
	var div2 = document.getElementById("div2");
	var content = recommendedSubjects.join(", ");
	div2.textContent = "Recommendations for you this term: " + content + ".";
}
var div = document.getElementById("div1");
var table = document.createElement("table");
table.setAttribute('class', 'table');
table.setAttribute('id', 'myTable')
div.appendChild(table);
function displaySubjects(classData) {
	var row = table.insertRow();
	var cell = row.insertCell();
	cell.innerHTML = classData.subjectId + ": " + classData.subjectName;
	row.setAttribute('id', classData.subjectId);
	var closebutton = document.createElement('button');
	closebutton.setAttribute('class', 'btn-close');
	closebutton.onclick = function clearSubject() {
		document.getElementById(classData.subjectId).remove();
		selectedSubjects.splice(selectedSubjects.indexOf(classData.subjectId), 1);
		
	};
	cell.appendChild(closebutton);
	console.log(selectedSubjects);
}
function subjectList() {
	$.ajax({
		url: '/Timetable/SubjectList',
		type: 'POST',
		contentType: 'application/json',
		dataType: 'json',
		data: JSON.stringify(selectedSubjects),
		success: function (response) {
			console.log(response);
			// Chuyen huong sang trang Arrange
			window.location.href = response;
		},
		error: function (error) {
			console.log(error);
		}
	});
}