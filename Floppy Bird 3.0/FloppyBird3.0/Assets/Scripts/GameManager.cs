using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static float points = 0f;
    private bool started = false;
    public Bird bird;
    public Spawn spawn;
    public Animator ground;
    public GameObject birdDefault;

	// Use this for initialization
	void Start () {
        ground.speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !started) {
            started = true;
            birdDefault.SetActive(false);
            bird.gameObject.SetActive(true);
            bird.rb.bodyType = RigidbodyType2D.Dynamic;
            bird.rb.AddForce(new Vector2(0, bird.force));
            spawn.Begin();
            ground.speed = 1;
        }
    }

    public static float Points {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }
}
