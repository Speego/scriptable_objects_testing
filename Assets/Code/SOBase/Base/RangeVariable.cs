using UnityEngine;

namespace SO.Base
{
    [CreateAssetMenu(menuName = "SOExtensions/Base/Range Variable")]
    // it would be better to have a serialized Tuple here but Unity doesn't support it by default so for now it's Vector2
    // it would be good to write a custom editor with a ranged slider
    public class RangeVariable : ScriptableVariable<Vector2>
    {

    }
}
