using System.Threading.Tasks;

namespace Fathym.LCU.Studio.StateAPI
{
    public interface ICounter
    {
        void Add(int amount);

        void Delete();
        
        Task<int> Get();
        
        Task Reset();
    }
}