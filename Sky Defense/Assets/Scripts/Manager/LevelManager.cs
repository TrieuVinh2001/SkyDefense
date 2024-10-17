using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int allLevel;
    [SerializeField] private GameObject levelPrefab;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        CreateLevel();
    }

    private void CreateLevel()
    {
        for (int i = 0; i <= allLevel; i++)
        {
            var levelButton = Instantiate(levelPrefab, transform);
            levelButton.GetComponent<LevelSelect>().level = i;
        }
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UnlockLevel()
    {
        PlayerPrefs.SetInt("Level", allLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DeleteData()
    {
        PlayerPrefs.SetInt("Level", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
