using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_Script : MonoBehaviour
{
    public float speed = 0.0f;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        this.rigidbody.velocity = new Vector2(speed, 0);

        float parentPosition = this.transform.parent.transform.position.x;

        if(this.transform.position.x - parentPosition >= 25.65f)
        {
            this.transform.position = new Vector3(parentPosition - 25.541f, this.transform.position.y, this.transform.position.z);
        }
    }
}
