using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float followSmooth = 5;
    private Vector3 refVelocity;

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow() {
        if (target == null)
        {
            return;
        }
        Vector3 pos = Camera.main.WorldToViewportPoint(target.position);
        Vector3 middle = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, pos.z));
        Vector3 diff = target.position - middle;
        Vector3 dest = transform.position + diff;
        dest.x = 0;

        //diff = new Vector3(transform.position.x, diff.y, transform.position.z);

        if (dest.y > transform.position.y)
        {
            transform.position = Vector3.SmoothDamp(transform.position, dest, ref refVelocity, followSmooth * Time.deltaTime);
            Debug.Log(refVelocity);
        }
    }
}
