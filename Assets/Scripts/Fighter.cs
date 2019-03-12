using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public enum PlayerType
    {
        HUMAN, AI
    };

    public string fighterName;
    public PlayerType player;
    public FighterStates currentState = FighterStates.IDLE;

    protected Animator animator;
    private Rigidbody myBody;

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

    // Update is called once per frame
    void Update()
    {
        if (player == PlayerType.HUMAN)
        {
            UpdateHumanInput();
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
