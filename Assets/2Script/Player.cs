using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject GameOverMenu;
    public GameObject LevelUp;
    GameManager gm;
    WhipWeapon whip;
    DaggerWeapon dagger;
    FireballWeapon fireball;
    CircleWeapon circle;
    LaserWeapon laser;
    RotationWeapon rotation;

    public Joystick js;
    [HideInInspector] public float lastX = 1;
    [HideInInspector] public float lastDX = 1;
    [HideInInspector] public float lastY = 0;
    public float speed = 3f;
    public int maxHp = 100;
    public int currentHp = 100;
    public int coin = 0;
    float Ucoin;
    public int level = 1;
    public int armor = 0;
    float exp = 0;
    int to_levelup_exp
    {
        get
        {
            return (level + 2) * level * 10;
        }
    }
    private bool isPause = false;
    private bool isGameOver;

    GameObject nearObject;

    new Rigidbody2D rigidbody;
    [HideInInspector] public Vector3 movementVector;
    [SerializeField] HpBar hpBar;
    [SerializeField] ExpBar expBar;
    [SerializeField] LevelUp levelUp;

    [SerializeField] List<GameObject> weapons;
    int[] Weapon;
    public int[] WeaponLevel;
    [HideInInspector] public int fireballDamage;

    BackEndGameInfo backEndGameInfo;
    BackEndManager backEndManager;

    public Text Twhip;
    public Text Tdagger;
    public Text Tfireball;
    public Text TCircle;
    public Text Tlaser;
    public Text Trotation;

    Animator anim;

    private void Awake()
    {
        js = GameObject.Find("Canvas/JoyStick").GetComponent<Joystick>();
        anim = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        backEndGameInfo = GameObject.Find("BackEndManager").GetComponent<BackEndGameInfo>();
        backEndManager = GameObject.Find("BackEndManager").GetComponent<BackEndManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        whip = GameObject.Find("Player/wpn_Whip").GetComponent<WhipWeapon>();
    }

    private void Start()
    {
        //backEndManager.AutoLogin();
        //googleManager.GPGSLogin();
        Weapon = new int[weapons.Count];
        WeaponLevel = new int[weapons.Count];
        for (int i = 0; i < weapons.Count; i++)
        {
            Weapon[i] = 0;
            WeaponLevel[i] = 0;
        }
        maxHp += gm.Phealth;
        currentHp = maxHp;
        armor = gm.Parmor;
        speed *= gm.Pspeed;

        coin = gm.haveCoin;

        Time.timeScale = 1;
        isGameOver = false;
        hpBar.SetState(currentHp, maxHp);
        expBar.UpdateExpSlider(exp, to_levelup_exp);
    }

    private void Update()
    {
        Move();
        Pause();
        GameOver();
    }

    //일시정지
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPause)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                isPause = true;
                return;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                isPause = false;
                return;
            }
        }
    }

    //이동과 방향
    private void Move()
    {
        Vector3 vec = new Vector3(js.Horizontal, js.Vertical, 0);
        rigidbody.velocity = Vector3.zero;

        //마지막 방향
        if (js.Horizontal != 0)
        {
            if (js.Horizontal > 0.15)
            {
                lastX = 1;
                lastDX = lastX ;
            }
            else if (js.Horizontal < -0.15)
            {
                lastX = -1;
                lastDX = lastX;
            }
            else
            {
                lastX = 0;
            }
        }
        else
            lastX = lastDX;
        if (js.Vertical != 0)
        {
            if (js.Vertical > 0.15)
            {
                lastY = 1;
            }
            else if (js.Vertical < -0.15)
            {
                lastY = -1;
            }
            else
            {
                lastY = 0;
            }
        }
        else
            lastY = 0;

        //애니메이션 처리
        if (js.Horizontal < 0)
        {
            hpBar.transform.localEulerAngles = new Vector3(0, -180, 0);
            transform.rotation = Quaternion.Euler(0, -180, 0);
            anim.SetBool("isMove", true);
        }
        else if (js.Horizontal > 0)
        {
            hpBar.transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        vec.Normalize();

        transform.position += vec * speed * Time.deltaTime;
    }

    //게임오버
    private void GameOver()
    {
        if(isGameOver)
        {
            GameOverMenu.SetActive(true);
            Time.timeScale = 0;
        }
        PlayerPrefs.SetInt("haveCoin", coin);
    }
    //피격
    public void TakeDamage(int damage)
    {
        ApplyAramor(ref damage);
        currentHp -= damage;

        if(currentHp <= 0)
        {
            isGameOver = true;
            backEndGameInfo.OnClickSave();
        }
        hpBar.SetState(currentHp, maxHp);
    }

    //아머
    private void ApplyAramor(ref int damage)
    {
        damage -= armor;
        if (damage < 0) 
            damage = 0;
    }

    //아이템
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch(item.type)
            {
                case Item.Type.Heart:
                    currentHp += item.value;
                    if(currentHp > maxHp)
                        currentHp = maxHp;
                    hpBar.SetState(currentHp, maxHp);
                    break;
                case Item.Type.Exp:
                    exp += item.value * gm.Pexp;
                    if (exp >= to_levelup_exp)
                    {
                        OpenLevelUp();
                        exp -= to_levelup_exp;
                        level += 1;
                    }
                    expBar.UpdateExpSlider(exp, to_levelup_exp);
                    break;
                case Item.Type.Coin:
                    Ucoin = item.value * gm.Pcoin;
                    coin += (int)Ucoin;
                    break;
            }
            other.gameObject.SetActive(false);
        }
    }

    //레벨업
    private void OpenLevelUp()
    {
        Time.timeScale = 0f;
        LevelUp.SetActive(true);
        gm.LevlUpSelection();
    }
    public void CloseLevelUp()
    {
        Time.timeScale = 1f;
        LevelUp.SetActive(false);
    }

    private void reseButton()
    {
        for (int i = 0; i < gm.LevelUp.Count; i++)
        {
            gm.LevelUp[i].gameObject.SetActive(false);
        }

    }
    public void Wpn_Whip()
    {
        Weapon[0]++;
        WeaponLevel[0]++;
        Time.timeScale = 1f;
        switch(WeaponLevel[0])
        {
            case 1:
                whip.whipDamage = 3;
                Twhip.text = string.Format("공격속도 증가");
                break;
            case 2:
                whip.timeToAttack = 2.5f;
                Twhip.text = string.Format("대미지 증가");
                break;
            case 3:
                whip.whipDamage = 3;
                Twhip.text = string.Format("양쪽 공격");
                break;
            default:
                whip.upgrade = true;
                Twhip.text = string.Format("레벨업 X");
                break;
        }
        reseButton();
        LevelUp.SetActive(false);
    }
    public void Wpn_Dagger()
    {
        Weapon[1]++;
        WeaponLevel[1]++;
        Time.timeScale = 1f;
        switch(WeaponLevel[1])
        {
            case 1:
                weapons[1].SetActive(true);
                dagger = GameObject.Find("Player/wpn_Dagger").GetComponent<DaggerWeapon>();
                Tdagger.text = string.Format("투사체 추가");
                break;
            case 2:
                dagger.level = 2;
                Tdagger.text = string.Format("공격속도 증가");
                break;
            case 3:
                dagger.timeToAttack -= 0.1f;
                Tdagger.text = string.Format("투사체 추가");
                break;
            case 4:
                dagger.level = 3;
                Tdagger.text = string.Format("최대 레벨");
                break;
            default:
                break;

        }
        reseButton();
        LevelUp.SetActive(false);
    }
    public void Wpn_Fireball()
    {
        Weapon[2]++;
        WeaponLevel[2]++;
        Time.timeScale = 1f;
        switch (WeaponLevel[2])
        {
            case 1:
                weapons[2].SetActive(true);
                fireball = GameObject.Find("Player/wpn_Fireball").GetComponent<FireballWeapon>();
                Tfireball.text = string.Format("대미지 1증가");
                break;
            case 2:
                fireballDamage = 4;
                Tfireball.text = string.Format("대미지 1증가");
                break;
            case 3:
                fireballDamage = 5;
                Tfireball.text = string.Format("대미지 1증가");
                break;
            default:
                Tfireball.text = string.Format("최대 레벨");
                break;
        }
        reseButton();
        LevelUp.SetActive(false);
    }
    public void Wpn_Circle()
    {
        Weapon[3]++;
        WeaponLevel[3]++;
        Time.timeScale = 1f;
        switch (WeaponLevel[3])
        {
            case 1:
                weapons[3].SetActive(true);
                circle = GameObject.Find("Player/wpn_Circle").GetComponent<CircleWeapon>();
                TCircle.text = string.Format("최대 레벨");
                break;
            default:
                TCircle.text = string.Format("최대 레벨");
                break;
        }
        reseButton();
        LevelUp.SetActive(false);
    }
    public void Wpn_Laser()
    {
        Weapon[4]++;
        WeaponLevel[4]++;
        Time.timeScale = 1f;
        switch (WeaponLevel[4])
        {
            case 1:
                weapons[4].SetActive(true);
                laser = GameObject.Find("Player/wpn_laser").GetComponent<LaserWeapon>();
                Tlaser.text = string.Format("공격속도 증가");
                break;
            case 2:
                laser.timetoAttack = 0.8f;
                Tlaser.text = string.Format("번개 추가");
                break;
            case 3:
                laser.level = 2;
                Tlaser.text = string.Format("번개 추가");
                break;
            default:
                Tlaser.text = string.Format("최대 레벨");
                break;
        }
        reseButton();
        LevelUp.SetActive(false);
    }
    public void Wpn_Rotation()
    {
        Weapon[5]++;
        WeaponLevel[5]++;
        Time.timeScale = 1f;
        weapons[5].SetActive(true);
        switch (WeaponLevel[5])
        {
            case 1:
                weapons[5].SetActive(true);
                rotation = GameObject.Find("wpn_Rotation").GetComponent<RotationWeapon>();
                Trotation.text = string.Format("투사체 추가");
                break;
            case 2:
                rotation.Level();
                Trotation.text = string.Format("투사체 추가");
                break;
            case 3:
                rotation.Level();
                Trotation.text = string.Format("투사체 추가");
                break;
            default:
                rotation.Level();
                Trotation.text = string.Format("최대 레벨");
                break;
        }
        reseButton();
        LevelUp.SetActive(false);
    }
}
