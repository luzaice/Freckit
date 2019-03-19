using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public enum PlayerType
    {
        HUMAN, AI
    };

    public static float MAX_HEALTH = 100f;

    public float health = MAX_HEALTH;
    public string fighterName;
    public PlayerType player;
    public FighterStates currentState = FighterStates.IDLE;

    protected Animator animator;
    private Rigidbody myBody;
    public Image health_UI;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void UpdateHumanInput()
    {
        if (Input.GetAxis("Horizontal") > 0.1)
            animator.SetBool("WALK", true);
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
            if (health >= damage)
            {
                health -= damage;
                if (health_UI != null)
                {
                    health_UI.fillAmount = health / 100f;
                }

            }
            else
                health = 0;
            if (health > 0)
            {
                animator.SetTrigger("TAKE_HIT");
            }
        }
        if(invulnerable)
        {
            currentState = FighterStates.IDLE;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == PlayerType.HUMAN)
        {
            UpdateHumanInput();
        }

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
