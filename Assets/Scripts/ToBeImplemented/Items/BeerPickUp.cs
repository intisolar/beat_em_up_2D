using UnityEngine;

public class BeerPickUp : ItemPickUp, IPickable
{
    public void OnPickup()
    {
        //Curar al jugador
        throw new System.NotImplementedException();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Curar al jugador
        OnPickup();
    }
}
