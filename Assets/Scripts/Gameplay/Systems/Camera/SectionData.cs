using UnityEngine;

[CreateAssetMenu(menuName = "BeatEmUp/Seccion")]
public class SectionData : ScriptableObject
{
    [System.Serializable]
    public struct EnemyInfo
    {
        public GameObject Prefab;
        public int Quantity;
    }

    public EnemyInfo[] Enemies;

    [Header("Limits")]
    public float MaxBoundX;
}