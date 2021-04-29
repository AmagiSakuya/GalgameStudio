using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    [CreateAssetMenu(menuName = "ADV Studio/AdventureGameDrama")]
    public class AdventureGameDrama : ScriptableObject
    {
        public List<ADV_Drama_Composition> drama;
    }

    public class ADV_Drama_Composition
    {
        public string content;
    }
}