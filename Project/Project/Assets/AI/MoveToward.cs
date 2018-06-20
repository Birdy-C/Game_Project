using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveToward : Action
{
	public SharedTransform target;
	public float StopDist; //停下来时与目标间隔的距离，请务必考虑高度差
	public float interval = 4.0f;
	private UnityEngine.AI.NavMeshAgent agent;
	float timer;

	public override void OnAwake() {
	    agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
	    timer = interval;
	}

	public override TaskStatus OnUpdate() {
		//Debug.Log(Vector3.Distance(transform.position,target.Value.position));
	    if (Vector3.Distance(transform.position,target.Value.position) < StopDist) {
	    	agent.destination = transform.position;
	    	Debug.Log("Reached");
	        return TaskStatus.Success;
	    }
	    timer += Time.deltaTime;
	    if(timer>interval) {
	   	    timer = 0;
	  	    if (target!=null) {
	  	        //Debug.Log(target);
	  	   	    agent.destination = target.Value.position;
	  	    }
	    }
	    return TaskStatus.Running;
	}

}