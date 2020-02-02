using System.Collections;
using System.Collections.Generic;
//using System.Threading;
using UnityEngine;

namespace MTLGJ
{

    public class BaseObject : MonoBehaviour
    {
        [SerializeField]
        public PlayfulSystems.ProgressBar.ProgressBarPro progressBar;

        [SerializeField]
        public Transform Transform;

        public Vector3Int PathfindPosition =>
            (Level.Instance.Tilemap.origin * -1) +
            Transform.position.FromWorldToCellPosition();

        [SerializeField]
        public SpriteRenderer SpriteRenderer;

        public Vector3Int CellPos => Level.Instance == null ? 
            new Vector3Int() : 
            Level.Instance.Tilemap.WorldToCell(Transform.position);

        [SerializeField]
        private Cirrus.Numeric.Range _maxHealth;        

        public Cirrus.Events.ObservableValue<float> MaxHealth = new Cirrus.Events.ObservableValue<float>();
        
        public Cirrus.Events.ObservableValue<float> Health = new Cirrus.Events.ObservableValue<float>();

        [SerializeField]
        private float _flashTime = 0.2f;

        [SerializeField]
        private Material _flashMaterial;


        private Cirrus.Timer _flashTimer;
        private Material _prevMaterial;

        public virtual void Awake()
        {
            //_flashTimer = new Cirrus.Timer();
            _healthBarTimer = new Cirrus.Timer(start:false);
            _flashTimer = new Cirrus.Timer(start: false);
            _healthBarTimer.OnTimeLimitHandler += OnHealthTimeout;
            _flashTimer.OnTimeLimitHandler += OnFlashTimeout;

            MaxHealth.OnValueChangedHandler += OnHealthChanged;
            Health.OnValueChangedHandler += OnHealthChanged;
            //Health = MaxHealth;

            MaxHealth.Value = _maxHealth.Value;
            Health.Value = MaxHealth.Value;
        }


        private Cirrus.Timer _healthBarTimer;
        public float _healthbartime = 0.5f;

        public void Flash()
        {
            _prevMaterial = SpriteRenderer.material;
            SpriteRenderer.material = _flashMaterial;
            _flashTimer.Start(_flashTime);

        }

        // Upg health bar when damaged
        public void OnHealthChanged(float va)
        {
            if (progressBar == null)
                return;

            progressBar.gameObject.SetActive(true);
            _healthBarTimer.Start(_healthbartime);
            progressBar.SetValue(Health.Value / MaxHealth.Value);
        }

        public void OnHealthTimeout()
        {
            if(progressBar != null)
            progressBar.gameObject.SetActive(false);
        }

        public void OnFlashTimeout()
        {
            if (gameObject == null)
                return;

            if (SpriteRenderer.gameObject == null)
                return;



            SpriteRenderer.material = _prevMaterial;
        }

        public virtual void ApplyDamage(float dmg)
        {
            Health.Value -= dmg;
            if (Health.Value < 0)
            {
                Health.Value = 0;
                return;
            }

            Flash();
        }


        // Start is called before the first frame update
        public virtual void Start()
        {
            if(progressBar != null)
            progressBar.gameObject.SetActive(false);
        }

        // Update is called once per frame
        public virtual void Update()
        {
           // if(SpriteRenderer != null)
               // SpriteRenderer.sortingOrder = (int)-Transform.position.y;
           
        }
    }
}