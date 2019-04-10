using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitColider : MonoBehaviour
{
    public string PunchName;
    public float damage;

    public Fighter owner;

    private void OnTriggerEnter(Collider other)
    {
        Fighter somebody = other.gameObject.GetComponent<Fighter>();
        Animator animator = GetComponent<Animator>();

        if(owner.attacking)
        {
            if (somebody != null && somebody != owner)
            {
                //Debug.Log("I hit somebody");
                somebody.hurt(damage);
               // animator.SetTrigger("ATTACK");
            }
        }
    }
}
