using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ButtonOptions : MonoBehaviour
{

    public GameObject screenBlack, mainMenu, audioMenu;
    public GameObject t_music, t_sfx;
    private GoogleAdMob ads;
    private AudioController myAudio;
    private SaveLoad sl;
    private Propulsor prop;

    private void Awake()
    {
        if (!screenBlack.activeSelf) screenBlack.SetActive(true);
    }
    private void Start()
    {
        prop = GetComponent<Propulsor>();
        ads = GameObject.Find("Ads").GetComponent<GoogleAdMob>();
        myAudio = GameObject.Find("SFX").GetComponent<AudioController>();
        sl = GameObject.Find("Ads").GetComponent<SaveLoad>();

        if (PlayerPrefs.HasKey("Music") && PlayerPrefs.GetInt("Music") == 0)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().Stop();
            t_music.GetComponent<Toggle>().isOn = false;
        }
        if (PlayerPrefs.HasKey("SFX") && PlayerPrefs.GetInt("SFX") == 0)
        {
            GameObject.Find("SFX").GetComponent<AudioSource>().volume = 0;
            t_sfx.GetComponent<Toggle>().isOn = false;
        }
    }

    public void Restart()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void NextLevel()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        SceneManager.LoadScene(GetComponent<Propulsor>().sceneLevelSelect);

    }

    public void OcClickMusic()
    {
        if (!t_music.GetComponent<Toggle>().isOn) { GameObject.Find("Music").GetComponent<AudioSource>().Stop(); sl.SaveMusic(0); }
        else { GameObject.Find("Music").GetComponent<AudioSource>().Play(); sl.SaveMusic(1); }



    }

    public void OcClickSFX()
    { 
        if (!t_sfx.GetComponent<Toggle>().isOn) { GameObject.Find("SFX").GetComponent<AudioSource>().volume = 0; sl.SaveSFX(0); }
        else { GameObject.Find("SFX").GetComponent<AudioSource>().volume = 1; sl.SaveSFX(1); }
    }

    public void RestartWithPrecaution()    {
        
        if (GetComponent<Propulsor>().gas <= 0 && !GetComponent<Propulsor>().levelComplete)
        {
            if (Time.timeScale == 0) Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            playAudio(myAudio.button);
        } else
        {
            screenBlack.GetComponent<Animator>().SetBool("fadeIn", false);
        }
    }

    public void Check5Seconds()
    {
        if (GetComponent<Propulsor>().gas <= 0 && !GetComponent<Propulsor>().levelComplete)
        {
            GetComponent<Propulsor>().FadeIn("Restarting...", 2);
            Invoke("RestartWithPrecaution", 2);
            screenBlack.GetComponent<Animator>().SetBool("fadeIn", true);
        }
    }

    public void RestartActivate()
    {
        Invoke("Check5Seconds", 4);
    }
    public async void OnClickQuit()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        playAudio(myAudio.button);
        screenBlack.GetComponent<Animator>().SetBool("fadeIn", true);
        await Task.Delay(System.TimeSpan.FromSeconds(2));
        SceneManager.LoadScene(GetComponent<Propulsor>().sceneLevelSelect );
    }

    public void OnClickSettings()
    {
        Debug.Log("Settings");

        playAudio(myAudio.button);
        if (Time.timeScale == 1) { mainMenu.SetActive(true); Time.timeScale = 0; }
        else { mainMenu.SetActive(false); Time.timeScale = 1; }
    }

    public void OnClickAudio()
    {
        playAudio(myAudio.button);
        mainMenu .SetActive(false);
        audioMenu.SetActive(true);
    }

    public void OnClickBack()
    {
        playAudio(myAudio.button);
        audioMenu.SetActive(false);
        mainMenu.SetActive(true);        
    }

    public void OnClickReload()
    {
        //Debug.Log("Reload");       
        playAudio(myAudio.button);
       // ads.ShowAdRewarded(); //Anuncios rewarded, lo quito porque es muy largo
        ads.ShowInterstitial();
        ads.premio = true;
        Premio();
        
    }

    public void Premio()
    {
        if (ads.premio)
        {
            prop.gas = 0.4f;
            prop.gasBar.size = prop.gas;
            prop.text_Reload.gameObject.SetActive(false);
            prop.text_Restart.gameObject.SetActive(false);
            GetComponent<MobileControls>().isReloaded = true;
            prop.Go();
            ads.premio = false;
        } 
    }


    public void OnClickRestart()
    {
        //Debug.Log("Restart");
       // if (Time.timeScale == 0) Time.timeScale = 1;        
        playAudio(myAudio.button);
        if (Time.timeScale == 0) Time.timeScale = 1;
        screenBlack.GetComponent<Animator>().SetBool("fadeIn", true);
        Invoke("Restart", 2);
        
    }
    
    public void OnClickNext()
    {
        Invoke("NextLevel", 2);
        playAudio(myAudio.button);
        screenBlack.GetComponent<Animator>().SetBool("fadeIn", true);
        //Debug.Log("Next");
    }

    private void playAudio(AudioClip clip, int pitch = 1)
    {
        myAudio.audioFx.loop = false;
        myAudio.audioFx.clip = clip;
        myAudio.audioFx.pitch = pitch;
        myAudio.audioFx.Play();
    }

    public void OnMouseDown()
    {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
            //Debug.Log(hit.collider.gameObject.name);
                // whatever tag you are looking for on your game object
                if (hit.collider.gameObject.name == "Text_Restart")
                {
                OnClickRestart();

                } else if (hit.collider.gameObject.name == "Text_Next")
                {
                OnClickNext();
                }
            else if (hit.collider.gameObject.name == "Text_Reload")
            {
                OnClickReload();
            }
            else if (hit.collider.gameObject.name == "Text_Exit")
            {
                Debug.Log("Exit");
            }

        }
    }
}
