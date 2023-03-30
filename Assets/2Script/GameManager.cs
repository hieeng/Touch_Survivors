using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameCamera;
    public GameObject world;
    public Player player;
    public Enemy enemy;
    public float playTime;
    public bool isPlay = false;

    public GameObject enemies;
    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject upgradePanel;
    public GameObject pausePanel;
    public GameObject LevelUpPanel;
    public GameObject loginPanel;

    public Text haveCoinText;
    public Text haveCoinText2;
    public Text coinText;
    public Text levelText;
    public Text playTimeText;

    public GameObject damage;
    public GameObject armor;
    public GameObject health;
    public GameObject speed;
    public GameObject exp;
    public GameObject coinU;

    public Text TLdamage;
    public Text TLarmor;
    public Text TLhealth;
    public Text TLspeed;
    public Text TLexp;
    public Text TLcoinU;

    int Ldamage = 0;
    int Larmor = 0;
    int Lhealth = 0;
    int Lspeed = 0;
    int Lexp = 0;
    int LcoinU = 0;

    public Text Tdamage;
    public Text Tarmor;
    public Text Thealth;
    public Text Tspeed;
    public Text Texp;
    public Text TcoinU;

    public int haveCoin;

    int BitterCoin;

    [HideInInspector] public float Pdamage;
    [HideInInspector] public int Parmor;
    [HideInInspector] public int Phealth;
    [HideInInspector] public float Pspeed;
    [HideInInspector] public float Pexp;
    [HideInInspector] public float Pcoin;

    public Transform playerTransform;

    public List<GameObject> LevelUp = new List<GameObject>();

    //타이틀
    private void Awake()
    {
        isPlay = false;
        instance = this;
        haveCoin = PlayerPrefs.GetInt("haveCoin");
        BitterCoin = PlayerPrefs.GetInt("BitterCoin");
        haveCoinText.text = string.Format("{0:n0}", haveCoin);

        Ldamage = PlayerPrefs.GetInt("Ldamage");
        Larmor = PlayerPrefs.GetInt("Larmor");
        Lhealth = PlayerPrefs.GetInt("Lhealth");
        Lspeed = PlayerPrefs.GetInt("Lspeed");
        Lexp = PlayerPrefs.GetInt("Lexp");
        LcoinU = PlayerPrefs.GetInt("LcoinU");
    }

    private void Start()
    {
        Pdamage = 1f + (0.1f * Ldamage);
        Parmor = 0 + Larmor;
        Phealth = 0 + (10 * Lhealth);
        Pspeed = 1f + (0.1f * Lspeed);
        Pexp = 1f + (0.1f * Lexp);
        Pcoin = 1f + (0.1f * LcoinU);
    }

    public void Login()
    {
        loginPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    //버튼
    public void GameStart()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);
        world.gameObject.SetActive(true);
        enemies.SetActive(true);

        isPlay = true;
    }

    public void Upgrade()
    {
        menuPanel.SetActive(false);
        upgradePanel.SetActive(true);
        haveCoinText2.text = string.Format("{0:n0}", haveCoin);

        Debug.Log(Ldamage);

        Tdamage.text = string.Format("{0:n0}", 100 + ((Ldamage + 2) * Ldamage * 100));
        Tarmor.text = string.Format("{0:n0}", 100 + ((Larmor + 2) * Larmor * 100));
        Thealth.text = string.Format("{0:n0}", 100 + ((Lhealth + 2) * Lhealth * 100));
        Tspeed.text = string.Format("{0:n0}", 100 + ((Lspeed + 2) * Lspeed * 100));
        Texp.text = string.Format("{0:n0}", 100 + ((Lexp + 2) * Lexp * 100));
        TcoinU.text = string.Format("{0:n0}", 100 + ((LcoinU + 2) * LcoinU * 100));

        TLdamage.text = string.Format("{0:n0}", Ldamage);
        TLarmor.text = string.Format("{0:n0}", Larmor);
        TLhealth.text = string.Format("{0:n0}", Lhealth);
        TLspeed.text = string.Format("{0:n0}", Lspeed);
        TLexp.text = string.Format("{0:n0}", Lexp);
        TLcoinU.text = string.Format("{0:n0}", LcoinU);
    }

    public void Quit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }

    public void GamePause()
    {
        isPlay = false;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }
    public void GameResume()
    {
        isPlay = true;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void GameMenu()
    {
        PlayerPrefs.SetInt("BitterCoin", BitterCoin);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    //업그레이드 리셋버튼
    public void ResetB()
    {
        Ldamage = 0;
        Larmor = 0;
        Lhealth = 0;
        Lspeed = 0;
        Lexp = 0;
        LcoinU = 0;

        Debug.Log(BitterCoin);
        haveCoin += BitterCoin;
        BitterCoin = 0;


        PlayerPrefs.SetInt("haveCoin", haveCoin);
        PlayerPrefs.SetInt("Ldamage", Ldamage);
        PlayerPrefs.SetInt("Larmor", Larmor);
        PlayerPrefs.SetInt("Lhealth", Lhealth);
        PlayerPrefs.SetInt("Lspeed", Lspeed);
        PlayerPrefs.SetInt("Lexp", Lexp);
        PlayerPrefs.SetInt("LcoinU", LcoinU);
        PlayerPrefs.SetInt("BiiterCoin", BitterCoin);

        haveCoinText2.text = string.Format("{0:n0}", haveCoin);
        Tdamage.text = string.Format("{0:n0}", 100);
        TLdamage.text = string.Format("{0:n0}", Ldamage);
        Tarmor.text = string.Format("{0:n0}", 100);
        TLarmor.text = string.Format("{0:n0}", Larmor);
        Thealth.text = string.Format("{0:n0}", 100);
        TLhealth.text = string.Format("{0:n0}", Lhealth);
        Tspeed.text = string.Format("{0:n0}", 100);
        TLspeed.text = string.Format("{0:n0}", Lspeed);
        Texp.text = string.Format("{0:n0}", 100);
        TLexp.text = string.Format("{0:n0}", Lexp);
        TcoinU.text = string.Format("{0:n0}", 100);
        TLcoinU.text = string.Format("{0:n0}", LcoinU);
    }

    private void Update()
    {
        if (isPlay)
            playTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        GameUI();
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }

    //UI 로직
    private void GameUI()
    {
        int min = (int)(playTime / 60);
        int sec = (int)(playTime % 60);

        playTimeText.text = string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec);
        coinText.text = string.Format("Coin : {0:n0}", player.coin);
        levelText.text = "LV : " + player.level;
    }

    //업그레이드
    public void Damage()
    {
        if (100 + ((Ldamage + 2) * Ldamage * 100) < haveCoin)
        {
            PlayerPrefs.SetInt("Ldamage", Ldamage);
            Pdamage += 0.1f;
            haveCoin -= 100 + ((Ldamage + 2) * Ldamage * 100);
            BitterCoin += 100 + ((Ldamage + 2) * Ldamage * 100);
            PlayerPrefs.SetInt("BitterCoin", BitterCoin);
            PlayerPrefs.SetInt("haveCoin", haveCoin);
            Ldamage++;
            Debug.Log(Ldamage);
            haveCoinText2.text = string.Format("{0:n0}", haveCoin);
            Tdamage.text = string.Format("{0:n0}", 100 + ((Ldamage + 2) * Ldamage * 100));
            TLdamage.text = string.Format("{0:n0}", Ldamage);
        }
        else
            return;
    }

    public void Armor()
    {
        if (100 + ((Larmor + 5) * Larmor * 300) < haveCoin)
        {
            Parmor += 1;
            haveCoin -= 100 + ((Larmor + 5) * Larmor * 300);
            BitterCoin += 100 + ((Larmor + 5) * Larmor * 300);
            PlayerPrefs.SetInt("BitterCoin", BitterCoin);
            PlayerPrefs.SetInt("haveCoin", haveCoin);
            Larmor++;
            PlayerPrefs.SetInt("Larmor", Larmor);
            haveCoinText2.text = string.Format("{0:n0}", haveCoin);
            Tarmor.text = string.Format("{0:n0}", 100 + ((Larmor + 5) * Larmor * 300));
            TLarmor.text = string.Format("{0:n0}", Larmor);
        }
        else
            return;
    }

    public void Health()
    {
        if (100 + ((Lhealth + 2) * Lhealth * 100) < haveCoin)
        {
            Phealth += 10;
            haveCoin -= 100 + ((Lhealth + 2) * Lhealth * 100);
            BitterCoin += 100 + ((Lhealth + 2) * Lhealth * 100);
            PlayerPrefs.SetInt("BitterCoin", BitterCoin);
            PlayerPrefs.SetInt("haveCoin", haveCoin);
            Lhealth++;
            PlayerPrefs.SetInt("Lhealth", Lhealth);
            haveCoinText2.text = string.Format("{0:n0}", haveCoin);
            Thealth.text = string.Format("{0:n0}", 100 + ((Lhealth + 2) * Lhealth * 100));
            TLhealth.text = string.Format("{0:n0}", Lhealth);
        }
        else
            return;
    }

    public void Speed()
    {
        if (100 + ((Lspeed + 2) * Lspeed * 100) < haveCoin)
        {

            Pspeed += 0.1f;
            haveCoin -= 100 + ((Lspeed + 2) * Lspeed * 100);
            BitterCoin += 100 + ((Lspeed + 2) * Lspeed * 100);
            PlayerPrefs.SetInt("BitterCoin", BitterCoin);
            PlayerPrefs.SetInt("haveCoin", haveCoin);
            Lspeed++;
            PlayerPrefs.SetInt("Lspeed", Lspeed);
            haveCoinText2.text = string.Format("{0:n0}", haveCoin);
            Tspeed.text = string.Format("{0:n0}", 100 + ((Lspeed + 2) * Lspeed * 100));
            TLspeed.text = string.Format("{0:n0}", Lspeed);
        }
        else
            return;
    }

    public void Exp()
    {
        if (100 + ((Lexp + 2) * Lexp * 100) < haveCoin)
        {
            Pexp += 0.1f;
            haveCoin -= 100 + ((Lexp + 2) * Lexp * 100);
            BitterCoin += 100 + ((Lexp + 2) * Lexp * 100);
            PlayerPrefs.SetInt("BitterCoin", BitterCoin);
            PlayerPrefs.SetInt("haveCoin", haveCoin);
            Lexp++;
            PlayerPrefs.SetInt("Lexp", Lexp);
            haveCoinText2.text = string.Format("{0:n0}", haveCoin);
            Texp.text = string.Format("{0:n0}", 100 + ((Lexp + 2) * Lexp * 100));
            TLexp.text = string.Format("{0:n0}", Lexp);
        }
        else
            return;
    }

    public void CoinU()
    {
        if (100 + ((LcoinU + 2) * LcoinU * 100) < haveCoin)
        {
            Pcoin += 0.1f;
            haveCoin -= 100 + ((LcoinU + 2) * LcoinU * 100);
            BitterCoin += 100 + ((LcoinU + 2) * LcoinU * 100);
            PlayerPrefs.SetInt("BitterCoin", BitterCoin);
            PlayerPrefs.SetInt("haveCoin", haveCoin);
            LcoinU++;
            PlayerPrefs.SetInt("LcoinU", LcoinU);
            haveCoinText2.text = string.Format("{0:n0}", haveCoin);
            TcoinU.text = string.Format("{0:n0}", 100 + ((LcoinU + 2) * LcoinU * 100));
            TLcoinU.text = string.Format("{0:n0}", LcoinU);
        }
        return;
    }

    //레벨업 선택지
    public void LevlUpSelection()
    {
        int[] list = new int[3];
        bool check = true;

        for (int i = 0; i < 3; i++)
        {
            list[i] = Random.Range(0, LevelUp.Count);

            for (int j = 0; j < i; j++)
            {
                if (list[i] == list[j])
                {
                    i--;
                    check = false;
                }
                else
                {
                    check = true;
                }
            }

            if (check)
            {
                if (i == 0)
                {
                    LevelUp[list[i]].SetActive(true);
                    LevelUp[list[i]].transform.localPosition = new Vector3(0, 0, 0);
                }
                else if (i == 1)
                {
                    LevelUp[list[i]].SetActive(true);
                    LevelUp[list[i]].transform.localPosition = new Vector3(0, -150, 0);
                }
                else if (i == 2)
                {
                    LevelUp[list[i]].SetActive(true);
                    LevelUp[list[i]].transform.localPosition = new Vector3(0, -300, 0);
                }
            }
        }
    }
}
