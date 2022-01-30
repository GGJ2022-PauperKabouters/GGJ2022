using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTile : MonoBehaviour
{
    private Transform _playerTransform;
    private float _attractDuration = 0;
    private Rigidbody m_Rigidbody;
    private LineRenderer m_LineRenderer;

    private Vector3 _initialScale;
    private float _initialDistanceToPlayer;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_LineRenderer = GetComponent<LineRenderer>();
        _initialScale = transform.localScale;
    }

    void FixedUpdate()
    {
        if (_playerTransform != null)
        {
            _attractDuration += Time.deltaTime;
            
            // Update the line between tile and player
            m_LineRenderer.SetPositions(new Vector3[]{transform.position, _playerTransform.position});

            Vector3 direction = _playerTransform.position - transform.position;
            if (_attractDuration <= 1f)
                m_Rigidbody.AddForce(direction * 0.1f * _attractDuration, ForceMode.VelocityChange);
                
            // Make the tile smaller as it approaches the player
            float distanceToPlayer = Vector3.Distance(_playerTransform.position, transform.position);
            float progress = distanceToPlayer / _initialDistanceToPlayer;
            
            if (progress < 0.5f)
                transform.localScale = new Vector3(_initialScale.x * progress * 2, _initialScale.y * progress * 2, _initialScale.z * progress * 2);
            
            // Player obtains the tile when it gets too close
            if (distanceToPlayer < 0.4f)
            {
                _playerTransform.GetComponent<PlayerController>().OnTileObtained();
                Destroy(gameObject);
            }
        }
    }

    public void SetOutline(bool value)
    {
        //Outline when the player is targeting the tile would be nice here
    }
    

    public void OnHarpoonHit(Transform targetPlayerTransform)
    {
        _playerTransform = targetPlayerTransform;
        _initialDistanceToPlayer = Vector3.Distance(_playerTransform.position, transform.position);

        //give it a lil bit of a spin
        m_Rigidbody.angularVelocity = Random.rotation.eulerAngles * 0.004f;
        
        
        m_LineRenderer.enabled = true;
    }
}
