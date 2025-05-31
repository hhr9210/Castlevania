using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class RetroTVEffect : MonoBehaviour
{
    public Material tvMaterial;     // ԭ����Ч������
    public Material bloomMaterial;  // �¼ӵ� bloom ����

    private RenderTexture tempTexture;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (bloomMaterial != null && tvMaterial != null)
        {
            RenderTexture temp = RenderTexture.GetTemporary(src.width, src.height);

            // �� Bloom
            Graphics.Blit(src, temp, bloomMaterial);

            // �� TV Ч��
            Graphics.Blit(temp, dest, tvMaterial);

            RenderTexture.ReleaseTemporary(temp);
        }
        else if (bloomMaterial != null)
        {
            Graphics.Blit(src, dest, bloomMaterial);
        }
        else if (tvMaterial != null)
        {
            Graphics.Blit(src, dest, tvMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

}
