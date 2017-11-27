using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bird : MonoBehaviour {

    public Rigidbody2D rb;
    [SerializeField]
    public float force;
    //3.803
    public float vel =0;
    public float velM = 0;

    public static bool dead = false;
    private Animator animator;
    public AudioSource audioSource;
    public AudioClip point;
    public AudioClip hit;
    public AudioClip jump;
    private bool clip;


    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        clip = true;

        switch (PlayerPrefs.GetString("skin"))
        {
            case "defaultSkin":
                animator.SetInteger("skin", 1);
                break;
            case "dogeSkin":
                animator.SetInteger("skin", 5);
                break;
            case "mexicanSkin":
                animator.SetInteger("skin", 4);
                break;
            case "zombieSkin":
                animator.SetInteger("skin", 3);
                break;
            case "vampireSkin":
                animator.SetInteger("skin", 2);
                break;

        }
    }


    void FixedUpdate()
    {
        animator.SetBool("click", false);
        if (Input.GetMouseButtonDown(0) && !dead)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, force));
            animator.SetBool("click", true);
            audioSource.PlayOneShot(jump);

        }

        this.transform.rotation = Quaternion.Euler(
            this.transform.rotation.x,
            this.transform.rotation.y,
            Mathf.Lerp(-45, 45, (rb.velocity.y / 3.8f + 1f) / 2)
        );

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dead = true;
        GameObject.FindObjectsOfType<Pipe>().ToList().ForEach(pipe => pipe.velocity = 0);
        GameObject.FindObjectOfType<Spawn>().CancelInvoke();
        GameObject.Find("ground").GetComponent<Animator>().speed = 0;
        if(GameManager.Points > PlayerPrefs.GetInt("maxpoints"))
        {
            PlayerPrefs.SetInt("maxpoints", (int) GameManager.Points);
        }
        if (clip)
        {
            audioSource.PlayOneShot(hit);
            clip = false;
        }
        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Points++;
        audioSource.PlayOneShot(point);
        
    }

}
