using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace Fathym.LCU.Enterprise.StateAPI
{
    public class LCUState
    {
        #region Helpers
        protected virtual IDurableEntityContext context
        {
            get
            {
                return Entity.Current;
            }
        }
        #endregion
    }
}