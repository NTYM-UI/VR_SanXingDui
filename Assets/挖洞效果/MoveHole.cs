using System.Collections.Generic;
using UnityEngine;

public class MoveHole : MonoBehaviour
{
    /// <summary>
    /// 复制网格
    /// </summary>
    private static Mesh CopyMesh(Mesh mesh)
    {
        return new Mesh
        {
            vertices = mesh.vertices,//顶点
            triangles = mesh.triangles,//三角形
            uv = mesh.uv,           //UV
            normals = mesh.normals,//法线
            colors = mesh.colors,//颜色值
            tangents = mesh.tangents//切线
        };
    }

    private MeshFilter mf;
    private MeshCollider mc;

    private Mesh useMesh;//最后要使用到的网格
    private List<Vector3> meshVertexsBegin  , meshVertexsGame;
    private List<int> circleVertexIndex , boxVertexIndex;

    public float x, y;
    public float radius = 1;
    public float SceneSize = 1;

    private void Awake()
    {
        mf = GetComponent<MeshFilter>();
        mc = GetComponent<MeshCollider>();
        mf.mesh = useMesh = CopyMesh(mf.mesh);//复制网格

        meshVertexsBegin = new List<Vector3>(useMesh.vertices) ;//将原本的顶点信息存在List中
        meshVertexsGame = new List<Vector3>(useMesh.vertices);//将原本的顶点信息存在List中

        circleVertexIndex = new List<int>();
        boxVertexIndex = new List<int>();
        
        var index = 0;// 记录index
        foreach (var vert in meshVertexsBegin)
        {
            //如果顶点的xy距离（0，0）小于2时，顶点为模型中间的那个园的顶点，否则为外围的顶点
            if (Vector2.Distance(new Vector2(vert.x, vert.y), Vector2.zero) < 0.02)
                circleVertexIndex.Add(index);//记录模型中间的圆形顶点的下标
            else
                boxVertexIndex.Add(index);//记录外围顶点的下标
            index++;
        }
    }

    private void FixedUpdate()
    {
        foreach (var cv in circleVertexIndex)
        {
            meshVertexsGame[cv] = new Vector3
            {
                x = meshVertexsBegin[cv].x * radius + x,//radius控制圆的半径大小，x控制左右偏移的坐标
                y = meshVertexsBegin[cv].y * radius + y,//radius控制圆的半径大小，y控制上下偏移的坐标
                z = meshVertexsBegin[cv].z              //z值保持不变
            };
        }
        foreach (var bv in boxVertexIndex)
        {
            meshVertexsGame[bv] = new Vector3
            {
                x = meshVertexsBegin[bv].x * SceneSize,//控制模型外围x的大小
                y = meshVertexsBegin[bv].y * SceneSize,//控制模型外围y的大小
                z = meshVertexsBegin[bv].z              //z值保持不变
            };
        }
        useMesh.SetVertices(meshVertexsGame);//将新网格的顶点信息赋值给要使用的网格
        mf.mesh = useMesh;
        mc.sharedMesh = useMesh;
    }
}
