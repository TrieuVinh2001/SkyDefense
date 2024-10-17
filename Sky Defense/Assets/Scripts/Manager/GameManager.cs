using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gem;
    public int crystal;
    public int money;
    //public int energy;
    //public int energyMax;
    //public int slotBuild;
    //public int slotBuildMax;
    public float time;

    public int level;

    public AudioSource explosionSfx;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1f;
    }

    private void Update()
    {
        TimeCountDown();
    }

    public void Gem(int gemCount)
    {
        gem += gemCount;
        UIManager.instance.UpdateResourcesUI();
    }

    public void Crystal(int crystalCount)
    {
        crystal += crystalCount;
        UIManager.instance.UpdateResourcesUI();
    }

    public void Money(int moneyCount)
    {
        money += moneyCount;
        UIManager.instance.UpdateResourcesUI();
    }

    //public void Energy(int energyCount)
    //{
    //    energy += energyCount;
    //    UIManager.instance.UpdateResourcesUI();
    //}
    
    //public void EnergyMax(int energyCount)
    //{
    //    energyMax += energyCount;
    //    UIManager.instance.UpdateResourcesUI();
    //}

    //public void SlotBuild(int builCount)
    //{
    //    slotBuild += builCount;
    //    UIManager.instance.UpdateResourcesUI();
    //}
    
    //public void SlotBuildMax(int builCount)
    //{
    //    slotBuildMax += builCount;
    //    UIManager.instance.UpdateResourcesUI();
    //}

    private void TimeCountDown()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0;
            UIManager.instance.WinGame();
        }

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        UIManager.instance.timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ExplosionSFX()
    {
        explosionSfx.Play();
    }
}
