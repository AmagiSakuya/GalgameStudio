using System;
using UnityEngine;

namespace AdventureGame
{
    [CreateAssetMenu(menuName = "ADV Studio/UI Style Define/Dialog Style Define")]
    public class GameDialogStyleDefine : ScriptableObject
    {
        public Sprite backgroundImage;
        public ADVDramaTextSettings charaNameTextSettings;
        public ADVDramaTextSettings contentTextSettings;

        public Action Apply;
        public void OnValidate()
        {
            Apply?.Invoke();
        }
    }
}