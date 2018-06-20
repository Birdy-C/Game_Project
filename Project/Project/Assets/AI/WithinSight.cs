using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class WithinSight : Conditional
{
	// How wide of an angle the object can see
	public float fieldOfViewAngle;
	// 射线长度
	public float rayLeng = 50;
	// The tag of the targets
	public string targetTag;
	// Set the target variable when a target has been found so the subsequent tasks know which object is the target
	public SharedTransform target;
	// A cache of all of the possible targets
	private Transform[] possibleTargets; 

	public override void OnAwake()
	{
	    // Cache all of the transforms that have a tag of targetTag
	    var targets = GameObject.FindGameObjectsWithTag(targetTag);
	    Debug.Log(targetTag);
	    possibleTargets = new Transform[targets.Length];
	    for (int i = 0; i < targets.Length; ++i) {
	    	Debug.Log(targets[i].transform);
	        possibleTargets[i] = targets[i].transform;
	    }
	}

	public override TaskStatus OnUpdate()
	{
	    // Return success if a target is within sight
	    for (int i = 0; i < possibleTargets.Length; ++i) {
	        if (withinSight(possibleTargets[i], fieldOfViewAngle)) {
	            // Set the target so other tasks will know which transform is within sight
	            target.Value = possibleTargets[i];
	            return TaskStatus.Success;
	        }
        }
        return TaskStatus.Failure; //or TaskStatus.Running
	}

	// Returns true if targetTransform is within sight of current transform
	public bool withinSight(Transform targetTransform, float fieldOfViewAngle)
	{
	    Vector3 direction = targetTransform.position - transform.position;

	    if (Vector3.Angle(direction, transform.forward) < fieldOfViewAngle) {
	    	//Debug.Log(transform.position);
	    	//Debug.Log(targetTransform.position);
	        //定义一条射线，起点为GO1的物体坐标,终点为GO2的物体坐标  
	        Ray ray = new Ray(transform.position, targetTransform.position - transform.position);    
	        //定义一个光线投射碰撞   
	        RaycastHit hit;
	        //发射射线长度为rayLeng 
	        Physics.Raycast(ray, out hit, rayLeng);
	        //Debug.Log(hit.transform);
	        if (hit.transform == targetTransform) {
	        	Debug.Log(targetTransform);
	      	    return true;
	        }
	    }
	    return false;
	}
}
