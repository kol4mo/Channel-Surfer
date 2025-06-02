using UnityEngine;

/// <summary>
/// Subject Interface: Defines the expected behavior for TV content
/// This interface represents the common operations that both the real subject and proxy will implement
/// </summary>
public interface ITVContent
{
    /// <summary>
    /// Loads the content for the TV channel
    /// </summary>
    void LoadContent();

    /// <summary>
    /// Displays the content on the TV screen
    /// </summary>
    void DisplayContent();

    /// <summary>
    /// Gets the current channel information
    /// </summary>
    /// <returns>Channel information including name and number</returns>
    ChannelInfo GetChannelInfo();
}

/// <summary>
/// Data structure to hold channel information
/// </summary>
public struct ChannelInfo
{
    public string ChannelName;
    public int ChannelNumber;
    public bool IsPremium;
} 