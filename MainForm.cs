using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WMPLib;
using ReaperV2;


namespace testrun2
{
    public partial class ReaperV1 : Form
        
    {
        public Point mouseLocation;
        private string filePath = @"C:\Program Files (x86)\Steam\steamapps\common\ARK\Engine\Config\BaseDeviceProfiles.ini";
        public ReaperV1()
        {
            InitializeComponent();
        }

        private void ReaperV1_Load(object sender, EventArgs e)
        {
            PlayLaunchSound();
        }

        private void PlayLaunchSound()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string soundFilePath = Path.Combine(appDirectory, "launch.mp3");

            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.URL = soundFilePath;
            player.controls.play();
        }


        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimizeLabel_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Default_Click(object sender, EventArgs e)
        {
            try
            {
                // Use the same logic as your working Open_File_Click
                string steamPath = Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",
                    "InstallPath", null) as string
                    ?? Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam",
                    "InstallPath", null) as string;

                if (steamPath == null)
                {
                    MessageBox.Show("Steam installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string arkDir = Path.Combine(steamPath, "steamapps", "common", "ARK", "Engine", "Config");
                string filePath = Path.Combine(arkDir, "BaseDeviceProfiles.ini");

                // Check if ARK directory exists
                if (!Directory.Exists(arkDir))
                {
                    MessageBox.Show("ARK installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ensure directory exists
                Directory.CreateDirectory(arkDir);

                // Handle read-only files
                if (File.Exists(filePath))
                {
                    FileAttributes attributes = File.GetAttributes(filePath);
                    if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                    }
                }

                // YOUR INI CONTENT GOES HERE
                string fileContents = "[DeviceProfiles]\r\n" +
                    "+DeviceProfileNameAndTypes=Windows,Windows\r\n" +
                    "+DeviceProfileNameAndTypes=WindowsNoEditor,WindowsNoEditor\r\n" +
                    "+DeviceProfileNameAndTypes=WindowsServer,WindowsServer\r\n" +
                    "+DeviceProfileNameAndTypes=IOS,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPad2,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPad3,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPad4,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPadAir,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPadMini,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPadMini2,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPhone4,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPhone4S,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPhone5,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPhone5S,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPodTouch5,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPhone6,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=iPhone6Plus,IOS\r\n" +
                    "+DeviceProfileNameAndTypes=Android,Android\r\n" +
                    "+DeviceProfileNameAndTypes=PS4,PS4\r\n" +
                    "+DeviceProfileNameAndTypes=XboxOne,XboxOne\r\n" +
                    "+DeviceProfileNameAndTypes=HTML5,HTML5\r\n" +
                    "+DeviceProfileNameAndTypes=Mac,Mac\r\n" +
                    "+DeviceProfileNameAndTypes=MacNoEditor,MacNoEditor\r\n" +
                    "+DeviceProfileNameAndTypes=MacServer,MacServer\r\n" +
                    "+DeviceProfileNameAndTypes=WinRT,WinRT\r\n" +
                    "+DeviceProfileNameAndTypes=Linux,Linux\r\n" +
                    "+DeviceProfileNameAndTypes=LinuxNoEditor,LinuxNoEditor\r\n" +
                    "+DeviceProfileNameAndTypes=LinuxServer,LinuxServer\r\n\r\n" +
                    "[Windows DeviceProfile]\r\nDeviceType=Windows\r\nBaseProfileName=\r\n\r\n" +
                    "[WindowsNoEditor DeviceProfile]\r\nDeviceType=WindowsNoEditor\r\nBaseProfileName=Windows\r\n\r\n" +
                    "[WindowsServer DeviceProfile]\r\nDeviceType=WindowsServer\r\nBaseProfileName=Windows\r\n\r\n" +
                    "[IOS DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.RefractionQuality=0\r\n\r\n" +
                    "[iPad2 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n" +
                    "[iPad3 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n" +
                    "[iPad4 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n" +
                    "[iPadAir DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.BloomQuality=1\r\n\r\n" +
                    "[iPadMini DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n" +
                    "[iPadMini2 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=iPadAir\r\n\r\n" +
                    "[iPhone4 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n" +
                    "[iPhone4S DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n" +
                    "[iPhone5 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n" +
                    "[iPhone5S DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=2\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n" +
                    "[iPodTouch5 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n" +
                    "[iPhone6 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=0\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n" +
                    "[iPhone6Plus DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=0\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n" +
                    "[Android DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=\r\n+CVars=r.MobileContentScaleFactor=1\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.RefractionQuality=0\r\n\r\n" +
                    "[Android_Low DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.MobileContentScaleFactor=0.5\r\n\r\n" +
                    "[Android_Mid DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.MobileContentScaleFactor=0.8\r\n\r\n" +
                    "[Android_High DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n+CVars=r.MobileContentScaleFactor=1.0\r\n\r\n" +
                    "[Android_Unrecognized DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Mid\r\n\r\n" +
                    "[Android_Adreno320 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Mid\r\n\r\n" +
                    ";This offset needs to be set for the mosaic fallback to work on Galaxy S4 (SAMSUNG-IGH-I337)\r\n;+CVars=r.DemosaicVposOffset=0.5\r\n\r\n" +
                    "[Android_Adreno330 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_High\r\n\r\n" +
                    "[Android_Adreno330_Ver53 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Adreno330\r\n+CVars=r.DisjointTimerQueries=1\r\n\r\n" +
                    "[PS4 DeviceProfile]\r\nDeviceType=PS4\r\nBaseProfileName=\r\n\r\n" +
                    "[XboxOne DeviceProfile]\r\nDeviceType=XboxOne\r\nBaseProfileName=\r\n+CVars=r.TonemapperQuality=0\r\n+CVars=r.AmbientOcclusionSampleSetQuality=0\r\n+CVars=r.AmbientOcclusionLevels=1\r\n+CVars=r.AmbientOcclusionRadiusScale=2\r\n\r\n" +
                    "[HTML5 DeviceProfile]\r\nDeviceType=HTML5\r\nBaseProfileName=\r\n+CVars=r.RefractionQuality=0\r\n\r\n" +
                    "[Mac DeviceProfile]\r\nDeviceType=Mac\r\nBaseProfileName=\r\n\r\n" +
                    "[MacNoEditor DeviceProfile]\r\nDeviceType=MacNoEditor\r\nBaseProfileName=Mac\r\n\r\n" +
                    "[MacServer DeviceProfile]\r\nDeviceType=MacServer\r\nBaseProfileName=Mac\r\n\r\n" +
                    "[WinRT DeviceProfile]\r\nDeviceType=WinRT\r\nBaseProfileName=\r\n\r\n" +
                    "[Linux DeviceProfile]\r\nDeviceType=Linux\r\nBaseProfileName=\r\nMeshLODSettings=\r\nTextureLODSettings=\r\n\r\n" +
                    "[LinuxNoEditor DeviceProfile]\r\nDeviceType=LinuxNoEditor\r\nBaseProfileName=Linux\r\nMeshLODSettings=\r\nTextureLODSettings=\r\n\r\n" +
                    "[LinuxServer DeviceProfile]\r\nDeviceType=LinuxServer\r\nBaseProfileName=Linux\r\nMeshLODSettings=\r\nTextureLODSettings=";

                // Write with error handling
                try
                {
                    File.WriteAllText(filePath, fileContents);
                    MessageBox.Show("Configuration updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Access denied. Please run as administrator or close ARK/Steam.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"File access error: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Soft_Click(object sender, EventArgs e)
        {
            try
            {
                // Use the same logic as your working Open_File_Click
                string steamPath = Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",
                    "InstallPath", null) as string
                    ?? Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam",
                    "InstallPath", null) as string;

                if (steamPath == null)
                {
                    MessageBox.Show("Steam installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string arkDir = Path.Combine(steamPath, "steamapps", "common", "ARK", "Engine", "Config");
                string filePath = Path.Combine(arkDir, "BaseDeviceProfiles.ini");

                // Check if ARK directory exists
                if (!Directory.Exists(arkDir))
                {
                    MessageBox.Show("ARK installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ensure directory exists
                Directory.CreateDirectory(arkDir);

                // Handle read-only files
                if (File.Exists(filePath))
                {
                    FileAttributes attributes = File.GetAttributes(filePath);
                    if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                    }
                }

                // Your file content
                string fileContents = "test-i-got-no-soft-ini-4-some-reason-use-other-ones";

                // Write with error handling
                try
                {
                    File.WriteAllText(filePath, fileContents);
                    MessageBox.Show("Configuration updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Access denied. Please run as administrator or close ARK/Steam.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"File access error: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void hard_Click(object sender, EventArgs e)
        {
            try
            {
                // Use the same logic as your working Open_File_Click
                string steamPath = Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",
                    "InstallPath", null) as string
                    ?? Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam",
                    "InstallPath", null) as string;

                if (steamPath == null)
                {
                    MessageBox.Show("Steam installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string arkDir = Path.Combine(steamPath, "steamapps", "common", "ARK", "Engine", "Config");
                string filePath = Path.Combine(arkDir, "BaseDeviceProfiles.ini");

                // Check if ARK directory exists
                if (!Directory.Exists(arkDir))
                {
                    MessageBox.Show("ARK installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ensure directory exists
                Directory.CreateDirectory(arkDir);

                // Handle read-only files
                if (File.Exists(filePath))
                {
                    FileAttributes attributes = File.GetAttributes(filePath);
                    if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                    }
                }

                // YOUR INI CONTENT GOES HERE
                string fileContents = "[DeviceProfiles]\r\n + DeviceProfileNameAndTypes = Windows,Windows\r\n + DeviceProfileNameAndTypes = WindowsNoEditor,WindowsNoEditor\r\n + DeviceProfileNameAndTypes = WindowsServer,WindowsServer\r\n + DeviceProfileNameAndTypes = IOS,IOS\r\n + DeviceProfileNameAndTypes = iPad2,IOS\r\n + DeviceProfileNameAndTypes = iPad3,IOS\r\n + DeviceProfileNameAndTypes = iPad4,IOS\r\n + DeviceProfileNameAndTypes = iPadAir,IOS\r\n + DeviceProfileNameAndTypes = iPadMini,IOS\r\n + DeviceProfileNameAndTypes = iPadMini2,IOS\r\n + DeviceProfileNameAndTypes = iPhone4,IOS\r\n + DeviceProfileNameAndTypes = iPhone4S,IOS\r\n + DeviceProfileNameAndTypes = iPhone5,IOS\r\n + DeviceProfileNameAndTypes = iPhone5S,IOS\r\n + DeviceProfileNameAndTypes = iPodTouch5,IOS\r\n + DeviceProfileNameAndTypes = iPhone6,IOS\r\n + DeviceProfileNameAndTypes = iPhone6Plus,IOS\r\n + DeviceProfileNameAndTypes = Android,Android\r\n + DeviceProfileNameAndTypes = PS4,PS4\r\n + DeviceProfileNameAndTypes = XboxOne,XboxOne\r\n + DeviceProfileNameAndTypes = HTML5,HTML5\r\n + DeviceProfileNameAndTypes = Mac,Mac\r\n + DeviceProfileNameAndTypes = MacNoEditor,MacNoEditor\r\n + DeviceProfileNameAndTypes = MacServer,MacServer\r\n + DeviceProfileNameAndTypes = WinRT,WinRT\r\n + DeviceProfileNameAndTypes = Linux,Linux\r\n + DeviceProfileNameAndTypes = LinuxNoEditor,LinuxNoEditor\r\n + DeviceProfileNameAndTypes = LinuxServer,LinuxServer\r\n\r\n[Windows DeviceProfile]\r\nDeviceType = Windows\r\nBaseProfileName =\r\n\r\n\r\n + CVars = foliage.UseOcclusionType = 0\r\n + CVars = ShowFloatingDamageText = True\r\n + CVars = FogDensity = 0.0\r\n + CVars = FrameRateCap = 144\r\n + CVars = FrameRateMinimum = 144\r\n + CVars = MaxES2PixelShaderAdditiveComplexityCount = 45\r\n + CVars = MaxPixelShaderAdditiveComplexityCount = 128\r\n + CVars = MaxSmoothedFrameRate = 144\r\n + CVars = MinDesiredFrameRate = 70\r\n + CVars = MinSmoothedFrameRate = 5\r\n + CVars = NearClipPlane = 12.0\r\n + CVars = ShowFlag.AmbientOcclusion = 0\r\n + CVars = ShowFlag.AntiAliasing = 0\r\n + CVars = ShowFlag.Atmosphere = 0\r\n + CVars = ShowFlag.AtmosphericFog = 0\r\n + CVars = ShowFlag.AudioRadius = 0\r\n + CVars = ShowFlag.BSP = 0\r\n + CVars = ShowFlag.BSPSplit = 0\r\n + CVars = ShowFlag.BSPTriangles = 0\r\n + CVars = ShowFlag.BillboardSprites = 0\r\n + CVars = ShowFlag.Brushes = 0\r\n + CVars = ShowFlag.BuilderBrush = 0\r\n + CVars = ShowFlag.CameraAspectRatioBars = 0\r\n + CVars = ShowFlag.CameraFrustums = 0\r\n + CVars = ShowFlag.CameraImperfections = 0\r\n + CVars = ShowFlag.CameraInterpolation = 0\r\n + CVars = ShowFlag.CameraSafeFrames = 0\r\n + CVars = ShowFlag.ColorGrading = 0\r\n + CVars = ShowFlag.CompositeEditorPrimitives = 0\r\n + CVars = ShowFlag.Constraints = 0\r\n + CVars = ShowFlag.Cover = 0\r\n + CVars = ShowFlag.Decals = 0\r\n + CVars = ShowFlag.DeferredLighting = 0\r\n + CVars = ShowFlag.DepthOfField = 0\r\n + CVars = ShowFlag.Diffuse = 0\r\n + CVars = ShowFlag.DirectLighting = 0\r\n + CVars = ShowFlag.DirectionalLights = 0\r\n + CVars = ShowFlag.DistanceCulledPrimitives = 0\r\n + CVars = ShowFlag.DistanceFieldAO = 0\r\n + CVars = ShowFlag.DynamicShadows = 0\r\n + CVars = ShowFlag.Editor = 0\r\n + CVars = ShowFlag.EyeAdaptation = 0\r\n + CVars = ShowFlag.Fog = 1\r\n + CVars = ShowFlag.Game = 0\r\n + CVars = ShowFlag.LOD = 0\r\n + CVars = ShowFlag.Landscape = 0\r\n + CVars = ShowFlag.LargeVertices = 0\r\n + CVars = ShowFlag.LensFlares = 0\r\n + CVars = ShowFlag.LevelColoration = 0\r\n + CVars = ShowFlag.LightComplexity = 0\r\n + CVars = ShowFlag.LightInfluences = 0\r\n + CVars = ShowFlag.LightMapDensity = 0\r\n + CVars = ShowFlag.LightRadius = 0\r\n + CVars = ShowFlag.LightShafts = 0\r\n + CVars = ShowFlag.Lighting = 0\r\n + CVars = ShowFlag.LpvLightingOnly = 0\r\n + CVars = ShowFlag.Materials = 0\r\n + CVars = ShowFlag.MeshEdges = 0\r\n + CVars = ShowFlag.MotionBlur = 0\r\n + CVars = ShowFlag.OnScreenDebug = 0\r\n + CVars = ShowFlag.OverrideDiffuseAndSpecular = 0\r\n + CVars = ShowFlag.Paper2DSprites = 0\r\n + CVars = ShowFlag.Particles = 0\r\n + CVars = ShowFlag.Pivot = 0\r\n + CVars = ShowFlag.PointLights = 0\r\n + CVars = ShowFlag.PostProcessMaterial = 0\r\n+CVars=ShowFlag.PostProcessing=0\r\n+CVars=ShowFlag.PrecomputedVisibility=0\r\n+CVars=ShowFlag.PreviewShadowsIndicator=0\r\n+CVars=ShowFlag.ReflectionEnvironment=0\r\n+CVars=ShowFlag.ReflectionOverride=0\r\n+CVars=ShowFlag.Refraction=0\r\n+CVars=ShowFlag.SelectionOutline=0\r\n+CVars=ShowFlag.ShaderComplexity=0\r\n+CVars=ShowFlag.ShadowFrustums=0\r\n+CVars=ShowFlag.ShadowsFromEditorHiddenActors=0\r\n+CVars=ShowFlag.SkeletalMeshes=0\r\n+CVars=ShowFlag.SkyLighting=0\r\n+CVars=ShowFlag.Snap=0\r\n+CVars=ShowFlag.Specular=0\r\n+CVars=ShowFlag.SpotLights=0\r\n+CVars=ShowFlag.StaticMeshes=0\r\n+CVars=ShowFlag.StationaryLightOverlap=0\r\n+CVars=ShowFlag.StereoRendering=0\r\n+CVars=ShowFlag.SubsurfaceScattering=0\r\n+CVars=ShowFlag.TemporalAA=0\r\n+CVars=ShowFlag.Tessellation=0\r\n+CVars=ShowFlag.TestImage=0\r\n+CVars=ShowFlag.TextRender=0\r\n+CVars=ShowFlag.TexturedLightProfiles=0\r\n+CVars=ShowFlag.Tonemapper=0\r\n+CVars=ShowFlag.Translucency=0\r\n+CVars=ShowFlag.VectorFields=0\r\n+CVars=ShowFlag.VertexColors=0\r\n+CVars=ShowFlag.Vignette=0\r\n+CVars=ShowFlag.VisualizeAdaptiveDOF=0\r\n+CVars=ShowFlag.VisualizeBuffer=0\r\n+CVars=ShowFlag.VisualizeDOF=0\r\n+CVars=ShowFlag.VisualizeDistanceFieldAO=0\r\n+CVars=ShowFlag.VisualizeHDR=0\r\n+CVars=ShowFlag.VisualizeLPV=0\r\n+CVars=ShowFlag.VisualizeLightCulling=0\r\n+CVars=ShowFlag.VisualizeMotionBlur=0\r\n+CVars=ShowFlag.VisualizeOutOfBoundsPixels=0\r\n+CVars=ShowFlag.VisualizeSSR=0\r\n+CVars=ShowFlag.VisualizeSenses=0\r\n+CVars=ShowFlag.VolumeLightingSamples=0\r\n+CVars=ShowFlag.Wireframe=0\r\n+CVars=SmoothedFrameRateRange=(LowerBound=(Type=\"ERangeBoundTypes::Inclusive\",Value=60),UpperBound=(Type=\"ERangeBoundTypes::Exclusive\",Value=70))\r\n+CVars=TEXTUREGROUP_Character=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_CharacterNormalMap=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_CharacterSpecular=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Cinematic=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Effects=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=linear,MipFilter=point)\r\n+CVars=TEXTUREGROUP_EffectsNotFiltered=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Lightmap=(MinLODSize=1,MaxLODSize=8,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_MobileFlattened=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_RenderTarget=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Shadowmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point,NumStreamedMips=3)\r\n+CVars=TEXTUREGROUP_Skybox=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Terrain_Heightmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Terrain_Weightmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_UI=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Vehicle=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_VehicleNormalMap=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_VehicleSpecular=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Weapon=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WeaponNormalMap=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WeaponSpecular=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_World=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WorldNormalMap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WorldSpecular=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=bDisablePhysXHardwareSupport=True\r\n+CVars=bFirstRun=False\r\n+CVars=bSmoothFrameRate=true\r\n+CVars=r.AOTrimOldRecordsFraction=0\r\n+CVars=r.AmbientOcclusionLevels=0\r\n+CVars=r.Atmosphere=0\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.ClearWithExcludeRects=0\r\n+CVars=r.CompileShadersForDevelopment=0\r\n+CVars=r.CustomDepth=0\r\n+CVars=r.DefaultFeature.AmbientOcclusion=False\r\n+CVars=r.DefaultFeature.AntiAliasing=0\r\n+CVars=r.DefaultFeature.AutoExposure=False\r\n+CVars=r.DefaultFeature.Bloom=False\r\n+CVars=r.DefaultFeature.LensFlare=False\r\n+CVars=r.DefaultFeature.MotionBlur=False\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.DetailMode=0\r\n+CVars=r.EarlyZPass=0\r\n+CVars=r.ExposureOffset=0.3\r\n+CVars=r.HZBOcclusion=0\r\n+CVars=r.LensFlareQuality=0\r\n+CVars=r.LightFunctionQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.LightShafts=0\r\n+CVars=r.MaxAnisotropy=0\r\n+CVars=r.MotionBlurQuality=0\r\n+CVars=r.PostProcessAAQuality=0\r\n+CVars=r.ReflectionEnvironment=0\r\n+CVars=r.RefractionQuality=0\r\n+CVars=r.SSAOSmartBlur=0\r\n+CVars=r.SSR.Quality=0\r\n+CVars=r.SSS.SampleSet=0\r\n+CVars=r.SSS.Scale=0\r\n+CVars=r.SceneColorFringe.Max=0\r\n+CVars=r.SceneColorFringeQuality=0\r\n+CVars=r.Shadow.CSM.MaxCascades=1\r\n+CVars=r.Shadow.CSM.TransitionScale=0\r\n+CVars=r.Shadow.DistanceScale=0.1\r\n+CVars=r.Shadow.MaxResolution=2\r\n+CVars=r.Shadow.MinResolution=2\r\n+CVars=r.Shadow.RadiusThreshold=0.1\r\n+CVars=r.ShadowQuality=0\r\n+CVars=r.TonemapperQuality=0\r\n+CVars=r.TriangleOrderOptimization=1\r\n+CVars=r.TrueSkyQuality=0\r\n+CVars=r.UpsampleQuality=0\r\n+CVars=r.ViewDistanceScale=0\r\n+CVars=r.oneframethreadlag=1\r\n+CVars=t.maxfps=165\r\n\r\n[WindowsNoEditor DeviceProfile]\r\nDeviceType=WindowsNoEditor\r\nBaseProfileName=Windows\r\n\r\n[WindowsServer DeviceProfile]\r\nDeviceType=WindowsServer\r\nBaseProfileName=Windows\r\n\r\n[IOS DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.RefractionQuality=0\r\n\r\n[iPad2 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPad3 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPad4 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPadAir DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.BloomQuality=1\r\n\r\n[iPadMini DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPadMini2 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=iPadAir\r\n\r\n[iPhone4 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone4S DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone5 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone5S DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=2\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[iPodTouch5 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone6 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=0\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[iPhone6Plus DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=0\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[Android DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=\r\n+CVars=r.MobileContentScaleFactor=1\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.RefractionQuality=0\r\n\r\n[Android_Low DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.MobileContentScaleFactor=0.5\r\n\r\n[Android_Mid DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.MobileContentScaleFactor=0.8\r\n\r\n[Android_High DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n+CVars=r.MobileContentScaleFactor=1.0\r\n\r\n[Android_Unrecognized DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Mid\r\n\r\n[Android_Adreno320 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Mid\r\n\r\n;This offset needs to be set for the mosaic fallback to work on Galaxy S4 (SAMSUNG-IGH-I337)\r\n;+CVars=r.DemosaicVposOffset=0.5\r\n\r\n[Android_Adreno330 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_High\r\n\r\n[Android_Adreno330_Ver53 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Adreno330\r\n+CVars=r.DisjointTimerQueries=1\r\n\r\n[PS4 DeviceProfile]\r\nDeviceType=PS4\r\nBaseProfileName=\r\n\r\n[XboxOne DeviceProfile]\r\nDeviceType=XboxOne\r\nBaseProfileName=\r\n; we output 10:10:10, not 8:8:8 so we don't need color quantization\r\n+CVars=r.TonemapperQuality=0\r\n; For SSAO we rely on TemporalAA (with a randomized sample pattern over time) so we can use less samples\r\n+CVars=r.AmbientOcclusionSampleSetQuality=0\r\n; less passes, and no upsampling as even upsampling costs some performance\r\n+CVars=r.AmbientOcclusionLevels=1\r\n; larger radius to compensate for fewer passes\r\n+CVars=r.AmbientOcclusionRadiusScale=2\r\n\r\n\r\n[HTML5 DeviceProfile]\r\nDeviceType=HTML5\r\nBaseProfileName=\r\n+CVars=r.RefractionQuality=0\r\n\r\n[Mac DeviceProfile]\r\nDeviceType=Mac\r\nBaseProfileName=\r\n\r\n[MacNoEditor DeviceProfile]\r\nDeviceType=MacNoEditor\r\nBaseProfileName=Mac\r\n\r\n[MacServer DeviceProfile]\r\nDeviceType=MacServer\r\nBaseProfileName=Mac\r\n\r\n[WinRT DeviceProfile]\r\nDeviceType=WinRT\r\nBaseProfileName=\r\n\r\n[Linux DeviceProfile]\r\nDeviceType=Linux\r\nBaseProfileName=\r\nMeshLODSettings=\r\nTextureLODSettings=\r\n\r\n[LinuxNoEditor DeviceProfile]\r\nDeviceType=LinuxNoEditor\r\nBaseProfileName=Linux\r\nMeshLODSettings=\r\nTextureLODSettings=\r\n\r\n[LinuxServer DeviceProfile]\r\nDeviceType=LinuxServer\r\nBaseProfileName=Linux\r\nMeshLODSettings=\r\nTextureLODSettings=";

                // Write with error handling
                try
                {
                    File.WriteAllText(filePath, fileContents);
                    MessageBox.Show("Configuration updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Access denied. Please run as administrator or close ARK/Steam.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"File access error: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenTwo_Click(object sender, EventArgs e)
        {
            try
            {
                // Use the same logic as your working Open_File_Click
                string steamPath = Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",
                    "InstallPath", null) as string
                    ?? Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam",
                    "InstallPath", null) as string;

                if (steamPath == null)
                {
                    MessageBox.Show("Steam installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string arkDir = Path.Combine(steamPath, "steamapps", "common", "ARK", "Engine", "Config");
                string filePath = Path.Combine(arkDir, "BaseDeviceProfiles.ini");

                // Check if ARK directory exists
                if (!Directory.Exists(arkDir))
                {
                    MessageBox.Show("ARK installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ensure directory exists
                Directory.CreateDirectory(arkDir);

                // Handle read-only files
                if (File.Exists(filePath))
                {
                    FileAttributes attributes = File.GetAttributes(filePath);
                    if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                    }
                }

                // YOUR INI CONTENT GOES HERE
                string fileContents = "[DeviceProfiles]\r\n+DeviceProfileNameAndTypes=Windows,Windows\r\n+DeviceProfileNameAndTypes=WindowsNoEditor,WindowsNoEditor\r\n+DeviceProfileNameAndTypes=WindowsServer,WindowsServer\r\n+DeviceProfileNameAndTypes=IOS,IOS\r\n+DeviceProfileNameAndTypes=iPad2,IOS\r\n+DeviceProfileNameAndTypes=iPad3,IOS\r\n+DeviceProfileNameAndTypes=iPad4,IOS\r\n+DeviceProfileNameAndTypes=iPadAir,IOS\r\n+DeviceProfileNameAndTypes=iPadMini,IOS\r\n+DeviceProfileNameAndTypes=iPadMini2,IOS\r\n+DeviceProfileNameAndTypes=iPhone4,IOS\r\n+DeviceProfileNameAndTypes=iPhone4S,IOS\r\n+DeviceProfileNameAndTypes=iPhone5,IOS\r\n+DeviceProfileNameAndTypes=iPhone5S,IOS\r\n+DeviceProfileNameAndTypes=iPodTouch5,IOS\r\n+DeviceProfileNameAndTypes=iPhone6,IOS\r\n+DeviceProfileNameAndTypes=iPhone6Plus,IOS\r\n+DeviceProfileNameAndTypes=Android,Android\r\n+DeviceProfileNameAndTypes=PS4,PS4\r\n+DeviceProfileNameAndTypes=XboxOne,XboxOne\r\n+DeviceProfileNameAndTypes=HTML5,HTML5\r\n+DeviceProfileNameAndTypes=Mac,Mac\r\n+DeviceProfileNameAndTypes=MacNoEditor,MacNoEditor\r\n+DeviceProfileNameAndTypes=MacServer,MacServer\r\n+DeviceProfileNameAndTypes=WinRT,WinRT\r\n+DeviceProfileNameAndTypes=Linux,Linux\r\n+DeviceProfileNameAndTypes=LinuxNoEditor,LinuxNoEditor\r\n+DeviceProfileNameAndTypes=LinuxServer,LinuxServer\r\n\r\n[Windows DeviceProfile]\r\nDeviceType=Windows\r\nBaseProfileName=\r\n\r\n+CVars=ShowFlag.Materials=0\r\n+CVars=FrameRateCap=144\r\n+CVars=FrameRateMinimum=144\r\n+CVars=MaxSmoothedFrameRate=144\r\n+CVars=MinDesiredFrameRate=144\r\n+CVars=NearClipPlane=12.0\r\n+CVars=ShowFlag.MeshEdges=0\r\n+CVars=ShowFlag.MotionBlur=0\r\n+CVars=ShowFlag.AmbientOcclusion=0\r\n+CVars=ShowFlag.AntiAliasing=0\r\n+CVars=ShowFlag.Atmosphere=0\r\n+CVars=ShowFlag.AtmosphericFog=0\r\n+CVars=ShowFlag.AudioRadius=0\r\n+CVars=ShowFlag.BSP=0\r\n+CVars=ShowFlag.BSPSplit=0\r\n+CVars=ShowFlag.BSPTriangles=0\r\n+CVars=ShowFlag.BillboardSprites=0\r\n+CVars=ShowFlag.Brushes=0\r\n+CVars=ShowFlag.BuilderBrush=0\r\n+CVars=ShowFlag.CameraAspectRatioBars=0\r\n+CVars=ShowFlag.CameraFrustums=0\r\n+CVars=ShowFlag.CameraImperfections=0\r\n+CVars=ShowFlag.CameraInterpolation=0\r\n+CVars=ShowFlag.CameraSafeFrames=0\r\n+CVars=ShowFlag.ColorGrading=0\r\n+CVars=ShowFlag.CompositeEditorPrimitives=0\r\n+CVars=ShowFlag.Constraints=0\r\n+CVars=ShowFlag.Cover=0\r\n+CVars=ShowFlag.Decals=0\r\n+CVars=ShowFlag.DeferredLighting=0\r\n+CVars=ShowFlag.DepthOfField=0\r\n+CVars=ShowFlag.Diffuse=0\r\n+CVars=ShowFlag.DirectLighting=0\r\n+CVars=ShowFlag.DirectionalLights=0\r\n+CVars=ShowFlag.DistanceCulledPrimitives=0\r\n+CVars=ShowFlag.DistanceFieldAO=0\r\n+CVars=ShowFlag.DynamicShadows=0\r\n+CVars=ShowFlag.Editor=0\r\n+CVars=ShowFlag.EyeAdaptation=0\r\n+CVars=ShowFlag.Fog=1\r\n+CVars=ShowFlag.Game=0\r\n+CVars=ShowFlag.OnScreenDebug=0\r\n+CVars=ShowFlag.LOD=0\r\n+CVars=ShowFlag.Landscape=0\r\n+CVars=ShowFlag.LargeVertices=0\r\n+CVars=ShowFlag.LensFlares=0\r\n+CVars=ShowFlag.LevelColoration=0\r\n+CVars=ShowFlag.LightComplexity=0\r\n+CVars=ShowFlag.LightInfluences=0\r\n+CVars=ShowFlag.LightMapDensity=0\r\n+CVars=ShowFlag.LightRadius=0\r\n+CVars=ShowFlag.LightShafts=0\r\n+CVars=ShowFlag.Lighting=0\r\n+CVars=ShowFlag.LpvLightingOnly=0\r\n+CVars=ShowFlag.PointLights=0\r\n+CVars=ShowFlag.PostProcessMaterial=0\r\n+CVars=ShowFlag.PostProcessing=0\r\n+CVars=ShowFlag.PrecomputedVisibility=0\r\n+CVars=ShowFlag.PreviewShadowsIndicator=0\r\n+CVars=ShowFlag.ReflectionEnvironment=0\r\n+CVars=ShowFlag.ReflectionOverride=0\r\n+CVars=ShowFlag.Refraction=0\r\n+CVars=ShowFlag.SelectionOutline=0\r\n+CVars=ShowFlag.ShaderComplexity=0\r\n+CVars=ShowFlag.ShadowFrustums=0\r\n+CVars=ShowFlag.ShadowsFromEditorHiddenActors=0\r\n+CVars=ShowFlag.SkeletalMeshes=0\r\n+CVars=ShowFlag.SkyLighting=0\r\n+CVars=ShowFlag.Snap=0\r\n+CVars=ShowFlag.Specular=0\r\n+CVars=ShowFlag.SpotLights=0\r\n+CVars=ShowFlag.StaticMeshes=0\r\n+CVars=ShowFlag.StationaryLightOverlap=0\r\n+CVars=ShowFlag.StereoRendering=0\r\n+CVars=ShowFlag.SubsurfaceScattering=0\r\n+CVars=ShowFlag.TemporalAA=0\r\n+CVars=ShowFlag.Tessellation=0\r\n+CVars=ShowFlag.TestImage=0\r\n+CVars=ShowFlag.TextRender=0\r\n+CVars=ShowFlag.TexturedLightProfiles=0\r\n+CVars=ShowFlag.Tonemapper=0\r\n+CVars=ShowFlag.Translucency=0\r\n+CVars=ShowFlag.VectorFields=0\r\n+CVars=ShowFlag.VertexColors=0\r\n+CVars=ShowFlag.Vignette=0\r\n+CVars=ShowFlag.VisualizeAdaptiveDOF=0\r\n+CVars=ShowFlag.VisualizeBuffer=0\r\n+CVars=ShowFlag.VisualizeDOF=0\r\n+CVars=ShowFlag.VisualizeDistanceFieldAO=0\r\n+CVars=ShowFlag.VisualizeHDR=0\r\n+CVars=ShowFlag.VisualizeLPV=0\r\n+CVars=ShowFlag.VisualizeLightCulling=0\r\n+CVars=ShowFlag.VisualizeMotionBlur=0\r\n+CVars=ShowFlag.VisualizeOutOfBoundsPixels=0\r\n+CVars=ShowFlag.VisualizeSSR=0\r\n+CVars=ShowFlag.VisualizeSenses=0\r\n+CVars=ShowFlag.VolumeLightingSamples=0\r\n+CVars=ShowFlag.Wireframe=0\r\n+CVars=SmoothedFrameRateRange=(LowerBound=(Type=\"ERangeBoundTypes::Inclusive\",Value=60),UpperBound=(Type=\"ERangeBoundTypes::Exclusive\",Value=70))\r\n+CVars=TEXTUREGROUP_Character=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_CharacterNormalMap=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_CharacterSpecular=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Cinematic=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Effects=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=linear,MipFilter=point)\r\n+CVars=TEXTUREGROUP_EffectsNotFiltered=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Lightmap=(MinLODSize=1,MaxLODSize=8,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_MobileFlattened=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_RenderTarget=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Shadowmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point,NumStreamedMips=3)\r\n+CVars=TEXTUREGROUP_Skybox=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Terrain_Heightmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Terrain_Weightmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_UI=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Vehicle=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_VehicleNormalMap=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_VehicleSpecular=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Weapon=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WeaponNormalMap=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WeaponSpecular=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_World=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WorldNormalMap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WorldSpecular=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=r.DefaultFeature.AmbientOcclusion=False\r\n+CVars=r.DefaultFeature.AntiAliasing=0\r\n+CVars=r.DefaultFeature.AutoExposure=False\r\n+CVars=r.DefaultFeature.Bloom=False\r\n+CVars=r.DefaultFeature.LensFlare=False\r\n+CVars=r.DefaultFeature.MotionBlur=False\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.DetailMode=0\r\n+CVars=r.EarlyZPass=0\r\n+CVars=r.ExposureOffset=0.3\r\n+CVars=r.HZBOcclusion=0\r\n+CVars=r.LensFlareQuality=0\r\n+CVars=r.LightFunctionQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.LightShafts=0\r\n+CVars=r.MaxAnisotropy=0\r\n+CVars=r.MotionBlurQuality=0\r\n+CVars=r.PostProcessAAQuality=0\r\n+CVars=r.ReflectionEnvironment=0\r\n+CVars=r.RefractionQuality=0\r\n+CVars=r.SSAOSmartBlur=0\r\n+CVars=r.SSR.Quality=0\r\n+CVars=r.SSS.SampleSet=0\r\n+CVars=r.SSS.Scale=0\r\n+CVars=r.SceneColorFringe.Max=0\r\n+CVars=r.SceneColorFringeQuality=0\r\n+CVars=r.Shadow.CSM.MaxCascades=1\r\n+CVars=r.Shadow.CSM.TransitionScale=0\r\n+CVars=r.Shadow.DistanceScale=0.1\r\n+CVars=r.Shadow.MaxResolution=2\r\n+CVars=r.Shadow.MinResolution=2\r\n+CVars=r.Shadow.RadiusThreshold=0.1\r\n+CVars=r.ShadowQuality=0\r\n+CVars=r.TonemapperQuality=0\r\n+CVars=r.TriangleOrderOptimization=1\r\n+CVars=r.TrueSkyQuality=0\r\n+CVars=r.UpsampleQuality=0\r\n+CVars=r.ViewDistanceScale=0\r\n+CVars=r.oneframethreadlag=1\r\n+CVars=t.maxfps=144\r\n\r\n[WindowsNoEditor DeviceProfile]\r\nDeviceType=WindowsNoEditor\r\nBaseProfileName=Windows\r\n\r\n[WindowsServer DeviceProfile]\r\nDeviceType=WindowsServer\r\nBaseProfileName=Windows\r\n\r\n[IOS DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.RefractionQuality=0\r\n\r\n[iPad2 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPad3 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPad4 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPadAir DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.BloomQuality=1\r\n\r\n[iPadMini DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPadMini2 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=iPadAir\r\n\r\n[iPhone4 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone4S DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone5 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone5S DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=2\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[iPodTouch5 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone6 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=0\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[iPhone6Plus DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=0\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[Android DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=\r\n+CVars=r.MobileContentScaleFactor=1\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.RefractionQuality=0\r\n\r\n[Android_Low DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.MobileContentScaleFactor=0.5\r\n\r\n[Android_Mid DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.MobileContentScaleFactor=0.8\r\n\r\n[Android_High DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n+CVars=r.MobileContentScaleFactor=1.0\r\n\r\n[Android_Unrecognized DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Mid\r\n\r\n[Android_Adreno320 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Mid\r\n\r\n;This offset needs to be set for the mosaic fallback to work on Galaxy S4 (SAMSUNG-IGH-I337)\r\n;+CVars=r.DemosaicVposOffset=0.5\r\n\r\n[Android_Adreno330 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_High\r\n\r\n[Android_Adreno330_Ver53 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Adreno330\r\n+CVars=r.DisjointTimerQueries=1\r\n\r\n[PS4 DeviceProfile]\r\nDeviceType=PS4\r\nBaseProfileName=\r\n\r\n[XboxOne DeviceProfile]\r\nDeviceType=XboxOne\r\nBaseProfileName=\r\n; we output 10:10:10, not 8:8:8 so we don't need color quantization\r\n+CVars=r.TonemapperQuality=0\r\n; For SSAO we rely on TemporalAA (with a randomized sample pattern over time) so we can use less samples\r\n+CVars=r.AmbientOcclusionSampleSetQuality=0\r\n; less passes, and no upsampling as even upsampling costs some performance\r\n+CVars=r.AmbientOcclusionLevels=1\r\n; larger radius to compensate for fewer passes\r\n+CVars=r.AmbientOcclusionRadiusScale=2\r\n\r\n\r\n[HTML5 DeviceProfile]\r\nDeviceType=HTML5\r\nBaseProfileName=\r\n+CVars=r.RefractionQuality=0\r\n\r\n[Mac DeviceProfile]\r\nDeviceType=Mac\r\nBaseProfileName=\r\n\r\n[MacNoEditor DeviceProfile]\r\nDeviceType=MacNoEditor\r\nBaseProfileName=Mac\r\n\r\n[MacServer DeviceProfile]\r\nDeviceType=MacServer\r\nBaseProfileName=Mac\r\n\r\n[WinRT DeviceProfile]\r\nDeviceType=WinRT\r\nBaseProfileName=\r\n\r\n[Linux DeviceProfile]\r\nDeviceType=Linux\r\nBaseProfileName=\r\nMeshLODSettings=\r\nTextureLODSettings=\r\n\r\n[LinuxNoEditor DeviceProfile]\r\nDeviceType=LinuxNoEditor\r\nBaseProfileName=Linux\r\nMeshLODSettings=\r\nTextureLODSettings=\r\n\r\n[LinuxServer DeviceProfile]\r\nDeviceType=LinuxServer\r\nBaseProfileName=Linux\r\nMeshLODSettings=\r\nTextureLODSettings=";

                // Write with error handling
                try
                {
                    File.WriteAllText(filePath, fileContents);
                    MessageBox.Show("Configuration updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Access denied. Please run as administrator or close ARK/Steam.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"File access error: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pvp_Click(object sender, EventArgs e)
        {
            try
            {
                // Use the same logic as your working Open_File_Click
                string steamPath = Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",
                    "InstallPath", null) as string
                    ?? Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam",
                    "InstallPath", null) as string;

                if (steamPath == null)
                {
                    MessageBox.Show("Steam installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string arkDir = Path.Combine(steamPath, "steamapps", "common", "ARK", "Engine", "Config");
                string filePath = Path.Combine(arkDir, "BaseDeviceProfiles.ini");

                // Check if ARK directory exists
                if (!Directory.Exists(arkDir))
                {
                    MessageBox.Show("ARK installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ensure directory exists
                Directory.CreateDirectory(arkDir);

                // Handle read-only files
                if (File.Exists(filePath))
                {
                    FileAttributes attributes = File.GetAttributes(filePath);
                    if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                    }
                }

                // YOUR INI CONTENT GOES HERE
                string fileContents = "[DeviceProfiles]\r\n+DeviceProfileNameAndTypes=Windows,Windows\r\n+DeviceProfileNameAndTypes=WindowsNoEditor,WindowsNoEditor\r\n+DeviceProfileNameAndTypes=WindowsServer,WindowsServer\r\n+DeviceProfileNameAndTypes=IOS,IOS\r\n+DeviceProfileNameAndTypes=iPad2,IOS\r\n+DeviceProfileNameAndTypes=iPad3,IOS\r\n+DeviceProfileNameAndTypes=iPad4,IOS\r\n+DeviceProfileNameAndTypes=iPadAir,IOS\r\n+DeviceProfileNameAndTypes=iPadMini,IOS\r\n+DeviceProfileNameAndTypes=iPadMini2,IOS\r\n+DeviceProfileNameAndTypes=iPhone4,IOS\r\n+DeviceProfileNameAndTypes=iPhone4S,IOS\r\n+DeviceProfileNameAndTypes=iPhone5,IOS\r\n+DeviceProfileNameAndTypes=iPhone5S,IOS\r\n+DeviceProfileNameAndTypes=iPodTouch5,IOS\r\n+DeviceProfileNameAndTypes=iPhone6,IOS\r\n+DeviceProfileNameAndTypes=iPhone6Plus,IOS\r\n+DeviceProfileNameAndTypes=Android,Android\r\n+DeviceProfileNameAndTypes=PS4,PS4\r\n+DeviceProfileNameAndTypes=XboxOne,XboxOne\r\n+DeviceProfileNameAndTypes=HTML5,HTML5\r\n+DeviceProfileNameAndTypes=Mac,Mac\r\n+DeviceProfileNameAndTypes=MacNoEditor,MacNoEditor\r\n+DeviceProfileNameAndTypes=MacServer,MacServer\r\n+DeviceProfileNameAndTypes=WinRT,WinRT\r\n+DeviceProfileNameAndTypes=Linux,Linux\r\n+DeviceProfileNameAndTypes=LinuxNoEditor,LinuxNoEditor\r\n+DeviceProfileNameAndTypes=LinuxServer,LinuxServer\r\n\r\n[Windows DeviceProfile]\r\nDeviceType=Windows\r\nBaseProfileName=\r\n\r\n+CVars=bDisablePhysXHardwareSupport=True\r\n+CVars=bFirstRun=False\r\n+CVars=FogDensity=0.0\r\n+CVars=NearClipPlane=12.0\r\n+CVars=ShowFlag.AmbientOcclusion=0\r\n+CVars=ShowFlag.AntiAliasing=0\r\n+CVars=ShowFlag.Atmosphere=0\r\n+CVars=ShowFlag.AtmosphericFog=0\r\n+CVars=ShowFlag.AudioRadius=0\r\n+CVars=ShowFlag.BSP=0\r\n+CVars=ShowFlag.BSPSplit=0\r\n+CVars=ShowFlag.BSPTriangles=0\r\n+CVars=ShowFlag.BillboardSprites=0\r\n+CVars=ShowFlag.Brushes=0\r\n+CVars=ShowFlag.BuilderBrush=0\r\n+CVars=ShowFlag.CameraAspectRatioBars=0\r\n+CVars=ShowFlag.CameraFrustums=0\r\n+CVars=ShowFlag.CameraImperfections=0\r\n+CVars=ShowFlag.CameraInterpolation=0\r\n+CVars=ShowFlag.CameraSafeFrames=0\r\n+CVars=ShowFlag.ColorGrading=0\r\n+CVars=ShowFlag.CompositeEditorPrimitives=0\r\n+CVars=ShowFlag.Constraints=0\r\n+CVars=ShowFlag.Cover=0\r\n+CVars=ShowFlag.Decals=0\r\n+CVars=ShowFlag.DeferredLighting=0\r\n+CVars=ShowFlag.DepthOfField=0\r\n+CVars=ShowFlag.Diffuse=0\r\n+CVars=ShowFlag.DirectLighting=0\r\n+CVars=ShowFlag.DirectionalLights=0\r\n+CVars=ShowFlag.DistanceCulledPrimitives=0\r\n+CVars=ShowFlag.DistanceFieldAO=0\r\n+CVars=ShowFlag.DynamicShadows=0\r\n+CVars=ShowFlag.Editor=0\r\n+CVars=ShowFlag.EyeAdaptation=0\r\n+CVars=ShowFlag.Game=0\r\n+CVars=ShowFlag.LOD=0\r\n+CVars=ShowFlag.Landscape=0\r\n+CVars=ShowFlag.LargeVertices=0\r\n+CVars=ShowFlag.LensFlares=0\r\n+CVars=ShowFlag.LevelColoration=0\r\n+CVars=ShowFlag.LightInfluences=0\r\n+CVars=ShowFlag.LightMapDensity=0\r\n+CVars=ShowFlag.LightRadius=0\r\n+CVars=ShowFlag.Lighting=0\r\n+CVars=ShowFlag.LpvLightingOnly=0\r\n+CVars=ShowFlag.MeshEdges=0\r\n+CVars=ShowFlag.MotionBlur=0\r\n+CVars=ShowFlag.OnScreenDebug=0\r\n+CVars=ShowFlag.OverrideDiffuseAndSpecular=0\r\n+CVars=ShowFlag.Paper2DSprites=0\r\n+CVars=ShowFlag.Particles=0\r\n+CVars=ShowFlag.Pivot=0\r\n+CVars=ShowFlag.PointLights=0\r\n+CVars=ShowFlag.PostProcessMaterial=0\r\n+CVars=ShowFlag.PostProcessing=0\r\n+CVars=ShowFlag.PrecomputedVisibility=0\r\n+CVars=ShowFlag.PreviewShadowsIndicator=0\r\n+CVars=ShowFlag.ReflectionEnvironment=0\r\n+CVars=ShowFlag.ReflectionOverride=0\r\n+CVars=ShowFlag.SelectionOutline=0\r\n+CVars=ShowFlag.ShadowFrustums=0\r\n+CVars=ShowFlag.ShadowsFromEditorHiddenActors=0\r\n+CVars=ShowFlag.SkeletalMeshes=0\r\n+CVars=ShowFlag.Snap=0\r\n+CVars=ShowFlag.Specular=0\r\n+CVars=ShowFlag.SpotLights=0\r\n+CVars=ShowFlag.StaticMeshes=0\r\n+CVars=ShowFlag.StationaryLightOverlap=0\r\n+CVars=ShowFlag.StereoRendering=0\r\n+CVars=ShowFlag.SubsurfaceScattering=0\r\n+CVars=ShowFlag.TemporalAA=0\r\n+CVars=ShowFlag.Tessellation=0\r\n+CVars=ShowFlag.TestImage=0\r\n+CVars=ShowFlag.TextRender=0\r\n+CVars=ShowFlag.TexturedLightProfiles=0\r\n+CVars=ShowFlag.Tonemapper=0\r\n+CVars=ShowFlag.Translucency=0\r\n+CVars=ShowFlag.VectorFields=0\r\n+CVars=ShowFlag.VertexColors=0\r\n+CVars=ShowFlag.Vignette=0\r\n+CVars=ShowFlag.VisualizeAdaptiveDOF=0\r\n+CVars=ShowFlag.VisualizeBuffer=0\r\n+CVars=ShowFlag.VisualizeDOF=0\r\n+CVars=ShowFlag.VisualizeDistanceFieldAO=0\r\n+CVars=ShowFlag.VisualizeHDR=0\r\n+CVars=ShowFlag.VisualizeLPV=0\r\n+CVars=ShowFlag.VisualizeLightCulling=0\r\n+CVars=ShowFlag.VisualizeMotionBlur=0\r\n+CVars=ShowFlag.VisualizeOutOfBoundsPixels=0\r\n+CVars=ShowFlag.VisualizeSSR=0\r\n+CVars=ShowFlag.VisualizeSenses=0\r\n+CVars=ShowFlag.Wireframe=0\r\n+CVars=ShowFloatingDamageText=True\r\n+CVars=TEXTUREGROUP_Character=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_CharacterNormalMap=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_CharacterSpecular=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Cinematic=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Effects=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=linear,MipFilter=point)\r\n+CVars=TEXTUREGROUP_EffectsNotFiltered=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Lightmap=(MinLODSize=1,MaxLODSize=8,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_MobileFlattened=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_RenderTarget=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Shadowmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point,NumStreamedMips=3)\r\n+CVars=TEXTUREGROUP_Skybox=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Terrain_Heightmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Terrain_Weightmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_UI=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Vehicle=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_VehicleNormalMap=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_VehicleSpecular=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Weapon=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WeaponNormalMap=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WeaponSpecular=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_World=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WorldNormalMap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WorldSpecular=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=r.AOTrimOldRecordsFraction=0\r\n+CVars=r.AmbientOcclusionLevels=0\r\n+CVars=r.Atmosphere=0\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.ClearWithExcludeRects=0\r\n+CVars=r.CompileShadersForDevelopment=0\r\n+CVars=r.DefaultFeature.AmbientOcclusion=False\r\n+CVars=r.DefaultFeature.AntiAliasing=0\r\n+CVars=r.DefaultFeature.AutoExposure=False\r\n+CVars=r.DefaultFeature.Bloom=False\r\n+CVars=r.DefaultFeature.LensFlare=False\r\n+CVars=r.DefaultFeature.MotionBlur=False\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.DetailMode=0\r\n+CVars=r.EarlyZPass=0\r\n+CVars=r.ExposureOffset=0.3\r\n+CVars=r.HZBOcclusion=0\r\n+CVars=r.LensFlareQuality=0\r\n+CVars=r.LightFunctionQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.LightShafts=0\r\n+CVars=r.MaxAnisotropy=0\r\n+CVars=r.MotionBlurQuality=0\r\n+CVars=r.OneFrameThreadLag=1\r\n+CVars=r.PostProcessAAQuality=0\r\n+CVars=r.ReflectionEnvironment=0\r\n+CVars=r.RefractionQuality=0\r\n+CVars=r.SSAOSmartBlur=0\r\n+CVars=r.SSR.Quality=0\r\n+CVars=r.SSS.SampleSet=0\r\n+CVars=r.SSS.Scale=0\r\n+CVars=r.SceneColorFringe.Max=0\r\n+CVars=r.SceneColorFringeQuality=0\r\n+CVars=r.Shadow.CSM.MaxCascades=1\r\n+CVars=r.Shadow.CSM.TransitionScale=0\r\n+CVars=r.Shadow.DistanceScale=0.1\r\n+CVars=r.Shadow.MaxResolution=2\r\n+CVars=r.Shadow.MinResolution=2\r\n+CVars=r.Shadow.RadiusThreshold=0.1\r\n+CVars=r.ShadowQuality=0\r\n+CVars=r.TonemapperQuality=0\r\n+CVars=r.TriangleOrderOptimization=1\r\n+CVars=r.TrueSkyQuality=0\r\n+CVars=r.UpsampleQuality=0\r\n+CVars=r.ViewDistanceScale=0\r\n+CVars=foliage.UseOcclusionType=0\r\n+CVars=FX.MaxCPUParticlesPerEmitter=1\r\n+CVars=ShowFlag.GameplayDebug=0\r\n+CVars=ShowFlag.Splines=0\r\n+CVars=ShowFlag.Materials=0\r\nr.CustomDepth = 0\r\n+CVars=ShowFlag.Fog=0\r\n+CVars=ShowFlag.LightComplexity=0\r\n+CVars=ShowFlag.LightShafts = 0\r\n+CVars=ShowFlag.ShaderComplexity = 0\r\n+CVars=ShowFlag.Refraction = 0\r\n\r\n[WindowsNoEditor DeviceProfile]\r\nDeviceType=WindowsNoEditor\r\nBaseProfileName=Windows\r\n\r\n[WindowsServer DeviceProfile]\r\nDeviceType=WindowsServer\r\nBaseProfileName=Windows\r\n\r\n[IOS DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.RefractionQuality=0\r\n\r\n[iPad2 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPad3 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPad4 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPadAir DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.BloomQuality=1\r\n\r\n[iPadMini DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPadMini2 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=iPadAir\r\n\r\n[iPhone4 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone4S DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone5 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone5S DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=2\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[iPodTouch5 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone6 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=0\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[iPhone6Plus DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=0\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[Android DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=\r\n+CVars=r.MobileContentScaleFactor=1\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.RefractionQuality=0\r\n\r\n[Android_Low DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.MobileContentScaleFactor=0.5\r\n\r\n[Android_Mid DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.MobileContentScaleFactor=0.8\r\n\r\n[Android_High DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n+CVars=r.MobileContentScaleFactor=1.0\r\n\r\n[Android_Unrecognized DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Mid\r\n\r\n[Android_Adreno320 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Mid\r\n\r\n;This offset needs to be set for the mosaic fallback to work on Galaxy S4 (SAMSUNG-IGH-I337)\r\n;+CVars=r.DemosaicVposOffset=0.5\r\n\r\n[Android_Adreno330 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_High\r\n\r\n[Android_Adreno330_Ver53 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Adreno330\r\n+CVars=r.DisjointTimerQueries=1\r\n\r\n[PS4 DeviceProfile]\r\nDeviceType=PS4\r\nBaseProfileName=\r\n\r\n[XboxOne DeviceProfile]\r\nDeviceType=XboxOne\r\nBaseProfileName=\r\n; we output 10:10:10, not 8:8:8 so we don't need color quantization\r\n+CVars=r.TonemapperQuality=0\r\n; For SSAO we rely on TemporalAA (with a randomized sample pattern over time) so we can use less samples\r\n+CVars=r.AmbientOcclusionSampleSetQuality=0\r\n; less passes, and no upsampling as even upsampling costs some performance\r\n+CVars=r.AmbientOcclusionLevels=1\r\n; larger radius to compensate for fewer passes\r\n+CVars=r.AmbientOcclusionRadiusScale=2\r\n\r\n\r\n[HTML5 DeviceProfile]\r\nDeviceType=HTML5\r\nBaseProfileName=\r\n+CVars=r.RefractionQuality=0\r\n\r\n[Mac DeviceProfile]\r\nDeviceType=Mac\r\nBaseProfileName=\r\n\r\n[MacNoEditor DeviceProfile]\r\nDeviceType=MacNoEditor\r\nBaseProfileName=Mac\r\n\r\n[MacServer DeviceProfile]\r\nDeviceType=MacServer\r\nBaseProfileName=Mac\r\n\r\n[WinRT DeviceProfile]\r\nDeviceType=WinRT\r\nBaseProfileName=\r\n\r\n[Linux DeviceProfile]\r\nDeviceType=Linux\r\nBaseProfileName=\r\nMeshLODSettings=\r\nTextureLODSettings=\r\n\r\n[LinuxNoEditor DeviceProfile]\r\nDeviceType=LinuxNoEditor\r\nBaseProfileName=Linux\r\nMeshLODSettings=\r\nTextureLODSettings=\r\n\r\n[LinuxServer DeviceProfile]\r\nDeviceType=LinuxServer\r\nBaseProfileName=Linux\r\nMeshLODSettings=\r\nTextureLODSettings=";

                // Write with error handling
                try
                {
                    File.WriteAllText(filePath, fileContents);
                    MessageBox.Show("Configuration updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Access denied. Please run as administrator or close ARK/Steam.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"File access error: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Extinction_Click(object sender, EventArgs e)
        {
            try
            {
                // Use the same logic as your working Open_File_Click
                string steamPath = Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",
                    "InstallPath", null) as string
                    ?? Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam",
                    "InstallPath", null) as string;

                if (steamPath == null)
                {
                    MessageBox.Show("Steam installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string arkDir = Path.Combine(steamPath, "steamapps", "common", "ARK", "Engine", "Config");
                string filePath = Path.Combine(arkDir, "BaseDeviceProfiles.ini");

                // Check if ARK directory exists
                if (!Directory.Exists(arkDir))
                {
                    MessageBox.Show("ARK installation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ensure directory exists
                Directory.CreateDirectory(arkDir);

                // Handle read-only files
                if (File.Exists(filePath))
                {
                    FileAttributes attributes = File.GetAttributes(filePath);
                    if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                    }
                }

                // YOUR INI CONTENT GOES HERE
                string fileContents = "[DeviceProfiles]\r\n+DeviceProfileNameAndTypes=Windows,Windows\r\n+DeviceProfileNameAndTypes=WindowsNoEditor,WindowsNoEditor\r\n+DeviceProfileNameAndTypes=WindowsServer,WindowsServer\r\n+DeviceProfileNameAndTypes=IOS,IOS\r\n+DeviceProfileNameAndTypes=iPad2,IOS\r\n+DeviceProfileNameAndTypes=iPad3,IOS\r\n+DeviceProfileNameAndTypes=iPad4,IOS\r\n+DeviceProfileNameAndTypes=iPadAir,IOS\r\n+DeviceProfileNameAndTypes=iPadMini,IOS\r\n+DeviceProfileNameAndTypes=iPadMini2,IOS\r\n+DeviceProfileNameAndTypes=iPhone4,IOS\r\n+DeviceProfileNameAndTypes=iPhone4S,IOS\r\n+DeviceProfileNameAndTypes=iPhone5,IOS\r\n+DeviceProfileNameAndTypes=iPhone5S,IOS\r\n+DeviceProfileNameAndTypes=iPodTouch5,IOS\r\n+DeviceProfileNameAndTypes=iPhone6,IOS\r\n+DeviceProfileNameAndTypes=iPhone6Plus,IOS\r\n+DeviceProfileNameAndTypes=Android,Android\r\n+DeviceProfileNameAndTypes=PS4,PS4\r\n+DeviceProfileNameAndTypes=XboxOne,XboxOne\r\n+DeviceProfileNameAndTypes=HTML5,HTML5\r\n+DeviceProfileNameAndTypes=Mac,Mac\r\n+DeviceProfileNameAndTypes=MacNoEditor,MacNoEditor\r\n+DeviceProfileNameAndTypes=MacServer,MacServer\r\n+DeviceProfileNameAndTypes=WinRT,WinRT\r\n+DeviceProfileNameAndTypes=Linux,Linux\r\n+DeviceProfileNameAndTypes=LinuxNoEditor,LinuxNoEditor\r\n+DeviceProfileNameAndTypes=LinuxServer,LinuxServer\r\n\r\n[Windows DeviceProfile]\r\nDeviceType=Windows\r\nBaseProfileName=\r\n\r\n+CVars=foliage.UseOcclusionType=0\r\n+CVars=ShowFloatingDamageText=True\r\n+CVars=FrameRateCap=144\r\n+CVars=FrameRateMinimum=144\r\n+CVars=MaxES2PixelShaderAdditiveComplexityCount=45\r\n+CVars=MaxPixelShaderAdditiveComplexityCount=128\r\n+CVars=MaxSmoothedFrameRate=144\r\n+CVars=MinDesiredFrameRate=120\r\n+CVars=MinSmoothedFrameRate=5\r\n+CVars=NearClipPlane=12.0\r\n+CVars=ShowFlag.VisualizeLightCulling=0\r\n+CVars=ShowFlag.AmbientOcclusion=0\r\n+CVars=ShowFlag.AntiAliasing=0\r\n+CVars=ShowFlag.Atmosphere=0\r\n+CVars=ShowFlag.AtmosphericFog=0\r\n+CVars=ShowFlag.AudioRadius=0\r\n+CVars=ShowFlag.BSP=0\r\n+CVars=ShowFlag.BSPSplit=0\r\n+CVars=ShowFlag.BSPTriangles=0\r\n+CVars=ShowFlag.BillboardSprites=0\r\n+CVars=ShowFlag.Brushes=0\r\n+CVars=ShowFlag.BuilderBrush=0\r\n+CVars=ShowFlag.CameraAspectRatioBars=0\r\n+CVars=ShowFlag.CameraFrustums=0\r\n+CVars=ShowFlag.CameraImperfections=0\r\n+CVars=ShowFlag.CameraInterpolation=0\r\n+CVars=ShowFlag.CameraSafeFrames=0\r\n+CVars=ShowFlag.ColorGrading=0\r\n+CVars=ShowFlag.CompositeEditorPrimitives=0\r\n+CVars=ShowFlag.Constraints=0\r\n+CVars=ShowFlag.Cover=0\r\n+CVars=ShowFlag.Decals=0\r\n+CVars=ShowFlag.DeferredLighting=0\r\n+CVars=ShowFlag.DepthOfField=0\r\n+CVars=ShowFlag.Diffuse=0\r\n+CVars=ShowFlag.DirectLighting=0\r\n+CVars=ShowFlag.DirectionalLights=0\r\n+CVars=ShowFlag.DistanceCulledPrimitives=0\r\n+CVars=ShowFlag.DistanceFieldAO=0\r\n+CVars=ShowFlag.DynamicShadows=0\r\n+CVars=ShowFlag.Editor=0\r\n+CVars=ShowFlag.EyeAdaptation=0\r\n+CVars=ShowFlag.Fog=0\r\n+CVars=ShowFlag.LOD=0\r\n+CVars=ShowFlag.Landscape=0\r\n+CVars=ShowFlag.LargeVertices=0\r\n+CVars=ShowFlag.LensFlares=0\r\n+CVars=ShowFlag.LevelColoration=0\r\n+CVars=ShowFlag.LightComplexity=0\r\n+CVars=ShowFlag.LightInfluences=0\r\n+CVars=ShowFlag.LightMapDensity=0\r\n+CVars=ShowFlag.LightRadius=0\r\n+CVars=ShowFlag.LightShafts=0\r\n+CVars=ShowFlag.Lighting=0\r\n+CVars=ShowFlag.LpvLightingOnly=0\r\n+CVars=ShowFlag.MeshEdges=0\r\n+CVars=ShowFlag.MotionBlur=0\r\n+CVars=ShowFlag.OnScreenDebug=0\r\n+CVars=ShowFlag.OverrideDiffuseAndSpecular=0\r\n+CVars=ShowFlag.Paper2DSprites=0\r\n+CVars=ShowFlag.Particles=0\r\n+CVars=ShowFlag.Pivot=0\r\n+CVars=ShowFlag.PointLights=0\r\n+CVars=ShowFlag.PostProcessMaterial=0\r\n+CVars=ShowFlag.PostProcessing=0\r\n+CVars=ShowFlag.PrecomputedVisibility=0\r\n+CVars=ShowFlag.PreviewShadowsIndicator=0\r\n+CVars=ShowFlag.ReflectionEnvironment=0\r\n+CVars=ShowFlag.ReflectionOverride=0\r\n+CVars=ShowFlag.Refraction=0\r\n+CVars=ShowFlag.SelectionOutline=0\r\n+CVars=ShowFlag.ShaderComplexity=0\r\n+CVars=ShowFlag.ShadowFrustums=0\r\n+CVars=ShowFlag.ShadowsFromEditorHiddenActors=0\r\n+CVars=ShowFlag.SkeletalMeshes=0\r\n+CVars=ShowFlag.SkyLighting=0\r\n+CVars=ShowFlag.Snap=0\r\n+CVars=ShowFlag.Specular=0\r\n+CVars=ShowFlag.SpotLights=0\r\n+CVars=ShowFlag.StaticMeshes=0\r\n+CVars=ShowFlag.StationaryLightOverlap=0\r\n+CVars=ShowFlag.StereoRendering=0\r\n+CVars=ShowFlag.SubsurfaceScattering=0\r\n+CVars=ShowFlag.TemporalAA=0\r\n+CVars=ShowFlag.Tessellation=0\r\n+CVars=ShowFlag.TestImage=0\r\n+CVars=ShowFlag.TextRender=0\r\n+CVars=ShowFlag.TexturedLightProfiles=0\r\n+CVars=ShowFlag.Tonemapper=0\r\n+CVars=ShowFlag.Translucency=0\r\n+CVars=ShowFlag.VectorFields=0\r\n+CVars=ShowFlag.VertexColors=0\r\n+CVars=ShowFlag.Vignette=0\r\n+CVars=ShowFlag.VisualizeAdaptiveDOF=0\r\n+CVars=ShowFlag.VisualizeBuffer=0\r\n+CVars=ShowFlag.VisualizeDOF=0\r\n+CVars=ShowFlag.VisualizeDistanceFieldAO=0\r\n+CVars=ShowFlag.VisualizeHDR=0\r\n+CVars=ShowFlag.VisualizeLPV=0\r\n+CVars=ShowFlag.VisualizeLightCulling=0\r\n+CVars=ShowFlag.VisualizeMotionBlur=0\r\n+CVars=ShowFlag.VisualizeOutOfBoundsPixels=0\r\n+CVars=ShowFlag.VisualizeSSR=0\r\n+CVars=ShowFlag.VisualizeSenses=0\r\n+CVars=ShowFlag.VolumeLightingSamples=0\r\n+CVars=ShowFlag.Wireframe=1\r\n+CVars=SmoothedFrameRateRange=(LowerBound=(Type=\"ERangeBoundTypes::Inclusive\",Value=60),UpperBound=(Type=\"ERangeBoundTypes::Exclusive\",Value=70))\r\n+CVars=TEXTUREGROUP_Character=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_CharacterNormalMap=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_CharacterSpecular=(MinLODSize=1,MaxLODSize=4,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Cinematic=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Effects=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=linear,MipFilter=point)\r\n+CVars=TEXTUREGROUP_EffectsNotFiltered=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Lightmap=(MinLODSize=1,MaxLODSize=8,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_MobileFlattened=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_RenderTarget=(MinLODSize=1,MaxLODSize=128,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Shadowmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point,NumStreamedMips=3)\r\n+CVars=TEXTUREGROUP_Skybox=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Terrain_Heightmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Terrain_Weightmap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_UI=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Vehicle=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_VehicleNormalMap=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_VehicleSpecular=(MinLODSize=1,MaxLODSize=256,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_Weapon=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WeaponNormalMap=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WeaponSpecular=(MinLODSize=1,MaxLODSize=64,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_World=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WorldNormalMap=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=TEXTUREGROUP_WorldSpecular=(MinLODSize=1,MaxLODSize=2,LODBias=0,MinMagFilter=aniso,MipFilter=point)\r\n+CVars=bDisablePhysXHardwareSupport=True\r\n+CVars=bFirstRun=False\r\n+CVars=bSmoothFrameRate=true\r\n+CVars=r.AOTrimOldRecordsFraction=0\r\n+CVars=r.AmbientOcclusionLevels=0\r\n+CVars=r.Atmosphere=0\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.ClearWithExcludeRects=0\r\n+CVars=r.CompileShadersForDevelopment=0\r\n+CVars=r.DefaultFeature.AmbientOcclusion=False\r\n+CVars=r.DefaultFeature.AntiAliasing=0\r\n+CVars=r.DefaultFeature.AutoExposure=False\r\n+CVars=r.DefaultFeature.Bloom=False\r\n+CVars=r.DefaultFeature.LensFlare=False\r\n+CVars=r.DefaultFeature.MotionBlur=False\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.DetailMode=0\r\n+CVars=r.EarlyZPass=0\r\n+CVars=r.ExposureOffset=0.3\r\n+CVars=r.HZBOcclusion=0\r\n+CVars=r.LensFlareQuality=0\r\n+CVars=r.LightFunctionQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.LightShafts=0\r\n+CVars=r.MaxAnisotropy=0\r\n+CVars=r.MotionBlurQuality=0\r\n+CVars=r.PostProcessAAQuality=0\r\n+CVars=r.ReflectionEnvironment=0\r\n+CVars=r.RefractionQuality=0\r\n+CVars=r.SSAOSmartBlur=0\r\n+CVars=r.SSR.Quality=0\r\n+CVars=r.SSS.SampleSet=0\r\n+CVars=r.SSS.Scale=0\r\n+CVars=r.SceneColorFringe.Max=0\r\n+CVars=r.SceneColorFringeQuality=0\r\n+CVars=r.Shadow.CSM.MaxCascades=1\r\n+CVars=r.Shadow.CSM.TransitionScale=0\r\n+CVars=r.Shadow.DistanceScale=0.1\r\n+CVars=r.Shadow.MaxResolution=2\r\n+CVars=r.Shadow.MinResolution=2\r\n+CVars=r.Shadow.RadiusThreshold=0.1\r\n+CVars=r.ShadowQuality=0\r\n+CVars=r.TonemapperQuality=0\r\n+CVars=r.TriangleOrderOptimization=1\r\n+CVars=r.TrueSkyQuality=0\r\n+CVars=r.UpsampleQuality=0\r\n+CVars=r.ViewDistanceScale=0\r\n+CVars=r.oneframethreadlag=1\r\n+CVars=t.maxfps=120\r\n\r\n\r\n[WindowsNoEditor DeviceProfile]\r\nDeviceType=WindowsNoEditor\r\nBaseProfileName=Windows\r\n\r\n[WindowsServer DeviceProfile]\r\nDeviceType=WindowsServer\r\nBaseProfileName=Windows\r\n\r\n[IOS DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.RefractionQuality=0\r\n\r\n[iPad2 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPad3 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPad4 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPadAir DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.BloomQuality=1\r\n\r\n[iPadMini DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPadMini2 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=iPadAir\r\n\r\n[iPhone4 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone4S DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone5 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone5S DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=2\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[iPodTouch5 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.RenderTargetSwitchWorkaround=1\r\n\r\n[iPhone6 DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=0\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[iPhone6Plus DeviceProfile]\r\nDeviceType=IOS\r\nBaseProfileName=IOS\r\n+CVars=r.MobileContentScaleFactor=0\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n\r\n[Android DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=\r\n+CVars=r.MobileContentScaleFactor=1\r\n+CVars=r.BloomQuality=0\r\n+CVars=r.DepthOfFieldQuality=0\r\n+CVars=r.LightShaftQuality=0\r\n+CVars=r.RefractionQuality=0\r\n\r\n[Android_Low DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.MobileContentScaleFactor=0.5\r\n\r\n[Android_Mid DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.MobileContentScaleFactor=0.8\r\n\r\n[Android_High DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android\r\n+CVars=r.BloomQuality=1\r\n+CVars=r.DepthOfFieldQuality=1\r\n+CVars=r.LightShaftQuality=1\r\n+CVars=r.MobileContentScaleFactor=1.0\r\n\r\n[Android_Unrecognized DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Mid\r\n\r\n[Android_Adreno320 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Mid\r\n\r\n;This offset needs to be set for the mosaic fallback to work on Galaxy S4 (SAMSUNG-IGH-I337)\r\n;+CVars=r.DemosaicVposOffset=0.5\r\n\r\n[Android_Adreno330 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_High\r\n\r\n[Android_Adreno330_Ver53 DeviceProfile]\r\nDeviceType=Android\r\nBaseProfileName=Android_Adreno330\r\n+CVars=r.DisjointTimerQueries=1\r\n\r\n[PS4 DeviceProfile]\r\nDeviceType=PS4\r\nBaseProfileName=\r\n\r\n[XboxOne DeviceProfile]\r\nDeviceType=XboxOne\r\nBaseProfileName=\r\n; we output 10:10:10, not 8:8:8 so we don't need color quantization\r\n+CVars=r.TonemapperQuality=0\r\n; For SSAO we rely on TemporalAA (with a randomized sample pattern over time) so we can use less samples\r\n+CVars=r.AmbientOcclusionSampleSetQuality=0\r\n; less passes, and no upsampling as even upsampling costs some performance\r\n+CVars=r.AmbientOcclusionLevels=1\r\n; larger radius to compensate for fewer passes\r\n+CVars=r.AmbientOcclusionRadiusScale=2\r\n\r\n\r\n[HTML5 DeviceProfile]\r\nDeviceType=HTML5\r\nBaseProfileName=\r\n+CVars=r.RefractionQuality=0\r\n\r\n[Mac DeviceProfile]\r\nDeviceType=Mac\r\nBaseProfileName=\r\n\r\n[MacNoEditor DeviceProfile]\r\nDeviceType=MacNoEditor\r\nBaseProfileName=Mac\r\n\r\n[MacServer DeviceProfile]\r\nDeviceType=MacServer\r\nBaseProfileName=Mac\r\n\r\n[WinRT DeviceProfile]\r\nDeviceType=WinRT\r\nBaseProfileName=\r\n\r\n[Linux DeviceProfile]\r\nDeviceType=Linux\r\nBaseProfileName=\r\nMeshLODSettings=\r\nTextureLODSettings=\r\n\r\n[LinuxNoEditor DeviceProfile]\r\nDeviceType=LinuxNoEditor\r\nBaseProfileName=Linux\r\nMeshLODSettings=\r\nTextureLODSettings=\r\n\r\n[LinuxServer DeviceProfile]\r\nDeviceType=LinuxServer\r\nBaseProfileName=Linux\r\nMeshLODSettings=\r\nTextureLODSettings=";

                // Write with error handling
                try
                {
                    File.WriteAllText(filePath, fileContents);
                    MessageBox.Show("Configuration updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Access denied. Please run as administrator or close ARK/Steam.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"File access error: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Open_File_Click(object sender, EventArgs e)
        {
            try
            {
                string steamPath = Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",
                    "InstallPath", null) as string
                    ?? Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam",
                    "InstallPath", null) as string;

                if (steamPath == null) return;

                string arkPath = Path.Combine(steamPath, "steamapps", "common", "ARK", "Engine", "Config", "BaseDeviceProfiles.ini");

                if (File.Exists(arkPath))
                {
                    Process.Start(arkPath);
                }
            }
            catch { }
        }


        private void Clear_File_Click(object sender, EventArgs e)
        {
            try
            {
                string steamPath = Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",
                    "InstallPath", null) as string
                    ?? Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam",
                    "InstallPath", null) as string;

                if (steamPath == null) return;

                string arkPath = Path.Combine(steamPath, "steamapps", "common", "ARK", "Engine", "Config", "BaseDeviceProfiles.ini");

                if (File.Exists(arkPath))
                {
                    File.WriteAllText(arkPath, "");
                }
            }
            catch { }
        }



        private void Open_CV_Folder_Click(object sender, EventArgs e)
        {
            try
            {
                string steamPath = Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam", "InstallPath", null) as string
                    ?? Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam", "InstallPath", null) as string;

                if (steamPath == null) return;

                string folderPath = Path.Combine(
                    steamPath, "steamapps", "common", "ARK", "ShooterGame", "Saved", "MyPaintings");

                if (Directory.Exists(folderPath))
                {
                    Process.Start("explorer.exe", folderPath);
                }
            }
            catch { }
        }


        private void Canvas_Browser_Click(object sender, EventArgs e)
        {
            Process.Start("https://arktested.com/paintings");
        }

        private void Launch_Game_Click(object sender, EventArgs e)
        {
            string steamLink = "steam://rungameid/346110";

            try
            {
                Process.Start(steamLink);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to open the Steam game: " + ex.Message);
            }

            Console.ReadLine(); 
        }

        private void Close_Game_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("ShooterGame"))
            {
                process.Kill();
            }
        }

        private void mouse_down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void mouse_move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                this.Location = mousePose;
            }
        }


        private void Reconnect_Click(object sender, EventArgs e)
        {
            ArkSurvivalEvolvedReconnector.ReconnectToArkSurvivalEvolved();
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            ArkSurvivalEvolvedDisconnect.DisconnectToArkSurvivalEvolved();
        }

        private void Create_INI_File_Click(object sender, EventArgs e)
        {
            try
            {
                string steamPath = Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",
                    "InstallPath", null) as string
                    ?? Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam",
                    "InstallPath", null) as string;

                if (steamPath == null) return;

                string arkDir = Path.Combine(steamPath, "steamapps", "common", "ARK", "Engine", "Config");
                string iniPath = Path.Combine(arkDir, "BaseDeviceProfiles.ini");

                Directory.CreateDirectory(arkDir);

                using (StreamWriter writer = File.CreateText(iniPath))
                {
                    writer.WriteLine("[DeviceProfiles]");
                    writer.WriteLine("Profile0=(DeviceType=Desktop,OperatingSystem=Windows)");
                }
            }
            catch { }
        }


        private void MoveFilesToBackup()
        {
            string backupFolder = @"C:\ARK_DELETED_FILES";
            string arkBasePath = @"C:\Program Files (x86)\Steam\steamapps\common\ARK\ShooterGame\Content\PrimalEarth";

            var fileGroups = GetFileGroups(arkBasePath);

            ProcessFileGroups(fileGroups, backupFolder);
        }

        private Dictionary<string, string[]> GetFileGroups(string basePath)
        {
            return new Dictionary<string, string[]>
            {
                // Weapon textures
                [Path.Combine(basePath, "WeaponHarpoon", "Textures")] = new[]
                {
            "T_HarpoonProjectile_Net_N.uasset",
            "T_HarpoonProjectile_Net_Layered.uasset",
            "T_HarpoonProjectile_Net_BC.uasset"
        },

                [Path.Combine(basePath, "WeaponCompoundBow", "Textures")] = new[]
                {
            "T_CompoundBow_Colorize_d.uasset",
            "T_CompoundBow_Colorize_m.uasset",
            "T_CompoundBow_N.uasset",
            "T_CompoundBow_Layered.uasset"
        },

                [Path.Combine(basePath, "WeaponHatchet", "Colorization")] = new[]
                {
            "Hatchet_colorize_m.uasset",
            "Hatchet_colorize_d.uasset"
        },

                // Tek Gen2 armor
                [Path.Combine(basePath, "Human", "Male", "Outfits", "TekGen2")] = new[]
                {
            "tekGen2_armor_male_shoes.uasset",
            "tekGen2_armor_male_shirt.uasset",
            "tekGen2_armor_male_pants.uasset",
            "tekGen2_armor_male_helmet.uasset",
            "tekGen2_armor_male_gloves.uasset",
            "tekGen2_armor_male_FPV.uasset",
            "Tek_Gen2_Armor_Emissive_Colorize_MIC.uasset",
            "MM_Tek_Emissive_Colorized_Cyan.uasset",
            "MIC_TekSuitGen2_Shoes_Colorized.uasset",
            "MIC_TekSuitGen2_Shirt_Colorized.uasset",
            "MIC_TekSuitGen2_Shirt.uasset",
            "MIC_TekSuitGen2_Pants_Colorized.uasset",
            "MIC_TekSuitGen2_Jetpack_Colorized.uasset",
            "MIC_TekSuitGen2_Helmet_Colorized.uasset",
            "MIC_TekSuitGen2_Gloves_Colorized.uasset",
            "MIC_TekSuitGen2_Emissive.uasset",
            "MIC_TekSuitGen2_Colorized.uasset"
        },

                [Path.Combine(basePath, "Human", "Male", "Outfits", "TekGen2", "Textures")] = new[]
                {
            "T_Gen2_TekHelmet_bc.uasset",
            "T_Gen2_TekHelmet_LightMask.uasset",
            "T_Gen2_TekHelmet_N.uasset",
            "T_HM_TekSuitGen2_Gloves_FPV_Skin_M.uasset",
            "T_HM_TekSuitGen2_Gloves_Skin_M.uasset",
            "T_HM_TekSuitGen2_Helmet_Skin_M.uasset",
            "T_HM_TekSuitGen2_Pants_Skin_M.uasset",
            "T_HM_TekSuitGen2_Shoes_Skin_M.uasset",
            "T_TekSuitGen2_Gloves_Colorized_D.uasset",
            "T_TekSuitGen2_Gloves_Colorized_M.uasset",
            "T_TekSuitGen2_Gloves_Layered.uasset",
            "T_TekSuitGen2_Gloves_N.uasset",
            "T_TekSuitGen2_Helmet_Colorized_D.uasset",
            "T_TekSuitGen2_Helmet_Colorized_M.uasset",
            "T_TekSuitGen2_Helmet_Layered.uasset",
            "T_TekSuitGen2_Helmet_N.uasset",
            "T_TekSuitGen2_Jetpack_Colorized_D.uasset",
            "T_TekSuitGen2_Jetpack_Colorized_M.uasset",
            "T_TekSuitGen2_Jetpack_Layered.uasset",
            "T_TekSuitGen2_Jetpack_N.uasset",
            "T_TekSuitGen2_Pants_Colorized_D.uasset",
            "T_TekSuitGen2_Pants_Colorized_M.uasset",
            "T_TekSuitGen2_Pants_Layered.uasset",
            "T_TekSuitGen2_Pants_N.uasset",
            "T_TekSuitGen2_Shirt_BC.uasset",
            "T_TekSuitGen2_Shirt_Colorized_D.uasset",
            "T_TekSuitGen2_Shirt_Colorized_M.uasset",
            "T_TekSuitGen2_Shirt_Layered.uasset",
            "T_TekSuitGen2_Shirt_N.uasset",
            "T_TekSuitGen2_Shoes_Colorized_M.uasset",
            "T_TekSuitGen2_Shoes_Layered.uasset",
            "T_TekSuitGen2_Shoes_N.uasset"
        },

                // Tek armor
                [Path.Combine(basePath, "Human", "Male", "Outfits", "Tek")] = new[]
                {
            "HUD_Tek_Boots_Colorized_MIC.uasset",
            "HUD_Tek_Colorized_MIC.uasset",
            "HUD_Tek_Gloves_Colorized_MIC.uasset",
            "HUD_Tek_Hat_Colorized_MIC.uasset",
            "HUD_Tek_Pants_Colorized_MIC.uasset",
            "HUD_Tek_Shirt_Colorized_MIC.uasset",
            "MM_Tek_Helmet.uasset",
            "Tek_Armor_Emissive_Colorize_MIC.uasset",
            "tek_armor_male_FPV.uasset",
            "tek_armor_male_gloves.uasset",
            "tek_armor_male_helmet.uasset",
            "tek_armor_male_pants.uasset",
            "tek_armor_male_shirt.uasset",
            "tek_armor_male_shoes.uasset",
            "Tek_Colorize_BaseMIC.uasset",
            "Tek_gloves_Colorize_MIC.uasset",
            "tek_Gloves_FPV_Colorize_MIC.uasset",
            "Tek_helmet_Colorize_MIC.uasset",
            "Tek_HelmetGlass_NEW_MIC.uasset",
            "Tek_jetpack_Colorize_MIC.uasset",
            "Tek_pants_Colorize_MIC.uasset",
            "Tek_shirt_Colorize_MIC.uasset",
            "Tek_shoes_Colorize_MIC.uasset"
        },

                [Path.Combine(basePath, "Human", "Male", "Outfits", "Tek", "Textures")] = new[]
                {
            "jetpack_Layered.uasset",
            "jetpack_N.uasset",
            "T_HexPattern_MNR.uasset",
            "tek_armor_FPV_Layered.uasset",
            "tek_armor_FPV_N.uasset",
            "tek_armor_gloves_Layered.uasset",
            "tek_armor_gloves_N.uasset",
            "tek_armor_helmet_G.uasset",
            "tek_armor_helmet_Layered.uasset",
            "tek_armor_helmet_N.uasset",
            "tek_armor_helmetGlass_H.uasset",
            "tek_armor_helmetGlass_M.uasset",
            "tek_armor_pants_Layered.uasset",
            "tek_armor_pants_N.uasset",
            "tek_armor_shirt_D.uasset",
            "tek_armor_shirt_Layered.uasset",
            "tek_armor_shirt_N.uasset",
            "tek_armor_shoes_Layered.uasset",
            "tek_armor_shoes_N.uasset",
            "Tek_Helmet_BC.uasset",
            "Tek_Helmet_LightingMask.uasset",
            "Tek_Helmet_N.uasset",
            "Tek_HelmetGlass_Layered.uasset",
            "Tek_HelmetGlass_N.uasset",
            "tek_Male_gloves_FPV_skin_m.uasset",
            "tek_Male_gloves_skin_m.uasset",
            "tek_Male_pants_skin_m.uasset",
            "tek_Male_shirt_skin_m.uasset"
        },

                // Metal armor
                [Path.Combine(basePath, "Human", "Male", "Outfits", "Metal")] = new[]
                {
            "Male_FPV_Metal_Gloves.uasset",
            "Male_Metal_Gloves.uasset",
            "Male_Metal_Hat.uasset",
            "Male_Metal_Shirt.uasset",
            "maleMetal_boots.uasset",
            "maleMetal_Gloves.uasset",
            "maleMetal_Pants.uasset",
            "maleMetal_Shirt.uasset"
        },

                [Path.Combine(basePath, "Human", "Male", "Outfits", "Metal", "Textures")] = new[]
                {
            "Male_Metal_Boots_Layered.uasset",
            "Male_Metal_Boots_N.uasset",
            "Male_Metal_Boots_skin_m.uasset",
            "Male_Metal_FPV_Layered.uasset",
            "Male_Metal_FPV_N.uasset",
            "Male_Metal_Gloves_D.uasset",
            "Male_Metal_Gloves_Layered.uasset",
            "Male_Metal_Gloves_N.uasset",
            "Male_Metal_Gloves_skin_m.uasset",
            "Male_Metal_Hat_D.uasset",
            "Male_Metal_Hat_Layered.uasset",
            "Male_Metal_Hat_N.uasset",
            "Male_Metal_Pants_Layered.uasset",
            "Male_Metal_Pants_N.uasset",
            "Male_Metal_Pants_skin_m.uasset",
            "Male_Metal_Shirt_D.uasset",
            "Male_Metal_Shirt_Layered.uasset",
            "Male_Metal_Shirt_N.uasset",
            "Male_Metal_Shirt_skin_m.uasset"
        },

                // C4 weapon
                [Path.Combine(basePath, "WeaponC4")] = new[]
                {
            "detonator_TPV_RIG_Skeleton.uasset",
            "detonator_TPV_RIG.uasset",
            "c4_TPV_RIG_Skeleton.uasset",
            "c4_TPV_RIG.uasset",
            "c4_FPV_RIG_Skeleton.uasset",
            "c4_FPV_RIG.uasset",
            "C4_FPV_AnimBP.uasset",
            "C4_explosive.uasset"
        },

                [Path.Combine(basePath, "WeaponC4", "Textures")] = new[]
                {
            "C4Detonator_E.uasset",
            "C4Detonator_layered.uasset",
            "C4Detonator_N.uasset",
            "Explosive_BC.uasset",
            "Explosive_E.uasset",
            "Explosive_Layered.uasset",
            "Explosive_N.uasset"
        },

                // Crossbow weapon
                [Path.Combine(basePath, "WeaponCrossbow")] = new[]
                {
            "crossbow_FPV_AnimBlueprint.uasset",
            "crossbow_FPV_RIG.uasset",
            "crossbow_FPV_RIG_Skeleton.uasset",
            "crossbow_TPV_AnimBlueprint.uasset",
            "crossbow_TPV_RIG.uasset",
            "crossbow_TPV_RIG_Skeleton.uasset",
            "crossbowArrow.uasset"
        },

                [Path.Combine(basePath, "WeaponCrossbow", "Textures")] = new[]
                {
            "Crossbow_Arrow_Metal_D.uasset",
            "Crossbow_Arrow_Metal_Layered.uasset",
            "Crossbow_Arrow_Metal_N.uasset",
            "Crossbow_Arrow_Sedative_D.uasset",
            "Crossbow_Arrow_Sedative_Layered.uasset",
            "Crossbow_Arrow_Sedative_N.uasset",
            "Crossbow_Arrow_Stone_D.uasset",
            "Crossbow_Arrow_Stone_Layered.uasset",
            "Crossbow_Arrow_Stone_N.uasset",
            "Crossbow_Colorize_d.uasset",
            "Crossbow_Colorize_m.uasset",
            "Crossbow_Layered.uasset",
            "Crossbow_N.uasset",
            "HUD_ForgedCrossbow_Colorize_d.uasset",
            "HUD_ForgedCrossbow_Colorize_m.uasset"
        },

                // Harpoon weapon
                [Path.Combine(basePath, "WeaponHarpoon")] = new[]
                {
            "Arrow_MIC.uasset",
            "Harpoon_Colorize_MIC.uasset",
            "harpoon_FPV_AnimBlueprint.uasset",
            "Harpoon_FPV_RIG.uasset",
            "Harpoon_FPV_RIG_Skeleton.uasset",
            "HUD_Harpoon_Colorize_MIC.uasset",
            "MIC_HarpoonProjectile_Net.uasset",
            "SK_HarpoonProjectile_Net.uasset",
            "SM_Harpoon.uasset",
            "SM_HarpoonAmmo_Net.uasset",
            "SM_HarpoonProjectile.uasset"
        }
            };
        }

        private void ProcessFileGroups(Dictionary<string, string[]> fileGroups, string backupFolder)
        {
            foreach (var group in fileGroups)
            {
                string sourceFolder = NormalizePath(group.Key);
                string[] files = group.Value;

                ProcessFileGroup(sourceFolder, files, backupFolder);
            }
        }

        private void ProcessFileGroup(string sourceFolder, string[] files, string backupFolder)
        {
            string destinationFolder = Path.Combine(backupFolder, Path.GetFileName(sourceFolder));

            foreach (string file in files)
            {
                MoveFileToBackup(sourceFolder, file, destinationFolder);
            }
        }

        private void MoveFileToBackup(string sourceFolder, string fileName, string destinationFolder)
        {
            string sourceFilePath = Path.Combine(sourceFolder, fileName);
            string destinationFilePath = Path.Combine(destinationFolder, fileName);

            try
            {
                Directory.CreateDirectory(destinationFolder);

                if (!File.Exists(sourceFilePath))
                {
                    Console.WriteLine($"File not found: {sourceFilePath}");
                    return;
                }

                if (File.Exists(destinationFilePath))
                {
                    Console.WriteLine($"File already exists in backup: {destinationFilePath}");
                    return;
                }

                File.Move(sourceFilePath, destinationFilePath);
                Console.WriteLine($"Successfully moved: {sourceFilePath} -> {destinationFilePath}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Permission error moving file {sourceFilePath}: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO error moving file {sourceFilePath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error moving file {sourceFilePath}: {ex.Message}");
            }
        }

        private string NormalizePath(string path)
        {
            return Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        private void Gamma_Input_TextChanged(object sender, EventArgs e)
        {
            string gammaValue = Gamma_Input.Text;
            GammaController.Set_Gamma_Input(gammaValue);
        }



        private void btnRestore_Click(object sender, EventArgs e)
        {
            string backupFolder = @"C:\ARK_DELETED_FILES";
            string arkBasePath = GetArkInstallPath();

            if (string.IsNullOrEmpty(arkBasePath))
            {
                MessageBox.Show("ARK installation path not found. Please verify ARK is installed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var fileGroups = GetFileRestoreConfiguration(arkBasePath);

            int successCount = 0;
            int failureCount = 0;
            var errors = new List<string>();

            foreach (var group in fileGroups)
            {
                string targetFolder = group.Key;
                string[] files = group.Value;

                foreach (var file in files)
                {
                    var result = RestoreFile(backupFolder, targetFolder, file);

                    if (result.Success)
                    {
                        successCount++;
                        Console.WriteLine($"✓ Restored: {file}");
                    }
                    else
                    {
                        failureCount++;
                        errors.Add(result.ErrorMessage);
                        Console.WriteLine($"✗ Failed: {result.ErrorMessage}");
                    }
                }
            }

            // Show summary
            string summary = $"Restore completed:\n• {successCount} files restored successfully\n• {failureCount} files failed";
            if (errors.Count > 0)
            {
                summary += $"\n\nFirst few errors:\n{string.Join("\n", errors.Take(5))}";
            }

            MessageBox.Show(summary, "Restore Summary", MessageBoxButtons.OK,
                failureCount == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private string GetArkInstallPath()
        {
            // Check common Steam installation paths
            string[] possiblePaths = {
        @"C:\Program Files (x86)\Steam\steamapps\common\ARK",
        @"C:\Program Files\Steam\steamapps\common\ARK",
        @"D:\Steam\steamapps\common\ARK",
        @"E:\Steam\steamapps\common\ARK"
    };

            foreach (string path in possiblePaths)
            {
                if (Directory.Exists(Path.Combine(path, "ShooterGame")))
                {
                    return path;
                }
            }

            // Try to find Steam installation from registry
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Valve\Steam"))
                {
                    if (key?.GetValue("InstallPath") is string steamPath)
                    {
                        string arkPath = Path.Combine(steamPath, "steamapps", "common", "ARK");
                        if (Directory.Exists(Path.Combine(arkPath, "ShooterGame")))
                        {
                            return arkPath;
                        }
                    }
                }
            }
            catch
            {
                // Registry access failed, continue with other methods
            }

            return null;
        }

        private Dictionary<string, string[]> GetFileRestoreConfiguration(string arkBasePath)
        {
            string shooterGamePath = Path.Combine(arkBasePath, "ShooterGame", "Content", "PrimalEarth");

            return new Dictionary<string, string[]>
    {
        // Weapon files
        {
            Path.Combine(shooterGamePath, "WeaponHarpoon", "Textures"),
            new[] {
                "T_HarpoonProjectile_Net_N.uasset",
                "T_HarpoonProjectile_Net_Layered.uasset",
                "T_HarpoonProjectile_Net_BC.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "WeaponCompoundBow", "Textures"),
            new[] {
                "T_CompoundBow_Colorize_d.uasset",
                "T_CompoundBow_Colorize_m.uasset",
                "T_CompoundBow_N.uasset",
                "T_CompoundBow_Layered.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "WeaponHatchet", "Colorization"),
            new[] {
                "Hatchet_colorize_m.uasset",
                "Hatchet_colorize_d.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "WeaponC4"),
            new[] {
                "detonator_TPV_RIG_Skeleton.uasset",
                "detonator_TPV_RIG.uasset",
                "c4_TPV_RIG_Skeleton.uasset",
                "c4_TPV_RIG.uasset",
                "c4_FPV_RIG_Skeleton.uasset",
                "c4_FPV_RIG.uasset",
                "C4_FPV_AnimBP.uasset",
                "C4_explosive.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "WeaponC4", "Textures"),
            new[] {
                "C4Detonator_E.uasset",
                "C4Detonator_layered.uasset",
                "C4Detonator_N.uasset",
                "Explosive_BC.uasset",
                "Explosive_E.uasset",
                "Explosive_Layered.uasset",
                "Explosive_N.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "WeaponCrossbow"),
            new[] {
                "crossbow_FPV_AnimBlueprint.uasset",
                "crossbow_FPV_RIG.uasset",
                "crossbow_FPV_RIG_Skeleton.uasset",
                "crossbow_TPV_AnimBlueprint.uasset",
                "crossbow_TPV_RIG.uasset",
                "crossbow_TPV_RIG_Skeleton.uasset",
                "crossbowArrow.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "WeaponCrossbow", "Textures"),
            new[] {
                "Crossbow_Arrow_Metal_D.uasset",
                "Crossbow_Arrow_Metal_Layered.uasset",
                "Crossbow_Arrow_Metal_N.uasset",
                "Crossbow_Arrow_Sedative_D.uasset",
                "Crossbow_Arrow_Sedative_Layered.uasset",
                "Crossbow_Arrow_Sedative_N.uasset",
                "Crossbow_Arrow_Stone_D.uasset",
                "Crossbow_Arrow_Stone_Layered.uasset",
                "Crossbow_Arrow_Stone_N.uasset",
                "Crossbow_Colorize_d.uasset",
                "Crossbow_Colorize_m.uasset",
                "Crossbow_Layered.uasset",
                "Crossbow_N.uasset",
                "HUD_ForgedCrossbow_Colorize_d.uasset",
                "HUD_ForgedCrossbow_Colorize_m.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "WeaponHarpoon"),
            new[] {
                "Arrow_MIC.uasset",
                "Harpoon_Colorize_MIC.uasset",
                "harpoon_FPV_AnimBlueprint.uasset",
                "Harpoon_FPV_RIG.uasset",
                "Harpoon_FPV_RIG_Skeleton.uasset",
                "HUD_Harpoon_Colorize_MIC.uasset",
                "MIC_HarpoonProjectile_Net.uasset",
                "SK_HarpoonProjectile_Net.uasset",
                "SM_Harpoon.uasset",
                "SM_HarpoonAmmo_Net.uasset",
                "SM_HarpoonProjectile.uasset"
            }
        },
        
        // Armor files
        {
            Path.Combine(shooterGamePath, "Human", "Male", "Outfits", "TekGen2"),
            new[] {
                "tekGen2_armor_male_shoes.uasset",
                "tekGen2_armor_male_shirt.uasset",
                "tekGen2_armor_male_pants.uasset",
                "tekGen2_armor_male_helmet.uasset",
                "tekGen2_armor_male_gloves.uasset",
                "tekGen2_armor_male_FPV.uasset",
                "Tek_Gen2_Armor_Emissive_Colorize_MIC.uasset",
                "MM_Tek_Emissive_Colorized_Cyan.uasset",
                "MIC_TekSuitGen2_Shoes_Colorized.uasset",
                "MIC_TekSuitGen2_Shirt_Colorized.uasset",
                "MIC_TekSuitGen2_Shirt.uasset",
                "MIC_TekSuitGen2_Pants_Colorized.uasset",
                "MIC_TekSuitGen2_Jetpack_Colorized.uasset",
                "MIC_TekSuitGen2_Helmet_Colorized.uasset",
                "MIC_TekSuitGen2_Gloves_Colorized.uasset",
                "MIC_TekSuitGen2_Emissive.uasset",
                "MIC_TekSuitGen2_Colorized.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "Human", "Male", "Outfits", "TekGen2", "Textures"),
            new[] {
                "T_Gen2_TekHelmet_bc.uasset",
                "T_Gen2_TekHelmet_LightMask.uasset",
                "T_Gen2_TekHelmet_N.uasset",
                "T_HM_TekSuitGen2_Gloves_FPV_Skin_M.uasset",
                "T_HM_TekSuitGen2_Gloves_Skin_M.uasset",
                "T_HM_TekSuitGen2_Helmet_Skin_M.uasset",
                "T_HM_TekSuitGen2_Pants_Skin_M.uasset",
                "T_HM_TekSuitGen2_Shoes_Skin_M.uasset",
                "T_TekSuitGen2_Gloves_Colorized_D.uasset",
                "T_TekSuitGen2_Gloves_Colorized_M.uasset",
                "T_TekSuitGen2_Gloves_Layered.uasset",
                "T_TekSuitGen2_Gloves_N.uasset",
                "T_TekSuitGen2_Helmet_Colorized_D.uasset",
                "T_TekSuitGen2_Helmet_Colorized_M.uasset",
                "T_TekSuitGen2_Helmet_Layered.uasset",
                "T_TekSuitGen2_Helmet_N.uasset",
                "T_TekSuitGen2_Jetpack_Colorized_D.uasset",
                "T_TekSuitGen2_Jetpack_Colorized_M.uasset",
                "T_TekSuitGen2_Jetpack_Layered.uasset",
                "T_TekSuitGen2_Jetpack_N.uasset",
                "T_TekSuitGen2_Pants_Colorized_D.uasset",
                "T_TekSuitGen2_Pants_Colorized_M.uasset",
                "T_TekSuitGen2_Pants_Layered.uasset",
                "T_TekSuitGen2_Pants_N.uasset",
                "T_TekSuitGen2_Shirt_BC.uasset",
                "T_TekSuitGen2_Shirt_Colorized_D.uasset",
                "T_TekSuitGen2_Shirt_Colorized_M.uasset",
                "T_TekSuitGen2_Shirt_Layered.uasset",
                "T_TekSuitGen2_Shirt_N.uasset",
                "T_TekSuitGen2_Shoes_Colorized_M.uasset",
                "T_TekSuitGen2_Shoes_Layered.uasset",
                "T_TekSuitGen2_Shoes_N.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "Human", "Male", "Outfits", "Tek"),
            new[] {
                "HUD_Tek_Boots_Colorized_MIC.uasset",
                "HUD_Tek_Colorized_MIC.uasset",
                "HUD_Tek_Gloves_Colorized_MIC.uasset",
                "HUD_Tek_Hat_Colorized_MIC.uasset",
                "HUD_Tek_Pants_Colorized_MIC.uasset",
                "HUD_Tek_Shirt_Colorized_MIC.uasset",
                "MM_Tek_Helmet.uasset",
                "Tek_Armor_Emissive_Colorize_MIC.uasset",
                "tek_armor_male_FPV.uasset",
                "tek_armor_male_gloves.uasset",
                "tek_armor_male_helmet.uasset",
                "tek_armor_male_pants.uasset",
                "tek_armor_male_shirt.uasset",
                "tek_armor_male_shoes.uasset",
                "Tek_Colorize_BaseMIC.uasset",
                "Tek_gloves_Colorize_MIC.uasset",
                "tek_Gloves_FPV_Colorize_MIC.uasset",
                "Tek_helmet_Colorize_MIC.uasset",
                "Tek_HelmetGlass_NEW_MIC.uasset",
                "Tek_jetpack_Colorize_MIC.uasset",
                "Tek_pants_Colorize_MIC.uasset",
                "Tek_shirt_Colorize_MIC.uasset",
                "Tek_shoes_Colorize_MIC.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "Human", "Male", "Outfits", "Tek", "Textures"),
            new[] {
                "jetpack_Layered.uasset",
                "jetpack_N.uasset",
                "T_HexPattern_MNR.uasset",
                "tek_armor_FPV_Layered.uasset",
                "tek_armor_FPV_N.uasset",
                "tek_armor_gloves_Layered.uasset",
                "tek_armor_gloves_N.uasset",
                "tek_armor_helmet_G.uasset",
                "tek_armor_helmet_Layered.uasset",
                "tek_armor_helmet_N.uasset",
                "tek_armor_helmetGlass_H.uasset",
                "tek_armor_helmetGlass_M.uasset",
                "tek_armor_pants_Layered.uasset",
                "tek_armor_pants_N.uasset",
                "tek_armor_shirt_D.uasset",
                "tek_armor_shirt_Layered.uasset",
                "tek_armor_shirt_N.uasset",
                "tek_armor_shoes_Layered.uasset",
                "tek_armor_shoes_N.uasset",
                "Tek_Helmet_BC.uasset",
                "Tek_Helmet_LightingMask.uasset",
                "Tek_Helmet_N.uasset",
                "Tek_HelmetGlass_Layered.uasset",
                "Tek_HelmetGlass_N.uasset",
                "tek_Male_gloves_FPV_skin_m.uasset",
                "tek_Male_gloves_skin_m.uasset",
                "tek_Male_pants_skin_m.uasset",
                "tek_Male_shirt_skin_m.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "Human", "Male", "Outfits", "Metal"),
            new[] {
                "Male_FPV_Metal_Gloves.uasset",
                "Male_Metal_Gloves.uasset",
                "Male_Metal_Hat.uasset",
                "Male_Metal_Shirt.uasset",
                "maleMetal_boots.uasset",
                "maleMetal_Gloves.uasset",
                "maleMetal_Pants.uasset",
                "maleMetal_Shirt.uasset"
            }
        },
        {
            Path.Combine(shooterGamePath, "Human", "Male", "Outfits", "Metal", "Textures"),
            new[] {
                "Male_Metal_Boots_Layered.uasset",
                "Male_Metal_Boots_N.uasset",
                "Male_Metal_Boots_skin_m.uasset",
                "Male_Metal_FPV_Layered.uasset",
                "Male_Metal_FPV_N.uasset",
                "Male_Metal_Gloves_D.uasset",
                "Male_Metal_Gloves_Layered.uasset",
                "Male_Metal_Gloves_N.uasset",
                "Male_Metal_Gloves_skin_m.uasset",
                "Male_Metal_Hat_D.uasset",
                "Male_Metal_Hat_Layered.uasset",
                "Male_Metal_Hat_N.uasset",
                "Male_Metal_Pants_Layered.uasset",
                "Male_Metal_Pants_N.uasset",
                "Male_Metal_Pants_skin_m.uasset",
                "Male_Metal_Shirt_D.uasset",
                "Male_Metal_Shirt_Layered.uasset",
                "Male_Metal_Shirt_N.uasset",
                "Male_Metal_Shirt_skin_m.uasset"
            }
        }
    };
        }

        private (bool Success, string ErrorMessage) RestoreFile(string backupFolder, string targetFolder, string fileName)
        {
            try
            {
                // Create target directory if it doesn't exist
                Directory.CreateDirectory(targetFolder);

                // Build file paths
                string backupFilePath = Path.Combine(backupFolder, Path.GetFileName(targetFolder), fileName);
                string targetFilePath = Path.Combine(targetFolder, fileName);

                if (!File.Exists(backupFilePath))
                {
                    return (false, $"Backup file not found: {fileName}");
                }

                // Move file from backup to target location
                File.Move(backupFilePath, targetFilePath);
                return (true, null);
            }
            catch (UnauthorizedAccessException)
            {
                return (false, $"Access denied for {fileName} - Run as Administrator");
            }
            catch (IOException ex)
            {
                return (false, $"IO error for {fileName}: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Error restoring {fileName}: {ex.Message}");
            }
        }


        private void gammaTimer_Tick(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            MoveFilesToBackup();
        }

        private void open_restore_directory_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("C:\\ARK_DELETED_FILES");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Game_Section_Click(object sender, EventArgs e)
        {

        }

        private void Note_Box_TextChanged(object sender, EventArgs e)
        {

        }

        private void server_options_Click(object sender, EventArgs e)
        {
            ArkServerManager arkForm = new ArkServerManager();
            arkForm.Show();
        }

        private void Canvas_Section_Click(object sender, EventArgs e)
        {

        }

        private void CS_Section_Click(object sender, EventArgs e)
        {

        }

        private void TabOptions_Click(object sender, EventArgs e)
        {

        }

        private void Current_Section_Click(object sender, EventArgs e)
        {

        }

        private void INI_Section_Click(object sender, EventArgs e)
        {

        }

        private void MainLabel_Click(object sender, EventArgs e)
        {

        }
    }
}

public class FileDeleter
{
    public static void DeleteFiles(string[] filePaths)
    {
        foreach (string filePath in filePaths)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine("Deleted file: " + filePath);
                }
                else
                {
                    Console.WriteLine("File not found: " + filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting file: " + filePath + "\n" + ex.Message);
            }
        }
    }
}


public class ArkSurvivalEvolvedReconnector
{
    private const int SW_RESTORE = 9;
    private const uint KEYEVENTF_KEYUP = 0x0002;
    private const uint VK_TAB = 0x09;

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    public static void ReconnectToArkSurvivalEvolved()
    {
        Process[] processes = Process.GetProcessesByName("ShooterGame");

        if (processes.Length > 0)
        {
            IntPtr gameWindowHandle = processes[0].MainWindowHandle;

            ShowWindow(gameWindowHandle, SW_RESTORE);
            SetForegroundWindow(gameWindowHandle);

            Thread.Sleep(500);

            keybd_event((byte)VK_TAB, 0, 0, UIntPtr.Zero);
            keybd_event((byte)VK_TAB, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);

            Thread.Sleep(200);

            SendKeys.SendWait("reconnect");
            SendKeys.SendWait("{ENTER}");
        }
        else
        {
            MessageBox.Show("ArkSurvivalEvolved is not running!");
        }
    }
}

public class ArkSurvivalEvolvedDisconnect
{
    private const int SW_RESTORE = 9;
    private const uint KEYEVENTF_KEYUP = 0x0002;
    private const uint VK_TAB = 0x09;

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    public static void DisconnectToArkSurvivalEvolved()
    {
        Process[] processes = Process.GetProcessesByName("ShooterGame");

        if (processes.Length > 0)
        {
            IntPtr gameWindowHandle = processes[0].MainWindowHandle;

            ShowWindow(gameWindowHandle, SW_RESTORE);
            SetForegroundWindow(gameWindowHandle);

            Thread.Sleep(500);

            keybd_event((byte)VK_TAB, 0, 0, UIntPtr.Zero);
            keybd_event((byte)VK_TAB, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);

            Thread.Sleep(200);

            SendKeys.SendWait("disconnect");
            SendKeys.SendWait("{ENTER}");
        }
        else
        {
            MessageBox.Show("ArkSurvivalEvolved is not running!");
        }
    }
}

public class GammaController
{
    private const int SW_RESTORE = 9;
    private const uint KEYEVENTF_KEYUP = 0x0002;
    private const uint VK_TAB = 0x09;

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    /// <summary>
    /// Validates and sets gamma value for ShooterGame (original method name for compatibility)
    /// </summary>
    /// <param name="gammaValue">Gamma value as string (0-10, decimals allowed)</param>
    public static void Set_Gamma_Input(string gammaValue)
    {
        // Validate input first
        if (!IsValidGammaValue(gammaValue, out float validatedValue))
        {
            MessageBox.Show($"Invalid gamma value!\nPlease enter a number between 0 and 10.\nExamples: 2.5, 3,7, 5, 0.8",
                          "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Check if game is running
        Process[] processes = Process.GetProcessesByName("ShooterGame");
        if (processes.Length == 0)
        {
            MessageBox.Show("ShooterGame is not running!\nPlease start the game first.",
                          "Game Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            IntPtr gameWindowHandle = processes[0].MainWindowHandle;

            // Bring game window to foreground
            ShowWindow(gameWindowHandle, SW_RESTORE);
            SetForegroundWindow(gameWindowHandle);
            Thread.Sleep(500);

            // Open console with Tab key
            keybd_event((byte)VK_TAB, 0, 0, UIntPtr.Zero);
            keybd_event((byte)VK_TAB, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            Thread.Sleep(200);

            // Send gamma command with validated value
            // Use dot as decimal separator for game console
            string formattedValue = validatedValue.ToString("F2", CultureInfo.InvariantCulture);
            SendKeys.SendWait($"gamma {formattedValue}");
            SendKeys.SendWait("{ENTER}");

            // Optional: Show success message
            // MessageBox.Show($"Gamma set to {formattedValue}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to set gamma value.\nError: {ex.Message}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Alternative method with return value for more control
    /// </summary>
    /// <param name="gammaInput">Gamma value as string (0-10, decimals allowed)</param>
    /// <returns>True if successful, false if validation failed or game not found</returns>
    public static bool SetGammaValue(string gammaInput)
    {
        // Validate input
        if (!IsValidGammaValue(gammaInput, out float gammaValue))
        {
            return false;
        }

        // Find the game process
        Process[] processes = Process.GetProcessesByName("ShooterGame");
        if (processes.Length == 0)
        {
            return false; // Game not running
        }

        try
        {
            IntPtr gameWindowHandle = processes[0].MainWindowHandle;

            // Bring game window to foreground
            ShowWindow(gameWindowHandle, SW_RESTORE);
            SetForegroundWindow(gameWindowHandle);
            Thread.Sleep(500);

            // Open console with Tab key
            keybd_event((byte)VK_TAB, 0, 0, UIntPtr.Zero);
            keybd_event((byte)VK_TAB, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            Thread.Sleep(200);

            // Send gamma command with validated value
            // Use dot as decimal separator for game console
            string formattedValue = gammaValue.ToString("F2", CultureInfo.InvariantCulture);
            SendKeys.SendWait($"gamma {formattedValue}");
            SendKeys.SendWait("{ENTER}");

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validates gamma input - only accepts numbers between 0 and 10
    /// </summary>
    /// <param name="input">Input string to validate</param>
    /// <param name="value">Parsed float value if valid</param>
    /// <returns>True if valid, false otherwise</returns>
    private static bool IsValidGammaValue(string input, out float value)
    {
        value = 0f;

        if (string.IsNullOrWhiteSpace(input))
            return false;

        // Try parsing with both comma and dot as decimal separators
        bool isValid = float.TryParse(input.Replace(',', '.'),
                                    NumberStyles.Float,
                                    CultureInfo.InvariantCulture,
                                    out value);

        // Check if parsing was successful and value is in valid range
        return isValid && value >= 0f && value <= 10f;
    }

    /// <summary>
    /// Interactive method to get gamma value from user with validation
    /// </summary>
    public static void SetGammaInteractive()
    {
        Console.WriteLine("Enter gamma value (0-10, decimals allowed):");
        Console.WriteLine("Press Enter to confirm, or 'q' to quit");

        while (true)
        {
            Console.Write("Gamma: ");
            string input = Console.ReadLine();

            if (input?.ToLower() == "q")
            {
                Console.WriteLine("Cancelled.");
                return;
            }

            if (IsValidGammaValue(input, out float gammaValue))
            {
                Console.WriteLine($"Setting gamma to {gammaValue:F2}...");

                if (SetGammaValue(input))
                {
                    Console.WriteLine("Gamma value set successfully!");
                    return;
                }
                else
                {
                    Console.WriteLine("Failed to set gamma. Make sure ShooterGame is running.");
                    Console.WriteLine("Try again? (y/n)");
                    if (Console.ReadLine()?.ToLower() != "y")
                        return;
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a number between 0 and 10.");
                Console.WriteLine("Examples: 2.5, 3,7, 5, 0.8");
            }
        }
    }
}