using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public int totalPoints, levelActualStars, levelActualPoints, levelActualSeconds;
    private int levelActual;
    bool reset;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("TotalPoints")) totalPoints = PlayerPrefs.GetInt("TotalPoints");
        levelActual = SceneManager.GetActiveScene().buildIndex;
       
    }

    public void SaveMusic(int p)
    {
        PlayerPrefs.SetInt("Music" , p);
    }
    public void SaveSFX(int p)
    {
        PlayerPrefs.SetInt("SFX", p);
    }

    public void Save(int stars, int seconds)
    {
        int thisLevelPoints = 0;
        if(PlayerPrefs.HasKey ("Level" + levelActual + "Points")) thisLevelPoints = PlayerPrefs.GetInt("Level" + levelActual + "Points");
        if (thisLevelPoints < (stars * 100) - seconds)
        {
            PlayerPrefs.SetInt("Stars" + levelActual, stars);
            PlayerPrefs.SetInt("Seconds" + levelActual, seconds);
            int points;
            totalPoints -= PlayerPrefs.GetInt("Level" + levelActual + "Points");
            points = (stars * 100) - seconds;
            PlayerPrefs.SetInt("Level" + levelActual + "Points", points);
            if (levelActual > PlayerPrefs.GetInt("LastLevelFinished"))
            {
                PlayerPrefs.SetInt("LastLevelFinished", levelActual);
            }
            totalPoints += PlayerPrefs.GetInt("Level" + levelActual + "Points");
            PlayerPrefs.SetInt("TotalPoints", totalPoints);
        }
        Debug.Log("Guardado en level actual: " + levelActual);
    }

    public void Load()
    {
        totalPoints = PlayerPrefs.GetInt("TotalPoints");
        LoadTheLevel(levelActual);
    }

    public void LoadTheLevel(int level)
    {
        levelActualStars = PlayerPrefs.GetInt("Stars" + level);
        levelActualSeconds = PlayerPrefs.GetInt("Seconds" + level);
        levelActualPoints = PlayerPrefs.GetInt("Level" + level + "Points");
        //Debug.Log("Cargado level " + level + " Stars: " + levelActualStars + " Seconds: " + levelActualSeconds);

    }

    
}


