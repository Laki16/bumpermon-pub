using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Character {

    public ParticleSystem particleSystem;
    private float speed;

	private void Start()
    {
        speed = GetComponent<PlayerController>().speed;
	}

	private void Update()
    {
        speed = GetComponent<PlayerController>().speed;
        var main = particleSystem.main;
        main.simulationSpeed = speed / 100.0f;
	}
}
