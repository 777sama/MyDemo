using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class EventCollider : MonoBehaviour
    {
        public GameObject LookNpcTimeLine;
        public GameObject Image;

        [SerializeField] private string ObjectID;



        private void Awake()
        {
            
        }

        private void Start()
        {
            if (GameObjActiveManager.MainInstance.DestroyedObjs.Contains(ObjectID))
            {
                Destroy(gameObject);
            }
        }

        public void SafeDestroy()
        {
            GameObjActiveManager.MainInstance.DestroyedObjs.Add(ObjectID);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            LookNpcTimeLine.SetActive(true);
            StartCoroutine(Event());
            
        }


        IEnumerator Event()
        {
            
            yield return new WaitForSecondsRealtime(1f);

            SafeDestroy();
            yield return new WaitForSecondsRealtime(5f);
            
            LookNpcTimeLine.SetActive(false);
            
            

        }
    }
}
