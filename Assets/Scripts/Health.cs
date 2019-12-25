using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _minHealth;
    [SerializeField]
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    //damage(int damageAmount)
    //current health - damageAmount
    //check if dead(current health is less than min health)
    //destroy if dead
    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if(_currentHealth < _minHealth)
        {
            Destroy(this.gameObject);
        }
    }
}
