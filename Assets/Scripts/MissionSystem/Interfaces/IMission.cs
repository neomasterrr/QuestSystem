using System;
using System.Threading.Tasks;

namespace MissionSystem.Interfaces
{
    public interface IMission
    {
        event Action OnStarted;
        event Action OnMissionPointReached;
        event Action OnFinished;
        
        Task Start();
    }

}