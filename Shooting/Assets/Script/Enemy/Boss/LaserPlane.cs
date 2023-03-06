using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserPlane : MonoBehaviour
{
    float time;
    [SerializeField] GameObject LaserWarning;
    [SerializeField] GameObject Misslie;
    int FireNum = 0;
    private void OnDrawGizmos() 
    {
        Debug.DrawRay(transform.position, -transform.up * int.MaxValue, Color.red);
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1.5f)
        {
            time = 0;
            Instantiate(Misslie,transform.position,transform.rotation);
            FireNum++;
            if (FireNum == 3)
            {
                Destroy(gameObject);
            }
            print(FireNum);
        }
        else if (time > 1)
        {

        }
        else if (time > 0.5f)
        {
            var vec = GameManager.instance.player.transform.position - transform.position;
            var deg = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, deg + 90);
            LaserWarning.SetActive(true);
        }
        else
        {
            var vec = GameManager.instance.player.transform.position - transform.position;
            var deg = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, deg + 90);
            LaserWarning.SetActive(false);
        }
    }
}
