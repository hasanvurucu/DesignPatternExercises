using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BotState
{
    STANDART,
    QUEUE
}

public class CommandControlBot : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Queue<Command> _commands = new Queue<Command>();
    private Command _currentCommand;

    public BotState botState;

    private void Awake() => botState = BotState.STANDART;
    private void Start() => _agent = GetComponent<NavMeshAgent>();

    private void Update()
    {
        ListenForCommands();
        ProcessCommands();
    }

    private void ListenForCommands()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (botState == BotState.QUEUE)
            {
                while(_commands.Count > 0)
                {
                    _commands.Dequeue();
                }
            }
             _agent.SetDestination(transform.position);
        }

        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out var hitInfo))
            {
                if(botState == BotState.STANDART)
                {
                    _agent.SetDestination(hitInfo.point);
                }
                else if(botState == BotState.QUEUE)
                {
                    var moveCommand = new MoveCommand(hitInfo.point, _agent);
                    _commands.Enqueue(moveCommand);
                }
            }
        }
        
    }
    
    private void ProcessCommands()
    {
        if (_currentCommand != null && _currentCommand.IsFinished == false)
            return;

        if (_commands.Count == 0)
            return;

        _currentCommand = _commands.Dequeue();
        _currentCommand.Execute();
    }

    internal class MoveCommand: Command
    {
        private readonly Vector3 _destination;
        private readonly NavMeshAgent _agent;

        public MoveCommand(Vector3 destination, NavMeshAgent _agent)
        {
            _destination = destination;
            this._agent = _agent;
        }

        public override void Execute()
        {
            _agent.SetDestination(_destination);
        }

        public override bool IsFinished => _agent.remainingDistance < 0.1f;
    }
}
