using UnityEngine;
using System.Collections;

public class ProcessInput2 : MonoBehaviour {

    Openchest2 chestScript;

    // Use this for initialization
    void Start()
    {
        chestScript = gameObject.GetComponent<Openchest2>();      
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
