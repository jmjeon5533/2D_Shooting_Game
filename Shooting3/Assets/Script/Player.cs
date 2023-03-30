using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("¸Ê Á¦ÇÑ")]
    public Vector2 Clamp;
    [Header("½ºÅÝ")]
    public float MoveSpeed;
    public float HP, MaxHP;
    [Space(5)]
    public float Fuel;
    public float MaxFuel;
    [Header("ÇÁ¸®ÆÕ")]
    [SerializeField] GameObject[] Bullet;
    [SerializeField] Transform[] BulletPos;
    [Header("¹ß»ç °ü·Ã")]
    public float Cooltime;
    float Curtime;
    void Start()
    {
        MaxHP = HP;
        MaxFuel = Fuel;
    }

    // Update is called once per frame
    void Update()
    {
        var g = GameManager.instance;
        if (!g.IsClear || !g.IsClearMove)
        {
            Movement();
            if(Input.GetKey(KeyCode.Space))
            Fire();
        }
        else if (g.IsClearMove)
        {
            transform.Translate(new Vector3(0, 1) * MoveSpeed * Time.deltaTime);
            if (transform.position.y >= 8)
            {
                
                gameObject.SetActive(false);
            }
        }
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
            switch (GameManager.WeaponLevel)
            {
                case 0:
                    {
                        Instantiate(Bullet[0], BulletPos[0].position,Quaternion.identity);
                    }
                    break;
                case 1:
                    {
                        Instantiate(Bullet[0], BulletPos[1].position, Quaternion.identity);
                        Instantiate(Bullet[0], BulletPos[2].position, Quaternion.identity);
                        break;
                    }
                case 2:
                    {
                        Instantiate(Bullet[1], BulletPos[0].position, Quaternion.identity);
                        Instantiate(Bullet[0], BulletPos[1].position, Quaternion.identity);
                        Instantiate(Bullet[0], BulletPos[2].position, Quaternion.identity);
                        break;
                    }
            }
        }
    }
    public void Damage(float Damage)
    {
        HP -= Damage;
    }
}
