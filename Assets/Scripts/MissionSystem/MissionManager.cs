using System;
using System.Collections.Generic;
using System.Linq;
using MissionSystem.Core;
using MissionSystem.Demo;
using UnityEngine;

namespace MissionSystem
{
    public class MissionManager : MonoBehaviour
    {
        private static MissionManager Instance { get; set; }
        private static UIHandler UIHandler { get; set; }

        [SerializeField] private List<MissionChainSO> chainQueue;

        /* Private variables*/
        private List<MissionChainSO> activeChains = new List<MissionChainSO>();
        private int chainIndex = 0;
        
        private void Awake()
        {
            // Ensure we have just one object
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);

            UIHandler = FindAnyObjectByType<UIHandler>();
            if (!UIHandler)
                Debug.LogWarning("UIHandler not found");
        }

        public void StartNewChain()
        {
            var chain = chainQueue[chainIndex];

            void OnChainFinishedHandler()
            {
                UIHandler.SetChainName($"{chain.chainName} completed!");
                
                // Removing a chain from the activeChains list.
                activeChains.Remove(chain);
            }

            if (!activeChains.Contains(chain))
            {
                activeChains.Add(chain);

                chain.OnChainFinished += OnChainFinishedHandler;
                
                StartCoroutine(chain.StartChain());
            }


            string chainsList = activeChains
                .Aggregate("", (current, chains) => 
                    current + (chains.chainName.ToString() + "\n"));

            UIHandler.SetChainName(chainsList);
            
            var mission = chain.GetCurrentMission();
            UIHandler.SetMissionName(mission.missionName + "  " + mission.GetProgress() + "/" + mission.GetGoal());
            
            chainIndex++;
        }

        public void AddProgress()
        {
            var mission = chainQueue[chainIndex].GetCurrentMission();
            mission.AddCount();
            
            UIHandler.SetMissionName(mission.missionName + "  " + mission.GetProgress() + "/" + mission.GetGoal());
        }
    }
}