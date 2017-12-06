using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject back;
    public GameObject play;
    public GameObject score;
    public GameObject skins;

    public GameObject panel;
    Camera camara;
    private bool touched = false;
    private Transform buttonTouched;

    private string url = "http://www.cgfcarlos.gdk.mx/flappy_bird/checkConnection.php";


    void Start()
    {
        if (System.String.IsNullOrEmpty(PlayerPrefs.GetString("nick")))
        {
            StartCoroutine(checkConnection(url));

        }
        camara = GameObject.FindObjectOfType<Camera>();
        
    }

   

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Ended && touched)
            {
                buttonTouched.transform.position = new Vector2(buttonTouched.transform.position.x, buttonTouched.transform.position.y + 0.062f);
                touched = false;
                buttonTouched = null;
            }
            Vector3 pos = camara.ScreenToWorldPoint(Input.GetTouch(0).position);
            // Vector3 pos = camara.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.tag == "Start")
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
                    {
                        hit.transform.position = new Vector2(hit.transform.position.x, hit.transform.position.y - 0.062f);
                        buttonTouched = hit.transform;
                        touched = true;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        SceneManager.LoadScene("Game");
                    }
                }
                else if (hit.transform.tag == "Score")
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
                    {
                        hit.transform.position = new Vector2(hit.transform.position.x, hit.transform.position.y - 0.062f);
                        buttonTouched = hit.transform;
                        touched = true;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        SceneManager.LoadScene("Score");
                        //StartCoroutine(CambiarNivel("Score"));
                    }
                }
                else if (hit.transform.tag == "Skins")
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
                    {
                        hit.transform.position = new Vector2(hit.transform.position.x, hit.transform.position.y - 0.062f);
                        buttonTouched = hit.transform;
                        touched = true;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {   
                        SceneManager.LoadScene("Skins");
                        //StartCoroutine(CambiarNivel("Skins"));
                    }
                }
                //else if (hit.transform.tag == "Back")
                //{
                //    if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
                //    {
                //        hit.transform.position = new Vector2(hit.transform.position.x, -0.682f);
                //    }
                //    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                //    {
                //        hit.transform.position = new Vector2(hit.transform.position.x, -0.64f);
                //        Application.Quit();
                //    }
                //}
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Submit(Text nick)
    {
        StartCoroutine(createUser(nick.text));
    }

    string createUserUrl = "http://www.cgfcarlos.gdk.mx/flappy_bird/createUser.php";

    IEnumerator createUser(string nick)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickPost", nick);

        WWW www = new WWW(createUserUrl, form);
        yield return www;//para esperar a que se descarguen los datos de la pagina
        if(www.text != "0")
        {
            back.SetActive(true);
            panel.SetActive(false);
            skins.SetActive(true);
            play.SetActive(true);
            score.SetActive(true);
            PlayerPrefs.SetString("nick",nick);
        }
    }

    IEnumerator checkConnection(string url)
    {
        WWW www = new WWW(url);

        yield return www;
        if (www.isDone && www.bytesDownloaded>0)
        {
            back.SetActive(false);
            panel.SetActive(true);
            skins.SetActive(false);
            play.SetActive(false);
            score.SetActive(false);
        }
    }
}
