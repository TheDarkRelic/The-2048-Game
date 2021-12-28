using UnityEngine;
using System;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace VoidPixel
{
    public class GameController : MonoBehaviour
    {

        public static GameController instance;

        public static int Ticker;
        public static Action<string> slide;
        public int Score;
        int isGameOver;

        [SerializeField] GameObject gameOverPanel;
        [SerializeField] TMP_Text scoreTxt;
        [SerializeField] PlayerInput input;
        [SerializeField] GameObject fillPreFab;
        [SerializeField] Cell[] allCells;
        [SerializeField] int winningScore;
        [SerializeField] GameObject winPanel;

        bool hasWon;

        public Color[] fillColors;

        private void OnEnable()
        {
            if (instance == null) { instance = this; }
            else { Destroy(this); }
        }

        private void Start()
        {
            StartSpawnFill();
            StartSpawnFill();
        }

        private void Update()
        {

            if (input.Left)
            {
                Ticker = 0;
                isGameOver = 0;
                slide("Left");
            }

            if (input.Right)
            {
                Ticker = 0;
                isGameOver = 0;
                slide("Right");
            }

            if (input.Down)
            {
                Ticker = 0;
                isGameOver = 0;
                slide("Down");
            }
            
            if (input.Up)
            {
                Ticker = 0;
                isGameOver = 0;
                slide("Up");
            }
        }

        public void SpawnFill()
        {
            bool isFull = true;
            for (int i = 0; i < allCells.Length; i++)
            {
                if (allCells[i].fill == null) { isFull = false; }
            }

            if (isFull) { return; }

            int whichSpawn = Random.Range(0, allCells.Length);
            if (allCells[whichSpawn].transform.childCount != 0)
            {
                SpawnFill();
                return;
            }
            float chance = Random.Range(0.0f, 1.0f);

            if (chance < 0.2f) { return; }
            else if (chance < 0.8f)
            {
                GameObject tempFill = Instantiate(fillPreFab, allCells[whichSpawn].transform);
                Fill tempFillComponent = tempFill.GetComponent<Fill>();
                allCells[whichSpawn].GetComponent<Cell>().fill = tempFillComponent;
                tempFillComponent.UpdateFillValue(2);
                TweenObjectScale(tempFill);

            }
            else
            {
                GameObject tempFill = Instantiate(fillPreFab, allCells[whichSpawn].transform);
                Fill tempFillComponent = tempFill.GetComponent<Fill>();
                allCells[whichSpawn].GetComponent<Cell>().fill = tempFillComponent;
                tempFillComponent.UpdateFillValue(4);
                TweenObjectScale(tempFill);
            }
        }

        private void TweenObjectScale(GameObject tempFill)
        {
            var tempTransform = tempFill.GetComponent<RectTransform>();
            if (tempFill == null) { return; }
            tempTransform.DOShakeScale(.25f, .5f, 10, 90, false);

        }

        public void StartSpawnFill()
        {
            int whichSpawn = Random.Range(0, allCells.Length);
            if (allCells[whichSpawn].transform.childCount != 0)
            {
                SpawnFill();
                return;
            }

            GameObject tempFill = Instantiate(fillPreFab, allCells[whichSpawn].transform);
            Fill tempFillComponent = tempFill.GetComponent<Fill>();
            allCells[whichSpawn].GetComponent<Cell>().fill = tempFillComponent;
            tempFillComponent.UpdateFillValue(2);
        }

        public void UpdateScore(int scoreIn)
        {
            Score += scoreIn;
            scoreTxt.text = Score.ToString();
        }

        public void GameOverCheck()
        {
            isGameOver++;
            if (isGameOver >= 16) { gameOverPanel.SetActive(true); }
        }

        public void Restart() { SceneManager.LoadScene(0); }

        public void WinCheck(int highestFill)
        {
            if (hasWon) { return; }

            if (highestFill == winningScore)
            {
                winPanel.SetActive(true);
                hasWon = true;
            }
        }

        public void KeepPlaying() { winPanel.SetActive(false); }
    }
}

