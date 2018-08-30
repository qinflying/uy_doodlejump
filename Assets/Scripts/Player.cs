using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D oCollider) {
        if (oCollider.tag == "platform") {
            ToJumpAction();
        }
    }

    void ToJumpAction(float factor = 1f) {
        //跳起之前清空之前的速度
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 12 * factor), ForceMode2D.Impulse);
    }
}
