using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject PvPmenu;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private Text playerName1, playerName2, mapName, points, strengthPoints, hitPoints;
    private int index1, index2, indexM;
    private string[] charEnum = { "Ryu", "Ken", "Soldier", "Titan" };
    //Maps are unnamed so far, we'll likely name them later. If you're one of our small team of devs, feel free to name them yourself!
    
    public void PlayPLAYERvPLAYER()
    {

        PvPmenu.SetActive(true);
        gameObject.SetActive(false);

        
        //SceneManager.LoadScene(1);
    }

    public void Cycle1Left()
    {
        index1--;
        if (index1 < 0)
            index1 = charEnum.Length - 1;
        playerName1.text = charEnum[index1];

    }
    public void Cycle1Right()
    {
        index1++;
        if (index1 >= charEnum.Length)
            index1 = 0;
        playerName1.text = charEnum[index1];

    }
    public void Cycle2Left()
    {
        index2--;
        if (index2 < 0)
            index2 = charEnum.Length - 1;
        playerName2.text = charEnum[index2];
    }
    public void Cycle2Right()
    {
        index2++;
        if (index2 >= charEnum.Length)
            index2 = 0;
        playerName2.text = charEnum[index2];
    }
    public void CycleMLeft()
    {
        indexM--;
        if (indexM < 0)
            indexM = 2;
        mapName.text = (indexM + 1).ToString();
    }
    public void CycleMRight()
    {
        indexM++;
        if (indexM >= 3)
            indexM = 0;
        mapName.text = (indexM + 1).ToString();
    }
    public void Confirm()
    {
        PlayerPrefs.SetInt("Character1", index1);
        PlayerPrefs.SetInt("Character2", index2);
        SceneManager.LoadScene(indexM + 1);
    }
    public void Back()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void PlayPLAYERvBOT()
    {

        SceneManager.LoadScene(4);
    }

    public void Start()
    {
        points.text = "Points: " + PlayerPrefs.GetInt("UpgradePoints").ToString();
        strengthPoints.text = PlayerPrefs.GetInt("Strength").ToString();
        hitPoints.text = PlayerPrefs.GetInt("Hitpoints").ToString();

    }
    public void AddStrength()
    {
        if (PlayerPrefs.GetInt("UpgradePoints") > 0)
        {
            PlayerPrefs.SetInt("UpgradePoints", (PlayerPrefs.GetInt("UpgradePoints") - 1));
            PlayerPrefs.SetInt("Strength", (PlayerPrefs.GetInt("Strength") + 1));
            strengthPoints.text = PlayerPrefs.GetInt("Strength").ToString();
            points.text = "Points: " + PlayerPrefs.GetInt("UpgradePoints").ToString();
        }
    }
    public void RemoveStrength()
    {
        if (PlayerPrefs.GetInt("Strength") > 0)
        {
            PlayerPrefs.SetInt("UpgradePoints", (PlayerPrefs.GetInt("UpgradePoints") + 1));
            PlayerPrefs.SetInt("Strength", (PlayerPrefs.GetInt("Strength") - 1));
            strengthPoints.text = PlayerPrefs.GetInt("Strength").ToString();
            points.text = "Points: " + PlayerPrefs.GetInt("UpgradePoints").ToString();
        }
    }
    public void AddHitpoints()
    {
        if (PlayerPrefs.GetInt("UpgradePoints") > 0)
        {
            PlayerPrefs.SetInt("UpgradePoints", (PlayerPrefs.GetInt("UpgradePoints") - 1));
            PlayerPrefs.SetInt("Hitpoints", (PlayerPrefs.GetInt("Hitpoints") + 1));
            hitPoints.text = PlayerPrefs.GetInt("Hitpoints").ToString();
            points.text = "Points: " + PlayerPrefs.GetInt("UpgradePoints").ToString();
        }
    }
    public void RemoveHitpoints()
    {
        if (PlayerPrefs.GetInt("Hitpoints") > 0)
        {
            PlayerPrefs.SetInt("UpgradePoints", (PlayerPrefs.GetInt("UpgradePoints") + 1));
            PlayerPrefs.SetInt("Hitpoints", (PlayerPrefs.GetInt("Hitpoints") - 1));
            hitPoints.text = PlayerPrefs.GetInt("Hitpoints").ToString();
            points.text = "Points: " + PlayerPrefs.GetInt("UpgradePoints").ToString();
        }
    }
}
