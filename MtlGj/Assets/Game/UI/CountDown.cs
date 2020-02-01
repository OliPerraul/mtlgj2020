using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTLGJ
{

    public class CountDown : MonoBehaviour
    {
        public static CountDown Instance;

        [SerializeField]
        private UnityEngine.UI.Text _text;

        private int _value = 3;

        private string Message => "Wave "+ (Game.Instance.Session.WaveIndex +1).ToString();

        [SerializeField]
        private Game _game;


        public void OnValidate()
        {
            if (_game == null)
                _game = FindObjectOfType<Game>();
        }

        public void Awake()
        {
            Instance = this;
            _text.gameObject.SetActive(false);
            ////_game.OnNewRoundHandler += OnNewRound;
            ////_game.On
        }

        //private void OnNewRound(Round round)
        //{
        //    //round.OnCountdownHandler += OnRoundCountdown;
        //    //round.OnIntermissionHandler += OnIntermission;
        //}


        public int Number
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;

                if (_value == 0)
                {
                    _text.gameObject.SetActive(true);
                    _value = 0;
                    _text.text = Message;
                    return;
                }
                else if (_value < 0)
                {
                    _text.gameObject.SetActive(false);
                    return;
                }

                _text.gameObject.SetActive(true);
                _text.text = _value.ToString();
            }
        }

        public void OnRoundCountdown(int count)
        {
            Enabled = true;
            Number = count;
        }

        private bool _enabled = false;

        public bool Enabled
        {
            get
            {
                return _enabled;
            }

            set
            {
                _enabled = value;
                transform.GetChild(0).gameObject.SetActive(_enabled);
            }
        }
    }
}