
using DG.Tweening;
using TestTask.Attribute;
using TestTask.Core;
using UnityEngine;


namespace TestTask.Movement
{

    public abstract class Enemy : Attributes
    {     
        float stationaryTime;
        protected Transform player;
        protected new void Awake()
        {
            base.Awake();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            stationaryTime = Random.Range(1, 10f);

        }
        
        protected override void IsDeath(float hp)
        {
           
            transform.DOKill(false);
            if (hp <= 0)
            {
                GameHandler.instance.RemoveEnemy(this.transform);
                //Instantiate the coin/gems/diamond to the position after enemy death
                InstantiateSomeCoins();
                //gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }

        private void InstantiateSomeCoins()

        {
            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                //Randomly instantiate the coins near player
                CoinDrop coin = GameHandler.instance.CoinPool.Request();
                Instantiate(coin.gameObject, transform.position + transform.forward * Random.Range(-1, 1f)
                + transform.right * Random.Range(-1, 1f), coin.transform.rotation);
            }            
        }

        private void Update()
        {
            transform.LookAt(player.position);
            if (stationaryTime < 0)
            {
                EnemyAttack();
                stationaryTime = Random.Range(3, 6);
            }
            else
            {
                stationaryTime -= Time.deltaTime;
            }
        }

        public abstract void EnemyAttack();
    }
}
