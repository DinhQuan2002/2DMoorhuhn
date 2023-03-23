using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject homeUI;
    public GameObject gameUI;

    public Dialog gameDialog;

    public Text timerText;
    public Text pointText;

    Dialog currentDialog;
    public Dialog CurrentDialog { get => currentDialog; set => currentDialog = value; }

    [SerializeField] GameObject ammoImagePrefab;
    [SerializeField] Transform ammoTF;
    public GameObject[] imageList;

    public void ShowGameUI(bool isShow)
    {
        if (gameUI)
        {
            gameUI.SetActive(isShow);
        }
        if (homeUI)
        {
            homeUI.SetActive(!isShow);
        }
    }

    public void UpdateTimer(string time)
    {
        if (timerText)
        {
            timerText.text = time;
        }
    }
    public void UpdatePointCouter(int point)
    {
        if (pointText != null)
        {
            pointText.text =  point.ToString(); //hien thi x100
        }
    }

    public void Replay()
    {
        Time.timeScale = 1.0f;
        if (currentDialog)
        {
            currentDialog.Show(false);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.PlayGame();
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Application.Quit();
    }

    public void SpawnBullet()
    {
        //foreach (var image in imageList)
        //{
        //    GameObject imageGO = Instantiate(ammoImagePrefab, ammoTF);
        //    //imageGO.GetComponent<UnityEngine.UI.Image>().sprite = image.GetComponent<UnityEngine.UI.Image>().sprite;
        //    imageList[image] = 
        //}
        for (int i = 0; i < imageList.Length; i++)
        {
            GameObject imageGO = Instantiate(ammoImagePrefab, ammoTF);
            imageList[i] = imageGO;
            imageList[i].GetComponent<UnityEngine.UI.Image>().enabled = true;
        }

    }
    public void UpdateBulletCount(int count)
    {
        if (count < 0) return;
        imageList[count].GetComponent<UnityEngine.UI.Image>().enabled = false;
    }

    public void ReLoadBullet()
    {
        for (int i = 0; i < imageList.Length; i++)
        {
            imageList[i].GetComponent<UnityEngine.UI.Image>().enabled = true;
        }
    }
}
