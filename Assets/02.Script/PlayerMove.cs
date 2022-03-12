using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Vector3 foward;
    public bool jump = false;
    public SpriteRenderer flipRenderer;

    [SerializeField]
    private Rigidbody2D rb;
    WaitForSeconds wateTime = new WaitForSeconds(0.2f);
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        flipRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        foward = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += foward * speed * Time.deltaTime;

       if (Input.GetKeyDown(KeyCode.A) && jump==false)
       {
            jump = true;
            rb.AddForce(Vector2.up * 500);
            StartCoroutine(Goahead());                        
       }

    }
    WaitForSeconds waitTime = new WaitForSeconds(0.2f);


    public void StartFlip()
    {
        StartCoroutine(FlipFoward());
    }

    IEnumerator FlipFoward()
    {
        yield return waitTime;
        if(foward == Vector3.right)
        {
            flipRenderer.flipX = false;
        } else if (foward == Vector3.left)
        {
            flipRenderer.flipX = true;
        }
    }

    IEnumerator Goahead()
    {
        yield return wateTime;
        jump = false;
    }
}
