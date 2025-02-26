using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class AutoDeactivate : MonoBehaviour
    {
        [SerializeField] bool destoryGameObject;
        [SerializeField] float lifetime = 3f;

        WaitForSeconds waitLifttime;

        private void Awake()
        {
            waitLifttime = new WaitForSeconds(lifetime);
        }

        private void OnEnable()
        {
            StartCoroutine(DeactivateCoroutine());
        }

        IEnumerator DeactivateCoroutine()
        {
            yield return waitLifttime;
            if (destoryGameObject)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
