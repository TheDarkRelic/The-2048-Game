using UnityEngine;
using System;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

namespace VoidPixel
{
    public class GameController : MonoBehaviour
    {

        public static GameController instance;

        public static int Ticker;
        public static Action<string> slide;
        public int Score;
        int isGameOver;

        [SerializeField] Cell[] allCells;

        [SerializeField] AudioManager audioManager;
        [SerializeField] AnimationHandler animHandler;
        [SerializeField] TMP_Text scoreTxt;
        [SerializeField] PlayerInput input;
        [SerializeField] GameObject fillPreFab;
        [SerializeField] GameObject winPanel;
        [SerializeField] GameObject gameOverPanel;
        [SerializeField] int winningScore;
        [SerializeField] float nextMoveDelay = .25f;

        bool _hasWon;
        bool isProcessingMove;

        public bool IsProcessingMove { get { return isProcessingMove; } set { isProcessingMove = value; } }

        public Color[] fillColors;

        private void OnEnable()
        {
            if (GameController.instance == null) { GameController.instance = this; }
            else
            {
                if (GameController.instance != this) { Destroy(this.gameObject); }
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void Start()
        {
            StartSpawnFill();
            StartSpawnFill();
        }

        private void Update()
        {
            if (IsProcessingMove) { return; }

            if (input.Left)
            {
                StartCoroutine(DelayNextInput());
                Ticker = 0;
                isGameOver = 0;
                slide("Left");
            }

            if (input.Right)
            {
                StartCoroutine(DelayNextInput());
                Ticker = 0;
                isGameOver = 0;
                slide("Right");
            }

            if (input.Down)
            {
                StartCoroutine(DelayNextInput());
                Ticker = 0;
                isGameOver = 0;
                slide("Down");
            }
            
            if (input.Up)
            {
                StartCoroutine(DelayNextInput());
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

            if (chance < 0.8f)
            {
                GameObject tempFill = Instantiate(fillPreFab, allCells[whichSpawn].transform);
                Fill tempFillComponent = tempFill.GetComponent<Fill>();
                allCells[whichSpawn].GetComponent<Cell>().fill = tempFillComponent;
                tempFillComponent.UpdateFillValue(2);
                animHandler.TweenShakeScale(tempFill, .25f, .5f); // fix magic numbers
            }
            else
            {
                GameObject tempFill = Instantiate(fillPreFab, allCells[whichSpawn].transform);
                Fill tempFillComponent = tempFill.GetComponent<Fill>();
                allCells[whichSpawn].GetComponent<Cell>().fill = tempFillComponent;
                tempFillComponent.UpdateFillValue(4);
                animHandler.TweenShakeScale(tempFill, .25f, .5f); // fix magic numbers
            }
            
        }

        IEnumerator DelayNextInput()
        {
            if (!isProcessingMove)
            {
                IsProcessingMove = true;
                if (isGameOver < 16)
                {
                    audioManager.PlaySlideSound();
                }
                
                yield return new WaitForSeconds(nextMoveDelay);
                IsProcessingMove = false;
            }
            yield break;
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
            if (_hasWon) { return; }

            if (highestFill == winningScore)
            {
                winPanel.SetActive(true);
                _hasWon = true;
            }
        }

        public void KeepPlaying() { winPanel.SetActive(false); }
    }
}

