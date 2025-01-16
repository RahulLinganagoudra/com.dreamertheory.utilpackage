using UnityEngine;

namespace DT_Helpers
{
	public class AdManager : MonoBehaviour//,IUnityAdsListener
	{

		[SerializeField] private string rewardedVideoPlacement = "rewardedVideo";
		[SerializeField] private string videoPlacement = "rewardedVideo";
		[SerializeField] private string gameId;

		public static AdManager instance;
		public string RewardedVideo
		{
			get { return rewardedVideoPlacement; }
		}
		public string Video
		{
			get { return videoPlacement; }
		}


		private void Awake()
		{
			instance = this;

		}

		private void Start()
		{
			// Advertisement.AddListener(this);
			// Advertisement.Initialize(gameId,true);
			Debug.LogError("Admob not Installed");
			DontDestroyOnLoad(gameObject);
		}
		public void ShowAds(string p)
		{
			// Advertisement.Show(p);
		}

		//public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
		//{
		//    if (showResult==ShowResult.Finished)
		//    {

		//        //RewardPlayer
		//    }
		//    else
		//    {
		//        //do nothing
		//    }
		//}
		public void OnUnityAdsReady(string placementId)
		{

		}

		public void OnUnityAdsDidError(string message)
		{

		}

		public void OnUnityAdsDidStart(string placementId)
		{

		}
	}
}