import { BrowserJsPlumbInstance, BrowserJsPlumbDefaults } from '@jsplumb/browser-ui';
export declare class JSPlumbInterop {
    static Alert(message: string): void;
    static Create(container: Element, defaults?: BrowserJsPlumbDefaults): BrowserJsPlumbInstance;
    static RegisterHandlers(dotNetHelper: any, instance: BrowserJsPlumbInstance): void;
}
