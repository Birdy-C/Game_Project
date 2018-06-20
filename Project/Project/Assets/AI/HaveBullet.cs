using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HaveBullet : Conditional {

	public override TaskStatus OnUpdate () {
		int bulletNum = 1;
		if (bulletNum>0)
			return TaskStatus.Success;
		return TaskStatus.Failure;
	}

}
