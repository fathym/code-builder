import { newInstance } from '@jsplumb/browser-ui';
import { INTERCEPT_BEFORE_DROP, EVENT_CONNECTION } from '@jsplumb/core';

class JSPlumbInterop {
    static Alert(message) {
        alert(message);
    }
    static Create(container, defaults) {
        return newInstance(Object.assign({ container: container }, (defaults || {})));
    }
    static RegisterHandlers(dotNetHelper, instance) {
        instance.bind(INTERCEPT_BEFORE_DROP, (params) => {
            return dotNetHelper.invokeMethodAsync('BeforeDropIntercept', params);
        });
        instance.bind(EVENT_CONNECTION, (params) => {
            dotNetHelper.invokeMethodAsync('Connected', params);
        });
    }
}

export { JSPlumbInterop };
