using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRanged : MonoBehaviour
{
    Transform tempDestination;
    [SerializeField] Transform target;
    [SerializeField] float rotationSpeed = 25f;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float damage = 1;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LayerMask layerMask;


    float fireCount;

    public Transform Target { get => target; set => target = value; }

    // Start is called before the first frame update
    void Start()
    {
        
        fireCount = 0;
        //GetComponent<SphereCollider>().radius = range;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null && target.gameObject.GetComponentInParent<BuildPlate>().Health > 0){
            Aim();
            fireCount += Time.deltaTime;
            if(fireCount >= fireRate){
                Fire();
                fireCount = 0;
            }
        }
    }
    void Aim(){
        Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position);
        Vector3 angles = targetRot.eulerAngles;
        angles.x = 0;
        angles.z = 0;
        targetRot = Quaternion.Euler(angles);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot,  rotationSpeed * Time.deltaTime);
    }
    
    void Fire(){
        if(target == null){
            return;
        }
        else{
            RaycastHit hit;
            Vector3 direction = (target.position - transform.position).normalized;
            float range = (target.position - transform.position).magnitude;
            if(Physics.Raycast(transform.position, direction, out hit, range, layerMask) && hit.collider.tag == "Build Plate"){
                //deal damage top tower
                //Debug.Log(hit.collider.name + "shot this");
                StartCoroutine(ShowProjectileLine(hit.point));
                hit.collider.gameObject.GetComponentInParent<BuildPlate>().TakeDamage(damage);
                if(target.gameObject.GetComponentInParent<BuildPlate>().Health <= 0 ){
                    target = null;
                    GetComponent<NavMeshAgent>().isStopped = false;
                }
            }
        }

        
    }

    IEnumerator ShowProjectileLine(Vector3 point){
        float time = 0;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, point);

        while(true){
            time += Time.deltaTime;
            if(time >= fireRate/5){
                lineRenderer.SetPosition(0, Vector3.zero);
                lineRenderer.SetPosition(1, Vector3.zero);
                break; 
            }
            else{
                yield return null;
            }
        }
        yield break;
    }
    
    
}
