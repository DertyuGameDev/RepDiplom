using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PostProc : MonoBehaviour
{
    public Slider contr, br;
    public VolumeProfile volume;
    public Bloom b;
    public ColorAdjustments c;
    private void Start()
    {
        volume.TryGet<Bloom>(out b);
        volume.TryGet<ColorAdjustments>(out c);
    }
    public void Update()
    {
        c.contrast.value = contr.value;
        b.intensity.value = br.value;
    }
}
