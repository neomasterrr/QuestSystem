using UnityEngine;

namespace MissionSystem.Core
{
    [CreateAssetMenu (fileName ="Mission Chain", menuName = "Missions/Mission Chain")]
    public class MissionChain : ScriptableObject
    {
        public enum Type
        {
            Sequential,
            Parallel
        }
        public Type type;
        
        public MissionSO[] missions;
    }
}