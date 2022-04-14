using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireTurn : MonoBehaviour
{
    [SerializeField] Player Player;
    Quaternion objRot;
    Quaternion targetRot;
    void Start()
    {
        //Player = GetComponent<Player>();
        objRot = transform.rotation;
    }

    void Update()
    {
        if (Player.mouseDif.x < 0)
        {
            targetRot = Quaternion.Euler(0, -20, 0);
            transform.rotation = Quaternion.Lerp(objRot, targetRot, 10 * Time.deltaTime);
        }
        else if (Player.mouseDif.x > 0)
        {
            targetRot = Quaternion.Euler(0, 20, 0);
            transform.rotation = Quaternion.Slerp(objRot, targetRot, 10 * Time.deltaTime);
        }
        else if (Player.mouseDif.x == 0)
        {
            targetRot = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Lerp(objRot, targetRot, 10 * Time.deltaTime);
        }


    }
}
