using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonitorAppsManagerUI : MonoBehaviour
{
    public enum ApplicationType
    {
        DataContainerPasswordScreen,
        DoorApp,
        NotepadApp,
        PasswordCrackingApp,
        TracingApp,
    }

    [SerializeField]
    private Transform appsHolder;

    [SerializeField]
    private DataContainerPasswordScreenUI dataContainerPasswordScreenPrefab;
    [SerializeField]
    private DoorAppUI doorAppPrefab;
    [SerializeField]
    private NotepadAppUI notepadAppPrefab;
    [SerializeField]
    private PasswordCrackingAppUI passwordCrackingAppPrefab;
    [SerializeField]
    private TracingAppUI tracingAppPrefab;

    private Dictionary<ApplicationType, MonitorAppUI> openApps;
    private List<ApplicationType> lastFocusedApps;

    private void Start()
    {
        openApps = new();
        lastFocusedApps = new();

        PowerManager.Instance.OnPowerStateChanged += (isPowerOn) =>
        {
            if (!isPowerOn)
            {
                CloseAllApps();
            }
        };

        ServerConnectionManager.Instance.OnServerConnectionStateChanged += (isConnected) =>
        {
            if (!isConnected)
            {
                CloseAllApps();
            }
        };
    }

    public bool TryOpenApp(ApplicationType appType, out MonitorAppUI monitorAppUI)
    {
        if (IsAppOpen(appType))
        {
            monitorAppUI = null;
            return false;
        }

        monitorAppUI = OpenApplication(appType);
        return true;
    }

    public MonitorAppUI ForceOpenApp(ApplicationType appType)
    {
        TryCloseApp(appType);
        return OpenApplication(appType);
    }

    private MonitorAppUI OpenApplication(ApplicationType appType)
    {
        MonitorAppUI instantiatedApp;

        switch (appType)
        {
            default:
                return null;
            case ApplicationType.DataContainerPasswordScreen:
                instantiatedApp = Instantiate(dataContainerPasswordScreenPrefab.gameObject, appsHolder).GetComponent<MonitorAppUI>();
                break;
            case ApplicationType.DoorApp:
                instantiatedApp = Instantiate(doorAppPrefab.gameObject, appsHolder).GetComponent<MonitorAppUI>();
                break;
            case ApplicationType.NotepadApp:
                instantiatedApp = Instantiate(notepadAppPrefab.gameObject, appsHolder).GetComponent<MonitorAppUI>();
                break;
            case ApplicationType.PasswordCrackingApp:
                instantiatedApp = Instantiate(passwordCrackingAppPrefab.gameObject, appsHolder).GetComponent<MonitorAppUI>();
                break;
            case ApplicationType.TracingApp:
                instantiatedApp = Instantiate(tracingAppPrefab.gameObject, appsHolder).GetComponent<MonitorAppUI>();
                break;
        }
        instantiatedApp.InitializeApp(this, appType);
        openApps[appType] = instantiatedApp;
        lastFocusedApps.Add(appType);
        return instantiatedApp;
    }

    public bool IsAppOpen(ApplicationType applicationType)
    {
        return openApps.ContainsKey(applicationType) && openApps[applicationType] != null;
    }

    public bool IsAppMinimized(ApplicationType applicationType)
    {
        return openApps[applicationType].IsMinimized;
    }

    public bool IsAppFocused(ApplicationType applicationType)
    {
        return lastFocusedApps.Count > 0 && applicationType == lastFocusedApps[^1];
    }

    public MonitorAppUI GetOpenApp(ApplicationType applicationType)
    {
        return openApps[applicationType];
    }

    public void ToggleMinimizeApp(ApplicationType applicationType)
    {
        MonitorAppUI monitorApp = openApps[applicationType];
        bool targetState = !monitorApp.IsMinimized;
        monitorApp.SetAppMinimized(targetState);
        if (!targetState)
        {
            FocusApp(applicationType);
        }
        else
        {
            TryRemoveFocusedApp(applicationType);
            FocusLastApp();
        }
    }

    public void TryMinimizeApp(ApplicationType applicationType)
    {
        if (IsAppOpen(applicationType) && !openApps[applicationType].IsMinimized)
        {
            ToggleMinimizeApp(applicationType);
        }
    }

    public void FocusApp(ApplicationType applicationType)
    {
        MonitorAppUI montiorApp = openApps[applicationType];
        montiorApp.BringAppToFront();
        TryRemoveFocusedApp(applicationType);
        lastFocusedApps.Add(applicationType);
    }

    private void FocusLastApp()
    {
        if (lastFocusedApps.Count > 0)
        {
            ApplicationType applicationType = lastFocusedApps[^1];
            FocusApp(applicationType);
        }
    }

    private void CloseApp(ApplicationType applicationType)
    {
        MonitorAppUI monitorApp = openApps[applicationType];
        monitorApp.CloseAppFromAppsManager();
        openApps[applicationType] = null;
        TryRemoveFocusedApp(applicationType);
        FocusLastApp();
    }

    private bool TryRemoveFocusedApp(ApplicationType applicationType)
    {
        if (lastFocusedApps.Contains(applicationType))
        {
            lastFocusedApps.Remove(applicationType);
            return true;
        }

        return false;
    }

    public bool TryCloseApp(ApplicationType appType)
    {
        if (IsAppOpen(appType))
        {
            CloseApp(appType);
            return true;
        }

        return false;
    }

    public void CloseAllApps()
    {
        var allAppTypes = Enum.GetValues(typeof(ApplicationType));

        foreach (ApplicationType appType in allAppTypes)
        {
            TryCloseApp(appType);
        }
    }
}