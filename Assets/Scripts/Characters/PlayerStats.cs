using UnityEngine;

// Clase hija para los jugadores
public class PlayerStats : CharacterStats
{
    protected override void Start()
    {
        base.Start();
        // Aquí podrías poner cosas específicas para los jugadores
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("El jugador ha muerto. Mostrar pantalla de Game Over.");
        // Aquí puedes mostrar menú de Game Over o reiniciar nivel
    }
}

