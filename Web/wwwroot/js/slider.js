// Main Slider

var next = document.getElementById("next")
var prev = document.getElementById("prev")
var slider = document.querySelector(".slider ul")
var nckm = 0;
window.onload=function(){
    setInterval(nextSlide,6000)
    document.querySelector(".preloader").remove()
}
function nextSlide(){
    let sliderLi=slider.querySelectorAll("li")
    nckm++;
    if(nckm == sliderLi.length){
        nckm = 0;
    }
    var tw = innerWidth;
    var mes=-(nckm*tw);
    slider.style.left = mes+"px"
    sliderLi.forEach(s=>{
        s.querySelector(".text").classList.remove("text-active")
        s.querySelector(".textp").classList.remove("textp-active")
        s.querySelector(".textbtn").classList.remove("textbtn-active")
    })
    sliderLi[nckm].querySelector(".text").classList.add("text-active")
    sliderLi[nckm].querySelector(".textp").classList.add("textp-active")
    sliderLi[nckm].querySelector(".textbtn").classList.add("textbtn-active")
}

next.onclick=nextSlide

prev.onclick=function(){
    let sliderLi=slider.querySelectorAll("li")
    nckm--;
    if(nckm == -1){
        nckm=sliderLi.length-1;
    }
    var tw = innerWidth;
    var mes=-(nckm*tw);
    slider.style.left = mes+"px"
    sliderLi.forEach(s=>{
        s.querySelector(".text").classList.remove("text-active")
        s.querySelector(".textp").classList.remove("textp-active")
        s.querySelector(".textbtn").classList.remove("textbtn-active")
    })
    sliderLi[nckm].querySelector(".text").classList.add("text-active")
    sliderLi[nckm].querySelector(".textp").classList.add("textp-active")
    sliderLi[nckm].querySelector(".textbtn").classList.add("textbtn-active")
}


