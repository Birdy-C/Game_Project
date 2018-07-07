using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RunAway : Action
{
	public float saveDist = 10.0f;
	public SharedTransform enemy;
	private UnityEngine.AI.NavMeshAgent agent;

	public override void OnAwake() {
	  agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
	}

	public override TaskStatus OnUpdate() {
		float dist = Vector3.Distance(enemy.Value.position,transform.position);
	    if (dist > saveDist) {
	    	return TaskStatus.Success;
	    }
	    Vector3 mainDirec = (transform.position-enemy.Value.position)/dist*5.0f;
	    Vector3 bias = new Vector3(Random.Range(-2.0f, 2.0f), 0.0f, Random.Range(-2.0f, 2.0f));
	    agent.destination = transform.position + mainDirec + bias;
	    return TaskStatus.Running;
	}

}