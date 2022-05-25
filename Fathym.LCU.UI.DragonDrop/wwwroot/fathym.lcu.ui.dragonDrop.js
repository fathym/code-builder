// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function initialize(config) {
    config.DragClass = config.DragClass || 'draggable';

    config.RootElement = typeof config.RootElement !== 'string' ? config.RootElement :
        document.querySelector(config.RootElement);

    let rootEl = config.RootElement;

    if (rootEl != null) {
        rootEl.addEventListener('pointerdown', (event) => {
            userPressed(event);
        }, { passive: true });

        return {
            Code: 0,
            Message: 'Success'
        };
    } else {
        return {
            Code: 4,
            Message: 'Not Located'
        };
    }

    function userPressed(event) {
        let element = event.target;

        if (element.classList.contains(config.DragClass)) {
            rootEl.addEventListener('pointermove', userMoved, { passive: true });

            rootEl.addEventListener('pointerup', userReleased, { passive: true });

            rootEl.addEventListener('pointercancel', userReleased, { passive: true });
        };
    }

    function userMoved(event) {
        console.log("dragging!")
    };

    function userReleased(event) {
        console.log("dropped!");

        rootEl.removeEventListener('pointermove', userMoved);

        rootEl.removeEventListener('pointerup', userReleased);

        rootEl.removeEventListener('pointercancel', userReleased);
    };
}
