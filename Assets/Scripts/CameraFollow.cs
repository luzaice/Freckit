using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player1, player2;
    float x;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = (player1.position.x - player2.position.x) / 2;
        offset = new Vector3(0, 1, -Mathf.Sqrt(3*(x*x))-1);
        transform.position = (player1.position + player2.position) / 2 + offset;
    }
}
