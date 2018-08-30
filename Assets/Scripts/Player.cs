using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        OnHorizontalControl();
	}

    //水平移动控制
    void OnHorizontalControl() {
        Vector3 acc = Vector3.zero;
        Vector3 diff;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            acc.x = -0.1f;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            acc.x = 0.1f;
            transform.localScale = new Vector3(1, 1, 1);
        }

        Debug.Log(Time.time);
        diff = Vector3.MoveTowards(transform.localPosition, transform.localPosition + acc, 0.5f * Time.time);
        transform.localPosition = diff;
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
