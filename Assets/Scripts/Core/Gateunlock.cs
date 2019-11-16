using UnityEngine;
using System.Collections;

public class Gateunlock : MonoBehaviour {

	// Use this for initialization
    private Animator anim;
    private GameObject gateColliderObj;
    private BoxCollider2D gateCollider;
    public int counter;
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        gateColliderObj = GameObject.Find("Gate Collider");
        gateCollider = gateColliderObj.GetComponent<BoxCollider2D>();
        counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetInteger("Unlocked", counter);
	}

    public void addCounter()
    {
        counter++;
        if (counter == 3)
        {
            Destroy(gateCollider);
        }
    }


}
