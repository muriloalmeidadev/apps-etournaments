﻿namespace Caesareum.ETournamentsApp.Core.Settings;

public sealed class CorsSettings
{
    public string[] AllowedHeaders { get; set; }
    public string[] AllowedOrigins { get; set; }
}