﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTECircleScript : MonoBehaviour {

    public float LifeTime = 1.0f;
    private float _startTime;

    [HideInInspector]
    public string _qteType;

    [SerializeField]
    public GameObject FatherObject;

	// Use this for initialization
	void Start ()
    {
        _startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if ( Time.time - _startTime >= LifeTime )
        {
            if (_qteType == "Enemy")
            {
                FatherObject.GetComponent<EnemyController>().CancelQTE();
            }
            else if (  _qteType == "Dog")
            {
                Time.timeScale = 1.0f;
            }

            Destroy(gameObject);
        }
	}

    void OnMouseUp()
    {
        Debug.Log("klik");
        GameObject.FindGameObjectWithTag("Player").GetComponent<FightSystem>().ClickedTheCircle = true;
        if ( _qteType == "Enemy" )
        {
            FatherObject.GetComponent<EnemyController>().SetQTETimeStamp();
            Destroy(gameObject);
        }
        else if (_qteType == "Dog")
        {
            FatherObject.GetComponent<WildDogScript>().ClickedTheCircle = true;
            Time.timeScale = 1.0f;
            Destroy(gameObject);
        }
    }
}
