using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

    public float velocity = -3f;
    public Spawn spawn; 
    private Rigidbody2D pipe;

	// Use this for initialization
	void Start () {
        pipe = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        pipe.velocity = new Vector2(velocity, 0);
        if(pipe.transform.position.x <= -3.5)
        {
            Destroy(this.gameObject);
        }
    }

}
