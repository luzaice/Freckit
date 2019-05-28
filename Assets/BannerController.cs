using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerController : MonoBehaviour
{
    public Fighter player1;
    public Fighter player2;
    public Transform characterList1;
    public Transform characterList2;
    protected Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (characterList1 != null && characterList2 != null)
        {
            player1 = characterList1.GetChild(PlayerPrefs.GetInt("Character1")).gameObject.GetComponent(typeof(Fighter)) as Fighter;
            player1.gameObject.SetActive(true);
            player2 = characterList2.GetChild(PlayerPrefs.GetInt("Character2")).gameObject.GetComponent(typeof(Fighter)) as Fighter;
            player2.gameObject.SetActive(true);
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
