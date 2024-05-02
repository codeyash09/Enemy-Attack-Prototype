using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attacks")]
    public bool machineGun;
    public bool paperCut;
    public bool toxic;
    public bool laserBeam;

    [Header("GameObjects Needed for Attacks")]
    public GameObject staple;
    public GameObject slashMark;
    public GameObject laser;

    [Header("Other Important Things")]
    public GameObject player;
    public Transform firePoint;

    Dictionary<int, string> attacks = new Dictionary<int, string>();

    public int myAttack;


    //Timers and Counters
    float attackTimer;
    int attackCounter;

    float reload;
    float counter;


    //Single Use Variables (Will Try to Optimize Later)
    bool hasUsedAttack;
    bool direction;


    // Start is called before the first frame update
    void Start()
    {
        int index = 0;

        attacks.Clear();
        if (machineGun)
        {
            attacks.Add(index, "MachineGun");
            index++;
        }
        if (paperCut)
        {
            attacks.Add(index, "PaperCut");
            index++;

        }
        if (toxic)
        {
            attacks.Add(index, "Toxic");
            index++;

        }
        if (laserBeam)
        {
            attacks.Add(index, "LaserBeam");
            index++;

        }
        attacks.Add(index, "Rest");


        myAttack = FindAttackInDictionary("Rest");


        attackTimer = 3f;
        attackCounter = 3;

        hasUsedAttack = false;
    }

    public int FindAttackInDictionary(string name)
    {
        int key = -1;

        if (attacks.ContainsValue(name))
        {
            foreach(KeyValuePair<int, string> attack in attacks)
            {
                if(attack.Value == name)
                {
                    key = attack.Key;
                }
             
            }


        }


        return key;


    }

    public string FindAttackInDictionary(int value)
    {
        string attackName = "";

        if (attacks.ContainsKey(value))
        {
            foreach (KeyValuePair<int, string> attack in attacks)
            {
                if (attack.Key == value)
                {
                    attackName = attack.Value;
                }

            }


        }


        return attackName;


    }

    // Update is called once per frame
    void Update()
    {



        counter++;
        if(counter >= 360f)
        {
            counter = 0f;
        }
        attackTimer -= Time.deltaTime;

        


        if(attackCounter <= 0)
        {
            laser.SetActive(false);
            laser.transform.localScale = new Vector3(0, 1, 1);
            firePoint.transform.localPosition = new Vector3(0.5f, 0, 0);
            firePoint.localEulerAngles = Vector3.zero;
            myAttack = FindAttackInDictionary("Rest");
            if(attackTimer <= 0)
            {
                direction = RandomBool();

                ChooseAttack();
                attackTimer = 3f;
                attackCounter = 3;
            }
        }
        else
        {
            if (attackTimer <= 0)
            {
                ChooseAttack();
                attackTimer = 3f;
                attackCounter -= 1;
                direction = RandomBool();
                firePoint.transform.localPosition = new Vector3(0.5f, 0, 0);
                firePoint.localEulerAngles = Vector3.zero;
                laser.SetActive(false);
                laser.transform.localScale = new Vector3(0, 1, 1);

            }
        }

        Attack(myAttack);


    }

    public bool RandomBool()
    {
        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;
    }


public void ChooseAttack()
    {
        string attackName = FindAttackInDictionary(Random.Range(0, attacks.Count-1));

        myAttack = FindAttackInDictionary(attackName);
        direction = RandomBool();

        hasUsedAttack = false;
        laser.SetActive(false);
        laser.transform.localScale = new Vector3(0, 1, 1);
        firePoint.transform.localPosition = new Vector3(0.5f, 0, 0);
        firePoint.localEulerAngles = Vector3.zero;
    }

    public void Attack(int attackIndex)
    {
        switch (FindAttackInDictionary(attackIndex))
        {
            case "MachineGun":
                Machine();
                break;
            case "PaperCut":
                PaperCut();
                break;
            case "Toxic":
                Debug.Log("");
                break;
            case "LaserBeam":
                LaserBeam();
                break;
            default:
                break;
        }
    }

    public void Machine()
    {

        reload -= Time.deltaTime;
        Vector3 rot = transform.InverseTransformPoint(player.transform.position);
        float angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        transform.Rotate(0, 0, angle / 10f);


        GameObject bullet = Instantiate(staple, firePoint);

        firePoint.transform.position = new Vector3(0, Mathf.Sin(counter) * 0.5f, 0);


        if (reload <= 0)
        {
            bullet.transform.parent = null;
            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * 15f;
            
            reload = 0.05f;
        }
    }

    public void PaperCut()
    {
        if (!hasUsedAttack)
        {
            for(int i = 0; i < 3; i++)
            {
                firePoint.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
                firePoint.transform.Rotate(0, 0, Random.Range(0, 360));

                GameObject slash = Instantiate(slashMark, firePoint);
                slash.transform.parent = null;
                firePoint.transform.localPosition = new Vector3(0.5f, 0, 0);
                firePoint.localEulerAngles = Vector3.zero;
                

            }
            hasUsedAttack = true;
        }
    }

    public void LaserBeam()
    {

        if (direction)
        {
            transform.Rotate(0, 0, 0.3f);
        }else
        {
            transform.Rotate(0, 0, -0.3f);

        }

        laser.SetActive(true);
        laser.transform.localScale = new Vector3(Mathf.Lerp(laser.transform.localScale.x, 1, 0.001f), 1, 1);
    }
}
