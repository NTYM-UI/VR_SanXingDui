using UnityEngine;

public class SetContAndChoseGlass : MonoBehaviour
{
    public Material glassMat;
    private int testNum = 1;

    private float glassObjLength;
    private float minVertexPosition = 0.0f;

    public GameObject Perfab_AO;
    public GameObject Perfab_TU;
    public GameObject Perfab_Glass;
  
    void Start()
    {
        //从操作者来讲，这个凸透镜距离应该比较近才启用，但是从进入场景的情况看，进入是凹透镜的情况会多一些；
        Perfab_AO.SetActive(true);//距离远的，倒立缩小的像；
        Perfab_TU.SetActive(false);//放大镜效果；
    }

    void Update()
    {
        //获取模型顶点的，前提是：此脚本需要赋予给需要观察的模型，并且模型具有 Mesh 组件；
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] verticels = mesh.vertices;

        for (int i = 0; i < verticels.Length; i++)
        {
            float minCont = (Perfab_Glass.transform.position - verticels[i]).sqrMagnitude;//sqrMagnitude 得到的是一个三维向量的距离值的 开方的值；节省资源消耗，避免先开方操作；/////对应的是开方之后的：Vector3.Magnitude；
            if (i == 0)
            {
                minVertexPosition = minCont;
            }
            if (i != 0)
            {
                if (minVertexPosition >= minCont)
                {
                    minVertexPosition = minCont;
                }
            }
        }
    }

    void FixedUpdate()
    {
        //用来判断距离和镜片的启用关系和材质球的参数控制控制逻辑；
        
        glassObjLength = Mathf.Sqrt(minVertexPosition);

        if (glassObjLength > 4)
        {
            testNum = 1;
            Perfab_AO.SetActive(true);
            Perfab_TU.SetActive(false);

            glassMat.SetFloat("_MagnifyingPower", -0.5f);
        }
        if (glassObjLength < 2)
        {
            testNum = 0;
            Perfab_AO.SetActive(false);
            Perfab_TU.SetActive(true);

            glassMat.SetFloat("_MagnifyingPower", 0.5f);
        }
        if (4 >= glassObjLength && 2 <= glassObjLength && testNum == 1)
        {
            if (4 >= glassObjLength && 3 <= glassObjLength)
            {
                Perfab_AO.SetActive(true);
                Perfab_TU.SetActive(false);
                glassMat.SetFloat("_MagnifyingPower", -3.0f * glassObjLength + 11.5f);
            }
            if (3 > glassObjLength && 2 <= glassObjLength)
            {
                Perfab_AO.SetActive(false);
                Perfab_TU.SetActive(true);
                glassMat.SetFloat("_MagnifyingPower", 2.0f * glassObjLength - 3.5f);
            }
        }
        if (4 >= glassObjLength && 2 <= glassObjLength && testNum == 0)
        {
            if (2 <= glassObjLength && 3 > glassObjLength)
            {
                Perfab_AO.SetActive(false);
                Perfab_TU.SetActive(true);
                glassMat.SetFloat("_MagnifyingPower", 2.0f * glassObjLength - 3.5f);
            }
            if (4 >= glassObjLength && 3 <= glassObjLength)
            {
                Perfab_AO.SetActive(true);
                Perfab_TU.SetActive(false);
                glassMat.SetFloat("_MagnifyingPower", -3.0f * glassObjLength + 11.5f);
            }
        }
    }
}
