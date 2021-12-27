using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    const float G = 667.4f;

    public Rigidbody rb;
    public static List<Attractor> _attractors;

    private void OnEnable() {
        if (_attractors == null)
            _attractors = new List<Attractor>();
        _attractors.Add(this);
    }

    private void OnDisable() {
        _attractors.Remove(this);
    }

    private void FixedUpdate() {
        _attractors.ForEach(attractor => {
            if (attractor != this) {
                Attract(attractor);
            }        
        });
    }

    private void Attract(Attractor objToAttract) {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0) return;
        float forceMarnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2f);
        Vector3 force = direction.normalized * forceMarnitude;

        rbToAttract.AddForce(force);
    }
}
