using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;
using System.Threading.Tasks;

public class Propulsor : MonoBehaviour
{
    public bool isTutorial;
    private Transform myTransform;
    private Rigidbody  rig;
    public int goodTime;
    public float velocity;
    public float gas;
    public Scrollbar gasBar;
    public bool upIsActive,leftIsActive, rightIsActive, gasActive, forwardIsActive;
    public bool yellowKey, redKey, blueKey;
    public Material yellowMaterial, redMaterial, blueMaterial;
    public ParticleSystem humoLeft, humoRight, humoDown;
    public GameObject SmokeEffect;
    private GameObject[] yellowDoors, redDoors, blueDoors;
    private GameObject textCenter, textUnderCenter;
    public TextMeshProUGUI textTime;
    //public TextMeshPro Text_Restart, Text_Next, Text_Reload, Text_Exit;
    public TextMeshProUGUI text_Restart, text_Next, text_Reload, text_Inicio; 
    public GameObject stars;
    public SpriteRenderer star1, star2, star3;
    public bool levelComplete, levelStart;
    public SaveLoad saveLoad;
    private int gasesAtStart, gasesAtEnd;
    private CameraController  cam;
    public string sceneLevelSelect;
    private AudioController myAudio;
    private AudioSource cubeSounds;
    private TextMeshPro textLevel;
    private Renderer rend;
    private Animator animatorThisGO;



    //public GameObject[] mobileControls;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        animatorThisGO = GetComponent<Animator>();
        velocity = 1000;
        if (!isTutorial) 
        {
            textLevel = GameObject.Find("Text_Level").GetComponent<TextMeshPro >();
            textLevel.text = "Level "+SceneManager.GetActiveScene().buildIndex;
        }
        myAudio = GameObject.Find("SFX").GetComponent<AudioController>();
        cubeSounds = GameObject.Find("CubeSounds").GetComponent<AudioSource >();

        /*
        if (PlayerPrefs.HasKey("Music") && PlayerPrefs.GetInt("Music") == 0)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().Stop();

        }
        */
            sceneLevelSelect = "SelectLevel";
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        cam = Camera.main.gameObject.GetComponent<CameraController>();
        gasBar.size = gas;
        myTransform = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        humoDown.Stop();
        humoLeft.Stop();
        humoRight.Stop();
        yellowKey = false;
        blueKey = false;
        redKey = false;
        yellowDoors = GameObject.FindGameObjectsWithTag("Amarillo");
        redDoors = GameObject.FindGameObjectsWithTag("Rojo");
        blueDoors = GameObject.FindGameObjectsWithTag("Azul");
        textCenter = GameObject.Find("Text_CenterScreen");
        textUnderCenter = GameObject.Find("Text_UnderCenterScreen");
        text_Restart = GameObject.Find("TextRestart").GetComponent<TextMeshProUGUI>();
        text_Restart.gameObject.SetActive(false);
        text_Next  = GameObject.Find("TextNext").GetComponent<TextMeshProUGUI>();
        text_Next.gameObject.SetActive(false);
        text_Reload  = GameObject.Find("TextReload").GetComponent<TextMeshProUGUI>();
        text_Reload.gameObject.SetActive(false);
        text_Inicio = GameObject.Find("TextInicio").GetComponent<TextMeshProUGUI>();
        gasesAtStart = GameObject.FindGameObjectsWithTag("Gas").Length;
        gasesAtEnd = gasesAtStart;
        //Debug.Log(calcPercent(gasesAtEnd));
        textTime.text = "0";
        if(!isTutorial) Invoke("StartGame", 5);
        GameObject[] sp = GameObject.FindGameObjectsWithTag("Gas");
        text_Inicio.text = "Total Gas <color=\"yellow\">"+sp.Length + "</color>\n"+ "Nice Time <color=\"yellow\">" + goodTime;
        
        borrarTextoInicio();
        Instantiate(SmokeEffect, gameObject.transform.position, Quaternion.identity);

    }

    async void borrarTextoInicio()
    {
        await Task.Delay(System.TimeSpan.FromSeconds(8));
        text_Inicio.gameObject.SetActive(false);
        text_Inicio.text = "Restart";
    }
    public void StartGame()
    {
        cam.CameraTarget = gameObject.transform;
        cam.MovementSmoothingValue = 20;
        
        FadeIn("Ready",3);
        Invoke("Go", 4);
    }
    private void playAudio(AudioClip clip, int pitch = 1)
    {
        myAudio.audioFx.loop = false;
        myAudio.audioFx.clip = clip;
        myAudio.audioFx.pitch = pitch;
        myAudio.audioFx.Play();
    }

    void Update() //OOJJO con esto looco, ponlo update si te pasaste
    {
         
       
         if(!gasActive )
        {
           cubeSounds.Stop();
        }

        if (!levelComplete && levelStart)
        {
            
            float time = 0;
            time = time + 1 * Time.deltaTime;
            if (!isTutorial) textTime.text = (int)Time.timeSinceLevelLoad - 9 + "";
            else
            {
                textTime.text = (int)Time.timeSinceLevelLoad - 53 + "";
            }



            if (gas > 0) { ControlBoxBools(); } //Si no hay gas no se puede mover
        else
        {
                gasActive = false;
                humoDown.Stop();
            humoRight.Stop();
            humoLeft.Stop();
            if (SimpleInput.GetAxis("Horizontal") > 0.8f || SimpleInput.GetAxis("Horizontal") < -0.8f || SimpleInput.GetAxis("Vertical") > 0.8f)
            {
                    if (gas <= 0)
                    {
                        if (!isTutorial) FadeIn2("Low fuel");
                        else gas = 0.1f;
                    }

            }

            //Debug.Log("Ya no hay gas");
        }
        }

    }

    private void LateUpdate()
    {
       if(levelComplete )
        {
            text_Reload.gameObject.SetActive(false);                
        }
    }
    public void Go()
    {        
        playAudio(myAudio.go);
        cam.MovementSmoothingValue = 0;
        FadeIn("GO!", 0.5f);
        levelStart = true;
    }
    private void FadeOut()
    {
        Animator anim = textCenter.GetComponent<Animator>();
        if(anim.GetFloat ("Direction")==1)
        {
            anim.SetFloat("Direction", -1);
            anim.Play("Anim_TextCenterFadeIn", 0, 1);
        }       
        //Debug.Log("Fade Out");
    }
    private void FadeOut2()
    {
        Animator anim = textUnderCenter.GetComponent<Animator>();
        if (anim.GetFloat("Direction") == 1)
        {
            anim.SetFloat("Direction", -1);
            anim.Play("Anim_TextCenterFadeIn", 0, 1);
        }
        //Debug.Log("Fade Out");
    }

    public void FadeIn(string text, float time=2)
    {
        if (text == "Ready")
        {
            
            playAudio(myAudio.ready);
        }
        if (text == "Level Complete" || text == "Tutorial Complete")
        {
            playAudio(myAudio.levelComplete);
        }


        textCenter.GetComponent<TextMeshPro>().text = text;
        Animator anim = textCenter.GetComponent<Animator>();
        if (anim.GetFloat("Direction") != 1)
        {
            anim.SetFloat("Direction", 1);
            anim.Play("Anim_TextCenterFadeIn", 0, 0);
        }
       // Debug.Log("Fade In");
        Invoke("FadeOut", time);
    }
    public void FadeIn2(string text, float time = 2)
    {
        textUnderCenter .GetComponent<TextMeshPro>().text = text;
        Animator anim = textUnderCenter.GetComponent<Animator>();
        if (anim.GetFloat("Direction") != 1)
        {
            anim.SetFloat("Direction", 1);
            anim.Play("Anim_TextCenterFadeIn", 0, 0);
        }
        // Debug.Log("Fade In");
        Invoke("FadeOut2", time);
    }

    private float calcPercent(float num)
    {
        return num / gasesAtStart* 100;
    }
   private void CameraNormal() {
       cam.CameraMode = CameraController.CameraModes.Isometric;
       cam.CameraTarget= gameObject.transform;
       
       Invoke("CamSmoothingCero",0.2f);
   }

   private void CamSmoothingCero() {
        cam.MovementSmoothingValue = 0;
   }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains ("Teleport")) 
        {
            //cam.FollowDistance = 40;
            //cam.CameraTarget = other.gameObject.transform;
            cam.MovementSmoothingValue = 5;
                    
            string[] t = other.name.Split ('.'); //Si entras en Teleport.1 t lleva a Releport.1
            playAudio(myAudio.levelComplete, 3);
            transform.position = GameObject.Find("Releport."+t[1]).transform.position ;            
            Invoke("CameraNormal", 0.1f);
            
            
               
        }
        if(other.CompareTag ("Gas"))
        {
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            playAudio(myAudio.getSphere);

            gasesAtEnd -= 1;
            Instantiate(SmokeEffect, gameObject.transform.position, Quaternion.identity);
            FadeOut();

            if (1 - gas >= 0.2f) gas = gas + 0.20f;
            else gas = gas + (1 - gas);

            if (other.name == "YellowKey")
            {
                yellowKey = true;
                ChangeCubeColor(yellowMaterial);
                redKey = false;
                blueKey = false;
                playAudio(myAudio.getKey);
            }

            if (other.name == "RedKey")
            {
                redKey = true;
                ChangeCubeColor(redMaterial );
                yellowKey = false;
                blueKey = false;
                playAudio(myAudio.getKey);
            }
            if (other.name == "BlueKey")
            {
                blueKey = true;
                ChangeCubeColor(blueMaterial );
                yellowKey = false;
                redKey = false;
                playAudio(myAudio.getKey);
            }
            gasBar.size = gas;
        }
    }

    private void ChangeCubeColor(Material material)
    {
        //Fetch the Renderer from the GameObject
        //Renderer rend = GetComponent<Renderer>();
        
        rend.material = material;
    }

    public void RestartNext()
    {
        text_Restart.gameObject.SetActive(true);
        text_Next.gameObject.SetActive(true);        
    }

    public void RestartReload()
    {
        text_Restart.gameObject.SetActive(true);
        text_Reload.gameObject.SetActive(true);
        
    }

    public void RestartExit()
    {
        //Text_Restart.gameObject.SetActive(true);
        //Text_Exit.gameObject.SetActive(true);
        GetComponent<ButtonOptions>().RestartActivate();
    }

    private void LoadSelectLevel()
    {
        SceneManager.LoadScene(sceneLevelSelect);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.collider.CompareTag("Amarillo") && yellowKey)
            || (collision.collider.CompareTag("Rojo") && redKey)
            || (collision.collider.CompareTag("Azul") && blueKey ))
        {            
                abrePuerta(collision.collider);
        }
        if (collision.collider.CompareTag("Final"))
        {
            animatorThisGO.enabled = true;
            text_Restart.gameObject.SetActive(false);
            text_Reload.gameObject.SetActive(false);
            if (!isTutorial) FadeIn("Level Complete", 4);
            else FadeIn("Tutorial Complete", 4);

            Invoke("RestartNext", 4);
            gasActive = false;
            int intStars = 1;
            int intTime = System.Convert.ToInt32(textTime.text);
            float floatPercent = (calcPercent(gasesAtEnd) - 100) * -1;
            if (intTime <= goodTime && floatPercent == 100)
            {
                intStars = 3;
                star1.color = Color.yellow;
                star2.color = Color.yellow;
                star3.color = Color.yellow;
            }
            else if (intTime <= goodTime + 10 && floatPercent >= 40)
            {
                intStars = 2;
                star1.color = Color.yellow;
                star2.color = Color.yellow;
            }
            else 
            {
                intStars = 1;
                star1.color = Color.yellow;
            }

            if (!isTutorial) stars.SetActive(true);
            levelComplete = true;
            humoDown.Stop();
            humoRight.Stop();
            humoLeft.Stop();

            if (!isTutorial) saveLoad.Save(intStars, System.Convert.ToInt32(textTime.text));
            if (!isTutorial) saveLoad.Load();

           
            Debug.Log("Final del nivel, has tardado " + textTime.text + " segundos, has recolectado el " + ((calcPercent(gasesAtEnd) - 100) * -1) +
                "% de los gases. Tienes " + intStars + " estrellas.");

            Debug.Log(saveLoad.levelActualSeconds + " segundos, " +  saveLoad.levelActualStars + " estrellas, " + saveLoad.levelActualPoints + " puntos. Total puntos: " + saveLoad.totalPoints);


        }
    }

    private async void abrePuerta(Collider go)
    {
        go.gameObject.GetComponent<SpawnEffect>().enabled = true;         
        go.enabled = false;
        playAudio(myAudio.openWall);
        await Task.Delay(System.TimeSpan.FromSeconds(2));
        go.gameObject.SetActive (false); //Destroy??¿
    }

    private void OpenDoors(string color)
    {
        if (color == "Amarillo")
        {
            foreach (GameObject y in yellowDoors)
            {
                Destroy(y);
            }
        }
        if (color == "Rojo")
        {
            foreach (GameObject y in redDoors)
            {
                Destroy(y);
            }
        }
        if (color == "Azul")
        {
            foreach (GameObject y in blueDoors)
            {
                Destroy(y);
            }
        }

    }


    private void ControlBoxBools()
    {


        if (forwardIsActive) //Para avanzar, paramos todo un momento y despues seguimos
        {
            rig.AddForce(Vector3.forward * (velocity) * Time.deltaTime);
            rig.constraints = RigidbodyConstraints.None;
           // return;
        }


        if (humoDown.isPlaying || humoRight.isPlaying || humoLeft.isPlaying) gasActive = true;
        else gasActive = false;

        if (gasActive)
        {
            if(!cubeSounds.isPlaying)
            {
                cubeSounds.Play();
            }
            
            
            gas -= 0.04f * Time.deltaTime;
            gasBar.size = gas;
        }
       
        

        if (upIsActive)
        {
            
            rig.AddForce(Vector3.up * velocity * Time.deltaTime);
            if (!humoDown.isPlaying) humoDown.Play();

        }
        else  humoDown.Stop();  

            if (leftIsActive)
        {
            //myTransform.Translate(Vector3.up * velocity * Time.deltaTime);
            rig.AddForce(Vector3.right * velocity/2 * Time.deltaTime);
            if(!humoLeft.isPlaying)humoLeft.Play();

        } else humoLeft.Stop();

        if (rightIsActive)
        {
            //myTransform.Translate(Vector3.up * velocity * Time.deltaTime);
            rig.AddForce(Vector3.left * velocity/2 * Time.deltaTime);
            if (!humoRight.isPlaying) humoRight.Play();
            
        } else humoRight.Stop();
    }
}
