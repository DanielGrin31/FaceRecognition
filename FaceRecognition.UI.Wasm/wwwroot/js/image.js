"use strict";

async function uploadFile(dataURL) {
    const randomString = Math.random().toString(36).substring(2, 10);
    const file = dataURLtoFile(dataURL, `${randomString}.png`);
    const data = new FormData();
    data.append('files', file);

    try {
        const response = await fetch(`${window.ServerURL}/api/upload`, {
            method: 'POST',
            body: data,
        });

        if (response.ok) {
            return await response.json();
        } else {
            console.error("Server returned an error:", response.status, response.statusText);
        }
    } catch (error) {
        console.error("Failed to upload file:", error);
    }

    return undefined;
}

async function stopFaceCapture(scanVideo,$videoObj,dotnetPage) {
    clearInterval(scanVideo);
    $videoObj.addClass("d-none");
    $videoObj[0].srcObject.getTracks().forEach(function (track) {
        track.stop();
    });
    await dotnetPage.invokeMethodAsync("VideoCaptured", undefined)
    
}

function startFaceCapture(dotnetPage) {
    const $videoObj = $('#video'); // Get the video element using jQuery
    const video = $videoObj[0]; // Get the video element using jQuery
    $("#startVideoBtn").prop("disabled", true);
    $videoObj.removeClass("d-none");
    Promise.all([
        faceapi.nets.tinyFaceDetector.loadFromUri("/models"),
        faceapi.nets.faceLandmark68Net.loadFromUri("/models"),
        faceapi.nets.faceRecognitionNet.loadFromUri("/models"),
        faceapi.nets.ssdMobilenetv1.loadFromUri("/models")
    ]).then(startVideo);

    function startVideo() {
        navigator.mediaDevices
            .getUserMedia({video: {}})
            .then((stream) => {
                video.srcObject = stream;
            })
            .catch((err) => {
                console.error("Error accessing the webcam: ", err);
            });
        video.addEventListener("play", async () => {
            const canvas = faceapi.createCanvasFromMedia(video);
            let continueScanning = true;

            $(canvas).addClass("start-0 position-absolute");
            $("#videoContainer").append(canvas);
            const displaySize = {width: 640, height: 480};
            faceapi.matchDimensions(canvas, displaySize);
            let scanVideo = setInterval(async () => {
                const detections = await faceapi
                    .detectAllFaces(video, new faceapi.TinyFaceDetectorOptions())
                    .withFaceLandmarks();
                if (detections.length > 0 && continueScanning) {
                    const canvas = document.createElement("canvas");
                    const ctx = canvas.getContext("2d");

                    // Set the canvas dimensions to match the video
                    canvas.width = video.videoWidth;
                    canvas.height = video.videoHeight;

                    // Draw the current frame of the video onto the canvas
                    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

                    // Convert the canvas to a data URL (base64 encoded)
                    const dataURL = canvas.toDataURL("image/png");
                    continueScanning = false;

                    let uploaded_file = await uploadFile(dataURL);
                    if (uploaded_file !== undefined) {
                        $("#video").addClass("d-none");
                        let canvas = $("canvas")
                        canvas.removeClass("d-none")
                        clearInterval(scanVideo);
                        video.srcObject.getTracks().forEach(function (track) {
                            track.stop();
                        });
                        await dotnetPage.invokeMethodAsync("VideoCaptured", uploaded_file)


                    } else {
                        await stopFaceCapture(scanVideo,$videoObj,dotnetPage);
                        // continueScanning = true;
                    }
                }
            }, 100);
        });
    }
}


function getImgParams($comboBox, areaNumber) {
    const faceNum = parseInt($comboBox.val());
    const fileName = current_images[areaNumber - 1];
    return {faceNum, fileName};
}

async function getFacePath($comboBox, areaNumber) {
    let $params = getImgParams($comboBox, areaNumber);
    let path;
    let face_num = $params.faceNum;
    if ($params.faceNum === -2) {
        path = window.ServerURL + `/pool/${$params.fileName}`;
    } else {
        path = window.ServerURL + `/static/aligned_${face_num}_${$params.fileName}`;
    }
    return {path, face_num};
}

function dataURLtoFile(dataurl, filename) {
    let arr = dataurl.split(","),
        mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[arr.length - 1]),
        n = bstr.length,
        u8arr = new Uint8Array(n);
    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new File([u8arr], filename, {type: mime});
}