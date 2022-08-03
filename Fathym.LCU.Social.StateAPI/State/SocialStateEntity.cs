using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCU.Personas.Enterprises.Architect.EnterprisesAsCode;
using LCU.Personas.Enterprises.Architect.State;
using LCU.Personas.Registry.EaC;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Fathym.LCU.Social.StateAPI.State
{
    public interface ISocialState
    {
    }

    public class SocialStateEntity : SocialState, ISocialState
    {
        #region Constructors
        public SocialStateEntity()
        {
        }
        #endregion

        #region API Methods
        //public virtual async Task ProcessEaC(EaCState eacState)
        //{
        //    Projects = eacState?.EaC?.Projects?.Select(proj =>
        //    {
        //        return new StudioProjectState()
        //        {
        //            Name = proj.Value.Project.Name,
        //            Lookup = proj.Key
        //        };
        //    }).ToList() ?? new List<StudioProjectState>();
        //}
        #endregion

        #region Life Cycle
        [FunctionName(nameof(SocialStateEntity))]
        public async Task Run([EntityTrigger] IDurableEntityContext ctx)
        {
            //if (!ctx.HasState)
            //    ctx.SetState(new EaCState());

            await ctx.DispatchAsync<SocialStateEntity>();
        }
        #endregion
    }
}
