using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro.Examples;

public class Portada : MonoBehaviour
{
    private CameraController cam;
    public Animator screenBlack;
    public Transform targetFinal;
    private AudioController myAudio;
    private bool privacyPolicyOK, twoSeconds;
    public GameObject panelPrivacyPolicy;
    public GameObject contentES, contentEN;

    void Awake()
    {
       
        if (!screenBlack.gameObject.activeSelf) screenBlack.gameObject.SetActive(true);
        myAudio = GameObject.Find("SFX").GetComponent<AudioController>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    private void Start()
    {

        if (Application.systemLanguage == SystemLanguage.Spanish)
        {
            contentEN.SetActive(false);
            contentES.SetActive(true);
        }

        
        //PlayerPrefs.SetInt("PrivacyPolicy", 0);  //for Debug
        if (PlayerPrefs.HasKey("PrivacyPolicy"))
        {
            if (PlayerPrefs.GetInt("PrivacyPolicy") == 1)
            {
                privacyPolicyOK = true;
                panelPrivacyPolicy.SetActive(false);
                cam.enabled = true;
                Invoke("TwoSecondsTrue", 3);

            }
        }
        /*
        firstTimePrivacyPolicyTouchInt = 0;
        if (!privacyPolicyOK) firstTimePrivacyPolicyTouchInt = 1;
        */
    }
    private void setContent(GameObject content)
    {
        content.SetActive(true);
    }

    private void TwoSecondsTrue()
    {
        twoSeconds = true;
    }

    public async void  OnClickPortada()
    {
        cam.CameraTarget = targetFinal;
        myAudio.audioFx.clip = myAudio.levelComplete;        
        myAudio.audioFx.Play();
        screenBlack.SetBool("fadeIn", true);
        await Task.Delay(System.TimeSpan.FromSeconds(2f));
        if (Time.timeScale == 0) Time.timeScale = 1;
        SceneManager.LoadScene("SelectLevel");
    }

    void Update()
    {
       
        if (privacyPolicyOK && twoSeconds)
        {
           
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                OnClickPortada();
                return;
            }
        }
    }

    public void OnClickAccept()
    {
        panelPrivacyPolicy.SetActive(false);
        print("Accept");
        privacyPolicyOK = true;
        PlayerPrefs.SetInt("PrivacyPolicy", 1);
        cam.enabled = true;
        Invoke("TwoSecondsTrue", 3);
    }

    public void OnClickExit()
    {
        Application.Quit();
        print("Exit");
    }

}
