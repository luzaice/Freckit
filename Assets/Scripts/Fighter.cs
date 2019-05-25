using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public enum PlayerType
    {
        HUMAN1, HUMAN2, EASYBOT, MEDIUMBOT
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

    private float nextActionTime = 0.0f;
    public float period = 0.7f;

    private AudioSource audioPlayer;

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
        audioPlayer = GetComponent<AudioSource>();
    }
    
    public void UpdateHumanInput()
    {
        if (player.ToString() == "HUMAN1")
        {
            if (Input.GetAxis("Horizontal") > 0.1)
            {
                animator.SetBool("WALK", true);
            }
            else
                animator.SetBool("WALK", false);

            if (Input.GetAxis("Horizontal") < -0.1)
                animator.SetBool("WALK_BACK", true);
            else
                animator.SetBool("WALK_BACK", false);

            if (Input.GetAxis("Vertical") < -0.1)
                animator.SetBool("DUCK", true);
            else
                animator.SetBool("DUCK", false);

            if (Input.GetKeyDown(KeyCode.W))
                animator.SetTrigger("JUMP");

            if (Input.GetKeyDown(KeyCode.J))
                animator.SetTrigger("PUNCH");

            if (Input.GetKeyDown(KeyCode.K))
                animator.SetTrigger("KICK");

            if (Input.GetKey(KeyCode.Space))
                animator.SetBool("DEFEND", true);
            else
                animator.SetBool("DEFEND", false);
        }

        if (player.ToString() == "HUMAN2")
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                animator.SetBool("WALK", true);
            else
                animator.SetBool("WALK", false);

            if (Input.GetKey(KeyCode.RightArrow))
                animator.SetBool("WALK_BACK", true);
            else
                animator.SetBool("WALK_BACK", false);

            if (Input.GetKey(KeyCode.DownArrow))
                animator.SetBool("DUCK", true);
            else
                animator.SetBool("DUCK", false);

            if (Input.GetKeyDown(KeyCode.UpArrow))
                animator.SetTrigger("JUMP");

            if (Input.GetKeyDown(KeyCode.Keypad1))
                animator.SetTrigger("PUNCH");

            if (Input.GetKeyDown(KeyCode.Keypad2))
                animator.SetTrigger("KICK");

            if (Input.GetKey(KeyCode.Keypad0))
                animator.SetBool("DEFEND", true);
            else
                animator.SetBool("DEFEND", false);
        }
    }

    public void UpdateBotInput(float distance)
    {
        if (player.ToString() == "EASYBOT")
        {
            rand = Random.Range(0, 4);
            if (distance >= 1.3)
            {
                animator.SetBool("DEFEND", false);
                animator.SetBool("WALK", true);
            }
            else
            {
                animator.SetBool("WALK", false);
                animator.SetBool("DEFEND", false);
                if (rand == 1)
                    animator.SetTrigger("KICK");
                else if (rand == 2)
                    animator.SetTrigger("PUNCH");
                else if (rand == 3)
                    animator.SetBool("DEFEND", true);
            }
        }

        else if (player.ToString() == "MEDIUMBOT")
        {
            if (distance >= 1.3)
            {
                rand = Random.Range(0, 4);
                animator.SetBool("DUCK", false);
                if (rand <= 1)
                {
                    animator.SetBool("DEFEND", false);
                    animator.SetBool("WALK", true);
                }
                else
                {
                    animator.SetBool("DEFEND", true);
                    animator.SetBool("WALK", false);
                }
            }

            else
            {
                rand = Random.Range(0, 5);
                animator.SetBool("WALK", false);
                animator.SetBool("DEFEND", false);
                animator.SetBool("DUCK", false);
                if (rand == 1 || rand == 0)
                    animator.SetTrigger("KICK");
                else if (rand == 2)
                {
                    animator.SetTrigger("PUNCH");
                    animator.SetTrigger("PUNCH");
                    animator.SetTrigger("PUNCH");
                    animator.SetTrigger("PUNCH");
                }
                else if (rand == 3)
                    animator.SetBool("DEFEND", true);
                else
                    animator.SetBool("DUCK", true);
            }
        }
    }

    public bool invulnerable
    {
        get
        {
            return currentState == FighterStates.TAKE_HIT || currentState == FighterStates.TAKE_HIT_DEFEND
                || currentState == FighterStates.DEAD;
        }
    }

    public bool attacking
    {
        get
        {
            return currentState == FighterStates.ATTACK;
        }
    }

    public virtual void hurt(float damage)
    {
        if(!invulnerable)
        {
            if (currentState == FighterStates.DEFEND || currentState == FighterStates.TAKE_HIT_DEFEND)
                damage /= 10;
            if (health >= damage / defenseMultiplier)
            {
                health -= damage / defenseMultiplier;

            }
            else
            {
                health = 0;
            }
            if (health_UI != null)
            {
                health_UI.fillAmount = health / 100f;
            }
            if (health > 0)
            {
                animator.SetTrigger("TAKE_HIT");
            }
        }
        if(invulnerable && currentState != FighterStates.DEAD)
        {
            currentState = FighterStates.IDLE;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == PlayerType.HUMAN1)
        {
            UpdateHumanInput();
        }
        if (player == PlayerType.HUMAN2)
        {
            UpdateHumanInput();
        }
        if (Time.time > nextActionTime && player == PlayerType.EASYBOT || Time.time > nextActionTime && player == PlayerType.MEDIUMBOT )
        {
            nextActionTime += period;
            distance_x = player2.position.x - player1.position.x;
            UpdateBotInput(distance_x);
        }

        if(health <= 0 && currentState != FighterStates.DEAD)
        {
            animator.SetTrigger("DEAD");
        }
    }

    public void playSound(AudioClip sound)
    {
        GameUtils.playSound(sound, audioPlayer);
    }

    public Rigidbody body
    {
        get
        {
            return this.myBody;
        }
    }
}
