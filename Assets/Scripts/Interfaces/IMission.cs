using System;

namespace Interfaces
{
    public interface IMission
    {
        event Action OnStarted;
        event Action OnMissionPointReached;
        event Action OnFinished;
        void Start();
    }

}