﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehavior : MonoBehaviour
{

    public int lifeTime = 30;

    public string target;

    public int damage = 30;

    public bool piercing = false;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        lifeTime--;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(target))
        {
            Health targethealth = other.GetComponent<Health>();
            if (targethealth)
            {
                targethealth.DamageHealth(damage);
            }

            if (!piercing)
            {
                Destroy(this.gameObject);
            }
        }
        else if (other.CompareTag("Ground") && !piercing)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        //Special effect
    }
}
