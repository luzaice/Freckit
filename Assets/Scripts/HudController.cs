using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudController : MonoBehaviour
{
    public Fighter player1;
    public Fighter player2;

    public Text leftText;
    public Text rightText;
    public int roundTime = 99;
    public Text timer = null;
    private float lastTimeUpdate = 0;
    public static int fighterOneWins = 0;
    public static int fighterTwoWins = 0;

    public BattleController battle;

    // Start is called before the first frame update
    void Start()
    {
        leftText.text = player1.fighterName;
        rightText.text = player2.fighterName;
    }

    // Update is called once per frame
    void Update()
    {
        if (roundTime > 0 && Time.time - lastTimeUpdate > 1)
        {
            roundTime--;
            timer.text = roundTime.ToString();
            lastTimeUpdate = Time.time;
            if (roundTime == 0)
            {
                if (player1.health < player2.health)
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
                        SceneManager.LoadScene(1);
                }

                else if (player2.health < player1.health)
                {
                    fighterOneWins++;
                    Debug.Log(fighterTwoWins);
                    if (fighterOneWins == 2)
                    {
                        fighterTwoWins = 0;
                        fighterOneWins = 0;
                        SceneManager.LoadScene(0);
                    }
                    else
                        SceneManager.LoadScene(1);
                }
            }
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
                SceneManager.LoadScene(1);
            }

            if (player2.health <= 0)
            {
                fighterOneWins++;
                if (fighterOneWins == 2)
                {
                    fighterTwoWins = 0;
                    fighterOneWins = 0;
                    SceneManager.LoadScene(0);
                }
                else
                SceneManager.LoadScene(1);
            }
        }

        if (roundTime == 0)
        {
            /*if (player1.health < player2.health)
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
                SceneManager.LoadScene(1);
            }

            else  if (player2.health < player1.health)
            {
                fighterOneWins++;
                Debug.Log(fighterTwoWins);
                if (fighterOneWins == 2)
                {
                    fighterTwoWins = 0;
                    fighterOneWins = 0;
                    SceneManager.LoadScene(0);
                }
                else
                    SceneManager.LoadScene(1);
            }*/
        }
    }
}
