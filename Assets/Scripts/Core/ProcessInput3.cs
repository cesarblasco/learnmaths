using UnityEngine;
using System.Collections;

public class ProcessInput3 : MonoBehaviour {

    GameObject theChest;
    Openchest3 chestScript;

    // Use this for initialization
    void Start()
    {
        theChest = GameObject.Find("chests_2");
        chestScript = gameObject.GetComponent<Openchest3>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getResultFromChest1()
    {
        int x = chestScript.x;
        int y = chestScript.y;
        int result = chestScript.result;
        return result;
    }

    public void deleteObjects()
    {
        Destroy(chestScript.minusNumber1);
        Destroy(chestScript.minusNumber2);
        Destroy(chestScript.minusObj);
        Destroy(chestScript.number1Obj);
        Destroy(chestScript.number2Obj);
        Destroy(chestScript.equalsObj);
        Destroy(chestScript.inputFieldObj);
    }
}
