// <copyright file="PluginConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UniqueDinosaurs.Configuration;

using System;
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
        get; set;
    }

    /// <summary>
    /// Gets or sets a string setting.
    /// </summary>
    public string Output
    {
        get
        {
            if (this.Command != string.Empty)
            {
                try
                {
                    System.Diagnostics.ProcessStartInfo procStartInfo =
                        new System.Diagnostics.ProcessStartInfo("/bin/bash", "-c '" + this.Command + "'")
                        {
                            RedirectStandardOutput = true,

                            UseShellExecute = false,

                            CreateNoWindow = true,
                        };

                    System.Diagnostics.Process proc = new System.Diagnostics.Process
                    {
                        StartInfo = procStartInfo,
                    };

                    proc.Start();

                    this.Output = proc.StandardOutput.ReadToEnd();
                }
                catch (Exception e)
                {
                    this.Output = e.Message;
                }

                return this.Output;
            }
            else
            {
                this.Output = "No command set";
                return this.Output;
            }
        }
        set { }
    }
}
