﻿using UnityEngine;
 using UnityEngine.Localization;

 namespace Dialogues.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Characters/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private LocalizedString _characterName;
        [SerializeField] private Color _textColor;
        [SerializeField] private Sprite _characterSprite;

        public LocalizedString CharacterName => _characterName;

        public Color TextColor => _textColor;

        public Sprite CharacterSprite => _characterSprite;
    }
}