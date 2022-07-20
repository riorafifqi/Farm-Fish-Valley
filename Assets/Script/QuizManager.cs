using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public SoalQuiz soalQuiz;
    public Text tombol1, tombol2;
    public Text soal;
    public EndGame endGame;
    public bool result;

    private void Awake()
    {
        endGame = GameObject.Find("GameManager").GetComponent<EndGame>();
    }

    private void Start()
    {
        Time.timeScale = 0;
        soal.text = soalQuiz.soal;
        tombol1.text = soalQuiz.tombol1;
        tombol2.text = soalQuiz.tombol2;
    }

    public void PressAnswer()
    {
        if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == soalQuiz.jawabanBenar)
        {
            // Jawaban benar
            Debug.Log("Benar");
            Time.timeScale = 1;

            result = true;
            transform.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Salah");
            //jawaban salah

            endGame.Lose();
            result = false;
        }
    }
}
