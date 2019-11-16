using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Openchest3 : MonoBehaviour {

    Animator anim, animPlayer;
    public GameObject number1Obj,number2Obj,
        minusObj, equalsObj, minusNumber1, minusNumber2, gate, coin;
    Gateunlock gateScript;
    AudioSource audio, audioCorrect, audioMistake;
    public GameObject player, inputFieldObj;
    bool xIsNegative, yIsNegative;
    public int x, y, result, userInput;
    InputField input;
    InputField.SubmitEvent se;




    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
        audio = allMyAudioSources[0];
        audioCorrect = allMyAudioSources[1];
        audioMistake = allMyAudioSources[2];
        player = GameObject.Find("Player");
        animPlayer = player.GetComponent<Animator>();
        inputFieldObj = GameObject.Find("InputField3");
        input = inputFieldObj.GetComponent<InputField>();
        se = new InputField.SubmitEvent();
        se.AddListener(SubmitInput);
        input.transform.position = new Vector3(-5000, 0, 0);
        input.onEndEdit = se;
        gate = GameObject.Find("gate_0");
        gateScript = gate.GetComponent<Gateunlock>();
        //input.ActivateInputField();
        result = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (anim.GetBool("opened") == false)
            {
                anim.SetBool("opened", true);
                audio.Play();
                animPlayer.SetBool("question", true);
                x = Random.Range(-9, 9);
                y = Random.Range(-9, 9);
                int displacement = 0;
                if (x < 0)
                {
                    minusNumber1 =
                        Instantiate(Resources.Load("Prefabs/Minus")) as GameObject;
                    minusNumber1.transform.position = new Vector3(-8, 0, 0);
                    xIsNegative = true;
                    x *= -1;
                }
                if (y < 0)
                {
                    //displacement = -2;
                    minusNumber2 =
                        Instantiate(Resources.Load("Prefabs/Minus")) as GameObject;
                    minusNumber2.transform.position = new Vector3(displacement -2 , 0, 0);
                    yIsNegative = true;
                    y *= -1;
                }
                else
                {
                    displacement = -2;
                }
                number1Obj = Instantiate(Resources.Load("Prefabs/Number" + x), new Vector3(-6, 0, 0), Quaternion.identity) as GameObject;
                minusObj =
                GameObject.Instantiate(Resources.Load("Prefabs/Minus")) as GameObject;
                minusObj.transform.position = new Vector3(-4, 0, 0);
                number2Obj = Instantiate(Resources.Load("Prefabs/Number" + y), new Vector3(displacement - 0, 0, 0), Quaternion.identity) as GameObject;
                equalsObj =
                Instantiate(Resources.Load("Prefabs/equals")) as GameObject;
                equalsObj.transform.position = new Vector3(2, 0, 0);
                if (xIsNegative)
                {
                    x *= -1;
                }

                if (yIsNegative)
                {
                    y *= -1;
                }
                result = x - y;
                input.transform.position = new Vector3(1050, 300, 0);
            }
        }
    }


    private void SubmitInput(string arg0)
    {
        int userInput;
        if (int.TryParse(arg0, out userInput))
        {
            GameObject go = GameObject.Find("chests_2");
            ProcessInput3 scriptPI = (ProcessInput3)go.GetComponent(typeof(ProcessInput3));
            int result = scriptPI.getResultFromChest1();
            if (userInput != result)
            {
                input.text = "";
                audioMistake.Play();
            }
            else
            {
                audioCorrect.Play();
                input.text = "";
                input.DeactivateInputField();
                scriptPI.deleteObjects();
                animPlayer.SetBool("question", false);
                gateScript.addCounter();
                input.transform.position = new Vector3(-5000, 300, 0);
                if (result < 0)
                {
                    result *= -1;
                }
                for (int i = 0; i < result; i++)
                {  
                    coin = Instantiate(Resources.Load("Prefabs/Coin")) as GameObject;
                    coin.transform.position = new Vector3(Random.Range(-9,8), Random.Range(-4,0), 0);
                }
            }
        }
    }  
}
