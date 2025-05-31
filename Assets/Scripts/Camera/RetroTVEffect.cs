using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class RetroTVEffect : MonoBehaviour
{
    public Material tvMaterial;     // 原来的效果材质
    public Material bloomMaterial;  // 新加的 bloom 材质

    private RenderTexture tempTexture;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (bloomMaterial != null && tvMaterial != null)
        {
            RenderTexture temp = RenderTexture.GetTemporary(src.width, src.height);

            // 先 Bloom
            Graphics.Blit(src, temp, bloomMaterial);

            // 再 TV 效果
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
