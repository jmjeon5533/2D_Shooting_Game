using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("¸Ê Á¦ÇÑ")]
    public Vector2 Clamp;
    [Header("½ºÅÝ")]
    public float MoveSpeed;
    [SerializeField] GameObject[] Bullet;
    [SerializeField] Transform[] BulletPos;
    [Header("¹ß»ç °ü·Ã")]
    public float Cooltime;
    float Curtime;

    int WeaponLevel = 1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //Fire();
    }
    void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(h, v).normalized * MoveSpeed * Time.deltaTime);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -Clamp.x, Clamp.x), Mathf.Clamp(transform.position.y, -Clamp.y, Clamp.y));
    }
    void Fire()
    {
        Curtime += Time.deltaTime;
        if (Curtime >= Cooltime)
        {
            Curtime = 0;
            switch (WeaponLevel - 1)
            {
                case 0:
                    {
                        Instantiate(Bullet[0], BulletPos[0].position,Quaternion.identity);
                    }
                    break;
                case 1:
                    {

                        break;
                    }
            }
        }
    }
}
