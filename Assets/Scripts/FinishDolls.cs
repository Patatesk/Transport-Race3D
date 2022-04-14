using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishDolls : MonoBehaviour
{
    Quaternion start;
    Quaternion end;
    Animator anim;
   
    void Start()
    {
        start = transform.rotation;
        end = Quaternion.Euler(0, 90, 0);
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (gameObject.tag == "Right")
        {
            if (transform.position.x <=3.82f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(4, transform.position.y, transform.position.z), 2 * Time.deltaTime);

            }
            else if (transform.position.x >= 3.80f)
            {
                anim.SetBool("Run", false);
                transform.DORotate(new Vector3(0,-90,0),1);
                anim.SetBool("Dance", true);

            }
            
        }
        else if (gameObject.tag== "Left")
        {
            if (transform.position.x >= -3.82f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(-4, transform.position.y, transform.position.z), 2 * Time.deltaTime);

            }
            else if (transform.position.x <= -3.80f)
            {
                anim.SetBool("Run", false);
                transform.DORotate(new Vector3(0, 90, 0), 1);
                anim.SetBool("Dance", true);

            }
            

        }

    }

   
}
