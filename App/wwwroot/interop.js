function registerEvents(dotNetHelper) {
    document.addEventListener("visibilitychange", () => {
        dotNetHelper.invokeMethodAsync("OnVisibilityChange", document.hidden);
    });

    function resizeCanvas() {
        const width = window.innerWidth;
        const height = window.innerHeight;
        dotNetHelper.invokeMethodAsync("OnResizeAsync", width, height);
    }

    window.addEventListener("resize", resizeCanvas);
    resizeCanvas();
}

function startAnimationFrame(dotNetHelper) {
    async function frameCallback() {
        
        const timestamp = performance.now();
        await dotNetHelper.invokeMethodAsync("OnFrameAsync", timestamp);
        
        window.requestAnimationFrame(frameCallback);
    }

    window.requestAnimationFrame(frameCallback);
}

function downloadScreenShot(filename) {
    html2canvas(document.querySelector("#app")).then(canvas => {
        saveAs(canvas.toDataURL(), filename + '.png')
    });
}

function getAvailableContexts() {
    // const possibleContexts = ['bitmaprenderer', '2d', 'webgl', 'webgl2', 'experimental-webgl', 'experimental-webgl2'];
    const possibleContexts = ['2d', 'webgl'];

    var availableContexts = possibleContexts.filter(context => {
        const canvas = document.createElement("canvas");
        return canvas.getContext(context) ? context : undefined;
    });

    return availableContexts;
}

function saveAs(uri, filename) {
    var link = document.createElement('a');

    if (typeof link.download === 'string') {
        link.href = uri;
        link.download = filename;

        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    } else {
        window.open(uri);
    }
}