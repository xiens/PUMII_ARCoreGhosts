using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour {

    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Vector3 endMarker;

    // Movement speed in units/sec.
    private float speed = 0.05F;
    private float rotationSpeed = 10f;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;


    public Animator catAnimator;

    void Start()
    {
        journeyLength = 0;
        //catAnimator.GetComponent<Animator>();
    }

    // Follows the target position like with a spring
    void Update()
    {
        if (journeyLength > 0)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker, fracJourney);

            if (fracJourney < 0.1)  //just at the beginning of the journey
            {
                var lookPos = endMarker - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
            try
            {
                if (Vector3.Distance(startMarker.position, endMarker) < 0.1)
                {
                    catAnimator.SetBool("IsRunning", false);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("Cannt kep runniign");
            }

        }
    }
    public void StartMove(Transform startPosition, Vector3 endPosition, bool FromRandom)
    {
        try
        {
            catAnimator.SetBool("IsRunning", true);
        }
        catch(System.Exception e)
        {
            Debug.Log("Ughh stupid");
        }
        if (!FromRandom)
            startMarker = this.transform;
        else
            startMarker = startPosition;

        endMarker = endPosition;
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker);
    }
}
