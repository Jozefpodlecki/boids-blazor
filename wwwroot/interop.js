function registerEvents(dotNetHelper) {
    document.addEventListener("visibilitychange", () => {
        dotNetHelper.invokeMethodAsync("OnVisibilityChange", document.hidden);
    });

    function resizeCanvas() {
        const width = window.innerWidth;
        const height = window.innerHeight;
        dotNetHelper.invokeMethodAsync("OnResize", width, height);
    }

    window.addEventListener("resize", resizeCanvas);
    resizeCanvas();
}

function startAnimationFrame(dotNetHelper) {
    async function frameCallback() {
        
        const timestamp = performance.now();
        await dotNetHelper.invokeMethodAsync("OnFrame", timestamp);
        
        window.requestAnimationFrame(frameCallback);
    }

    window.requestAnimationFrame(frameCallback);
}