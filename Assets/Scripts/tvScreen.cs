using UnityEngine;

/// <summary>
/// Client: Uses the proxy to access TV content
/// This component manages the TV screen and channel switching
/// </summary>
public class tvScreen : MonoBehaviour
{
    private ITVContent _currentChannel;
    private bool _isPremiumUser = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Example: Create a regular channel
        var regularChannelInfo = new ChannelInfo
        {
            ChannelName = "Regular Channel",
            ChannelNumber = 1,
            IsPremium = false
        };
        _currentChannel = new TVChannelProxy(regularChannelInfo, _isPremiumUser);
    }

    // Update is called once per frame
    void Update()
    {
        // Example: Switch channels with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchChannel(1, "Regular Channel", false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchChannel(2, "Premium Channel", true);
        }
    }

    private void SwitchChannel(int channelNumber, string channelName, bool isPremium)
    {
        var channelInfo = new ChannelInfo
        {
            ChannelName = channelName,
            ChannelNumber = channelNumber,
            IsPremium = isPremium
        };
        
        _currentChannel = new TVChannelProxy(channelInfo, _isPremiumUser);
        _currentChannel.DisplayContent();
    }

    // Method to toggle premium status (for testing)
    public void TogglePremiumStatus()
    {
        _isPremiumUser = !_isPremiumUser;
        Debug.Log($"Premium status: {_isPremiumUser}");
    }
}
