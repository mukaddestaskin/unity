using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopHareketi : MonoBehaviour
{
 
    Vector3 yön;
    public float hýz;
    public float eklenecekhýz;
    public ZeminSpawner  zeminspawnerscripti;
    public static bool düþtü_mü;
    public static int Highskor;
    public Text baþla;
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
        düþtü_mü = false;
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

        hýz = 0f;
        Pause.SetActive(true);
        sonskortext2.text = Skor.skor.ToString();

    }
    public void PlayGame()
    {

        hýz = 2f;
        Pause.SetActive(false);

    }

    public void OuitGame()
    {
        Application.Quit();
        Debug.Log("Kapandý");
    }

    // Update is called once per frame
    void Update()
       
    { 
        
       if (transform.position.y <= 0.5F)
        {
            düþtü_mü = true;
        }
        
        if(düþtü_mü==true)
        {
            sonskor.SetActive(true);
            Handheld.Vibrate();


        }
        if (Input.GetMouseButtonDown(0))
        {

            Destroy(baþla);
          
            if (yön.x == 0)
            {
                yön = Vector3.left;
                
            }
            else
            {
                yön = Vector3.forward;
                
            }
            hýz = hýz+eklenecekhýz * Time.deltaTime;

            
        }
    }
        private void FixedUpdate()
    {
        
        Vector3 hareket = yön * Time.deltaTime * hýz;
        transform.position += hareket;
    }
    private void OnCollisionExit(Collision collision)
    {
        
        
        if(collision.gameObject.tag=="zemin")
        {
            collision.gameObject.AddComponent<Rigidbody>();
            zeminspawnerscripti.zemin_oluþtur();
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
