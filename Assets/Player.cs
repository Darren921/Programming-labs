using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 _moveDir;
    //private Button _ShootButton;
    // Start is called before the first frame update
    void Start()
    {
       Inputs.Init(this);
       Inputs.GameMode();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (speed * Time.deltaTime * _moveDir);
    }
    public void SetMoveDirection(Vector3 NewDirection)
    {
     _moveDir = NewDirection;
    }

   /* public void detectShot(Button button)
    {
        _ShootButton = gameObject.AddComponent<Button>();
    }
   */
}
