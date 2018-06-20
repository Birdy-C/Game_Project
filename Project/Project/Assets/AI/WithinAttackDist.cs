using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class WithinAttackDist : Conditional
{
	public SharedTransform target;
	public float attackDist;
	private float sqrAttackDist;

	public override void OnAwake() {
	  sqrAttackDist = attackDist * attackDist; 
	}

	public override TaskStatus OnUpdate()
	{
	    if (Vector3.SqrMagnitude(transform.position - target.Value.position) < sqrAttackDist)
	        return TaskStatus.Success;
        return TaskStatus.Failure;
	}

}
