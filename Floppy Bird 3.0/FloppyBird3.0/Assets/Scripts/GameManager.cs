using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static float points = 0f;
    private bool started = false;
    public Bird bird;
    public Spawn spawn;
    public Animator ground;
    public GameObject birdDefault;

    public static Sprite[] digits;
    public Sprite[] digitos;
    public static SpriteRenderer digit0;
    public static SpriteRenderer digit1;
    public static SpriteRenderer digit2;

    public Sprite[] boardDigits;
    public SpriteRenderer boardDigit0;
    public SpriteRenderer boardDigit1;
    public SpriteRenderer boardDigit2;

    public SpriteRenderer maxDigit0;
    public SpriteRenderer maxDigit1;
    public SpriteRenderer maxDigit2;

    public GameObject board;
    public GameObject back;

    private bool aux = false;
    private bool touched = false;
    private Transform buttonTouched;

    // Use this for initialization
    void Start () {
        board.SetActive(false);
        back.SetActive(false);
        Bird.dead = false;
        ground.speed = 0;
        digits = digitos;
        digit0 = GameObject.Find("digit0").GetComponent<SpriteRenderer>();
        digit1 = GameObject.Find("digit1").GetComponent<SpriteRenderer>();
        digit2 = GameObject.Find("digit2").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !started)
        {
            started = true;
            birdDefault.SetActive(false);
            bird.gameObject.SetActive(true);
            bird.rb.bodyType = RigidbodyType2D.Dynamic;
            bird.rb.AddForce(new Vector2(0, bird.force));
            spawn.Begin();
            ground.speed = 1;
        }
        if (Bird.dead && !aux)
        {
            board.SetActive(true);
            back.SetActive(true);
            aux = true;

            int d2 = (int)points / 100;
            int d1 = (int)(points % 100) / 10;
            int d0 = (int)points % 10;

            boardDigit0.sprite = boardDigits[d0];
            boardDigit1.sprite = boardDigits[d1];
            boardDigit2.sprite = boardDigits[d2];

            int maxPoints = PlayerPrefs.GetInt("maxpoints");
            d2 = maxPoints / 100;
            d1 = (maxPoints % 100) / 10;
            d0 = maxPoints % 10;

            maxDigit0.sprite = boardDigits[d0];
            maxDigit1.sprite = boardDigits[d1];
            maxDigit2.sprite = boardDigits[d2];

        }

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        if (hit.collider != null && hit.transform.tag == "Back")
        {
            if (/*Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary || */Input.GetMouseButtonDown(0))
            {
                hit.transform.position = new Vector2(hit.transform.position.x, hit.transform.position.y - 0.062f);
                touched = true;
                buttonTouched = hit.transform;
            }

            else if (/*Input.GetTouch(0).phase == TouchPhase.Ended ||*/ Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene("Menu");
            }
            
        }
        if (Bird.dead && aux && !touched)
        {
            if (/*Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary || */Input.GetMouseButtonUp(0))
            {

                SceneManager.LoadScene("Game");
            }
        }
        if (/*Input.GetTouch(0).phase == TouchPhase.Ended ||*/ Input.GetMouseButtonUp(0) && touched)
        {
            touched = false;
            buttonTouched.position = new Vector2(buttonTouched.position.x, buttonTouched.position.y + 0.062f);
            buttonTouched = null;
            
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
            if (points < 1000)
            {
                int d2 = (int)points / 100;
                int d1 = (int)(points % 100) / 10;
                int d0 = (int)points % 10;

                digit0.sprite = digits[d0];
                digit1.sprite = digits[d1];
                digit2.sprite = digits[d2];
            }
            
        }
    }
}
