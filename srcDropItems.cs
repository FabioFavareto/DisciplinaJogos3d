using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class srcDropItems : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private GameObject item;
   [SerializeField] private int itemDropRate;
   [SerializeField] private int itemMinDrop;
   [SerializeField] private int itemMaxDrop;

   public void DropItem(){
       int rand = Random.Range(1,100);

       if(rand < itemDropRate){
           int amount = Random.Range(itemMinDrop,itemMaxDrop);

           for(int i = 0; i < amount; i++){
               Instantiate(item, transform.position,transform.rotation);
           }
       }
   }
}