using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidthAdjust : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Resize();
	}

    void Resize() {
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        float targetWidth = 2 * Camera.main.orthographicSize * Screen.width / Screen.height;
        Vector3 scale = transform.localScale;
        scale.x = targetWidth / width;
        transform.localScale = scale;
    }
}
