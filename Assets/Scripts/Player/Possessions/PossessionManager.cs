using UnityEngine;

namespace Player.Possessions
{
    public class PossessionManager : MonoBehaviour
    {
        BasePossession _currentPossession;
        readonly BeastmasterPossession _beastmasterPossession = new BeastmasterPossession();
        readonly FoxPossession _foxPossession = new FoxPossession();

        [SerializeField] GameObject beastmasterObject;
        [SerializeField] GameObject foxObject;

        // Start is called before the first frame update
        void Start()
        {
            // starting state dor the state machine
            _currentPossession = _beastmasterPossession;
            // "this" is a ref to the ctx (this EXACT MonoBehaviour script)
            _currentPossession.EnterPossession(this, beastmasterObject);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ReceiveMoveInput(Vector2 movementInput)
        {
            _currentPossession.Move(this, movementInput);
        }

        public void SwitchPossession()
        {
            _currentPossession.LeavePossession(this);
        
            // Switch possession
            if (_currentPossession == _beastmasterPossession)
            {
                _currentPossession = _foxPossession;
                _currentPossession.EnterPossession(this, foxObject);
            }
            else
            {
                _currentPossession = _beastmasterPossession;
                _currentPossession.EnterPossession(this, beastmasterObject);
            }
        }
    }
}