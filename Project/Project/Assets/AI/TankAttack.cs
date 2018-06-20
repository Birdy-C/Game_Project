using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TankAttack : Action
{
	// The transform that the object is moving towards
	public SharedTransform target;
	private AITankShooting shootingAgent;

	public override void OnAwake() {
	    shootingAgent = GetComponent<AITankShooting>(); 
	}

	public override TaskStatus OnUpdate() {
	    // Return a task status of success once we've reached the target
	    transform.LookAt(target.Value);
	    if (shootingAgent.b_idle()) {
	    	float dist = (transform.position - target.Value.position).magnitude;
	    	//Debug.Log(dist);
	    	shootingAgent.AIFire(dist);
	    }
	    return TaskStatus.Success;
	}

}