using TMPro;
using UnityEngine;

namespace Demo
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text chainName;
        [SerializeField] private TMP_Text missionName;

        public void SetChainName(string str) => chainName.text = str;

        public void SetMissionName(string str) => missionName.text = str;
    }
}