using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack : MonoBehaviour
{
   
    Collider axeCollider;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        axeCollider = gameObject.GetComponent<BoxCollider>();
        axeCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Attack(player.GetComponent<PlayerStats>().Attack, collider);
        Debug.Log("Triggered");
    }

    public void SetCollider(bool enable)
    {
        axeCollider.enabled = enable;
    }

    void Attack(int Damage, Collider collider)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeHit(Damage);
            Debug.Log("Attacked");
        }
    }
}
