using UnityEngine;

public class FlyerPickUp : ItemPickUp, IPickable
{
    public void OnPickup()
    {
        //Rellenar energía
        throw new System.NotImplementedException();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //recuperar energía
        OnPickup();
    }
}
