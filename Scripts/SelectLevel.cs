using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class SelectLevel : MonoBehaviour
{
    //ojo El grupo 8 tiene el text de las estrellas transparente mientras sea el último nivel.
    public GameObject[] levels;
    public GameObject[] groups;
    public GameObject[] panels;
    public GameObject[] buttonsNext;
    public Animator animCube, screenBlack;
    private bool[] textNow, levelIsPlayable;
    public int[] starsNecessary;
    private int gruposActivos, levelsActivos;
    private SaveLoad sl;
    private int totalStarsNecessary;
    private AudioController myAudio;
    private bool[] bNextActive;
    private int panelActivo;
   // private RewardedAdsScript ads;

    private void Awake()
    {
        if (!screenBlack.gameObject.activeSelf) screenBlack.gameObject.SetActive(true);
    }
    void Start()
    {
        if(PlayerPrefs.HasKey("Music") && PlayerPrefs.GetInt ("Music")==0)
        {            
            GameObject.Find("Music").GetComponent<AudioSource>().Stop();
        }
        if (PlayerPrefs.HasKey("SFX") && PlayerPrefs.GetInt("SFX") == 0)
        {
            GameObject.Find("SFX").GetComponent<AudioSource>().volume = 0;
        }
        //ads = GameObject.Find("Ads").GetComponent<RewardedAdsScript>();
        //ads.ShowBanner();
        panelActivo = 0;
        bNextActive = new bool[panels.Length];

        myAudio = GameObject.Find("SFX").GetComponent<AudioController>();
        myAudio.audioFx.pitch = 2;
        gruposActivos = 0;
        sl = GetComponent<SaveLoad>();
        levelsActivos = 0;
        SelectTheLevel();
        SelectTheGroup();
        //Debug.Log("Total stars: "+GetTotalStars());
        Debug.Log("Grupos activos: " + gruposActivos);
        if (gruposActivos % 5 == 0 )
        {
            bNextActive[panelActivo] = true;
            print("bNextActive"+panelActivo );
        }
        if (gruposActivos >= 5)
        {
            OnClickButtonNext(1);
        }
        
        
        totalStarsNecessary = 0;
        for( int i=0; i<gruposActivos+1;i++) 
        {
            totalStarsNecessary += starsNecessary[i];
            Debug.Log("Total stars necessary: "+totalStarsNecessary);

        }


        Debug.Log("Resta: " + (totalStarsNecessary - GetTotalStars()));

        // Si el nivel 3 del grupoActivo esta activo, el texto de las estrellas se enciende
        Button[] but = groups[gruposActivos-1].GetComponentsInChildren<Button>();
        if(but.Length > 2 && levelsActivos >=3*gruposActivos)
        {
            groups[gruposActivos-1].GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(true);
            int starsResta =  totalStarsNecessary - GetTotalStars();
            Debug.Log("Resta: " + starsResta);
            groups[gruposActivos - 1].GetComponentInChildren<TextMeshProUGUI>().text = "Need " + starsResta + " stars to the next level";
            
        } else
        {
            groups[gruposActivos-1].GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
            if (bNextActive[panelActivo]) buttonsNext[panelActivo].SetActive(true);
        }




    }

   

    //recorro los grupos y desactivo los no jugables segun las estrellas que necesitan
    private void SelectTheGroup() 
    {
        int tot = 0;
        for (int i = 0; i < groups.Length; i++)
        {
            
            tot += starsNecessary[i];
           // Debug.Log("Tot: " + tot);

            if (GetTotalStars() < tot)
            {
                groups[i].SetActive(false);
               // Debug.Log("Grupo["+groups[i]+"] active false");
            }
            else
            {
                gruposActivos += 1;
                if (i > 0) groups[i - 1].GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false); // Desactivo el texto de 'Necesitas tantas estrellas'
            }

            

        }
    }

    //recorro los niveles y desactivo los que aun no se pueden jugar
    private void SelectTheLevel() 
    {
        
        for (int i = 1; i < levels.Length+1; i++) 
        {
            sl.LoadTheLevel(i); //Cargamos el nivel numero i
            //if (levelsActivos == 0) levelsActivos = 1;
            if (sl.levelActualPoints == 0) //Si los levelActualPoints son 0 es que el nivel no se ha jugado
            {
                if (levels.Length > i )
                {
                    levels[i ].SetActive(false);
                }

            }
            else //Si los levelActualPoints son mayores que cero levelsActivos incrementa
            {
                levelsActivos += 1;
                //Debug.Log("LevelsActivos: " + levelsActivos);
                //Debug.Log(levels[i].gameObject.name);
            }
            
            
            Image[] starsImages = levels[i-1].GetComponentsInChildren<Image>();
           // Debug.Log("Nivel: " + levels[i -1] + " Stars: " + sl.levelActualStars);
            for (int e = 0; e < sl.levelActualStars; e++)
            {
                starsImages[e].color = Color.yellow;
            }
        }
    }

    //Devuelve el total de estrellas conseguidas
    private int GetTotalStars() 
    {
        int total = 0;
        for (int i = 0; i < levels.Length ; i++)
        {
            sl.LoadTheLevel(i);
            total += sl.levelActualStars; ;
        }
        return total;
    }

    /*
    async void Awake()
    {
        await DoSomethingAsync();
        DoSomethingElse();
    }
    */

    // Funciones OnClick para botones //
    public void OnClickPrivacyPolicy()
    {
        Application.OpenURL("https://www.artsfactory.es/privacy-policy.html");
    }
    public void OnClickButtonNext(int i)
    {
        myAudio.audioFx.clip = myAudio.button;
        myAudio.audioFx.Play();
        panels[i].SetActive(true);
        panels[i - 1].SetActive(false);
        panelActivo = i;
        if (gruposActivos % (5*(i+1)) == 0)
        {
            bNextActive[panelActivo] = true;
            print("bNextActive"+i);
        }
    }

    public void OnClickButtonBack(int i)
    {
        myAudio.audioFx.clip = myAudio.button;
        myAudio.audioFx.Play();
        panels[i].SetActive(true);
        panels[i+1].SetActive(false);
        panelActivo = i;
        if (gruposActivos >= (5 * (i + 1)) )
        {
            buttonsNext [panelActivo].SetActive (true);
            print("bNextActive"+i);
        }
    }

    public void OnClickTutorial()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public async void OnClickLevel(int scene) //OJO Using System.task puede no ser multidipositivo
    {
        myAudio.audioFx.clip = myAudio.button;
        myAudio.audioFx.Play();
        animCube.enabled = true;
        screenBlack.SetBool("fadeIn", true);
        await Task.Delay(System.TimeSpan.FromSeconds(2f));
        if (Time.timeScale == 0) Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public async void OnClickLevel(string scene) //OJO Using System.task puede no ser multidipositivo
    {
        myAudio.audioFx.clip = myAudio.button;
        myAudio.audioFx.Play();
        animCube.enabled = true;
        screenBlack.SetBool("fadeIn", true);
        await Task.Delay(System.TimeSpan.FromSeconds(2f));
        if (Time.timeScale == 0) Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
    public void  OnClickQuit()
    {
        
        Application.Quit();
    }
    int countResetClicks = 0;

    public void OnClickResetPoints()
    {
        myAudio.audioFx.clip = myAudio.button;
        myAudio.audioFx.Play();
        countResetClicks += 1;
        if(countResetClicks>5 )
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
