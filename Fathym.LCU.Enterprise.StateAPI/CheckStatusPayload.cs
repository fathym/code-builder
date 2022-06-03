using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Text.Json.Serialization;

namespace Fathym.LCU.Enterprise.StateAPI
{
    public class CheckStatusPayload
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("statusQueryGetUri")]
        public string StatusQueryGetUri { get; set; }

        [JsonPropertyName("sendEventPostUri")]
        public string SendEventPostUri { get; set; }

        [JsonPropertyName("terminatePostUri")]
        public string TerminatePostUri { get; set; }

        [JsonPropertyName("rewindPostUri")]
        public string RewindPostUri { get; set; }

        [JsonPropertyName("purgeHistoryDeleteUri")]
        public string PurgeHistoryDeleteUri { get; set; }

        [JsonPropertyName("restartPostUri")]
        public string RestartPostUri { get; set; }
    }
}