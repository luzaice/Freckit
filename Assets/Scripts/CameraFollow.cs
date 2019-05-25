using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform player1, player2;
    public Transform characterList1;
    public Transform characterList2;
    float x;

    [SerializeField]
    Vector3 offset;
    Vector3 offsetMin = new Vector3(0, 1, -3);

    // Start is called before the first frame update
    void Start()
    {
        player1 = characterList1.GetChild(PlayerPrefs.GetInt("Character1"));
        player2 = characterList2.GetChild(PlayerPrefs.GetInt("Character2"));
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
