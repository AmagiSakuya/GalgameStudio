using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AdventureGame;

namespace AdventureGameEditor
{
    [CustomEditor(typeof(AdventureGameBrain))]
    public class AdventureGameBrainEditor : Editor
    {
        AdventureGameBrain m_target;

        private void OnEnable()
        {
            m_target = (AdventureGameBrain)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying && m_target.dialogStyleDefine != null)
            {
                m_target.ApplyDialogStyleDefine();
                SceneView.RepaintAll();
            }
        }
    }
}