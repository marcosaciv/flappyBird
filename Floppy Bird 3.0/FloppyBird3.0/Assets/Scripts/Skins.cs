using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Skins : MonoBehaviour {

    public Sprite dogeSkin;
    public Sprite zombieSkin;
    public Sprite vampireSkin;
    public Sprite mexicanSkin;

    private bool touched = false;
    private Transform buttonTouched;

    // Use this for initialization
    void Start () {

        

        int maxPoints = PlayerPrefs.GetInt("maxpoints");
        if(maxPoints >= 25)
        {
            GameObject.Find("DogeSkin").GetComponent<Image>().sprite = dogeSkin;
        }

        if (maxPoints >= 50)
        {
            GameObject.Find("ZombieSkin").GetComponent<Image>().sprite = zombieSkin;
        }

        if (maxPoints >= 75)
        {
            GameObject.Find("VampireSkin").GetComponent<Image>().sprite = vampireSkin;
        }

        if (maxPoints >= 100)
        {
            GameObject.Find("MexicanSkin").GetComponent<Image>().sprite = mexicanSkin;
        }

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        if (hit.collider != null && hit.transform.tag == "Back")
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                hit.transform.position = new Vector2(hit.transform.position.x, hit.transform.position.y - 0.062f);
                touched = true;
                buttonTouched = hit.transform;
            }

            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                SceneManager.LoadScene("Menu");
            }

        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended && touched)
        {
            touched = false;
            buttonTouched.position = new Vector2(buttonTouched.position.x, buttonTouched.position.y + 0.062f);
            buttonTouched = null;

        }
    }

    public void SelectSkin(Button btn)
    {
        int maxPoints = PlayerPrefs.GetInt("maxpoints");
        Outline outl;
        switch (btn.gameObject.name)
        {
            case "DogeSkin":
                if (maxPoints >= 25)
                {
                    deleteOutline();
                    PlayerPrefs.SetString("skin", "dogeSkin");
                    outl = btn.gameObject.AddComponent<Outline>();
                    outl.effectColor = Color.red;
                    outl.effectDistance = new Vector2(5f, 5f);
                }
                break;
            case "VampireSkin":
                if(maxPoints >= 75)
                {
                    deleteOutline();
                    outl = btn.gameObject.AddComponent<Outline>();
                    outl.effectColor = Color.red;
                    outl.effectDistance = new Vector2(5f, 5f);
                    PlayerPrefs.SetString("skin", "vampireSkin");
                }
                    
                break;
            case "MexicanSkin":
                if(maxPoints >= 100)
                {
                    deleteOutline();
                    outl = btn.gameObject.AddComponent<Outline>();
                    outl.effectColor = Color.red;
                    outl.effectDistance = new Vector2(5f, 5f);
                    PlayerPrefs.SetString("skin", "mexicanSkin");
                }
                    
                break;
            case "ZombieSkin":
                if(maxPoints >= 50)
                {
                    deleteOutline();
                    PlayerPrefs.SetString("skin", "zombieSkin");
                    outl = btn.gameObject.AddComponent<Outline>();
                    outl.effectColor = Color.red;
                    outl.effectDistance = new Vector2(5f, 5f);
                }
                break;
            case "DefaultSkin":
                deleteOutline();
                outl = btn.gameObject.AddComponent<Outline>();
                outl.effectColor = Color.red;
                outl.effectDistance = new Vector2(5f, 5f);
                PlayerPrefs.SetString("skin", "defaultSkin");
                break;
        }
    }

    void deleteOutline()
    {
        foreach(Button btn in GameObject.FindObjectsOfType(typeof(Button)))
        {
            Destroy(btn.gameObject.GetComponent<Outline>());
        }
    }

    
}
