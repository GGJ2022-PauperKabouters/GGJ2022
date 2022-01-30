#pragma warning disable 649

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ActiveRagdoll
{
    // Author: Sergio Abreu García | https://sergioabreu.me

    public class ActiveRagdoll : MonoBehaviour
    {


        public List<GameObject> listOfChildrenAnimated;

        public List<GameObject> listOfChildrenPhysical;

        public GameObject hipAnimated;
        public GameObject hipPhysical;


        private void Start()
        {
            GetChildRecursive(hipAnimated,listOfChildrenAnimated);
            GetChildRecursive(hipPhysical,listOfChildrenPhysical);
        }
        private void GetChildRecursive(GameObject obj, List<GameObject> listOfChildren)
        {
            if (null == obj)
                return;

            foreach (Transform child in obj.transform)
            {
                if (null == child)
                    continue;
                //child.gameobject contains the current child you can do whatever you want like add it to an array
                listOfChildren.Add(child.gameObject);
                GetChildRecursive(child.gameObject,listOfChildren);
            }
        }


        private void FixedUpdate()
        {
            foreach (GameObject t in listOfChildrenAnimated)
            {
                foreach (GameObject g in listOfChildrenPhysical)
                {
                    if (g.name == t.name)
                    {
                        if (g.GetComponent<ConfigurableJoint>() != null)
                        {
                            g.GetComponent<ConfigurableJoint>().targetRotation = t.transform.rotation;
                            g.GetComponent<ConfigurableJoint>().targetPosition = t.transform.position;
                            g.GetComponent<ConfigurableJoint>().targetVelocity = new Vector3(0.2f, 0.2f, 0.2f);
                            g.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(0.8f, 0.8f, 0.8f);
                        }
                        
                        else
                        {

                            // g.transform.rotation = t.transform.rotation;
                            g.transform.position = t.transform.position;
                            g.transform.rotation = t.transform.rotation;
                        }
                        
                   

                    }
                    
                }
            }
        }


    }
} // namespace ActiveRagdoll