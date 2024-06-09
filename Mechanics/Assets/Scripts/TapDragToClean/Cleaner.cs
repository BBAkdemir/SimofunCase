using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private GameObject brushModel;
    [SerializeField] private float brushSize = 5.0f;
    [SerializeField] private Color brushColor = Color.red;
    [SerializeField] private int rayCount = 10; // Number of rails to be laid
    [SerializeField] private float rayDistance = 100f; // Distance to throw ray

    Texture2D texture;
    GameObject brushInstance;
    Vector3 lastPosition;

    void Start()
    {
        //Target Renderer is not assigned
        if (targetRenderer == null)
        {
            return;
        }

        // Create a texture copy and assign it to the material
        texture = Instantiate(targetRenderer.material.mainTexture) as Texture2D;
        targetRenderer.material.mainTexture = texture;

        // The texture is not readable. enable Read/Write on the texture.
        if (!texture.isReadable)
        {
            return;
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0.5f,0.5f,0.2f), 30 * Time.deltaTime);
        if (Input.GetMouseButton(0)) // Holding down the mouse
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Position the brush model
                if (brushInstance == null)
                {
                    brushInstance = Instantiate(brushModel, hit.point, Quaternion.identity);
                }
                else
                {
                    brushInstance.transform.position = hit.point;
                    brushInstance.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                }
            }
            for (int i = 0; i < rayCount; i++)
            {
                ray = camera.ScreenPointToRay(Input.mousePosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f) * rayDistance);
                if (Physics.Raycast(ray, out hit))
                {
                    // Get UV coordinates directly from MeshCollider
                    MeshCollider meshCollider = hit.collider as MeshCollider;
                    if (meshCollider != null && hit.collider != null)
                    {
                        Vector2 uv;
                        if (TryGetUV(hit, out uv))
                        {
                            PaintTexture(uv);
                        }
                    }
                }
            }
        }
        else
        {
            // Delete brush model when mouse button is released
            if (brushInstance != null)
            {
                Destroy(brushInstance);
            }
        }
    }

    bool TryGetUV(RaycastHit hit, out Vector2 uv)
    {
        uv = Vector2.zero;
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (meshCollider == null || meshCollider.sharedMesh == null)
        {
            return false;
        }

        Mesh mesh = meshCollider.sharedMesh;
        Vector2[] uvs = mesh.uv;
        int[] triangles = mesh.triangles;

        // Get the vertexes of the triangle at the hit point
        int triangleIndex = hit.triangleIndex * 3;
        Vector3 p0 = mesh.vertices[triangles[triangleIndex + 0]];
        Vector3 p1 = mesh.vertices[triangles[triangleIndex + 1]];
        Vector3 p2 = mesh.vertices[triangles[triangleIndex + 2]];

        // Get UV coordinates of triangle at hit point
        Vector2 uv0 = uvs[triangles[triangleIndex + 0]];
        Vector2 uv1 = uvs[triangles[triangleIndex + 1]];
        Vector2 uv2 = uvs[triangles[triangleIndex + 2]];

        // Interpolate UV coordinates based on hit point
        uv = InterpolateUV(hit.point, hit.collider.transform.TransformPoint(p0), hit.collider.transform.TransformPoint(p1), hit.collider.transform.TransformPoint(p2), uv0, uv1, uv2);
        return true;
    }

    Vector2 InterpolateUV(Vector3 hitPoint, Vector3 p0, Vector3 p1, Vector3 p2, Vector2 uv0, Vector2 uv1, Vector2 uv2)
    {
        Vector3 barycentric = Barycentric(hitPoint, p0, p1, p2);
        Vector2 uv = barycentric.x * uv0 + barycentric.y * uv1 + barycentric.z * uv2;
        return uv;
    }

    Vector3 Barycentric(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 v0 = b - a;
        Vector3 v1 = c - a;
        Vector3 v2 = p - a;
        float d00 = Vector3.Dot(v0, v0);
        float d01 = Vector3.Dot(v0, v1);
        float d11 = Vector3.Dot(v1, v1);
        float d20 = Vector3.Dot(v2, v0);
        float d21 = Vector3.Dot(v2, v1);
        float denom = d00 * d11 - d01 * d01;
        float v = (d11 * d20 - d01 * d21) / denom;
        float w = (d00 * d21 - d01 * d20) / denom;
        float u = 1.0f - v - w;
        return new Vector3(u, v, w);
    }

    void PaintTexture(Vector2 uv)
    {
        int centerX = Mathf.FloorToInt(uv.x * texture.width);
        int centerY = Mathf.FloorToInt(uv.y * texture.height);
        int radius = Mathf.FloorToInt(brushSize);

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                float distance = Mathf.Sqrt(x * x + y * y); // Calculate distance in circle
                if (distance <= radius) // Check if it is in circle
                {
                    int pixelX = centerX + x;
                    int pixelY = centerY + y;
                    if (pixelX >= 0 && pixelX < texture.width && pixelY >= 0 && pixelY < texture.height)
                    {
                        float alpha = Mathf.Clamp01(1.0f - (distance / radius)); // Alpha value calculation
                        Color pixelColor = Color.Lerp(texture.GetPixel(pixelX, pixelY), brushColor, alpha * brushColor.a);
                        texture.SetPixel(pixelX, pixelY, pixelColor);
                    }
                }
            }
        }
        texture.Apply();
    }
}
