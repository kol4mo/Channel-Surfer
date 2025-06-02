using UnityEngine;

/// <summary>
/// Proxy: Controls access to the real subject (TVChannel)
/// This class manages access to TV channels, including premium content and lazy loading
/// </summary>
public class TVChannelProxy : ITVContent
{
    private TVChannel _realChannel;
    private ChannelInfo _channelInfo;
    private bool _isPremiumUser;

    public TVChannelProxy(ChannelInfo channelInfo, bool isPremiumUser = false)
    {
        _channelInfo = channelInfo;
        _isPremiumUser = isPremiumUser;
    }

    public void LoadContent()
    {
        if (_realChannel == null)
        {
            // Lazy initialization of the real subject
            _realChannel = new TVChannel(_channelInfo);
        }
        _realChannel.LoadContent();
    }

    public void DisplayContent()
    {
        // Check if user has access to premium content
        if (_channelInfo.IsPremium && !_isPremiumUser)
        {
            Debug.Log($"Access denied: Channel {_channelInfo.ChannelNumber} is premium content");
            return;
        }

        if (_realChannel == null)
        {
            LoadContent();
        }
        _realChannel.DisplayContent();
    }

    public ChannelInfo GetChannelInfo()
    {
        return _channelInfo;
    }
} 