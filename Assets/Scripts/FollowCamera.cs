using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject thingToFollow;
    Vector3 high = new Vector3 (0, 0, -20);
    //
    // Update is called once per frame
    void Update()
    {
        transform.position = thingToFollow.transform.position + high;
    }

    public void SetThingToFollow(GameObject thing)
    {
        thingToFollow = thing;
    }
}
