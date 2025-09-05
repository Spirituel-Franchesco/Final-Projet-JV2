using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int _selectedWeapon = 0; // Index of the currently selected weapon

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = _selectedWeapon;
    }

    void SelectWeapon() 
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == _selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
