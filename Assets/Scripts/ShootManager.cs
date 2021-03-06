﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour {

    public int HowManyBullets = 2;

    private LineRenderer _shootLine;

    public float LineLength = 5.0f;

    public float LineVisibilityTime = 1.0f;
    private float _sideTimeStamp;

    private bool _shoot = false;

	// Use this for initialization
	void Start () {
        _shootLine = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Q) && !_shoot && HowManyBullets > 0)
        {
            GetComponentInChildren<Animator>().SetBool("Shoot", true);

            HowManyBullets--;

            Vector3[] _vectors = new Vector3[2];

            Vector2 _start;

            if(transform.localScale.x < 0)
            {
                _start = new Vector2(transform.position.x - 1.0f, transform.position.y + 0.5f);

                _vectors[1] = _start;
                _vectors[0] = _start - new Vector2( LineLength, 0f);
            }
            else
            {
                _start = new Vector2(transform.position.x + 1.0f, transform.position.y + 0.5f);

                _vectors[0] = _start;
                _vectors[1] = _start + new Vector2(LineLength, 0f);
            }

            _shootLine.SetPositions(_vectors);

            int layerMask = 1 << LayerMask.NameToLayer("Enemy");

            RaycastHit2D hit = Physics2D.Raycast(_start, new Vector2(1.0f, 0f), LineLength, layerMask);
            if (hit.collider != null)
            {
                if( hit.collider.gameObject.tag == "EnemyHead" )
                {
                    Debug.Log("hit");
                    hit.collider.GetComponentInParent<EnemyController>().GotShoot();
                }
                else
                {
                    Debug.Log(hit.collider.gameObject.tag);
                }

            }
            else
            {
                Debug.Log(" nulllll");
            }
            _shootLine.enabled = true;
            _shoot = true;
            _sideTimeStamp = Time.time;
        }

        if ( _shoot )
        {
            if ( Time.time - _sideTimeStamp >= LineVisibilityTime )
            {
                _shootLine.enabled = false;
                _shoot = false;
            }
        }

    }
}
