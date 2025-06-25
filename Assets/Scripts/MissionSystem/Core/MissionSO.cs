using System;
using System.Collections;
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
        private Timer timer = new Timer();
        private int progress = 0;

        public event Action OnStarted;
        public event Action OnMissionPointReached;
        public event Action OnFinished;
        
        /// <summary>
        /// The function is called every time when we start the new mission.
        /// It is self-subscribed for OnFinished event to handle the end of mission
        /// and automatically start the new one
        /// </summary>
        public async Task Start()
        {
            Debug.Log($"MissionSO: Mission {missionName} starting in {startDelay} seconds");
            
            OnStarted?.Invoke();
            Debug.Log($"MissionSO: Mission {missionName} started!");
        }

        /// <summary>
        /// Used only to handle the progress with Button
        /// </summary>
        public void AddCount()
        {
            progress++;
            OnMissionPointReached?.Invoke();
            
            if (progress == goal) OnFinished?.Invoke();
        }
        
        /* Used for UI */
        public int GetGoal() => goal;
        public int GetProgress() => progress;
    }
}