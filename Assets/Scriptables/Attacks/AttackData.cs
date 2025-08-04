using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "ScriptablesObjects/AttackData")]
public class AttackData : ScriptableObject
{
    [System.Serializable]
    public class Attack
    {
        public string AttackName;
        public byte AttackPower;
        public float AttackDuration;
        public float AttackDelay;
    }

    public List<Attack> Attacks;
}
