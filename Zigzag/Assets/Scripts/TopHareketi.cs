using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopHareketi : MonoBehaviour
{
 
    Vector3 y�n;
    public float h�z;
    public float eklenecekh�z;
    public ZeminSpawner  zeminspawnerscripti;
    public static bool d��t�_m�;
    public static int Highskor;
    public Text ba�la;
    public Text sonskortext;
    public Text sonskortext2;
    public Text Highskortext;
    public GameObject sonskor;
    public GameObject Pause;
    public Button restartButton; 
    public Button restartButton2;
    public Button quitButton; 
    public Button quitButton2;
    public Button pauseButton;
    public Button playButton;

    void Start()
    {
        d��t�_m� = false;
        sonskor.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        restartButton2.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(OuitGame);
        quitButton2.onClick.AddListener(OuitGame);
        pauseButton.onClick.AddListener(PauseGame);
        playButton.onClick.AddListener(PlayGame);
        Highskor = PlayerPrefs.GetInt("HighSkor", Skor.skor);
        Highskortext.text = Highskor.ToString();
       

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Pause.SetActive(false);

    }
    public void PauseGame()
    {

        h�z = 0f;
        Pause.SetActive(true);
        sonskortext2.text = Skor.skor.ToString();

    }
    public void PlayGame()
    {

        h�z = 2f;
        Pause.SetActive(false);

    }

    public void OuitGame()
    {
        Application.Quit();
        Debug.Log("Kapand�");
    }

    // Update is called once per frame
    void Update()
       
    { 
        
       if (transform.position.y <= 0.5F)
        {
            d��t�_m� = true;
        }
        
        if(d��t�_m�==true)
        {
            sonskor.SetActive(true);
            Handheld.Vibrate();


        }
        if (Input.GetMouseButtonDown(0))
        {

            Destroy(ba�la);
          
            if (y�n.x == 0)
            {
                y�n = Vector3.left;
                
            }
            else
            {
                y�n = Vector3.forward;
                
            }
            h�z = h�z+eklenecekh�z * Time.deltaTime;

            
        }
    }
        private void FixedUpdate()
    {
        
        Vector3 hareket = y�n * Time.deltaTime * h�z;
        transform.position += hareket;
    }
    private void OnCollisionExit(Collision collision)
    {
        
        
        if(collision.gameObject.tag=="zemin")
        {
            collision.gameObject.AddComponent<Rigidbody>();
            zeminspawnerscripti.zemin_olu�tur();
            StartCoroutine(ZeminiSil(collision.gameObject));
            Skor.skor++;
            sonskortext.text = Skor.skor.ToString();
            HighSkorHesaplama();
        }
    }
    private void HighSkorHesaplama()
    {
        if(Skor.skor>Highskor)
        {
            Highskor = Skor.skor;
            Highskortext.text = Highskor.ToString();
            PlayerPrefs.SetInt("HighSkor", Highskor);
        }
            
                
    }


    IEnumerator ZeminiSil(GameObject SilinecekZemin)
    {
        yield return new WaitForSeconds(3f);
        Destroy(SilinecekZemin);
    }
   
}
