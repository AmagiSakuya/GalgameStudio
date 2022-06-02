using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AdventureGame
{
    public class AdventureGameBrain : Singleton<AdventureGameBrain>
    {
        [Header("��Ϸ����")]
        public AdventureGameConfig gameConfig;

        [Header("��ʽ����")]
        public GameDialogStyleDefine dialogStyleDefine;

        [Header("ģ������")]
        public AdventureGameDramaPlayer advPlayer;

        // Start is called before the first frame update
        void Start()
        {
            gameConfig.Apply += ApplyGameConfig;
            dialogStyleDefine.Apply += ApplyDialogStyleDefine;
            ApplyGameConfig();
            ApplyDialogStyleDefine();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            gameConfig.Apply -= ApplyGameConfig;
            dialogStyleDefine.Apply -= ApplyDialogStyleDefine;
        }

        public void ApplyGameConfig()
        {
            if (gameConfig == null)
            {
                Debug.LogError("GameConfigΪ��", gameObject);
                return;
            }
            advPlayer.SetSoundSettings(gameConfig.soundSettings);
            advPlayer.SetTextSettings(gameConfig.textSettings);
        }

        public void ApplyDialogStyleDefine()
        {
            advPlayer.dialog.SetDefine(dialogStyleDefine, gameConfig.textSettings.font);
        }

    }
}