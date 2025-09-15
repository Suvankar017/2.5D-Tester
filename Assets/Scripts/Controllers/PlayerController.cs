using UnityEngine;
using UnityEngine.InputSystem;

namespace AAARRSS
{
    public class PlayerController : MonoBehaviour
    {
        private new Rigidbody2D rigidbody;
        private Vector2 moveInput;
        private bool isJumpKeyPressed;
        private bool isTeleportKeyPressed;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            moveInput = Vector2.zero;
        }

        private void Update()
        {
            Keyboard keyboard = Keyboard.current;
            moveInput.x = keyboard.aKey.isPressed ? -1.0f : keyboard.dKey.isPressed ? 1.0f : 0.0f;
            moveInput.y = keyboard.sKey.isPressed ? -1.0f : keyboard.wKey.isPressed ? 1.0f : 0.0f;

            if (keyboard.spaceKey.wasPressedThisFrame)
                isJumpKeyPressed = true;

            if (keyboard.qKey.wasPressedThisFrame)
                isTeleportKeyPressed = true;
        }

        private void FixedUpdate()
        {
            Vector2 moveInputDir = moveInput.normalized;
            //rigidbody.AddForce(10.0f * moveInput);
            //rigidbody.AddRelativeForce(10.0f * moveInput);

            rigidbody.AddForce(50.0f * moveInputDir);

            if (isJumpKeyPressed)
            {
                isJumpKeyPressed = false;
                rigidbody.AddForce(Vector2.up * 300.0f);
            }

            if (isTeleportKeyPressed)
            {
                isTeleportKeyPressed = false;
                rigidbody.MovePosition(Vector2.zero);
            }
        }
    }

    public abstract class BaseSensor
    {
        public enum CastDirection { Right, Left, Up, Down, Front, Back }

        public float castLength = 1.0f;
        public LayerMask layerMask = 255;

        protected System.Collections.Generic.List<Collider2D> hitColliders = new();
        protected System.Collections.Generic.List<Transform> hitTransforms = new();
        protected Transform transform;
        protected Collider2D collider;
        protected Vector3 origin;
        protected Vector3 hitNormal;
        protected Vector3 hitPosition;
        protected float hitDistance;
        protected CastDirection direction;
        protected bool hasDetectedHit;

        protected readonly int ignoreRaycastLayer;

        public bool HasHit => hasDetectedHit;
        public float Distance => hitDistance;
        public Vector3 Normal => hitNormal;
        public Vector3 Position => hitPosition;

        public BaseSensor(Transform transform, Collider2D collider)
        {
            this.transform = transform;
            this.collider = collider;
            origin = Vector3.zero;
            direction = CastDirection.Down;
            ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
        }

        public void Cast()
        {
            //ResetFlags();

            Vector2 worldOrigin = transform.TransformPoint(origin);
            Vector2 worldDirection = GetCastDirection();
        }

        public bool TryGetCollider(out Collider2D collider)
        {
            collider = null;

            if (hitColliders.Count == 0 || hitColliders[0] == null)
                return false;

            collider = hitColliders[0];
            return true;
        }

        public bool TryGetTransform(out Transform transform)
        {
            transform = null;

            if (hitTransforms.Count == 0 || hitTransforms[0] == null)
                return false;

            transform = hitTransforms[0];
            return true;
        }

        public void SetCastOrigin(Vector3 origin)
        {
            if (transform == null)
                return;

            this.origin = transform.InverseTransformPoint(origin);
        }

        public void SetCastDirection(CastDirection direction) => this.direction = direction;

        protected Vector2 GetCastDirection()
        {
            return direction switch
            {
                CastDirection.Right => transform.right,
                CastDirection.Left => -transform.right,
                CastDirection.Up => transform.up,
                CastDirection.Down => -transform.up,
                CastDirection.Front => transform.forward,
                CastDirection.Back => -transform.forward,
                _ => Vector2.one,
            };
        }

        protected abstract void Cast(Vector2 worldOrigin, Vector2 worldDirection);
    }

    public class RaycastSensor : BaseSensor
    {
        public RaycastSensor(Transform transform, Collider2D collider) : base(transform, collider)
        {

        }

        protected override void Cast(Vector2 worldOrigin, Vector2 worldDirection)
        {
            RaycastHit2D hit = Physics2D.Raycast(worldOrigin, worldDirection, castLength, layerMask & ~(1 << ignoreRaycastLayer));
            hasDetectedHit = hit.collider != null;
            if (hasDetectedHit)
            {
                hitColliders.Clear();
                hitTransforms.Clear();
                hitColliders.Add(hit.collider);
                hitTransforms.Add(hit.collider.transform);
                hitNormal = hit.normal;
                hitPosition = hit.point;
                hitDistance = hit.distance;
            }
            else
            {
                hitColliders.Clear();
                hitTransforms.Clear();
                hitNormal = Vector3.zero;
                hitPosition = Vector3.zero;
                hitDistance = 0.0f;
            }
            Debug.DrawRay(worldOrigin, worldDirection * castLength, hasDetectedHit ? Color.red : Color.green);
        }
    }
}
