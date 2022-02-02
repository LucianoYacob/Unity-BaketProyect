using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour
{
    private static readonly string gameId = "3869811";
    private static readonly string videoAd = "video";

    public bool showAds;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId, false);
    }

    private void Update()
    {
        if (showAds)
        {
            Advertisement.Show(videoAd);
            Advertisement.Banner.Hide();
            showAds = false;
        }
    }
}
