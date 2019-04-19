using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum Type { Health, Regen, Attack, Defense, Speed };

public class Powerup : MonoBehaviour
{
    const string powerupPath = "Prefabs/Powerups/Powerup_";

    public float multiplier = 2f;
    public float duration = 10f;
    public Collider player;
    public Type type;
    

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
           StartCoroutine (Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        Fighter fighter = player.GetComponent<Fighter>();
        switch (type)
        {
            case Type.Health:
                Heal(fighter, 10 * multiplier);
                Destroy(gameObject);
                break;
            case Type.Regen:
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                Regen(fighter, 15f * multiplier, duration);
                Destroy(gameObject);
                break;
            case Type.Attack:
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                fighter.attackMultiplier *= multiplier;
                yield return new WaitForSeconds(duration);
                fighter.attackMultiplier /= multiplier;
                Destroy(gameObject);
                break;
            case Type.Defense:
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                fighter.defenseMultiplier *= multiplier;
                yield return new WaitForSeconds(duration);
                fighter.defenseMultiplier /= multiplier;
                Destroy(gameObject);
                break;
            case Type.Speed:
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                fighter.speedMultiplier *= multiplier;
                yield return new WaitForSeconds(duration);
                fighter.speedMultiplier /= multiplier;
                Destroy(gameObject);
                break;
            default:
                Destroy(gameObject);
                break;


        }
                


    }

    IEnumerator Regen(Fighter fighter, float health, float duration)
    {
        float amountHealed = 0;
        float increment = health / duration;
        while (amountHealed < health)
        {
            Heal(fighter, increment);
            amountHealed += increment;
            yield return new WaitForSeconds(1f);
        }
    }
    void Heal(Fighter fighter, float health)
    {
        if (fighter.health + health >= fighter.maxHealth)
            fighter.health = fighter.maxHealth;
        else
            fighter.health += health;
        if (fighter.health_UI != null)
        {
            fighter.health_UI.fillAmount = fighter.health / fighter.maxHealth;
        }
    }

    
}
