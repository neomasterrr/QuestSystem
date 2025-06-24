using System;
using System.Collections.Generic;
using MissionSystem.Core;
using UnityEngine;

namespace MissionSystem
{
    public class MissionManager : MonoBehaviour
    {
        public static MissionManager Instance { get; private set; }

        [SerializeField] private List<MissionChainSO> chainQueue;

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
        }

        private void StartChain()
        {
            foreach (var chain in chainQueue)
            {
                if (!chain.IsRunning()) chain.Start();
            }
        }
    }
}