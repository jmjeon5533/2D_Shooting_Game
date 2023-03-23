using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Earth : MonoBehaviour
{
    [SerializeField] RectTransform[] ScrollTab;
    [SerializeField] float RotateSpeed;
    float direction = 450;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    void Rotate()
    {
        transform.Rotate(Vector3.left * RotateSpeed * Time.deltaTime);
    }
}
