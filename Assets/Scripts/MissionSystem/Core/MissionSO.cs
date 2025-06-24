using UnityEngine;

namespace MissionSystem.Core
{
    [CreateAssetMenu(fileName = "Mission", menuName = "Missions/Mission")]
    public class MissionSO : ScriptableObject
    {
        // public variables are editable for SO, so no need in [ScriptableObject] tag
        public string name;
        public int delay;
        
        // TODO: link to IMission and previous/next missions
    }
}