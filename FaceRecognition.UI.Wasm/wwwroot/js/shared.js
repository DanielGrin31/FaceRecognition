function display_faceActionMenu(e) {
    e.preventDefault();
    faceActionsMenu.style.display = "block";
    faceActionsMenu.style.left = `${e.pageX}px`;
    faceActionsMenu.style.top = `${e.pageY}px`;
    let src = $(e.currentTarget).find('img').attr('src');

    // Regular expression to extract face_num and image_name
    let regex = /static\/[^\/]+\/(.+)$/;
    let matches = src.match(regex);

    if (matches) {
        // Extract {image} (including 'aligned_{face_num}_{image_name}')
        current_selected_image=matches[1];
    }
}

$(document).ready();

async function setAsImage(idx) {
    await window.layout.invokeMethodAsync('SetImage', current_selected_image, idx);
}

function setupLayout(layout, serverURL) {
    window.layout = layout
    window.ServerURL = serverURL;
    const $faceActionsMenu = $("#faceActionsMenu");
    $(document).on("click", function () {
        $faceActionsMenu.hide();
    });
    $(document).on("contextmenu", ".gallery-image", display_faceActionMenu);
}

async function openFaceImage(face, face_num, path) {
    let canvas = await makeCanvas(path);
    let box = {x: face[0], y: face[1], width: face[2] - face[0], height: face[3] - face[1]}
    let num = (face_num === -2) ? i : parseInt(face_num);
    let drawOptions = {
        label: `face ${num + 1}`,
        lineWidth: 5
    }
    let drawBox = new faceapi.draw.DrawBox(box, drawOptions)
    drawBox.draw(canvas);
    let newWindow = window.open('', '_blank');
    $(newWindow.document.body).append(canvas);
}

function makeCanvas(src) {
    // Create a canvas element
    let canvas = document.createElement("canvas");
    let ctx = canvas.getContext("2d");

    // Create an Image object
    let img = new Image();
    img.crossOrigin = "anonymous"; // This enables CORS
    return new Promise((resolve, reject) => {
        img.onload = function () {
            const displaySize = {width: img.width, height: img.height};
            faceapi.matchDimensions(canvas, displaySize);
            ctx.drawImage(img, 0, 0, canvas.width, canvas.height);
            resolve(canvas);
        };
        img.onerror = (error) => reject(error);
        // Set the source of the image
        img.src = src;
    });
}
function setButtonsState(is_enabled)
{
    $('.btn').attr("disabled", !is_enabled);
    $('.submit-btn').attr("disabled", !is_enabled);
}