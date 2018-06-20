using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RandomMove : Action
{
	private UnityEngine.AI.NavMeshAgent agent;
	public float interval = 8.0f;
	public float range = 20.0f;
	float timer;

	public override void OnAwake() {
	  agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
	  timer = 0;
	}

	public override TaskStatus OnUpdate() {
		if (timer==0) {
	  	    //有高度差时该怎么办？
			Vector3 direc = new Vector3(Random.Range(-range, range), 0.0f, Random.Range(-range, range));
			agent.destination = transform.position + direc;
		}
		timer += Time.deltaTime;
	    if(timer>=interval) {
	    	timer = 0;
	   	    return TaskStatus.Success;
	    }
	    return TaskStatus.Running;
	}
}