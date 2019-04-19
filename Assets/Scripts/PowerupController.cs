using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { Health, Regen, Attack, Defense, Speed }

public class PowerupController : MonoBehaviour
{
    const string powerupPath = "Prefabs/Powerups/Powerup_";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PowerupLoop());
    }

    // Update is called once per frame
    public IEnumerator PowerupLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15, 25));

            Type type = (Type)Random.Range(0, 4);

            Vector3 position = GetRandomPosition();

            SpawnPowerup(type, position);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-15, 15);

        return new Vector3(x, 20, 0);
    }

    public static void SpawnPowerup(Type type, Vector3 position)
    {
        GameObject powerup = Instantiate(Resources.Load<GameObject>(powerupPath + type.ToString()), position, Quaternion.identity) as GameObject;
    }
}
