using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    private Animator animator;
    public Transform throwPoint;
    public GameObject fireBall;
    private float timeBetweenThrows = 3f;
    private float throwTimer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //throwPoint = transform;
        throwTimer = 2f; 
    }

    // Update is called once per frame
    void Update()
    {
        throwTimer += Time.deltaTime;
        if (throwTimer > timeBetweenThrows)
        {
            animator.SetTrigger("Throw");
            //ThrowFireBall();
            throwTimer = 0f;
        }
    }
    void ThrowFireBall()
    {
       Instantiate(fireBall, throwPoint.position, throwPoint.rotation); 
    }
}
