using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    //public float speed = 50f;
    public int maxSpeed = 15;
    public bool grounded;
    public bool facingRight;
    public int coinsCounter, swordPurchased, shieldPurchased, crownPurchased;
    private Rigidbody2D rb2D;
    private Animator anim;
    private AudioSource coinCollected, purchase, mistake;
    private GameObject coinTextObj, swordObj, shieldObj, crownObj, modalDialogObj, yesButton, modalPanel;
    private Text coinText;
    private bool isDialogOpen = false;

    private ModalPanel modalPanelScript;

    // Use this for initialization
    void Start () {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
        coinCollected = allMyAudioSources[0];
        purchase = allMyAudioSources[1];
        mistake = allMyAudioSources[2];
        coinTextObj = GameObject.Find("labelCoins");
        coinText = coinTextObj.GetComponent<Text>();
        coinsCounter += PlayerPrefs.GetInt("Coins");
        swordPurchased = PlayerPrefs.GetInt("Sword Purchased");
        shieldPurchased = PlayerPrefs.GetInt("Shield Purchased");
        crownPurchased = PlayerPrefs.GetInt("Crown Purchased");
        swordObj = GameObject.Find("sword");
        shieldObj = GameObject.Find("shield");
        crownObj = GameObject.Find("crown");
        modalPanelScript = gameObject.GetComponent<ModalPanel>();

        if (swordPurchased == 1)
        {
            Destroy(swordObj);
        }
        if (shieldPurchased == 1)
        {
            Destroy(shieldObj);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (crownPurchased == 1)
        {
            crownObj.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 0);
        }
	}

    void FixedUpdate()
    {
        coinText.text = "Coins: " + coinsCounter;
        if(anim.GetBool("question") == false && !isDialogOpen){
           if(Input.GetKey(KeyCode.D)){
                anim.SetBool("facingRight", true);
                anim.SetInteger("Speed", 1);
                rb2D.velocity = new Vector2(maxSpeed * Time.deltaTime * 15, rb2D.velocity.y);
           }
           else if(Input.GetKey(KeyCode.A))
           {
                 anim.SetBool("facingRight", false);
                 anim.SetInteger("Speed", 1);
                 rb2D.velocity = new Vector2(-maxSpeed * Time.deltaTime * 15, rb2D.velocity.y);
           }
            else if(Input.GetKey(KeyCode.W)){
                anim.SetInteger("Speed", 1);
                rb2D.velocity = new Vector2(rb2D.velocity.x, maxSpeed * Time.deltaTime * 15);
            }  
           else if(Input.GetKey(KeyCode.S)){
               anim.SetInteger("Speed", 1);
               rb2D.velocity = new Vector2(rb2D.velocity.x, -maxSpeed * Time.deltaTime * 15);
           }
           else
           {
               anim.SetInteger("Speed", 0);
               rb2D.velocity = new Vector2(0, 0);
           }

           if (Input.GetKeyDown(KeyCode.Return))
           {
                PlayerPrefs.SetInt("Coins", 0);
                PlayerPrefs.SetInt("Sword Purchased", 0);
                PlayerPrefs.SetInt("Shield Purchased", 0);
                PlayerPrefs.SetInt("Crown Purchased", 0);
                coinsCounter = 0;
                PlayerPrefs.Save();
                Application.LoadLevel(0);
               
           }

           if (Input.GetKeyUp(KeyCode.F1))
           {
               PlayerPrefs.SetInt("Coins", coinsCounter);
               PlayerPrefs.Save();
               Application.LoadLevel(0);
           }

           if (Input.GetKey(KeyCode.Space) && swordPurchased == 1)
           {
               anim.SetBool("Attacking", true);   
           }

           if (Input.GetKeyUp(KeyCode.Space))
           {
               anim.SetBool("Attacking", false);
           }
       }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "Coin")
        {
            Destroy(coll.gameObject);
            coinCollected.Play();
            coinsCounter++;       
        }
        else if (coll.gameObject.tag == "Swords")
        {
            if (coinsCounter >= 10)
            {
                swordPurchased = 1;
                PlayerPrefs.SetInt("Sword Purchased", 1);
                PlayerPrefs.SetInt("Shield Purchased", 1);
                coinsCounter -= 10;
                Destroy(coll.gameObject);
                Destroy(shieldObj);
                purchase.Play();
            }
            else
            {
                if (coinsCounter < 10)
                {
                    mistake.Play();
                }
            }
            PlayerPrefs.SetInt("Coins", coinsCounter);
        }
        else if (coll.gameObject.tag == "Throne" && crownPurchased == 0)
        {
            if (coinsCounter >= 50)
            {
                crownPurchased = 1;
                PlayerPrefs.SetInt("Crown Purchased", 1);
                coinsCounter -= 50;
                purchase.Play();
            }
            else
            {
                if (coinsCounter < 50)
                {
                    mistake.Play();
                }
            }
        }
        PlayerPrefs.Save();
    }
}
