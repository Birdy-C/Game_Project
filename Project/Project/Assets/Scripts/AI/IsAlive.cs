using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsAlive : Conditional {

	public SharedTransform target;
	
	public override TaskStatus OnUpdate () {
		bool b_dead = target.Value.GetComponent<TankHealth>().deadOrNot();
		if (b_dead)
			return TaskStatus.Failure;
		else 
			return TaskStatus.Success;
	}
}
