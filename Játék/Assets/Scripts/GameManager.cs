using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;

        
    }
    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    //public RectTransform hitpointBar;
    public GameObject hud;
    public GameObject menu;
    public Animator deathMenuAnim;
    public Animator menuAnim;


    //Logic
    public int pesos;
    public int experience;


    //floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max lvl
        if(weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Xp
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if(r == xpTable.Count)//max lvl
                return r;
        }
        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while(r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitpointChange();
    }

    //Save
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    //OnSceneLoaded
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;

    }

    //Death menu and respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        menuAnim.SetTrigger("hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        weapon.SetWeaponLevel(0);
        experience = 0;
        pesos = 0;
        player.hitpoint = 10;
        player.maxHitpoint = 10;
        player.Respawn();
    }

    public void Spawn()
    {
        //menuAnim.SetTrigger("hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        weapon.SetWeaponLevel(0);
        experience = 0;
        pesos = 0;
        player.hitpoint = 10;
        player.maxHitpoint = 10;
        player.Respawn();
    }

    public void Start(){
        weapon.SetWeaponLevel(0);
        experience = 0;
        pesos = 0;
        player.hitpoint = 10;
        player.maxHitpoint = 10;
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {

        SceneManager.sceneLoaded -= LoadState;

        if(!PlayerPrefs.HasKey("SaveState"))
            return;

       string[] data = PlayerPrefs.GetString("SaveState").Split('|');

       //Change player skin 
       pesos = int.Parse(data[1]);
       //xp
       experience = int.Parse(data[2]);
       if(GetCurrentLevel() != 1)
       {
         player.SetLevel(GetCurrentLevel());
       }
       //weaponlevel
       weapon.SetWeaponLevel(int.Parse(data[3]));
    }

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public void OnHitpointChange()
    {
        if(player.hitpoint > player.maxHitpoint){
            player.hitpoint = player.maxHitpoint;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < player.hitpoint)
            {
                hearts[i].sprite = fullHeart;
            }
            else{
                hearts[i].sprite = emptyHeart;
            }
            if(i < player.maxHitpoint){
                hearts[i].enabled = true;
            }else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void Update(){
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < player.maxHitpoint){
                hearts[i].enabled = true;
            }else
            {
                hearts[i].enabled = false;
            }
        }

    }
}
