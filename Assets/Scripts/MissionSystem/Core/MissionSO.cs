using System;
using System.Threading.Tasks;
using UnityEngine;
using MissionSystem.Interfaces;

namespace MissionSystem.Core
{
    [CreateAssetMenu(fileName = "Mission", menuName = "Missions/Mission")]
    public class MissionSO : ScriptableObject, IMission
    {
        [Header("Mission Setup")]
        public string missionName;
        public int startDelay = 0;
        public int goal = 0;

        [Header("Mission Chain")]
        public ScriptableObject currentMission;
        
        public MissionSO previousMission;
        public MissionSO nextMission;
        
        /* Private variables */
        private bool isRunning = false;
        private Timer timer = new Timer();

        public event Action OnStarted;
        public event Action OnMissionPointReached;
        public event Action OnFinished;
        public async Task Start()
        {
            isRunning = true;
            await timer.StartAsync(startDelay * 1000, OnStarted.Invoke);
        }

        public void AddCount()
        {
            goal++;
            OnMissionPointReached?.Invoke();
        }
    }
}