using UnityEngine;

namespace MissionSystem
{
    public class MissionManager : MonoBehaviour
    {
        // Creating one instance at once
        public static MissionManager Instance { get; private set; }

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
    }
}