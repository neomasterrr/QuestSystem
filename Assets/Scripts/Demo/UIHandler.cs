using TMPro;
using UnityEngine;

namespace MissionSystem.Demo
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text chainName;
        [SerializeField] private TMP_Text MissionName;
        
        public void SetChainName(string str)
        {
            chainName.text = str;
        }

        public void SetMissionName(string str)
        {
            MissionName.text = str;
        }
    }
}