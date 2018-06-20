using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CheeseAvailable : Conditional {

	public SharedTransform target;
	private Cheese cheeseAgent;
	
	public override TaskStatus OnUpdate () {
		cheeseAgent = target.Value.GetComponent<Cheese>();
		if (cheeseAgent.b_alive){
			Debug.Log("Cheese available");
			return TaskStatus.Success;
		}
		else {
			Debug.Log("Cheese not available");
			return TaskStatus.Failure;
		}
	}
}
