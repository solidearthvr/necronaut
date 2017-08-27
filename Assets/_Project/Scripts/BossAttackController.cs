﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour {

    public GameObject bulletPrefab;

    /// <summary>
    /// Holds each of the bullets: (GameObject, AttackStyle, aimDirection)
    /// </summary>
    public static List<Bullet> bullets = new List<Bullet>();
    public float timeBetweenFiring = 3f;
    public float timeUntilStart = 10f;
    public float timeBetweenAttackChange = 15f;

    //apply to gun
    // TODO: add other attack styles
    public enum AttackStyle { fire, blob, swarm, lightning, shockwave, bomb }
    public AttackStyle attackStyle = AttackStyle.fire;
    public int bulletNumber = 1;
    public float inaccuracy = 0;
    public int heat = 30;
    public int cooldown = 0;

    public int chargePercent = 0;
    public int windupReq = 60;

    //apply to bullet iself
    public float size = 1; //currently does nothing
    public float fireForce = 1000;
    public float bulletSpeed = 0.2f;
    public float startVelocity = 10;

    private Quaternion aimDirection;
    private GameObject player;
    private GameObject[] shots;

    /// <summary>
    ///  Implements a bullet game object class.
    /// </summary>
    public class Bullet {
        public GameObject obj;
        public AttackStyle attackStyle;
        public Quaternion aimDirection;
        public GameObject targetObj;
        public float velocity;

        public Bullet (GameObject go, AttackStyle style, Quaternion dir, GameObject targ, float startingVelocity)
        {
            obj = go;
            attackStyle = style;
            aimDirection = dir;
            targetObj = targ;
            velocity = startingVelocity;
        }
    }

	// Use this for initialization
	void Start () {
        if (bulletPrefab == null)
            Debug.LogError("bullet is not defined!!");

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.LogError("couldn't find player!!");

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        float startTime = Time.fixedTime;
        float curTime = startTime;
        while (true)
        {
            if (Time.fixedTime - startTime > timeBetweenAttackChange)
            {
                // TODO: implement attack change
            }

            // Aim bullet in player's direction.
            aimDirection = Quaternion.LookRotation(player.transform.position);
            yield return new WaitForSeconds(timeBetweenFiring);
            if (attackStyle == AttackStyle.fire)
            {
                fireBullet(aimDirection,player);
            }
        }
    }

    /// <summary>
    /// Instantiates a new bullet from the bulletPrefab and adds to the bullets list
    /// </summary>
    /// <param name="aimDir"></param>
    /// <param name="target"></param>
    void fireBullet(Quaternion aimDir,GameObject target)
    {
        GameObject bulletGO = Instantiate(bulletPrefab,transform.position,aimDir);
        Bullet bullet = new Bullet(bulletGO, attackStyle, aimDir,target, startVelocity);
        bullets.Add(bullet);
    }
        // Update is called once per frame
    void Update()
    {
        foreach (var bullet in bullets)
        {
            // Move the projectile forward towards the player's last known direction;
            bullet.obj.transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        }
    }
    //playerDir = transform.position


    public void attackChange(AttackStyle attack)
    {

        attackStyle = attack;
        switch (attack) //Update gun stats here
        {
            case AttackStyle.fire:
                {
                    bulletNumber = 1;
                    inaccuracy = 0;
                    heat = 20;
                    windupReq = 0;
                    size = 1;
                    fireForce = 1000;
                    break;
                }


        }
        //shot = shots[(int)attackStyle];
    }


}