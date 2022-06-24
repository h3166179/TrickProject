using UnityEditor;
using System.Collections;
using UnityEngine.Assertions;

namespace QFramework.ResKit.Tests
{
	public class ResKitTests
	{

	
		public void ResKit_TestPlatformName()
		{
			var platformName = FromUnityToDll.Setting.GetPlatformName();
			var abPlatformName = SettingFromUnityToDll.GetPlatformForAssetBundles(EditorUserBuildSettings.activeBuildTarget);

			Assert.AreEqual(platformName, abPlatformName);
		}

		// A UnityTest behaves like a coroutine in PlayMode
		// and allows you to yield null to skip a frame in EditMode
	
		public IEnumerator ResKitTestsWithEnumeratorPasses()
		{
			// Use the Assert class to test conditions.
			// yield to skip a frame
			yield return null;
		}
	}
}