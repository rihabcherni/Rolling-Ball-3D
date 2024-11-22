using UnityEngine;

public class SmoothFollow : MonoBehaviour
    {
        public Transform player;
        public Vector3 offset;   

        void Start()
        {
            if (player == null)
            {
                Debug.LogError("Player transform is not assigned to the CameraFollow script!");
            }
        }

        void LateUpdate()
        {
            if (player != null)
            {
                transform.position = player.position + offset;
            }
        }
    }
