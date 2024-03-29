using System.Collections.Generic;
using UnityEngine;

public class MultiCollider : MonoBehaviour
{
    public string tagFilter;
    public List<Collider> _collisions = new List<Collider>();
    public List<Collider> collisions { get { return GetNonNullCollisions(); } }

    private void OnTriggerEnter(Collider other)
    {
        if (tagFilter == null || other.tag == tagFilter)
        {
            _collisions.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _collisions.Remove(other);
    }

    private List<Collider> GetNonNullCollisions()
    {
        return _collisions.FindAll(c => c != null);
    }
}
