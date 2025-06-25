using System;
using System.Collections.Generic;
using System.Linq;
using Demo;
using MissionSystem.Core;
using UnityEngine;

namespace MissionSystem
{
    public class MissionManager : MonoBehaviour
    {
        private static MissionManager Instance { get; set; }
        private static UIHandler UIHandler { get; set; }

        [SerializeField] private List<MissionChainSO> chainQueue;

        /* Private variables*/
        private List<MissionChainSO> _activeChains;
        private List<MissionChainSO> _completedChains;
        private int _chainIndex = 0;
        
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
            
            _activeChains = new List<MissionChainSO>();
            _completedChains = new List<MissionChainSO>();
        }

        public void StartNewChain()
        {
            if (_chainIndex >= chainQueue.Count)
                return;
            
            var chain = chainQueue[_chainIndex];
            Debug.Log($"Current Chain: {chain}");

            if (!_activeChains.Contains(chain) && !_completedChains.Contains(chain))
            {
                _activeChains.Add(chain);
                
                Action OnChainFinishedHandler = null;
                OnChainFinishedHandler = () =>
                {
                    UIHandler.SetChainName($"{chain.chainName} completed!");

                    // Removing a chain from the activeChains list.
                    _activeChains.Remove(chain);
                    _completedChains.Add(chain);
                    
                    chain.OnChainFinished -= () => OnChainFinishedHandler();
                    
                    DrawChains();
                    DrawMissions();
                };

                chain.OnChainFinished += () => OnChainFinishedHandler();
                
                chain.StartChain();
            }


            DrawChains();
            DrawMissions();

            _chainIndex++;
        }

        private void DrawChains()
        {
            var chainsList = _activeChains
                .Aggregate("", (current, activeChains) =>
                    current + (activeChains.chainName.ToString() + "\n"));

            UIHandler.SetChainName(chainsList);
        }

        private void DrawMissions()
        {
            var missionsList = _activeChains
                .Aggregate("", (current, activeChains) =>
                    current + (activeChains.GetCurrentMission().missionName + "  " + 
                               activeChains.GetCurrentMission().GetProgress() + "/" + 
                               activeChains.GetCurrentMission().GetGoal() + "\n"));

            UIHandler.SetMissionName(missionsList);
        }

        public void AddProgress()
        {
            foreach (var chain in _activeChains.ToList())
            {
                var mission = chain.GetCurrentMission();
                mission.AddCount();
            
                DrawMissions();
            }
        }
    }
}