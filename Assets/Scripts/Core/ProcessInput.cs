using UnityEngine;
using System.Collections;

public class ProcessInput : MonoBehaviour 
{
    Openchest1 chestScript;
    

	// Use this for initialization
	void Start () {
        chestScript = gameObject.GetComponent<Openchest1>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int getResultFromChest1()
    {
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
