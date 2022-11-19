using System;
using Dialogues.Core;
using UnityEngine;
using UnityEngine.Localization;

namespace Dialogues.Data
{
    [Serializable]
    public class CustomDialogueLineData : IDialogueLineData
    {
        [SerializeField] private CharacterData _character;

        [Space(12)]
        [SerializeField] private LocalizedString _dialogueText;

        public CharacterData Character => _character;

        public LocalizedString DialogueText => _dialogueText;

        public override string ToString()
        {
            return $"{_character.CharacterName}: {_dialogueText}";
        }
    }
}