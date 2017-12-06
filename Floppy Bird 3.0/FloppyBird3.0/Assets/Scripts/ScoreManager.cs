using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public GameObject scoreDetail;
    public RectTransform parent;

    private bool touched = false;
    private Transform buttonTouched;

    // Update is called once per frame
    void Update () {
        if(Input.touchCount > 0)
        {
            //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null && hit.transform.tag == "Back")
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
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
        
    }

    public class Jugador : IComparable<Jugador>
    {
        private string name;
        private int puntuacion;

        public Jugador(string name1, int puntuacion1)
        {
            this.name = name1;
            this.puntuacion = puntuacion1;
        }

        public int getPuntuacion()
        {
            return this.puntuacion;
        }


        public int CompareTo(Jugador obj)
        {
            return this.getPuntuacion().CompareTo(obj.getPuntuacion());
        }

        public string ToString()
        {
            return name;
        }
    }

    public string[] jugadores;

    IEnumerator Start()
    {

        WWW jugadoresData = new WWW("http://www.cgfcarlos.gdk.mx/flappy_bird/");
        yield return jugadoresData;//para esperar a que se descarguen los datos de la pagina
        string jugadoresString = jugadoresData.text;
        // print(jugadoresString);
        jugadores = jugadoresString.Split(';');

        //ArrayList de jugadores
        List<Jugador> jug = new List<Jugador>();
        for (int i = 0; i < jugadores.Length - 1; i++)
        {   
            jug.Add(new Jugador(getDataValue(jugadores[i], "Nick:"), int.Parse(getDataValue(jugadores[i], "Puntuacion:"))));   
        }

        jug.Sort();
        jug.Reverse();
        for (int i = 0; i < jug.Count; i++)
        {
            GameObject score = Instantiate(scoreDetail);
            score.transform.SetParent(parent);
            score.GetComponent<ScoreDetail>().setScore(i+1, jug[i].ToString(),jug[i].getPuntuacion());

            //GameObject b = Instantiate(bot);
            //b.transform.SetParent(GameObject.Find("Rejilla").transform);
            //b.GetComponentInChildren<Text>().text = jug[i].ToString();
            //b.transform.localScale = new Vector3(1, 1, 1);
        }

        //PATRON -> Nick:x|Puntuacion:12|;Nick:y|Puntuacion:21

        //comprobar puntuacion
        //obtener puntuacion
        //int puntuacion = PlayerPrefs.GetInt("Puntuacion");
        //for (int i = 0; i < jugadores.Length - 1; i++)
        //{
        //    if (PlayerPrefs.GetString("nick") == getDataValue(jugadores[i], "Nick:"))
        //    {
        //        puntuacion = int.Parse(getDataValue(jugadores[i], "Puntuacion:"));
        //    }
        //}
        //modificado

        //PlayerPrefs.SetInt("Puntuacion", 0);



    }

    string getDataValue(string data, string index)//Nick o Puntuacion para index
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }
}
