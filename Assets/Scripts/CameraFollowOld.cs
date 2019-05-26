using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowOld : MonoBehaviour
{

    public Transform player1, player2;
    float x;

    [SerializeField]
    Vector3 offset;
    Vector3 offsetMin = new Vector3(0, 1, -3);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        x = (player1.position.x - player2.position.x) / 2;

        offset = new Vector3(0, 1, -Mathf.Sqrt(2*(x*x))-1);
        if (offset.z < offsetMin.z)
            transform.position = (player1.position + player2.position) / 2 + offset;
        else 
            transform.position = (player1.position + player2.position) / 2 + offsetMin;

    }
}
