using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public int roundTime = 100;
    private float lastTimeUpdate = 0;
    public int PlayerOneWins = 0;
    public int PlayerTwoWins = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (roundTime > 0 && Time.time - lastTimeUpdate > 1)
        {
            roundTime--;
            lastTimeUpdate = Time.time;
        }

 
    }
}
