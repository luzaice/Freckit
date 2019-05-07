using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EasyBot : MonoBehaviour
{
    public enum PlayerType
    {
        EASYBOT
    };

    public Transform player1, player2;
    float distance_x;
    int rand;

    public static float MAX_HEALTH = 100f;
    public static float STANDARD_ATTACK = 1f;
    public static float STANDARD_DEFENSE = 1f;
    public static float STANDARD_SPEED = 1f;

    public float maxHealth = MAX_HEALTH; //In the off chance max health is ever not 100
    public float health = MAX_HEALTH;
    public string fighterName;
    public PlayerType player;
    public FighterStates currentState = FighterStates.IDLE;

    protected Animator animator;
    private Rigidbody myBody;
    public Image health_UI;
    public float attackMultiplier = STANDARD_ATTACK;
    public float defenseMultiplier = STANDARD_DEFENSE;
    public float speedMultiplier = STANDARD_SPEED;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        rand = Random.RandomRange(0, 4);
    }

    public void BotAction(float distance, int rand)
    {
        if (distance >= 1.3)
        {
            animator.SetBool("WALK", true);
        }
        else
        {
            animator.SetBool("WALK", false);
            animator.SetBool("DEFEND", false);
            /*if (rand == 1)
                animator.SetTrigger("KICK");
            else if (rand == 2)
                animator.SetTrigger("PUNCH");
            else if (rand == 3)
                animator.SetBool("DEFEND", true);*/

            Debug.Log(rand);
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance_x = player2.position.x - player1.position.x;
        StartCoroutine(Wait());
        BotAction(distance_x, rand);

        if(health <= 0 && currentState != FighterStates.DEAD)
        {
            animator.SetTrigger("DEAD");
        }
    }

    public Rigidbody body
    {
        get
        {
            return this.myBody;
        }
    }
}
