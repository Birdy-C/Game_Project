using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnoughBullet : Conditional {
	public override TaskStatus OnUpdate () {
		int bulletNum = 3;
		if (bulletNum>=5)
			return TaskStatus.Success;
		if (Random.value < bulletNum/5) //Random.value属于[0.0,1.0]
			return TaskStatus.Success;
		return TaskStatus.Failure;
	}
}
