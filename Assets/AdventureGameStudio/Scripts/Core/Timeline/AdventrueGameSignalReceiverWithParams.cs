using Sakuya.UnityUIAnime.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace AdventureGame
{
    public class AdventrueGameSignalReceiverWithParams : MonoBehaviour, INotificationReceiver
    {
        [NonReorderable]
        public SignalAssetEventPair[] signalAssetEventPairs;

        [Serializable]
        public class SignalAssetEventPair
        {
            public SignalAsset signalAsset;
            public ParameterizedEvent events;

            [Serializable]
            public class ParameterizedEvent : UnityEvent<RectTransform_UIAnime_Define> { }
        }

        public void OnNotify(Playable origin, INotification notification, object context)
        {
            if (notification is AdventureGameParameterizedEmitter<RectTransform_UIAnime_Define> boolEmitter)
            {
                var matches = signalAssetEventPairs.Where(x => ReferenceEquals(x.signalAsset, boolEmitter.asset));
                foreach (var m in matches)
                {
                    m.events.Invoke(boolEmitter.parameter);
                }
            }
        }
    }
}