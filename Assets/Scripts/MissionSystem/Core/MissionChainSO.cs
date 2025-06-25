using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo;
using MissionSystem.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace MissionSystem.Core
{
    [CreateAssetMenu (fileName = "Mission Chain", menuName = "Missions/Mission Chain")]
    public class MissionChainSO : ScriptableObject
    {
        [Header("Chain Setup")]
        public string chainName;
        public List<MissionSO> missions;
        
        public static UIHandler UIHandler { get; private set; }
        
        /* Private variables */
        private MissionSO _currentMission;
        private int missionIndex = 0;
        
        private Timer timer = new Timer();
        
        // Event is intended to automatically finish the chain when all missions are completed
        public event Action OnChainFinished;

        public void StartChain()
        {
            missionIndex = 0;
            StartNextMission();
        }

        /// <summary>
        /// Function is used to track if the MissionChain still contains the missions to be done.
        /// If not, then it will throw a warning.
        /// </summary>
        private async Task StartNextMission()
        {
            try
            {
                _currentMission = Instantiate(missions[missionIndex]);

                _currentMission.OnFinished += OnMissionFinished;

                if (_currentMission.startDelay > 0)
                    await timer.StartAsync(_currentMission.startDelay * 1000);
                
                _currentMission.Start();
                missionIndex++;
                
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Exception in StartNextMission: {e.Message}\n{e.StackTrace}");
            }
        }

        private void OnMissionFinished()
        {
            _currentMission.OnFinished -= OnMissionFinished;
            if (missionIndex < missions.Count)
            {
                StartNextMission();
            }
            else
            {
                OnChainFinished?.Invoke();
            }
        }
        
        /* Used for UI */
        public MissionSO GetCurrentMission() => _currentMission;
    }
}