﻿using UnityEngine;

 namespace Dialogues.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Characters/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private string _characterName;
        [SerializeField] private Color _textColor;
        [SerializeField] private Sprite _characterSprite;

        public string CharacterName => _characterName;

        public Color TextColor => _textColor;

        public Sprite CharacterSprite => _characterSprite;
    }
}