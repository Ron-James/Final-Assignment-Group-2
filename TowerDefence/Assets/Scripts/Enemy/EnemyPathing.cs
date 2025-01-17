using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] Vector3 towerPosition;
    
    [SerializeField] bool attacksTowers = false;
    [SerializeField] float regularSpeed = 8;
    [SerializeField] float slowedSpeed = 4;
    GameObject Tower;
    NavMeshAgent navMeshAgent;
    Vector3 nearestTower;
    bool slowed;
    Coroutine slowCo;
    Transform towerLocation;
    Vector3 lastVelocity;
    float lastAnimatorSpd;

    public bool Slowed { get => slowed; set => slowed = value; }
    public Vector3 TowerPosition { get => towerPosition; set => towerPosition = value; }

    // Start is called before the first frame update
    void Start()
    {
        towerLocation = GameManager.instance.MainTower;
        navMeshAgent = GetComponent<NavMeshAgent>();
        Physics.IgnoreLayerCollision(9, 10, true);
        
        if(!attacksTowers){
            navMeshAgent.destination = towerLocation.position;
        }
        else{
            navMeshAgent.destination = nearestTower;
        }
    }
    private void OnEnable() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void OnDisable() {
        StopAllCoroutines();
    }
    void ControlSpeed(){
        if(slowed){
            navMeshAgent.speed = slowedSpeed;
        }
        else{
            navMeshAgent.speed = regularSpeed;
        }
    }
    
    public void PauseEnemy(){
        lastVelocity = GetComponent<NavMeshAgent>().velocity;
        GetComponent<NavMeshAgent>().isStopped = true;
        lastAnimatorSpd = GetComponent<Animator>().speed;
    }

    public void ResumeEnemy(){
        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponent<NavMeshAgent>().velocity = lastVelocity;
        GetComponent<Animator>().speed = lastAnimatorSpd;
    }

    public void SlowDownEnemy(float time){
        if(this.gameObject.activeInHierarchy){
            StartCoroutine(SlowEnemy(time));
        }
        else{
            return;
        }
       
        
    }
    
    IEnumerator SlowEnemy(float duration){
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        float time = 0;
        slowed = true;
        float regSpd = navMeshAgent.speed;
        float slowSpd = navMeshAgent.speed * GameManager.instance.EnemySlowSpeed;
        rigidbody.velocity = slowSpd * rigidbody.velocity.normalized; 
        navMeshAgent.speed = slowSpd;
        while(true){
            time += Time.deltaTime;

            if(time >= duration){
                slowed = false;
                navMeshAgent.speed = regSpd;
                slowCo = null;
                break;
            }
            else{
                yield return null;
            }
        }
    } 


}
