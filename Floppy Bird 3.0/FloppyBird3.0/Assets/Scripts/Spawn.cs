using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject pipe;
    public float force = -10f;
	// Use this for initialization
	public void Begin () {
        InvokeRepeating("SpawnPipe", 1f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //2.2 -1.2
    private void SpawnPipe()
    {
        GameObject aux = Instantiate(pipe, new Vector3(this.transform.position.x, Random.Range(-1.2f, 2.2f), this.transform.position.z), Quaternion.identity);
        aux.GetComponent<Pipe>().spawn = this;
    }
}
