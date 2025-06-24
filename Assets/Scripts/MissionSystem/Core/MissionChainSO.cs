using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MissionSystem.Demo;
using MissionSystem.Interfaces;
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
        
        // Event is intended to automatically finish the chain when all missions are completed
        public event Action OnChainFinished;

        public IEnumerator StartChain()
        {
            while (missionIndex < missions.Count)
            {
                bool finished = false;

                // Start the next mission
                StartNextMission();

                // Wait for the current mission to be assigned
                while (_currentMission == null)
                    yield return null;

                Debug.Log($"Starting new mission {_currentMission.name} on chain {chainName}");

                // Ensurance to sub and unsub from the event in MissionSO
                void OnFinishedHandler() => finished = true;
                
                // Subscribe to OnFinished
                _currentMission.OnFinished += OnFinishedHandler;

                // Wait for OnFinished event
                while (!finished)
                    yield return null;

                _currentMission.OnFinished -= OnFinishedHandler;


            }
            
            // Invoke for end of Chain
            OnChainFinished?.Invoke();
        }

        /// <summary>
        /// Function is used to track if the MissionChain still contains the missions to be done.
        /// If not, then it will throw a warning.
        /// </summary>
        private async void StartNextMission()
        {
            try
            {
                if (missionIndex < missions.Count)
                {
                    _currentMission = Instantiate(missions[missionIndex]);
                    await _currentMission.Start();
                    missionIndex++;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Exception in StartNextMission: {e.Message}\n{e.StackTrace}");
            }
        }
        
        /* Used for UI */
        public MissionSO GetCurrentMission() => _currentMission;
    }
}