import { newInstance, } from '@jsplumb/browser-ui';
import { EVENT_CONNECTION, INTERCEPT_BEFORE_DROP, } from '@jsplumb/core';
export class JSPlumbInterop {
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
//# sourceMappingURL=JSPlumbInterop.js.map