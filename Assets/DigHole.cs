using UnityEngine;

public class DigHoleOnGround : MonoBehaviour
{
    public float radius = 1.0f; // 挖洞半径
    public float depth = 0.5f;  // 挖洞深度
    public ParticleSystem particleSystem; // 粒子系统，用于模拟土块掉落

    private void OnCollisionEnter(Collision collision)
    {
        // 检测碰撞物体是否是地面
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("触碰地面了！");
            // 播放粒子效果
            particleSystem.transform.position = collision.contacts[0].point;
            particleSystem.Play();

            // 修改地面的网格
            MeshFilter meshFilter = collision.gameObject.GetComponent<MeshFilter>();
            MeshCollider meshCollider = collision.gameObject.GetComponent<MeshCollider>();

            if (meshFilter == null || meshCollider == null)
            {
                Debug.LogError("地面没有MeshFilter或MeshCollider组件！");
                return;
            }

            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;
            Vector3 center = meshFilter.transform.worldToLocalMatrix.MultiplyPoint(collision.contacts[0].point);

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vertex = vertices[i];
                float distance = Vector3.Distance(vertex, center);

                if (distance < radius)
                {
                    float normalizedDistance = 1 - (distance / radius);
                    vertices[i] += (vertex - center).normalized * normalizedDistance * depth;
                }
            }

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            // 更新 MeshCollider 的共享网格
            meshCollider.sharedMesh = mesh;
        }
    }
}