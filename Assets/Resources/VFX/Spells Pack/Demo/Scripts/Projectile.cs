using UnityEngine;

namespace ZakhanSpellsPack
{
    public class Projectile : MonoBehaviour
    {
        public GameObject ExplosionPrefab;
        public float DestroyExplosion = 4.0f;
        public float DestroyChildren = 2.0f;
        public Vector2 Velocity;
        string VFXname;

        Rigidbody rb;
        void Start()
        {
            //rb = gameObject.GetComponent<Rigidbody>();
           // rb.velocity = Velocity;

        }
        private void Update()
        {
            VFXname = this.gameObject.name;
           
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("enemy"))
            {
                EnemyHealth enemyHealth = col.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    if(VFXname == "Projectile_Fire(Clone)")

                         enemyHealth.TakeDamage(10);
                    else if (VFXname == "Projectile_Fire_3(Clone)")
                    {
                        Debug.Log("hit");
                        enemyHealth.TakeDamage(60);
                    }
                        

                }
            }
            var exp = Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);
            Destroy(exp, DestroyExplosion);
            Transform child;
            child = transform.GetChild(0);
            transform.DetachChildren();
            Destroy(child.gameObject, DestroyChildren);
            Destroy(gameObject);
        }
    }
}
