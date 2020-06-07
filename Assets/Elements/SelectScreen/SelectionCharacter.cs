using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Misc {
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "New Character")]
    public class SelectionCharacter : ScriptableObject {
        public int id;
        public string charName;
        public GameObject charDisplay;
        public Sprite selectionSprite;
    }
}