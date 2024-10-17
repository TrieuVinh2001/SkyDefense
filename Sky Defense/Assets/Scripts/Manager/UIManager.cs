using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject weaponBuyPanel;
    public GameObject weaponUpdatePanel;
    public GameObject buildingBuyPanel;
    public GameObject buildingDestroyPanel;
    public Transform weaponTranf;
    public Transform buildingTranf;

    public TextMeshProUGUI crystalText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI buildingSlotText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timeText;

    public GameObject noteText;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        //WeaponDefense.WeaponDefenseClick += BuyWeapon;
        //Building.BuildingClick += BuyBuilding;
        levelText.text = "Level " + PlayerPrefs.GetInt("LevelCurrent");
        UpdateResourcesUI();
    }

    //private void OnDisable()
    //{
    //    WeaponDefense.WeaponDefenseClick -= BuyWeapon;
    //    Building.BuildingClick -= BuyBuilding;
    //}

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyWeapon(Transform weaponTranform)
    {
        weaponTranf = weaponTranform;
        weaponBuyPanel.SetActive(true);
    }

    public void UpdateWeapon(Transform weaponTranform)
    {
        weaponTranf = weaponTranform;
        weaponUpdatePanel.SetActive(true);
    }

    public void BuyBuilding(Transform buildTranform)
    {
        buildingTranf = buildTranform;
        buildingBuyPanel.SetActive(true);
    }

    public void RePairBuilding()
    {
        if (GameManager.instance.money >= 100)
        {
            GameManager.instance.money -= 100;
            UpdateResourcesUI();
            buildingTranf.GetComponent<BuildingBase>().RepairBuildingFull();
            buildingDestroyPanel.SetActive(false);
        }
        else
        {
            NoteTextUp();
            
        }
    }

    public void NoteTextUp()
    {
        noteText.SetActive(true);
        StartCoroutine(NoteHide());
    }

    IEnumerator NoteHide()
    {
        yield return new WaitForSeconds(2f);
        noteText.SetActive(false);
    }

    public void RemoveWeapon()
    {
        weaponTranf.GetComponent<SpriteRenderer>().enabled = true;
        GameManager.instance.gem += weaponTranf.GetComponentInChildren<WeaponBase>().weaponSO.priceGem / 2;
        GameManager.instance.crystal += weaponTranf.GetComponentInChildren<WeaponBase>().weaponSO.priceCrystal / 2;
        GameManager.instance.money += weaponTranf.GetComponentInChildren<WeaponBase>().weaponSO.priceMoney / 2;
        UpdateResourcesUI();
        weaponUpdatePanel.SetActive(false);
        Destroy(weaponTranf.GetChild(0).gameObject);
    }

    public void DestoyBuildingPanel(Transform buildTranform)
    {
        buildingTranf = buildTranform;
        buildingDestroyPanel.SetActive(true);
    }

    public void RemoveBuilding()
    {
        if(buildingTranf.TryGetComponent<BuildingMain>(out BuildingMain build))
        {
            LoseGame();
        }
        else
        {
            //UIManager.instance.weaponTranf.GetComponent<SpriteRenderer>().enabled = true;
            GameManager.instance.gem += buildingTranf.GetComponent<BuildingBase>().buildingSO.priceGem / 2;
            GameManager.instance.crystal += buildingTranf.GetComponent<BuildingBase>().buildingSO.priceCrystal / 2;
            GameManager.instance.money += buildingTranf.GetComponent<BuildingBase>().buildingSO.priceMoney / 2;
            UpdateResourcesUI();
            buildingDestroyPanel.SetActive(false);
            buildingTranf.GetComponentInParent<CircleCollider2D>().enabled = true;
            buildingTranf.parent.GetComponentInParent<SpriteRenderer>().enabled = true;
            buildingTranf.parent.GetComponentInParent<BoxCollider2D>().enabled = true;
            Destroy(buildingTranf.gameObject);
        }
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void UpdateResourcesUI()
    {
        crystalText.text = "" + GameManager.instance.crystal;
        gemText.text = "" + GameManager.instance.gem;
        moneyText.text = "" + GameManager.instance.money;
        //buildingSlotText.text = GameManager.instance.slotBuild+" / "+GameManager.instance.slotBuildMax;
        //energyText.text = GameManager.instance.energy+" / "+GameManager.instance.energyMax;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        int levelCurrent = PlayerPrefs.GetInt("LevelCurrent");
        SceneManager.LoadScene("Level" + (levelCurrent + 1));

        PlayerPrefs.SetInt("LevelCurrent", levelCurrent + 1);
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
        int levelCurrent = PlayerPrefs.GetInt("LevelCurrent");
        if (levelCurrent + 1 > PlayerPrefs.GetInt("Level"))
        {
            PlayerPrefs.SetInt("Level", levelCurrent + 1);
        }

        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        losePanel.SetActive(true);

        Time.timeScale = 0f;
    }
}
