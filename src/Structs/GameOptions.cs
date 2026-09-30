using AmongUs.GameOptions;

namespace SkeldApi.Structs;

public readonly struct GameOptions(IGameOptions gameOptions)
{
    public readonly IGameOptions Options = gameOptions;

    public byte MapId
    {
        get => Options.TryGetByte(ByteOptionNames.MapId, out var value) ? value : default;
        set => Options.SetByte(ByteOptionNames.MapId, value);
    }

    public float KillCooldown
    {
        get => Options.TryGetFloat(FloatOptionNames.KillCooldown, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.KillCooldown, value);
    }

    public float PlayerSpeedMod
    {
        get => Options.TryGetFloat(FloatOptionNames.PlayerSpeedMod, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.PlayerSpeedMod, value);
    }

    public float ImpostorLightMod
    {
        get => Options.TryGetFloat(FloatOptionNames.ImpostorLightMod, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.ImpostorLightMod, value);
    }

    public float CrewLightMod
    {
        get => Options.TryGetFloat(FloatOptionNames.CrewLightMod, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.CrewLightMod, value);
    }

    public bool VisualTasks
    {
        get => Options.TryGetBool(BoolOptionNames.VisualTasks, out var value) && value;
        set => Options.SetBool(BoolOptionNames.VisualTasks, value);
    }

    public bool GhostsDoTasks
    {
        get => Options.TryGetBool(BoolOptionNames.GhostsDoTasks, out var value) && value;
        set => Options.SetBool(BoolOptionNames.GhostsDoTasks, value);
    }

    public bool ConfirmImpostor
    {
        get => Options.TryGetBool(BoolOptionNames.ConfirmImpostor, out var value) && value;
        set => Options.SetBool(BoolOptionNames.ConfirmImpostor, value);
    }

    public bool AnonymousVotes
    {
        get => Options.TryGetBool(BoolOptionNames.AnonymousVotes, out var value) && value;
        set => Options.SetBool(BoolOptionNames.AnonymousVotes, value);
    }

    public bool Roles
    {
        get => Options.TryGetBool(BoolOptionNames.Roles, out var value) && value;
        set => Options.SetBool(BoolOptionNames.Roles, value);
    }

    public int NumImpostors
    {
        get => Options.TryGetInt(Int32OptionNames.NumImpostors, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.NumImpostors, value);
    }

    public int KillDistance
    {
        get => Options.TryGetInt(Int32OptionNames.KillDistance, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.KillDistance, value);
    }

    public int NumCommonTasks
    {
        get => Options.TryGetInt(Int32OptionNames.NumCommonTasks, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.NumCommonTasks, value);
    }

    public int NumShortTasks
    {
        get => Options.TryGetInt(Int32OptionNames.NumShortTasks, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.NumShortTasks, value);
    }

    public int NumLongTasks
    {
        get => Options.TryGetInt(Int32OptionNames.NumLongTasks, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.NumLongTasks, value);
    }

    public int NumEmergencyMeetings
    {
        get => Options.TryGetInt(Int32OptionNames.NumEmergencyMeetings, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.NumEmergencyMeetings, value);
    }

    public int EmergencyCooldown
    {
        get => Options.TryGetInt(Int32OptionNames.EmergencyCooldown, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.EmergencyCooldown, value);
    }

    public int DiscussionTime
    {
        get => Options.TryGetInt(Int32OptionNames.DiscussionTime, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.DiscussionTime, value);
    }

    public int VotingTime
    {
        get => Options.TryGetInt(Int32OptionNames.VotingTime, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.VotingTime, value);
    }

    public int TaskBarMode
    {
        get => Options.TryGetInt(Int32OptionNames.TaskBarMode, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.TaskBarMode, value);
    }

    public int RulePreset
    {
        get => Options.TryGetInt(Int32OptionNames.RulePreset, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.RulePreset, value);
    }

    public float ShapeshifterCooldown
    {
        get => Options.TryGetFloat(FloatOptionNames.ShapeshifterCooldown, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.ShapeshifterCooldown, value);
    }

    public float ShapeshifterDuration
    {
        get => Options.TryGetFloat(FloatOptionNames.ShapeshifterDuration, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.ShapeshifterDuration, value);
    }

    public float ProtectionDurationSeconds
    {
        get => Options.TryGetFloat(FloatOptionNames.ProtectionDurationSeconds, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.ProtectionDurationSeconds, value);
    }

    public float GuardianAngelCooldown
    {
        get => Options.TryGetFloat(FloatOptionNames.GuardianAngelCooldown, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.GuardianAngelCooldown, value);
    }

    public float ScientistCooldown
    {
        get => Options.TryGetFloat(FloatOptionNames.ScientistCooldown, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.ScientistCooldown, value);
    }

    public float ScientistBatteryCharge
    {
        get => Options.TryGetFloat(FloatOptionNames.ScientistBatteryCharge, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.ScientistBatteryCharge, value);
    }

    public float EngineerCooldown
    {
        get => Options.TryGetFloat(FloatOptionNames.EngineerCooldown, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.EngineerCooldown, value);
    }

    public float EngineerInVentMaxTime
    {
        get => Options.TryGetFloat(FloatOptionNames.EngineerInVentMaxTime, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.EngineerInVentMaxTime, value);
    }

    public float PhantomCooldown
    {
        get => Options.TryGetFloat(FloatOptionNames.PhantomCooldown, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.PhantomCooldown, value);
    }

    public float PhantomDuration
    {
        get => Options.TryGetFloat(FloatOptionNames.PhantomDuration, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.PhantomDuration, value);
    }

    public float TrackerCooldown
    {
        get => Options.TryGetFloat(FloatOptionNames.TrackerCooldown, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.TrackerCooldown, value);
    }

    public float TrackerDuration
    {
        get => Options.TryGetFloat(FloatOptionNames.TrackerDuration, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.TrackerDuration, value);
    }

    public float TrackerDelay
    {
        get => Options.TryGetFloat(FloatOptionNames.TrackerDelay, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.TrackerDelay, value);
    }

    public float NoisemakerAlertDuration
    {
        get => Options.TryGetFloat(FloatOptionNames.NoisemakerAlertDuration, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.NoisemakerAlertDuration, value);
    }

    public float ViperDissolveTime
    {
        get => Options.TryGetFloat(FloatOptionNames.ViperDissolveTime, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.ViperDissolveTime, value);
    }

    public float DetectiveSuspectLimit
    {
        get => Options.TryGetFloat(FloatOptionNames.DetectiveSuspectLimit, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.DetectiveSuspectLimit, value);
    }

    public float JudgeTaskRequirementPercentage
    {
        get => Options.TryGetFloat(FloatOptionNames.JudgeTaskRequirementPercentage, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.JudgeTaskRequirementPercentage, value);
    }

    public bool ShapeshifterLeaveSkin
    {
        get => Options.TryGetBool(BoolOptionNames.ShapeshifterLeaveSkin, out var value) && value;
        set => Options.SetBool(BoolOptionNames.ShapeshifterLeaveSkin, value);
    }

    public bool ImpostorsCanSeeProtect
    {
        get => Options.TryGetBool(BoolOptionNames.ImpostorsCanSeeProtect, out var value) && value;
        set => Options.SetBool(BoolOptionNames.ImpostorsCanSeeProtect, value);
    }

    public bool NoisemakerImpostorAlert
    {
        get => Options.TryGetBool(BoolOptionNames.NoisemakerImpostorAlert, out var value) && value;
        set => Options.SetBool(BoolOptionNames.NoisemakerImpostorAlert, value);
    }

    public float CrewmateTimeInVent
    {
        get => Options.TryGetFloat(FloatOptionNames.CrewmateTimeInVent, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.CrewmateTimeInVent, value);
    }

    public float FinalEscapeTime
    {
        get => Options.TryGetFloat(FloatOptionNames.FinalEscapeTime, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.FinalEscapeTime, value);
    }

    public float EscapeTime
    {
        get => Options.TryGetFloat(FloatOptionNames.EscapeTime, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.EscapeTime, value);
    }

    public float SeekerFinalSpeed
    {
        get => Options.TryGetFloat(FloatOptionNames.SeekerFinalSpeed, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.SeekerFinalSpeed, value);
    }

    public float MaxPingTime
    {
        get => Options.TryGetFloat(FloatOptionNames.MaxPingTime, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.MaxPingTime, value);
    }

    public float CrewmateFlashlightSize
    {
        get => Options.TryGetFloat(FloatOptionNames.CrewmateFlashlightSize, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.CrewmateFlashlightSize, value);
    }

    public float ImpostorFlashlightSize
    {
        get => Options.TryGetFloat(FloatOptionNames.ImpostorFlashlightSize, out var value) ? value : default;
        set => Options.SetFloat(FloatOptionNames.ImpostorFlashlightSize, value);
    }

    public bool UseFlashlight
    {
        get => Options.TryGetBool(BoolOptionNames.UseFlashlight, out var value) && value;
        set => Options.SetBool(BoolOptionNames.UseFlashlight, value);
    }

    public bool SeekerFinalMap
    {
        get => Options.TryGetBool(BoolOptionNames.SeekerFinalMap, out var value) && value;
        set => Options.SetBool(BoolOptionNames.SeekerFinalMap, value);
    }

    public bool SeekerPings
    {
        get => Options.TryGetBool(BoolOptionNames.SeekerPings, out var value) && value;
        set => Options.SetBool(BoolOptionNames.SeekerPings, value);
    }

    public bool ShowCrewmateNames
    {
        get => Options.TryGetBool(BoolOptionNames.ShowCrewmateNames, out var value) && value;
        set => Options.SetBool(BoolOptionNames.ShowCrewmateNames, value);
    }

    public int CrewmateVentUses
    {
        get => Options.TryGetInt(Int32OptionNames.CrewmateVentUses, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.CrewmateVentUses, value);
    }

    public int ImpostorPlayerID
    {
        get => Options.TryGetInt(Int32OptionNames.ImpostorPlayerID, out var value) ? value : default;
        set => Options.SetInt(Int32OptionNames.ImpostorPlayerID, value);
    }

    /// <summary>
    /// Checks if a specific option exists in the current game options.
    /// </summary>
    public bool HasOption<T>(T optionName) where T : Enum
    {
        return optionName switch
        {
            ByteOptionNames byteOption => Options.TryGetByte(byteOption, out _),
            BoolOptionNames boolOption => Options.TryGetBool(boolOption, out _),
            FloatOptionNames floatOption => Options.TryGetFloat(floatOption, out _),
            Int32OptionNames intOption => Options.TryGetInt(intOption, out _),
            _ => false
        };
    }

    /// <summary>
    /// Overrides all options from the source options that exist in both.
    /// Automatically iterates through all enum values.
    /// </summary>
    public void OverrideFrom(IGameOptions sourceOptions)
    {
        // Override all Byte options
        foreach (ByteOptionNames option in Enum.GetValues(typeof(ByteOptionNames)))
        {
            if (option == ByteOptionNames.Invalid) continue;

            if (Options.TryGetByte(option, out var currentValue) &&
                sourceOptions.TryGetByte(option, out var newValue) &&
                currentValue != newValue)
            {
                Options.SetByte(option, newValue);
            }
        }

        // Override all Bool options
        foreach (BoolOptionNames option in Enum.GetValues(typeof(BoolOptionNames)))
        {
            if (option == BoolOptionNames.Invalid) continue;

            if (Options.TryGetBool(option, out var currentValue) &&
                sourceOptions.TryGetBool(option, out var newValue) &&
                currentValue != newValue)
            {
                Options.SetBool(option, newValue);
            }
        }

        // Override all Float options
        foreach (FloatOptionNames option in Enum.GetValues(typeof(FloatOptionNames)))
        {
            if (option == FloatOptionNames.Invalid) continue;

            if (Options.TryGetFloat(option, out var currentValue) &&
                sourceOptions.TryGetFloat(option, out var newValue) &&
                !Math.Abs(currentValue - newValue).Equals(0))
            {
                Options.SetFloat(option, newValue);
            }
        }

        // Override all Int options
        foreach (Int32OptionNames option in Enum.GetValues(typeof(Int32OptionNames)))
        {
            if (option == Int32OptionNames.Invalid) continue;

            if (Options.TryGetInt(option, out var currentValue) &&
                sourceOptions.TryGetInt(option, out var newValue) &&
                currentValue != newValue)
            {
                Options.SetInt(option, newValue);
            }
        }
    }

    /// <summary>
    /// Overrides only specific option types from the source.
    /// </summary>
    public void OverrideFrom<T>(IGameOptions sourceOptions) where T : Enum
    {
        if (typeof(T) == typeof(ByteOptionNames))
        {
            foreach (ByteOptionNames option in Enum.GetValues(typeof(ByteOptionNames)))
            {
                if (option == ByteOptionNames.Invalid) continue;

                if (Options.TryGetByte(option, out var currentValue) &&
                    sourceOptions.TryGetByte(option, out var newValue) &&
                    currentValue != newValue)
                {
                    Options.SetByte(option, newValue);
                }
            }
        }
        else if (typeof(T) == typeof(BoolOptionNames))
        {
            foreach (BoolOptionNames option in Enum.GetValues(typeof(BoolOptionNames)))
            {
                if (option == BoolOptionNames.Invalid) continue;

                if (Options.TryGetBool(option, out var currentValue) &&
                    sourceOptions.TryGetBool(option, out var newValue) &&
                    currentValue != newValue)
                {
                    Options.SetBool(option, newValue);
                }
            }
        }
        else if (typeof(T) == typeof(FloatOptionNames))
        {
            foreach (FloatOptionNames option in Enum.GetValues(typeof(FloatOptionNames)))
            {
                if (option == FloatOptionNames.Invalid) continue;

                if (Options.TryGetFloat(option, out var currentValue) &&
                    sourceOptions.TryGetFloat(option, out var newValue) &&
                    !Math.Abs(currentValue - newValue).Equals(0))
                {
                    Options.SetFloat(option, newValue);
                }
            }
        }
        else if (typeof(T) == typeof(Int32OptionNames))
        {
            foreach (Int32OptionNames option in Enum.GetValues(typeof(Int32OptionNames)))
            {
                if (option == Int32OptionNames.Invalid) continue;

                if (Options.TryGetInt(option, out var currentValue) &&
                    sourceOptions.TryGetInt(option, out var newValue) &&
                    currentValue != newValue)
                {
                    Options.SetInt(option, newValue);
                }
            }
        }
    }
}