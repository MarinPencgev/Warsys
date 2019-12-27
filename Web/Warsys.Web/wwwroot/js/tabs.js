function openPage(pageName, elmnt) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablink");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].style.backgroundColor = "";
    }
    document.getElementById(pageName).style.display = "block";
    elmnt.style.backgroundColor = '#db2b36';
}

document.addEventListener("DOMContentLoaded", function () {

    var e1 = document.getElementById("tabButton1");
    var e2 = document.getElementById("tabButton2");
    var e3 = document.getElementById("tabButton3");
    var e4 = document.getElementById("tabButton4");
    var e5 = document.getElementById("tabButton5");

    e1.addEventListener("click", function () { openPage('Tab1', e1) });
    e2.addEventListener("click", function () { openPage('Tab2', e2) });
    e3.addEventListener("click", function () { openPage('Tab3', e3) });
    e4.addEventListener("click", function () { openPage('Tab4', e4) });
    e5.addEventListener("click", function () { openPage('Tab5', e5) });

    document.getElementById("tabButton2").click();
});
