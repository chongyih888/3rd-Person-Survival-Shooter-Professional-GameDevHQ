using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle, 
        Chase,
        Attack
    }

    //reference to character controller
    private CharacterController _controller;
    private Transform _player;

    [SerializeField]
    private float _speed = 2.5f;

    private Vector3 _velocity;

    private float _gravity = 20.0f;

    [SerializeField]
    private EnemyState _currentState = EnemyState.Chase;

    private Health _playerHealth;

    [SerializeField]
    private float _attackDelay = 1.5f;
    private float _nextAttack = -1;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        if(_controller == null)
        {
            Debug.LogError("The CharacterController is NULL");
        }

        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _playerHealth = _player.GetComponent<Health>();

        if(_player == null || _playerHealth == null)
        {
            Debug.LogError("Player Components are NULL.");
        }
    }

    private void Update()
    {
        //don't calculate movement if attacking
        
        if (_currentState == EnemyState.Chase)
        {
            CalculateMovement();
        }

        if (_currentState == EnemyState.Attack)
        {

           

            //cooldown system
        }

        switch (_currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Chase:
                CalculateMovement();
                break;
        }
    }

    void CalculateMovement()
    {
        //check if grounded
        //calculate direction = destination(player or target) - source(self(transform))
        //calculate velocity = direction * speed
        if (_controller.isGrounded == true)
        {
            Vector3 direction = _player.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            //rotate towardsthe player
            transform.localRotation = Quaternion.LookRotation(direction);

            _velocity = direction * _speed;
        }

        _velocity.y -= _gravity;
        _controller.Move(_velocity * Time.deltaTime);
        //subtract gravity
        //move to velocity
    }

    void Attack()
    {

        if (Time.time > _nextAttack)
        {
            //damage the player
            if (_playerHealth != null)
            {
                _playerHealth.Damage(10);
            }
            _nextAttack = Time.time + _attackDelay;
        }
    }

    //ontriggerenter
    //begin attacking
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //In attack state
            _currentState = EnemyState.Attack;
        }
    }

   
    //ontriggerexit
    //resume movement
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _currentState = EnemyState.Chase;
        }
    }
}
