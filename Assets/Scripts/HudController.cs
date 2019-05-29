using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudController : MonoBehaviour
{
    const string imagePath = "UI/Character";
    public Fighter player1;
    public Fighter player2;
    public Transform characterList1;
    public Transform characterList2;
    public Image characterImage1, characterImage2;
    public Text leftText;
    public Text rightText;
    public int roundTime = 99;
    public Text timer = null;
    private float lastTimeUpdate = 0;
    public static int fighterOneWins = 0;
    public static int fighterTwoWins = 0;
    private int currentScene; 
    protected Animator animator;

    public BattleController battle;

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
            characterImage1.sprite = Resources.Load<Sprite>(imagePath + (PlayerPrefs.GetInt("Character1") + 1).ToString() + "L") as Sprite;
            characterImage2.sprite = Resources.Load<Sprite>(imagePath + (PlayerPrefs.GetInt("Character2") + 1).ToString() + "R") as Sprite;
        }
        leftText.text = player1.fighterName;
        rightText.text = player2.fighterName;
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene >= 4) {
            player1.health = player1.health + (PlayerPrefs.GetInt("Hitpoints") * 5);
            player1.maxHealth = player1.maxHealth + (PlayerPrefs.GetInt("Hitpoints") * 5);
            player1.attackMultiplier = player1.attackMultiplier + ((float)PlayerPrefs.GetInt("Strength") / 20);
                }

        if (fighterOneWins + fighterTwoWins == 0)
            animator.SetTrigger("RoundOne");
        else if (fighterOneWins + fighterTwoWins == 1)
            animator.SetTrigger("RoundTwo");
        else
            animator.SetTrigger("FinalRound");
    }

    IEnumerator PlayerOneWin()
    {
        animator.SetTrigger("YouWin");
        yield return new WaitForSeconds(3.0f);
        if (player2.health <= 0)
        {
            fighterOneWins++;
            if (fighterOneWins == 2)
            {
                fighterTwoWins = 0;
                fighterOneWins = 0;
                if (currentScene == 4)
                {
                    PlayerPrefs.SetInt("UpgradePoints", (PlayerPrefs.GetInt("UpgradePoints") + 1));
                    SceneManager.LoadScene(5);
                }
                else if (currentScene == 5)
                {
                    PlayerPrefs.SetInt("UpgradePoints", (PlayerPrefs.GetInt("UpgradePoints") + 1));
                    SceneManager.LoadScene(6);

                }
                else if (currentScene == 6)
                {
                    PlayerPrefs.SetInt("UpgradePoints", (PlayerPrefs.GetInt("UpgradePoints") + 2));
                    SceneManager.LoadScene(0);

                }
                else
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(currentScene);
            }
        }
    }

    IEnumerator PlayerTwoWin()
    {
        animator.SetTrigger("YouLose");
        yield return new WaitForSeconds(3.0f);
        if (player1.health <= 0)
        {
            fighterTwoWins++;
            Debug.Log(fighterTwoWins);
            if (fighterTwoWins == 2)
            {
                fighterTwoWins = 0;
                fighterOneWins = 0;
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(currentScene);
            }
        }
    }
    
    //End1 is called when a player won a match, the scene is set to the main menu
    IEnumerator End1()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(0);
    }

    //End2 is called when a player won a round, but not the whole match
    IEnumerator End2()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(currentScene);
    }

    //End3 is clled when a player won a match against an easy bot
    IEnumerator End3()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(5);
    }

    //End4 is called when a player won a match again a medium bot
    IEnumerator End4()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(6);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(roundTime);
        //Debug.Log("AAAAAAAAAAa");
        if (roundTime >= 0 && Time.time - lastTimeUpdate > 1)
        {
            roundTime--;
            timer.text = roundTime.ToString();
            lastTimeUpdate = Time.time;
            if (roundTime == 0)
            {
                if (player1.health < player2.health)
                {
                    StartCoroutine(PlayerTwoWin());
                    /*fighterTwoWins++;
                    if (fighterTwoWins == 2)
                    {
                        fighterTwoWins = 0;
                        fighterOneWins = 0;
                        StartCoroutine(End1());
                    }
                    else
                    {
                        StartCoroutine(End2());
                    }*/
                }

                else if (player2.health < player1.health)
                {
                    StartCoroutine(PlayerOneWin());
                    /*fighterOneWins++;
                    if (fighterOneWins == 2)
                    {
                        fighterTwoWins = 0;
                        fighterOneWins = 0;
                        if (currentScene == 2)
                            StartCoroutine(End3());
                        else
                            StartCoroutine(End1());
                    }
                    else
                    {
                        StartCoroutine(End2());
                    }*/
                }
            }

            if (player1.health <= 0)
                StartCoroutine(PlayerTwoWin());

            if (player2.health <= 0)
                StartCoroutine(PlayerOneWin());
        }
    }
}
