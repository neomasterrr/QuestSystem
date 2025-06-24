using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MissionSystem.Interfaces;
using UnityEngine;

namespace MissionSystem.Core
{
    [CreateAssetMenu (fileName = "Mission Chain", menuName = "Missions/Mission Chain")]
    public class MissionChainSO : ScriptableObject
    {
        [Header("Chain Setup")]
        public string chainName;
        public List<MissionSO> missions = new List<MissionSO>();
        
        /* Private variables */
        private bool isRunning = false;

        public void Start()
        {
            isRunning = true;

            for (int i = 0; i < missions.Count; i++)
            {
                var currentMission = missions[i];
                currentMission.OnStarted += Start;
            }
        }
        
        public bool IsRunning() => isRunning;
    }
}