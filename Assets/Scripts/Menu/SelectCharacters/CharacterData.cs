using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "ScriptablesObjects/Characters")]
public class CharacterData : ScriptableObject
{
    [System.Serializable] public class Character
    {
        public GameObject _player;
        public Sprite Image;
    }
    public List<Character> Characters;
}