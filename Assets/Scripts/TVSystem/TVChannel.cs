using UnityEngine;

/// <summary>
/// Real Subject: Implements the actual TV channel functionality
/// This class handles the real implementation of TV content loading and display
/// </summary>
public class TVChannel : ITVContent
{
    private ChannelInfo _channelInfo;
    private Texture2D _channelContent;
    private bool _isContentLoaded;

    public TVChannel(ChannelInfo channelInfo)
    {
        _channelInfo = channelInfo;
        _isContentLoaded = false;
    }

    public void LoadContent()
    {
        // In a real implementation, this would load the actual channel content
        // For example, loading a video stream or image
        Debug.Log($"Loading content for channel {_channelInfo.ChannelNumber}: {_channelInfo.ChannelName}");
        _isContentLoaded = true;
    }

    public void DisplayContent()
    {
        if (!_isContentLoaded)
        {
            LoadContent();
        }
        // In a real implementation, this would display the content on the TV screen
        Debug.Log($"Displaying content for channel {_channelInfo.ChannelNumber}: {_channelInfo.ChannelName}");
    }

    public ChannelInfo GetChannelInfo()
    {
        return _channelInfo;
    }
} 