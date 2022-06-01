using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    [System.Serializable]
    public class ADVSystemSettings
    {
        public ScreenMode screenMode = ScreenMode.Window;
        public bool animtionEffect = true;
        public MouseAutoHide mouseAutoHide = MouseAutoHide.Never;
        public MouseAutoMove mouseAutoMove = MouseAutoMove.Never;
    }

    public enum ScreenMode
    {
        FullScreen,
        Window
    }

    public enum MouseAutoHide
    {
        Never,
        Five,
        Ten,
        Twenty
    }

    public enum MouseAutoMove
    {
        Never,
        Yes,
        No
    }
}

