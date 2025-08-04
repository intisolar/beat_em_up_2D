using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "ScriptablesObjects/AttackData")]
public class AttackData : ScriptableObject
{
    [System.Serializable]
    public class Attack
    {
        public CommonAttackType AttackType;
        public byte AttackPower;
        public float AttackDuration;
        public float AttackDelay;
    }

    public enum CommonAttackType
    {
        Punch,
        Kick,
        Uppercut,
        SpecialMove,
        Grab,
        Throw
    }

    public List<Attack> Attacks;
}
