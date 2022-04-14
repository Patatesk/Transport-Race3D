using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bariyer : MonoBehaviour
{
    bool aþaðý;
    bool yukarý;
    [SerializeField] GameObject parent;
    Vector3 x;
    Vector3 y;
    // Start is called before the first frame update
    void Start()
    {
        x = new Vector3(0, 0, -0.5f);
        y = new Vector3(0, 0, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        if (!yukarý)
        {
            StopAllCoroutines();
        }
        if (transform.localPosition.y <= -3.6)
        {
            yukarý = true;
            aþaðý = false;
            parent.gameObject.GetComponent<BoxCollider>().enabled = false;

        }
        else if (transform.localPosition.y >= 0)
        {
            yukarý = false;
            aþaðý = true;

        }
        if (yukarý)
        {
            StartCoroutine(bekle());
        }
        else if (aþaðý)
        {
            transform.Translate(x * Time.deltaTime);

        }
    }
    IEnumerator bekle()
    {
        yield return new WaitForSeconds(1);
        transform.Translate(y * Time.deltaTime);
        parent.gameObject.GetComponent<BoxCollider>().enabled = true;


    }
}
