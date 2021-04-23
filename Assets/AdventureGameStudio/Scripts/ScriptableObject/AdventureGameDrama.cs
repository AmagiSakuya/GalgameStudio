using UnityEngine;

namespace AdventureGame
{
    [CreateAssetMenu(menuName = "ADV Studio/AdventureGameDrama")]
    public class AdventureGameDrama : ScriptableObject
    {
        public ADV_Drama_Composition[] drama;
    }

    public class ADV_Drama_Composition
    {
        public string content;
    }
}