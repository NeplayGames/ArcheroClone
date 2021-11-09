using DG.Tweening;
using TestTask.Core;
using TestTask.Fight;
using UnityEngine;

namespace TestTask.Movement
{
    public class Movement : Attributes, IBehaviour
    {

        [SerializeField] SphereCollider sphereCol;
        //Assign the animation mesh that will consist of character mesh to ma
        [SerializeField] protected Transform animationMesh;

        [SerializeField] LayerMask notPlayer;

        protected PlayerBehaviour behaviour;
        protected Attack attack;

        [Header("Angular steps that the rotation will shoot at")]
        [SerializeField] float maxDegreeDelta;
        Quaternion targetRotation;

        protected Transform enemy;
        public new void Awake()
        {
            base.Awake();
            behaviour = GetComponent<PlayerBehaviour>();
            attack = GetComponent<Attack>();
        }

        ///<summary>
        /// Rotate out facing direction horizontally.
        ///</summary>
        protected Quaternion LookAtDirection(Quaternion animationMesh, Vector3 moveDirection)
        {
            if (moveDirection.magnitude > 0)
                targetRotation = Quaternion.LookRotation(moveDirection);
            return Quaternion.RotateTowards(animationMesh, targetRotation, maxDegreeDelta * Time.deltaTime);
        }
        public void CancelBehavior()
        {

        }
        #region Collision  
        ///<summary>
        ///Checks for collision with walls and prevent player from going through without any gitterness
        ///</summary>x    
        protected void CollisionCheck()
        {
            //To store colliders touching the sphere collider
            Collider[] overlaps = new Collider[4];
            Collider myCollider = new Collider();
            int num = 0;
            if (sphereCol != null)
            {
                num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(sphereCol.center), sphereCol.radius, overlaps, notPlayer, QueryTriggerInteraction.UseGlobal);
                myCollider = sphereCol;
            }
            for (int i = 0; i < num; i++)
            {
                Transform t = overlaps[i].transform;
                if (Physics.ComputePenetration(myCollider, transform.localPosition, transform.rotation, overlaps[i], t.position, t.rotation, out Vector3 dir, out float dist))
                {
                    Vector3 penetrationVector = dir * dist;
                    transform.localPosition = transform.localPosition + penetrationVector;
                }
            }
        }

        protected override void Death()
        {
            //Todo: Implement What to do OnDeath of player.
        }
        #endregion
    }

}