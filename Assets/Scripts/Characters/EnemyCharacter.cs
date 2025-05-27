using UnityEngine;

/***
 * Representa un personaje enemigo.
 * Ataca al jugador al hacer contacto, causando daño.
 ***/
public class EnemyCharacter : CharacterBase
{
    public int damageAmount = 10;

    // Método de ataque genérico. Puede personalizarse según el tipo de enemigo.
    public override void PerformAttack()
    {
        // Este enemigo ataca automáticamente al hacer contacto, así que no necesita lógica activa acá.
        Debug.Log($"{name} performed an attack.");
    }

    public override void TakeDamage(int amount)
    {
        // Acá podés sumar efectos visuales, sonido o animaciones de recibir daño
        Debug.Log($"{name} took {amount} damage.");
        // Lógica futura: reducir salud, chequear muerte, etc.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log($"{name} hit {player.name}, dealing {damageAmount} damage.");
            player.TakeDamage(damageAmount);
            PerformAttack(); // Solo para mostrar que atacó
        }
    }
}
