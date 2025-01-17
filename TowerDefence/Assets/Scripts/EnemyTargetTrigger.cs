using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetTrigger : MonoBehaviour
{
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        switch(other.tag){
            case "Build Plate":
            if(other.gameObject.GetComponentInParent<BuildPlate>().BuildIndex > 0 && GetComponentInParent<EnemyRanged>().Target == null){
                GetComponentInParent<EnemyRanged>().Target = other.transform;
                //GetComponentInParent<NavMeshAgent>().isStopped = true;
                Debug.Log("Build Plate Targeted");
            }
            break;
            case "Tower":
                Debug.Log("entered the main tower");
                GetComponentInParent<EnemyRanged>().Target = other.transform;
                GetComponentInParent<EnemyFollower>().IsStopped = true;
                
            break;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.transform == GetComponentInParent<EnemyRanged>().Target){
            GetComponentInParent<EnemyRanged>().Target = null;
        }
    }
}
