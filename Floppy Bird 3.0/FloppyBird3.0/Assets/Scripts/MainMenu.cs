using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    Camera camara;
    

    void Start()
    {
        camara = GameObject.FindObjectOfType<Camera>();
    }

   

    void Update()
    {
        if (/*Input.touchCount > 0*/true)
        {
            //Vector3 pos = camara.ScreenToWorldPoint(Input.GetTouch(0).position);
           Vector3 pos = camara.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null)
            {

                if (hit.transform.tag == "Start")
                {
                    if (/*Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary || */Input.GetMouseButtonDown(0))
                    {
                        hit.transform.position = new Vector2(hit.transform.position.x, hit.transform.position.y - 0.062f);
                    }
                    else if (/*Input.GetTouch(0).phase == TouchPhase.Ended ||*/ Input.GetMouseButtonUp(0))
                    {
                        hit.transform.position = new Vector2(hit.transform.position.x, hit.transform.position.y + 0.062f);
                        SceneManager.LoadScene("Game");
                    }
                }
                else if (hit.transform.tag == "Score")
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
                    {
                        hit.transform.position = new Vector2(hit.transform.position.x, -1.062f);
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        hit.transform.position = new Vector2(hit.transform.position.x, -1.02f);
                        //StartCoroutine(CambiarNivel("Score"));
                    }
                }
                else if (hit.transform.tag == "Skins")
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
                    {
                        hit.transform.position = new Vector2(hit.transform.position.x, -0.682f);
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        hit.transform.position = new Vector2(hit.transform.position.x, -0.64f);
                        //StartCoroutine(CambiarNivel("Skins"));
                    }
                }
                else if (hit.transform.tag == "Back")
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
                    {
                        hit.transform.position = new Vector2(hit.transform.position.x, -0.682f);
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        hit.transform.position = new Vector2(hit.transform.position.x, -0.64f);
                        Application.Quit();
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
