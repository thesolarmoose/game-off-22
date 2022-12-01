using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Initialization
{
    public class GameStateResetter : MonoBehaviour
    {
        [SerializeField] private List<BoolVariable> _facts;
        [SerializeField] private ItemValueList _inventory;

        private void Start()
        {
            foreach (var fact in _facts)
            {
                fact.Value = false;
            }

            _inventory.Clear();
        }
    }
}