using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSpaceWorld : HealthBar
{
    //public Slider slider;
    //public Gradient gradient;
    //public Image fill;
    public GameObject player;

    private void LateUpdate()
    {
        if (player != null)
        {
            RotateTowardPlayer();
        }
    }

    private void RotateTowardPlayer()
	{
		// Determine which direction to rotate towards
		Vector3 targetDirection = player.transform.position - transform.position;

		// The step size is equal to speed times frame time.
		float singleStep = 3.5f * Time.deltaTime;

		// Rotate the forward vector towards the target direction by one step
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

		// Draw a ray pointing at our target in
		Debug.DrawRay(transform.position, newDirection, Color.red);

		// Calculate a rotation a step closer to the target and applies rotation to this object
		transform.rotation = Quaternion.LookRotation(newDirection);
	}

	

}
