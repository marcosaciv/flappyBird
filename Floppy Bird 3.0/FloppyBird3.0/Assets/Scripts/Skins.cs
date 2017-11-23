using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skins : MonoBehaviour {

    public Sprite dogeSkin;
    public Sprite zombieSkin;
    public Sprite vampireSkin;
    public Sprite mexicanSkin;

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
		
	}

    void SelectSkin()
    {
        
    }

    
}
