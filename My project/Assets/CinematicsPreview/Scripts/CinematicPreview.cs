using NUnit.Framework.Constraints;
using UnityEngine;

[CreateAssetMenu(fileName = "CinematicPreview", menuName = "Scriptable Objects/CinematicPreview")]
public class CinematicPreview : ScriptableObject
{
    [Header("Position Curves")]
    public AnimationCurve posX;
    public AnimationCurve posY;
    public AnimationCurve posZ;
    [Header("Rotation Curves")]
    public AnimationCurve rotX;
    public AnimationCurve rotY;
    public AnimationCurve rotZ;
    [Header("Slider")]
    public float totalTime = 0f;
}
