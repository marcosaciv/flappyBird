using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDetail : MonoBehaviour {

    public Text rank;
    public Text nombre;
    public Text score;

    public void setScore(int rank, string nombre, int score)
    {
        this.rank.text = rank.ToString();
        this.nombre.text = nombre;
        this.score.text = score.ToString();
    }
}
