using UnityEditor;

namespace Editor
{
    public class CreateAssetBundles
    {
        [MenuItem("Assets/Build AssetBundles")]
        static void Build()
        {
            BuildPipeline
                .BuildAssetBundles("Assets/AssetBundles",
                    BuildAssetBundleOptions.None,
                    BuildTarget.StandaloneWindows64);
        }
    }
}
