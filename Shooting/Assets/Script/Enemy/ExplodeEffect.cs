using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffect : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject,1.8f);
    }
}
