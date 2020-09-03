using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;
using System.Threading.Tasks;

public class HowToPlay : MonoBehaviour
{
    private bool go, firstTime, firstSphere;
    public TextMeshProUGUI  textCenter;
    public TextMeshPro textCenterGO;
    public CameraController cam;
    public GameObject finalGO, sphereGO, keyGO;
    private Propulsor prop;
    // Start is called before the first frame update
    void Start()
    {
        prop = GetComponent<Propulsor>();
        if (Application.systemLanguage == SystemLanguage.Spanish) Invoke("Empieza", 5);
        else Invoke("EmpiezaEnglish", 5);
    }
    private async void Empieza()
    {
        textCenter.text = "Eres un cubo";
        textCenter.gameObject.SetActive(true);
        //Time.timeScale = 0;
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.text = "Un cubo con motores";
        prop.humoLeft.Play();
        prop.humoRight.Play();
        prop.humoDown.Play();
        prop.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 300, 0));
        //Time.timeScale = 1;
        //cam.FollowDistance = 15;
        //Time.timeScale = 0;

        await Task.Delay(System.TimeSpan.FromSeconds(1f));
        prop.humoLeft.Stop();
        prop.humoRight.Stop();
        prop.humoDown.Stop();

        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        
        textCenter.gameObject.SetActive(false); 
        cam.CameraTarget = finalGO.transform;
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.text = "Este es tu objetivo";
        textCenter.gameObject.SetActive(true);
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.gameObject.SetActive(false);
        cam.CameraTarget = sphereGO.transform;
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.text = "Recolecta esferas para cargar combustible";
        textCenter.gameObject.SetActive(true);
        await Task.Delay(System.TimeSpan.FromSeconds(5f));

        textCenter.gameObject.SetActive(false);
        cam.CameraTarget = keyGO.transform;
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.text = "Recolecta esferas de color para abrir muros";
        textCenter.gameObject.SetActive(true);
        await Task.Delay(System.TimeSpan.FromSeconds(5f));

        textCenter.gameObject.SetActive(false);
        cam.CameraTarget = gameObject.transform;
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.text = "Si se acaba el combustible, puedes usar Reload una vez y recargar viendo un anuncio, o volver a empezar el nivel";
        textCenter.gameObject.SetActive(true);
        await Task.Delay(System.TimeSpan.FromSeconds(7f));


        textCenter.text = "Usa los controles para mover el cubo";
        await Task.Delay(System.TimeSpan.FromSeconds(4f));
        textCenter.gameObject.SetActive(false);
        await Task.Delay(System.TimeSpan.FromSeconds(1f));

        prop.StartGame();
        Debug.Log("Seguimos");
    }

    private async void EmpiezaEnglish()
    {
        textCenter.text = "You are a cube";
        textCenter.gameObject.SetActive(true);
        //Time.timeScale = 0;
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.text = "A cube with engines";

        prop.humoLeft.Play();
        prop.humoRight.Play();
        prop.humoDown.Play();
        prop.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 300, 0));
        //Time.timeScale = 1;
        //cam.FollowDistance = 15;
        //Time.timeScale = 0;

        await Task.Delay(System.TimeSpan.FromSeconds(1f));
        prop.humoLeft.Stop();
        prop.humoRight.Stop();
        prop.humoDown.Stop();

        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.gameObject.SetActive(false);
        cam.CameraTarget = finalGO.transform;
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.text = "This is your goal";
        textCenter.gameObject.SetActive(true);
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.gameObject.SetActive(false);
        cam.CameraTarget = sphereGO.transform;
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.text = "Collect spheres to reload fuel";
        textCenter.gameObject.SetActive(true);
        await Task.Delay(System.TimeSpan.FromSeconds(5f));

        textCenter.gameObject.SetActive(false);
        cam.CameraTarget = keyGO.transform;
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.text = "Collect colored spheres to open walls";
        textCenter.gameObject.SetActive(true);
        await Task.Delay(System.TimeSpan.FromSeconds(5f));

        textCenter.gameObject.SetActive(false);
        cam.CameraTarget = gameObject.transform;
        await Task.Delay(System.TimeSpan.FromSeconds(3f));

        textCenter.text = "If fuel runs out, you can use reload once and recharge after seeing an ad, or restart the level";
        textCenter.gameObject.SetActive(true);
        await Task.Delay(System.TimeSpan.FromSeconds(7f));


        textCenter.text = "Use the controls to move the cube";
        await Task.Delay(System.TimeSpan.FromSeconds(4f));
        textCenter.gameObject.SetActive(false);
        await Task.Delay(System.TimeSpan.FromSeconds(1f));

        prop.StartGame();
        Debug.Log("Seguimos");
    }
    
}
