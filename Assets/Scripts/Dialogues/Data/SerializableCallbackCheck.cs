using System;
using UnityEngine;
using Utils.Serializables;

namespace Dialogues.Data
{
    [Serializable]
    public class SerializableCheck : SerializableCallback<bool>{}
    
    [Serializable]
    public class SerializableCallbackCheck : ISerializablePredicate
    {
        [SerializeField] private SerializableCheck _callback;
        
        public bool IsMet()
        {
            return _callback.Invoke();
        }
    }
}