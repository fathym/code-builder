namespace Fathym.LCU.Studio.WASM.State
{
    public class LCUStudioState
    {
        public virtual LCUStudioActivityBarState? ActivityBar { get; set; }

        public virtual string? CurrentPackage { get; set; }

        public virtual string? CurrentPlugin { get; set; }

        public virtual LCUStudioHeaderState? Header { get; set; }
    }
}
