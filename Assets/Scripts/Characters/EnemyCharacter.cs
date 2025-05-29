using UnityEngine;

/***
 * Representa un personaje enemigo.
 * Ataca al jugador al hacer contacto, causando da�o.
 ***/
public class EnemyCharacter : CharacterBase
{
    public int damageAmount = 10;

    // M�todo de ataque gen�rico. Puede personalizarse seg�n el tipo de enemigo.
    public override void PerformAttack()
    {
        // Este enemigo ataca autom�ticamente al hacer contacto, as� que no necesita l�gica activa ac�.
        Debug.Log($"{name} performed an attack.");
    }

    public override void TakeDamage(int amount)
    {
        // Ac� pod�s sumar efectos visuales, sonido o animaciones de recibir da�o
        Debug.Log($"{name} took {amount} damage.");
        // L�gica futura: reducir salud, chequear muerte, etc.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log($"{name} hit {player.name}, dealing {damageAmount} damage.");
            player.TakeDamage(damageAmount);
            PerformAttack(); // Solo para mostrar que atac�
        }
    }
}
