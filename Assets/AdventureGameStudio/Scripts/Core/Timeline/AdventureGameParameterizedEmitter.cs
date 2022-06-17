using Sakuya.UnityUIAnime.Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace AdventureGame
{
    public class RectTransform_UIAnime_Signal : AdventureGameParameterizedEmitter<RectTransform_UIAnime_Define> { }

    public class AdventureGameParameterizedEmitter<T> : SignalEmitter
    {
        public T parameter;
    }
}