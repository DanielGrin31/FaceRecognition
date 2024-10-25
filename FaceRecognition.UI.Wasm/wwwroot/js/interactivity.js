function showOffCanvas(){
    let offcanvas= new bootstrap.Offcanvas($(".offcanvas")[0])
    offcanvas.show();
}
function showModal(id="myModal") {
    let myModalEl=$(`#${id}`)[0]
    let myModal = bootstrap.Modal.getOrCreateInstance(myModalEl);
    myModal.show();
}
function closeModal(id="myModal") {
    let myModalEl=$(`#${id}`)[0]
    let myModal = bootstrap.Modal.getOrCreateInstance(myModalEl);
    myModal.hide();
}
