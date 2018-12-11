using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink.Audio
{
    public class RandomPipeSound : MonoBehaviour
    {

        void Start()
        {
            StartCoroutine(PlaySound());
        } 

        IEnumerator PlaySound()
        {
            while(true)
            {
                yield return new WaitForSeconds(Random.Range(10f,20f));
                this.PlaySound("SubCreaking");
                yield return new WaitForSeconds(Random.Range(10f,20));
                this.PlaySound("WaterDrip");
            }
        }
    }
}