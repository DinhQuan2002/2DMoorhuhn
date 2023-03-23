using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Bird[] bird;
    [SerializeField] float spawnTime;
    [SerializeField] int timeLimit;

    [SerializeField] Camera mainCamera;

    int currentTimeLimit;
    int pointCounter;
    bool isGameOver;

    public int PointCounter { get => pointCounter; set => pointCounter = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

    private void Awake()
    {
        currentTimeLimit = timeLimit;
    }
    private void Start()
    {
        mainCamera = Camera.main;
        UIManager.Instance.ShowGameUI(false);
        UIManager.Instance.UpdatePointCouter(pointCounter);
    }
    public void PlayGame()
    {
        StartCoroutine(GameSpawn());
        StartCoroutine(TimeCountDown());
        UIManager.Instance.ShowGameUI(true);
        UIManager.Instance.SpawnBullet();
    }

    public void SpawnBird()
    {
        //tinh kich thuoc man hinh
        float screenHeight = mainCamera.orthographicSize * 2f;
        float screenWidth = screenHeight * mainCamera.aspect;

        //chon ben spawn ngau nhien trai = 0, phai = 1
        int side = UnityEngine.Random.Range(0, 2);

        float viewPortX = side == 0 ? 0f : 1f;
        float xPos = mainCamera.ViewportToWorldPoint(new Vector3(viewPortX, 0f, 0f)).x;
        float yPos = UnityEngine.Random.Range(-1, 4);
        xPos += side == 0 ? -2f : 2f;
        Vector2 spawnPos = new Vector2(xPos, yPos);
        if (bird != null)
        {
            int random = UnityEngine.Random.Range(0, bird.Length);
            if (bird[random] != null)
            {
                Bird birdClone = Instantiate(bird[random],spawnPos,quaternion.identity);
            }
        }
    }
    string IntToTime(int time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        return minutes.ToString("00") + " : " + seconds.ToString("00");
    }

    IEnumerator TimeCountDown()
    {
        while (currentTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTimeLimit--;
            if (currentTimeLimit <= 0)
            {
                isGameOver = true;
                UIManager.Instance.gameDialog.UpdateDialog("HIGHS SCORE",pointCounter.ToString());
                //if (pointCounter > Prefs.highScore)
                //{
                //    UIManager.Instance.gameDialog.UpdateDialog("NEW SCORE",pointCounter.ToString());
                //}
                //else
                //{
                //    UIManager.Instance.gameDialog.UpdateDialog("HIGHS SCORE",Prefs.highScore.ToString());
                //}
                //Prefs.highScore = pointCounter;

                UIManager.Instance.gameDialog.Show(true);
                UIManager.Instance.CurrentDialog = UIManager.Instance.gameDialog;
                Time.timeScale = 0f;

            }
            UIManager.Instance.UpdateTimer(IntToTime(currentTimeLimit));
        }
    }
    IEnumerator GameSpawn()
    {
        while (!isGameOver)
        {
            SpawnBird();
            yield return new WaitForSeconds(UnityEngine.Random.Range(0,spawnTime));
        }
    }
}
