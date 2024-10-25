function clickInputFile(browseBtn){
  $(browseBtn).parent().siblings("input[type='file']").click();
}
async function loadImage(id,src) {
    let $canvas=$(`#dragarea-${id}`).find("canvas")
    $canvas.removeClass("d-none")
    const ctx = $canvas[0].getContext("2d");
  
    const img = new Image();
    return new Promise((resolve, reject) => {
      img.onload = function () {
        const displaySize = { width: img.width, height: img.height };
        faceapi.matchDimensions($canvas[0], displaySize);
        ctx.drawImage(img, 0, 0, $canvas[0].width, $canvas[0].height);
        resolve()
      };
      img.onerror = (error) => reject(error);
      img.src = src; // Replace with your image URL
    });

  }

async function drawLandmarks(id,allLandmarks){
    let $canvas=$(`#dragarea-${id}`).find("canvas")
    const ctx = $canvas[0].getContext("2d")

    for (let i = 0; i < allLandmarks.length; i++) {
      let landmarks = allLandmarks[i];
      ctx.strokeStyle = "Red"
      ctx.fillStyle = "Red"
      let pointSize = 4
      landmarks.forEach(point => {
        ctx.beginPath()
        ctx.arc(point[0], point[1], pointSize, 0, 2 * Math.PI)
        ctx.fill()
      })
    }
}
function unloadImage(id) {
  let $canvas=$(`#dragarea-${id}`).find("canvas")
  const ctx = $canvas[0].getContext("2d");
  ctx.clearRect(0, 0, $canvas[0].width, $canvas[0].height);
  $canvas.addClass("d-none");
}

async function drawBBox(id,faces,face_num){
    let $canvas=$(`#dragarea-${id}`).find("canvas")
    for (let i = 0; i < faces.length; i++) {
      let face = faces[i];
      let box = { x: face[0], y: face[1], width: face[2] - face[0], height: face[3] - face[1] }
      let num = (face_num === -2) ? i : face_num;
      let drawOptions = {
        label: `face ${num + 1}`,
        lineWidth: 5
      }
      let drawBox = new faceapi.draw.DrawBox(box, drawOptions)
      drawBox.draw($canvas[0])
    }
}
