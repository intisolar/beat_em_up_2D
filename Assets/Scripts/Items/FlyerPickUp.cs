using UnityEngine;

public class FlyerPickUp : ItemPickUp, IPickable
{
    public void OnPickup()
    {
        //Rellenar energ�a
        throw new System.NotImplementedException();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //recuperar energ�a
        OnPickup();
    }
}
