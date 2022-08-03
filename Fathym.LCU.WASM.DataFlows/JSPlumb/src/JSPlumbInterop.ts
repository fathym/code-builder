import {
  newInstance,
  BrowserJsPlumbInstance,
  BrowserJsPlumbDefaults,
} from '@jsplumb/browser-ui';

import {
  BeforeDropParams,
  ConnectionEstablishedParams,
  EVENT_CONNECTION,
  INTERCEPT_BEFORE_DROP,
} from '@jsplumb/core';

export class JSPlumbInterop {
  public static Alert(message: string): void {
    alert(message);
  }

  public static Create(
    container: Element,
    defaults?: BrowserJsPlumbDefaults
  ): BrowserJsPlumbInstance {
    return newInstance({
      container: container,
      ...(defaults || {}),
    });
  }

  public static RegisterHandlers(
    dotNetHelper,
    instance: BrowserJsPlumbInstance
  ): void {
    instance.bind(INTERCEPT_BEFORE_DROP, (params: BeforeDropParams) => {
      return dotNetHelper.invokeMethodAsync('BeforeDropIntercept', params);
    });

    instance.bind(EVENT_CONNECTION, (params: ConnectionEstablishedParams) => {
      dotNetHelper.invokeMethodAsync('Connected', params);
    });
  }
}
