
using LCU.Personas.StateAPI;
using LCU.Personas.StateAPI.Durable;

namespace Fathym.LCU.Studio
{
    public class StudioState : LCUStateEntity
    {
        public virtual List<StudioProjectState> Projects { get; set; }
    }
}