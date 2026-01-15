using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

public static class ProjectSetup
{
    [MenuItem("Tools/Setup/Import Essential Assets")]
    static void ImmportEssentials()
    {
        Assets.ImportAsset("Hot Reload Edit Code Without Compiling.unitypackage", "The Naughty Cult/Editor ExtensionsUtilities");

        Assets.ImportAsset("Quantum Console.unitypackage", "QFSW/Editor ExtensionsUtilities");

        Assets.ImportAsset("Mouse Button Shortcuts and Selection History.unitypackage", "KAMGAM/Editor ExtensionsUtilities");

        Assets.ImportAsset("vHierarchy 2.unitypackage", "kubacho lab/Editor ExtensionsUtilities");
        Assets.ImportAsset("vInspector 2.unitypackage", "kubacho lab/Editor ExtensionsUtilities");

        Assets.ImportAsset("Asset Inventory 3.unitypackage", "Impossible Robert/Editor ExtensionsUtilities");

        Assets.ImportAsset("Hierarchy Focused Debug Console.unitypackage", "Burning Ice Cream/Editor ExtensionsUtilities");

        Assets.ImportAsset("Time Tweaker - Fast Iteration.unitypackage", "Compile Co/Editor ExtensionsUtilities");

        Assets.ImportAsset("Editor Console Pro.unitypackage", "FlyingWorm/Editor ExtensionsSystem");

        Assets.ImportAsset("Editor Auto Save.unitypackage", "IntenseNation/Editor ExtensionsUtilities");

        Assets.ImportAsset("Feel.unitypackage", "More Mountains/ScriptingEffects");
    }

    [MenuItem("Tools/Setup/Import Essential Packages(none)")]
    static void InstallPackages()
    {
        Packages.InstallPackages(new[]
        {
            ""
        });
    }

    [MenuItem("Tools/Setup/Create Folders")]
    public static void CreateFolders()
    {
        Folders.Create("", "Prefabs", "Materials", "Scripts", "Scripts/ScriptableObjectScripts", "Sprites", "Import");
        AssetDatabase.Refresh();
        Folders.Delete("TutorialInfo");
        AssetDatabase.Refresh();

        const string pathToInputAction = "InputSystem_Actions.inputactions";
        string destination = "/Settings/InputSystem_Actions.inputactions";
        AssetDatabase.MoveAsset(pathToInputAction, destination);

        const string pathToReadme = "Readme.asset";
        AssetDatabase.DeleteAsset(pathToReadme);
        AssetDatabase.Refresh();

    }

    static class Assets
    {
        public static void ImportAsset(string asset, string folder)
        {
            string basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            string assetsFolder = System.IO.Path.Combine(basePath, "Unity/Asset Store-5.x");

            UnityEditor.AssetDatabase.ImportPackage(Path.Combine(assetsFolder, folder, asset), false);
        }
    }

    static class Packages
    {
        static AddRequest request;
        static Queue<string> packagesToInstall = new Queue<string>();

        static async void StartNextPackageInstallation()
        {
            request = Client.Add(packagesToInstall.Dequeue());

            while (!request.IsCompleted) await Task.Delay(10);

            if (request.Status == StatusCode.Success) Debug.Log("Installed: " + request.Result.packageId);
            else if (request.Status >= StatusCode.Failure) Debug.LogError(request.Error.message);

            if (packagesToInstall.Count > 0)
            {
                await Task.Delay(1000);
                StartNextPackageInstallation();
            }
        }
        
        public static void InstallPackages(string[] packages)
        {
            foreach(string package in packages)
            {
                packagesToInstall.Enqueue(package);
            }

            if (packagesToInstall.Count > 0)
            {
                StartNextPackageInstallation();
            }
        }
    }

    static class Folders
    {
        public static void Delete(string folderName)
        {
            string pathToDelete = $"Assets/{folderName}";

            if (AssetDatabase.IsValidFolder(pathToDelete))
            {
                AssetDatabase.DeleteAsset(pathToDelete);
            }
        }

        public static void Move(string newParent, string folderName)
        {
            string sourcePath = $"Assets/{folderName}";
            if (AssetDatabase.IsValidFolder(sourcePath))
            {
                string destinationPath = $"Assets/{newParent}/{folderName}";
                string error = AssetDatabase.MoveAsset(sourcePath, destinationPath);

                if (!string.IsNullOrEmpty(error))
                {
                    Debug.LogError($"Failed to move {folderName}: {error}");
                }
            }
        }

        static void CreateSubFolders(string rootPath, string folderHierarchy)
        {
            var folders = folderHierarchy.Split('/');
            var  currentPath = rootPath;

            foreach (var folder in folders)
            {
                currentPath = Path.Combine(currentPath, folder);
                if (!Directory.Exists(currentPath))
                {
                    Directory.CreateDirectory(currentPath);
                }
            }
        }

        public static void Create(string root, params string[] folders)
        {
            var fullPath = Path.Combine(Application.dataPath, root);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            foreach (var folder in folders)
            {
                CreateSubFolders(fullPath, folder);
            }
        }
    }
}
