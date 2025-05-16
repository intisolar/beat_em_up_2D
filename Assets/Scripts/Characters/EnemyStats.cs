using UnityEngine;

public class EnemyStats : CharacterStats
{
    protected override void Start()
    {
        base.Start();
        // Aquí podrías inicializar la IA enemiga
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Un enemigo fue derrotado.");
        // Aquí podrías soltar objetos, dar puntos, etc.
    }
}

