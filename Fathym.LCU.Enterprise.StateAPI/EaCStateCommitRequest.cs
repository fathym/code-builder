using Fathym.API;
using LCU.Personas.Enterprises.Architect.EnterprisesAsCode;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace Fathym.LCU.Enterprise.StateAPI
{
    public class EaCStateCommitRequest : BaseRequest
    {
        public virtual CommitEnterpriseAsCodeRequest Commit { get; set; }

        public virtual DurableOrchestrationStatus CurrentStatus { get; set; }
    }
}