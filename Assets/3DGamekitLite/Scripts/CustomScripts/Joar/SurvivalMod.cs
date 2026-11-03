using Gamekit3D;
using UnityEngine;

public class SurvivalMod : MonoBehaviour
{
   [SerializeField] private float thirstLevel = 100f;
   [SerializeField] private float thirstSpeed;
   [SerializeField] private float timer = 10; 
   
   [SerializeField] private bool thirsty;
   [SerializeField] private bool parched; 
   
   [SerializeField] private GameObject playerObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
        
        /*Subtracting current value of thirstLevel with thirstSpeed, thirstSpeed is normalized by using
        Time.deltaTime so that it's frame rate independent. Mathf.Clamp restricts the thirstLevel between
        it's current max and min value. 
        */
        
        thirstLevel -= Mathf.Clamp(thirstSpeed*Time.deltaTime,0,thirstLevel);

        if (thirstLevel <= 50f && thirsty == false)
        { 
            Debug.Log("Getting thirsty...");
            thirsty = true;
        }
        
        if (thirstLevel <= 25f && parched == false)
        { 
            Debug.Log("Getting Parched..."); 
            parched = true;
        }
        
        if (thirstLevel <= 0 && thirsty)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
           { 
               
               Damageable.DamageMessage msg = new Damageable.DamageMessage()
            { 
                amount = 1,
                damager = this,
                damageSource = new Vector3(0,0,0),
                direction = new Vector3(0,0,0),
                throwing = false,
                stopCamera = false,
            };
                
            playerObject.GetComponent<Damageable>().ApplyDamage(msg);
            Debug.Log("Damage by thirst!");
            timer = 10f; 
          } 
            
            
        }
        
    }
}
