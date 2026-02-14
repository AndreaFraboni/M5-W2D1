using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MoverAgent : MonoBehaviour
{
    enum mode
    {
        click,
        waasd
    }

    [SerializeField] private NavMeshAgent _meshAgent;
    [SerializeField] private mode _mode;

    private Camera _cam;

    Ray _pointToRayMouse;
    private Vector3 _direction;

    bool _switchInputMode = true;

    private void Awake()
    {
        _cam = Camera.main;
        _meshAgent = GetComponent<NavMeshAgent>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _mode = (_mode == mode.click) ? mode.waasd : mode.click;
        }

        switch (_mode)
        {
            case mode.click:

                _meshAgent.velocity = Vector3.zero;

                if (Input.GetMouseButtonDown(0))
                {
                    _pointToRayMouse = _cam.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(_pointToRayMouse, out RaycastHit Hit))
                    {

                        _meshAgent.SetDestination(Hit.point);

                    }
                }

                break;

            case mode.waasd:


                Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                _meshAgent.velocity = dir * _meshAgent.speed;

                break;

            default:

                break;


        }

    }
}