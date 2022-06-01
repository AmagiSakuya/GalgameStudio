using Sakuya.UnityUIAnime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Sakuya.UnityUIAnimeEditor
{
    [CustomEditor(typeof(UIAnimeBase), true)]
    public class UIAnimeBaseEditor : Editor
    {
        UIAnimeBase t;
        private void OnEnable()
        {
            t = (UIAnimeBase)target;
            EditorApplication.update += ProgressIEnumerator;
        }

        private void OnDisable()
        {
            if (!Application.isPlaying)
            {
                t.Dispose();
            }
            EditorApplication.update -= ProgressIEnumerator;
        }

        void ProgressIEnumerator()
        {
            if (!Application.isPlaying)
            {
                EditorApplication.QueuePlayerLoopUpdate();
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.BeginVertical("GroupBox");
            GUILayout.BeginHorizontal();
            Color m_defaultColor = GUI.color;
            GUI.color = Color.green;
            if ( GUILayout.Button("Play"))
            {
                t.Play();
            }

            GUI.color = Color.red;
            if (GUILayout.Button("Dispose"))
            {
                t.Dispose();
            }
            GUI.color = m_defaultColor;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Pause"))
            {
                t.Pause();
            }

            if (GUILayout.Button("Resume"))
            {
                t.Resume();
            }
            GUI.color = m_defaultColor;
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
    }
}