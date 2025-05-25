using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace ReaperV2
{
    public partial class ArkServerManager : Form
    {
        private Panel headerPanel;
        private Label titleLabel;
        private Panel inputPanel;
        private Label serverIpLabel;
        private TextBox serverIpTextBox;
        private Button queryButton;
        private Panel infoPanel;
        private RichTextBox serverInfoTextBox;
        private Panel actionPanel;
        private Button favoriteButton;
        private Button connectButton;
        private Button createLinkButton;
        private Button closeButton;

        // Color scheme
        private readonly Color BackgroundColor = Color.FromArgb(15, 15, 15);
        private readonly Color PanelColor = Color.FromArgb(25, 25, 25);
        private readonly Color PurpleAccent = Color.FromArgb(138, 43, 226);
        private readonly Color PurpleHover = Color.FromArgb(160, 70, 255);
        private readonly Color GreenAccent = Color.FromArgb(34, 139, 34);
        private readonly Color GreenHover = Color.FromArgb(50, 205, 50);
        private readonly Color TextColor = Color.White;
        private readonly Color SubTextColor = Color.FromArgb(200, 200, 200);
        private readonly Color InputColor = Color.FromArgb(35, 35, 35);

        public ArkServerManager()
        {
            InitializeComponent();
            SetupForm();
        }

        private void ArkServerManager_Load(object sender, EventArgs e)
        {
        }

        private void SetupForm()
        {
            this.Text = "ARK Server Manager";
            this.Size = new Size(700, 600);
            this.BackColor = BackgroundColor;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;

            CreateControls();
            LayoutControls();
            ApplyStyles();
        }

        private void CreateControls()
        {
            // Header Panel
            headerPanel = new Panel();
            titleLabel = new Label()
            {
                Text = "ARK SERVER MANAGER",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = PurpleAccent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Input Panel
            inputPanel = new Panel();
            serverIpLabel = new Label()
            {
                Text = "Server IP Address:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = TextColor
            };

            serverIpTextBox = new TextBox()
            {
                Font = new Font("Consolas", 10),
                BackColor = InputColor,
                ForeColor = TextColor,
                BorderStyle = BorderStyle.FixedSingle,
                Text = "192.168.1.100:7777"
            };

            queryButton = new Button()
            {
                Text = "QUERY SERVER",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = PurpleAccent,
                ForeColor = TextColor,
                Cursor = Cursors.Hand
            };

            // Info Panel
            infoPanel = new Panel();
            serverInfoTextBox = new RichTextBox()
            {
                BackColor = InputColor,
                ForeColor = TextColor,
                Font = new Font("Consolas", 9),
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                Text = "Enter a server IP address and click 'Query Server' to get server information."
            };

            // Action Panel
            actionPanel = new Panel();
            favoriteButton = new Button()
            {
                Text = "ADD TO FAVORITES",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = GreenAccent,
                ForeColor = TextColor,
                Enabled = false,
                Cursor = Cursors.Hand
            };

            connectButton = new Button()
            {
                Text = "CONNECT NOW",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = PurpleAccent,
                ForeColor = TextColor,
                Enabled = false,
                Cursor = Cursors.Hand
            };

            createLinkButton = new Button()
            {
                Text = "CREATE LINK",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = TextColor,
                Enabled = false,
                Cursor = Cursors.Hand
            };

            closeButton = new Button()
            {
                Text = "CLOSE",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(139, 0, 0),
                ForeColor = TextColor,
                Cursor = Cursors.Hand
            };
        }

        private void LayoutControls()
        {
            // Header Panel
            headerPanel.Location = new Point(20, 20);
            headerPanel.Size = new Size(640, 60);
            headerPanel.BackColor = PanelColor;

            titleLabel.Dock = DockStyle.Fill;
            headerPanel.Controls.Add(titleLabel);

            // Input Panel
            inputPanel.Location = new Point(20, 100);
            inputPanel.Size = new Size(640, 80);
            inputPanel.BackColor = PanelColor;

            serverIpLabel.Location = new Point(20, 15);
            serverIpLabel.Size = new Size(120, 25);

            serverIpTextBox.Location = new Point(20, 40);
            serverIpTextBox.Size = new Size(400, 25);

            queryButton.Location = new Point(440, 38);
            queryButton.Size = new Size(180, 30);

            inputPanel.Controls.AddRange(new Control[] { serverIpLabel, serverIpTextBox, queryButton });

            // Info Panel
            infoPanel.Location = new Point(20, 200);
            infoPanel.Size = new Size(640, 280);
            infoPanel.BackColor = PanelColor;

            serverInfoTextBox.Location = new Point(15, 15);
            serverInfoTextBox.Size = new Size(610, 250);
            infoPanel.Controls.Add(serverInfoTextBox);

            // Action Panel
            actionPanel.Location = new Point(20, 500);
            actionPanel.Size = new Size(640, 60);
            actionPanel.BackColor = PanelColor;

            favoriteButton.Location = new Point(20, 15);
            favoriteButton.Size = new Size(140, 30);

            connectButton.Location = new Point(180, 15);
            connectButton.Size = new Size(140, 30);

            createLinkButton.Location = new Point(340, 15);
            createLinkButton.Size = new Size(140, 30);

            closeButton.Location = new Point(500, 15);
            closeButton.Size = new Size(120, 30);

            actionPanel.Controls.AddRange(new Control[] { favoriteButton, connectButton, createLinkButton, closeButton });

            // Add all panels to form
            this.Controls.AddRange(new Control[] { headerPanel, inputPanel, infoPanel, actionPanel });
        }

        private void ApplyStyles()
        {
            // Apply flat button styles and hover effects
            ApplyButtonStyle(queryButton, PurpleAccent, PurpleHover);
            ApplyButtonStyle(favoriteButton, GreenAccent, GreenHover);
            ApplyButtonStyle(connectButton, PurpleAccent, PurpleHover);
            ApplyButtonStyle(createLinkButton, Color.FromArgb(70, 70, 70), Color.FromArgb(100, 100, 100));
            ApplyButtonStyle(closeButton, Color.FromArgb(139, 0, 0), Color.FromArgb(200, 0, 0));

            // Wire up events
            queryButton.Click += QueryButton_Click;
            favoriteButton.Click += FavoriteButton_Click;
            connectButton.Click += ConnectButton_Click;
            createLinkButton.Click += CreateLinkButton_Click;
            closeButton.Click += (s, e) => this.Close();

            // Add panel borders
            AddPanelBorder(headerPanel);
            AddPanelBorder(inputPanel);
            AddPanelBorder(infoPanel);
            AddPanelBorder(actionPanel);
        }

        private void ApplyButtonStyle(Button button, Color normalColor, Color hoverColor)
        {
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = hoverColor;
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(Math.Max(0, hoverColor.R - 20),
                Math.Max(0, hoverColor.G - 20), Math.Max(0, hoverColor.B - 20));
        }

        private void AddPanelBorder(Panel panel)
        {
            panel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(60, 60, 60), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
                }
            };
        }

        private async void QueryButton_Click(object sender, EventArgs e)
        {
            string serverIp = serverIpTextBox.Text.Trim();
            if (string.IsNullOrEmpty(serverIp))
            {
                ShowMessage("Please enter a server IP:Port", "Input Required", MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SetQueryButtonState(false, "QUERYING...");
                serverInfoTextBox.Clear();
                serverInfoTextBox.AppendText("Querying server, please wait...\n");

                var serverInfo = await QueryArkServer(serverIp);

                if (serverInfo != null)
                {
                    DisplayServerInfo(serverInfo);
                    EnableActionButtons(true);
                }
                else
                {
                    serverInfoTextBox.Clear();
                    serverInfoTextBox.ForeColor = Color.FromArgb(255, 100, 100);
                    serverInfoTextBox.AppendText("❌ Failed to query server\n\n");
                    serverInfoTextBox.AppendText("Possible reasons:\n");
                    serverInfoTextBox.AppendText("• Server is offline or unreachable\n");
                    serverInfoTextBox.AppendText("• Invalid IP address or port\n");
                    serverInfoTextBox.AppendText("• Server doesn't respond to queries\n");
                    serverInfoTextBox.AppendText("• Firewall blocking connection\n\n");
                    serverInfoTextBox.AppendText("You can still try to connect manually using the buttons below.");
                    serverInfoTextBox.ForeColor = TextColor;
                    EnableActionButtons(true); // Allow manual connection attempts
                }
            }
            catch (Exception ex)
            {
                serverInfoTextBox.Clear();
                serverInfoTextBox.ForeColor = Color.FromArgb(255, 100, 100);
                serverInfoTextBox.AppendText($"❌ Error: {ex.Message}");
                serverInfoTextBox.ForeColor = TextColor;
                EnableActionButtons(false);
            }
            finally
            {
                SetQueryButtonState(true, "QUERY SERVER");
            }
        }

        private void SetQueryButtonState(bool enabled, string text)
        {
            queryButton.Enabled = enabled;
            queryButton.Text = text;
        }

        private void EnableActionButtons(bool enabled)
        {
            favoriteButton.Enabled = enabled;
            connectButton.Enabled = enabled;
            createLinkButton.Enabled = enabled;
        }

        private async Task<ArkServerInfo> QueryArkServer(string serverAddress)
        {
            try
            {
                string[] parts = serverAddress.Split(':');
                if (parts.Length != 2 || !int.TryParse(parts[1], out int port))
                    return null;

                string ip = parts[0];

                // Validate IP address
                if (!IPAddress.TryParse(ip, out _))
                    return null;

                // Try Steam Query Protocol (Source Engine Query)
                var serverInfo = await QuerySteamServer(ip, port);
                if (serverInfo != null)
                    return serverInfo;

                // If direct query fails, try common ARK query ports
                int[] commonPorts = { port + 1, 27015, 27016, 27017 };
                foreach (int queryPort in commonPorts)
                {
                    serverInfo = await QuerySteamServer(ip, queryPort);
                    if (serverInfo != null)
                    {
                        serverInfo.Port = port; // Keep original game port
                        return serverInfo;
                    }
                }

                // Last resort - basic connectivity test
                if (await TestConnection(ip, port))
                {
                    return new ArkServerInfo
                    {
                        Name = "ARK Server (Responsive)",
                        Map = "Unknown",
                        Players = "Unknown/Unknown",
                        Ping = "Unknown",
                        Version = "Unknown",
                        IsOnline = true,
                        IP = ip,
                        Port = port
                    };
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private async Task<ArkServerInfo> QuerySteamServer(string ip, int port)
        {
            try
            {
                using (var udpClient = new UdpClient())
                {
                    udpClient.Client.ReceiveTimeout = 3000;
                    var endpoint = new IPEndPoint(IPAddress.Parse(ip), port);

                    // Steam A2S_INFO query packet
                    byte[] queryPacket = new byte[] {
                        0xFF, 0xFF, 0xFF, 0xFF, // Header
                        0x54, // A2S_INFO
                        0x53, 0x6F, 0x75, 0x72, 0x63, 0x65, 0x20, 0x45, 0x6E, 0x67, 0x69, 0x6E, 0x65, 0x20, 0x51, 0x75, 0x65, 0x72, 0x79, 0x00 // "Source Engine Query"
                    };

                    var startTime = DateTime.Now;
                    await udpClient.SendAsync(queryPacket, queryPacket.Length, endpoint);

                    var response = await udpClient.ReceiveAsync();
                    var ping = (int)(DateTime.Now - startTime).TotalMilliseconds;

                    return ParseSteamResponse(response.Buffer, ip, port, ping);
                }
            }
            catch
            {
                return null;
            }
        }

        private ArkServerInfo ParseSteamResponse(byte[] data, string ip, int port, int ping)
        {
            try
            {
                if (data.Length < 6) return null;

                // Check for valid A2S_INFO response
                if (data[4] != 0x49) return null; // 'I' response type

                int pos = 5;

                // Skip protocol version
                pos++;

                // Parse server name
                string serverName = ReadString(data, ref pos);

                // Parse map name
                string mapName = ReadString(data, ref pos);

                // Skip folder and game name
                ReadString(data, ref pos); // folder
                ReadString(data, ref pos); // game

                // Skip app ID
                pos += 2;

                // Parse player count
                byte players = data[pos++];
                byte maxPlayers = data[pos++];

                return new ArkServerInfo
                {
                    Name = string.IsNullOrEmpty(serverName) ? "ARK Server" : serverName,
                    Map = string.IsNullOrEmpty(mapName) ? "Unknown" : mapName,
                    Players = $"{players}/{maxPlayers}",
                    Ping = $"{ping}ms",
                    Version = "Unknown",
                    IsOnline = true,
                    IP = ip,
                    Port = port
                };
            }
            catch
            {
                return null;
            }
        }

        private string ReadString(byte[] data, ref int pos)
        {
            var sb = new StringBuilder();
            while (pos < data.Length && data[pos] != 0)
            {
                sb.Append((char)data[pos]);
                pos++;
            }
            pos++; // Skip null terminator
            return sb.ToString();
        }

        private async Task<bool> TestConnection(string ip, int port)
        {
            try
            {
                using (var udpClient = new UdpClient())
                {
                    udpClient.Client.ReceiveTimeout = 2000;
                    var endpoint = new IPEndPoint(IPAddress.Parse(ip), port);

                    // Send a simple packet and see if we get any response
                    byte[] testPacket = { 0xFF, 0xFF, 0xFF, 0xFF };
                    await udpClient.SendAsync(testPacket, testPacket.Length, endpoint);

                    var response = await udpClient.ReceiveAsync();
                    return response.Buffer.Length > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        private void DisplayServerInfo(ArkServerInfo info)
        {
            serverInfoTextBox.Clear();
            serverInfoTextBox.ForeColor = GreenAccent;
            serverInfoTextBox.AppendText("✅ SERVER FOUND\n\n");
            serverInfoTextBox.ForeColor = TextColor;

            serverInfoTextBox.AppendText($"Server Name: {info.Name}\n");
            serverInfoTextBox.AppendText($"Map: {info.Map}\n");
            serverInfoTextBox.AppendText($"Players: {info.Players}\n");
            serverInfoTextBox.AppendText($"Response Time: {info.Ping}\n");
            serverInfoTextBox.AppendText($"Version: {info.Version}\n");

            serverInfoTextBox.ForeColor = info.IsOnline ? GreenAccent : Color.FromArgb(255, 100, 100);
            serverInfoTextBox.AppendText($"Status: {(info.IsOnline ? "ONLINE" : "OFFLINE")}\n");
            serverInfoTextBox.ForeColor = TextColor;

            serverInfoTextBox.AppendText($"Address: {info.IP}:{info.Port}\n\n");

            serverInfoTextBox.ForeColor = SubTextColor;
            serverInfoTextBox.AppendText("Note: Full server details require ARK-specific query protocol implementation.");
            serverInfoTextBox.ForeColor = TextColor;
        }

        private void FavoriteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string serverIp = serverIpTextBox.Text.Trim();
                AddToSteamFavorites(serverIp);
                ShowMessage("Server added to Steam favorites successfully!", "Success", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowMessage($"Failed to add to favorites:\n{ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                string serverIp = serverIpTextBox.Text.Trim();
                string[] parts = serverIp.Split(':');

                if (parts.Length == 2)
                {
                    string ip = parts[0];
                    int queryPort = int.Parse(parts[1]);

                    connectButton.Enabled = false;
                    connectButton.Text = "FINDING PORTS...";

                    // Discover the actual game port
                    int gamePort = await DiscoverGamePort(ip, queryPort);

                    if (gamePort > 0)
                    {
                        try
                        {
                            string steamUrl = $"steam://connect/{ip}:{gamePort}";
                            Process.Start(new ProcessStartInfo(steamUrl) { UseShellExecute = true });

                            ShowMessage($"Connection launched successfully!\n\n" +
                                      $"Server: {ip}:{gamePort}\n" +
                                      $"Query Port: {queryPort}\n" +
                                      $"Game Port: {gamePort} (discovered)\n\n" +
                                      $"Steam should now show the connection dialog.",
                                      "Connecting", MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage($"Found game port {gamePort} but failed to launch Steam:\n{ex.Message}",
                                      "Steam Launch Error", MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        ShowAdvancedConnectionHelp(ip, queryPort);
                    }
                }
                else
                {
                    ShowMessage("Invalid server address format. Use IP:PORT", "Error", MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Failed to connect:\n{ex.Message}", "Error", MessageBoxIcon.Error);
            }
            finally
            {
                connectButton.Enabled = true;
                connectButton.Text = "CONNECT NOW";
            }
        }

        private async Task<int> DiscoverGamePort(string ip, int queryPort)
        {
            // Comprehensive list of ports to test for ARK servers
            List<int> portsToTest = new List<int>();

            // Add query port variations
            portsToTest.Add(queryPort);
            portsToTest.Add(queryPort - 1);
            portsToTest.Add(queryPort + 1);

            // Add calculated ports based on query port
            if (queryPort >= 27015 && queryPort <= 27025)
            {
                int calculatedPort = 7777 + (queryPort - 27015);
                portsToTest.Add(calculatedPort);
            }

            // Add common ARK server ports
            portsToTest.AddRange(new int[] {
                7777, 7778, 7779, 7780, 7781, 7782, 7783, 7784, 7785,
                27000, 27001, 27002, 27003, 27004, 27005,
                28000, 28001, 28002, 28003, 28004, 28005
            });

            // Remove duplicates and sort
            portsToTest = portsToTest.Distinct().OrderBy(p => Math.Abs(p - queryPort)).ToList();

            // Test each port for connectivity
            var tasks = portsToTest.Take(15).Select(port => TestGamePort(ip, port)).ToArray();
            var results = await Task.WhenAll(tasks);

            // Return the first working port
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i])
                {
                    return portsToTest[i];
                }
            }

            return 0; // No working port found
        }

        private async Task<bool> TestGamePort(string ip, int port)
        {
            try
            {
                // Test TCP connection (ARK game servers use TCP)
                using (var tcpClient = new TcpClient())
                {
                    var connectTask = tcpClient.ConnectAsync(ip, port);
                    var completed = await Task.WhenAny(connectTask, Task.Delay(2000));

                    if (completed == connectTask && tcpClient.Connected)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                // Ignore connection failures
            }

            return false;
        }

        private void ShowAdvancedConnectionHelp(string ip, int queryPort)
        {
            Form helpForm = new Form()
            {
                Text = "Connection Troubleshooting",
                Size = new Size(650, 500),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = BackgroundColor,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false
            };

            Panel panel = new Panel()
            {
                Location = new Point(20, 20),
                Size = new Size(590, 380),
                BackColor = PanelColor
            };

            Label titleLabel = new Label()
            {
                Text = "⚠️ Could Not Find Game Port",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 165, 0),
                Location = new Point(15, 15),
                Size = new Size(560, 30)
            };

            RichTextBox infoTextBox = new RichTextBox()
            {
                Location = new Point(15, 50),
                Size = new Size(560, 200),
                BackColor = InputColor,
                ForeColor = TextColor,
                ReadOnly = true,
                Font = new Font("Segoe UI", 9),
                Text = $"Server Query: {ip}:{queryPort} ✅ (Working)\n" +
                       $"Game Connection: Unable to auto-discover ❌\n\n" +

                       $"POSSIBLE SOLUTIONS:\n\n" +

                       $"1. SERVER MAY BE PASSWORDED:\n" +
                       $"   Some servers require a password that's not shown publicly.\n\n" +

                       $"2. DIFFERENT CONNECTION METHOD:\n" +
                       $"   Try connecting through Steam Server Browser:\n" +
                       $"   Steam → View → Servers → Favorites → Add Server\n" +
                       $"   Use: {ip}:{queryPort}\n\n" +

                       $"3. MANUAL PORT TESTING:\n" +
                       $"   Common ARK ports: 7777, 7778, 7779\n" +
                       $"   Try: steam://connect/{ip}:7777\n\n" +

                       $"4. SERVER-SPECIFIC INFO:\n" +
                       $"   Check the server's website or Discord for exact connection details.\n\n" +

                       $"5. BATTLEYE/ANTI-CHEAT:\n" +
                       $"   Some servers require specific game versions or mods."
            };

            Button steamBrowserButton = new Button()
            {
                Text = "Open Steam Server Browser",
                Location = new Point(15, 260),
                Size = new Size(180, 35),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = TextColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            steamBrowserButton.Click += (s, e) => {
                try
                {
                    Process.Start(new ProcessStartInfo("steam://open/servers") { UseShellExecute = true });
                    ShowMessage($"Steam Server Browser opened!\n\nAdd this server manually:\n{ip}:{queryPort}",
                               "Steam Browser", MessageBoxIcon.Information);
                }
                catch { }
            };

            Button copyAddressButton = new Button()
            {
                Text = "Copy Server Address",
                Location = new Point(205, 260),
                Size = new Size(150, 35),
                BackColor = GreenAccent,
                ForeColor = TextColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            copyAddressButton.Click += (s, e) => {
                Clipboard.SetText($"{ip}:{queryPort}");
                ShowMessage("Server address copied to clipboard!", "Copied", MessageBoxIcon.Information);
            };

            Button tryManualButton = new Button()
            {
                Text = "Try Manual Ports",
                Location = new Point(365, 260),
                Size = new Size(130, 35),
                BackColor = Color.FromArgb(180, 0, 180),
                ForeColor = TextColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            tryManualButton.Click += (s, e) => TryManualPorts(ip, helpForm);

            Button closeButton = new Button()
            {
                Text = "Close",
                Location = new Point(520, 420),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(139, 0, 0),
                ForeColor = TextColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            closeButton.Click += (s, e) => helpForm.Close();

            panel.Controls.AddRange(new Control[] {
                titleLabel, infoTextBox, steamBrowserButton, copyAddressButton, tryManualButton
            });
            helpForm.Controls.AddRange(new Control[] { panel, closeButton });

            ApplyButtonStyle(steamBrowserButton, Color.FromArgb(0, 120, 215), Color.FromArgb(0, 140, 235));
            ApplyButtonStyle(copyAddressButton, GreenAccent, GreenHover);
            ApplyButtonStyle(tryManualButton, Color.FromArgb(180, 0, 180), Color.FromArgb(200, 0, 200));
            ApplyButtonStyle(closeButton, Color.FromArgb(139, 0, 0), Color.FromArgb(200, 0, 0));
            AddPanelBorder(panel);

            helpForm.ShowDialog();
        }

        private void TryManualPorts(string ip, Form parentForm)
        {
            parentForm.Close();

            int[] commonPorts = { 7777, 7778, 7779, 7780, 7781, 27000, 27001, 27002 };

            StringBuilder result = new StringBuilder();
            result.AppendLine("Launching manual connection attempts...\n");

            foreach (int port in commonPorts)
            {
                try
                {
                    string steamUrl = $"steam://connect/{ip}:{port}";
                    Process.Start(new ProcessStartInfo(steamUrl) { UseShellExecute = true });
                    result.AppendLine($"✓ Launched: {ip}:{port}");
                    System.Threading.Thread.Sleep(1000);
                }
                catch
                {
                    result.AppendLine($"✗ Failed to launch: {ip}:{port}");
                }
            }

            result.AppendLine("\n🎮 Check Steam for connection dialogs!");
            result.AppendLine("One of these attempts might work.");

            ShowMessage(result.ToString(), "Manual Connection Attempts", MessageBoxIcon.Information);
        }

        private void ShowConnectionHelp(string ip, int queryPort)
        {
            Form helpForm = new Form()
            {
                Text = "Connection Help",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = BackgroundColor,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false
            };

            Panel panel = new Panel()
            {
                Location = new Point(20, 20),
                Size = new Size(540, 300),
                BackColor = PanelColor
            };

            Label titleLabel = new Label()
            {
                Text = "Multiple Connection Options",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = PurpleAccent,
                Location = new Point(15, 15),
                Size = new Size(500, 25)
            };

            Label infoLabel = new Label()
            {
                Text = "ARK servers often use different ports for connection vs querying.\n" +
                       "Try these common connection methods:",
                Location = new Point(15, 45),
                Size = new Size(500, 40),
                ForeColor = TextColor,
                Font = new Font("Segoe UI", 9)
            };

            // Calculate possible game ports
            int baseGamePort = (queryPort >= 27015 && queryPort <= 27025) ?
                              7777 + (queryPort - 27015) : queryPort;

            Button directButton = new Button()
            {
                Text = $"Connect to {ip}:{baseGamePort}",
                Location = new Point(15, 90),
                Size = new Size(250, 30),
                BackColor = PurpleAccent,
                ForeColor = TextColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            directButton.Click += (s, e) => {
                try
                {
                    Process.Start(new ProcessStartInfo($"steam://connect/{ip}:{baseGamePort}") { UseShellExecute = true });
                    helpForm.Close();
                }
                catch { }
            };

            Button tryAllButton = new Button()
            {
                Text = "Try All Common Ports",
                Location = new Point(275, 90),
                Size = new Size(250, 30),
                BackColor = GreenAccent,
                ForeColor = TextColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            tryAllButton.Click += (s, e) => TryAllPorts(ip, queryPort, helpForm);

            RichTextBox portsTextBox = new RichTextBox()
            {
                Location = new Point(15, 130),
                Size = new Size(510, 120),
                BackColor = InputColor,
                ForeColor = SubTextColor,
                ReadOnly = true,
                Font = new Font("Consolas", 8),
                Text = $"Query Port: {queryPort} (confirmed working)\n\n" +
                       $"Likely Game Ports to try:\n" +
                       $"• {baseGamePort} (most common)\n" +
                       $"• {queryPort - 1} (query port - 1)\n" +
                       $"• 7777 (default ARK)\n" +
                       $"• 7778, 7779, 7780 (common alternatives)\n\n" +
                       $"Manual connection:\n" +
                       $"steam://connect/{ip}:[PORT]"
            };

            Button closeButton = new Button()
            {
                Text = "Close",
                Location = new Point(460, 340),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(139, 0, 0),
                ForeColor = TextColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            closeButton.Click += (s, e) => helpForm.Close();

            panel.Controls.AddRange(new Control[] { titleLabel, infoLabel, directButton, tryAllButton, portsTextBox });
            helpForm.Controls.AddRange(new Control[] { panel, closeButton });

            ApplyButtonStyle(directButton, PurpleAccent, PurpleHover);
            ApplyButtonStyle(tryAllButton, GreenAccent, GreenHover);
            ApplyButtonStyle(closeButton, Color.FromArgb(139, 0, 0), Color.FromArgb(200, 0, 0));
            AddPanelBorder(panel);

            helpForm.ShowDialog();
        }

        private void TryAllPorts(string ip, int queryPort, Form parentForm)
        {
            parentForm.Close();

            int[] portsToTry;
            if (queryPort >= 27015 && queryPort <= 27025)
            {
                int basePort = 7777 + (queryPort - 27015);
                portsToTry = new int[] { basePort, queryPort - 1, 7777, 7778, 7779, 7780, queryPort };
            }
            else
            {
                portsToTry = new int[] { queryPort, queryPort - 1, queryPort + 1, 7777, 7778, 7779, 7780 };
            }

            StringBuilder attempts = new StringBuilder();
            attempts.AppendLine("Attempting connections to all common ports...\n");

            foreach (int port in portsToTry.Distinct())
            {
                try
                {
                    string steamUrl = $"steam://connect/{ip}:{port}";
                    Process.Start(new ProcessStartInfo(steamUrl) { UseShellExecute = true });
                    attempts.AppendLine($"✓ Launched: {ip}:{port}");
                    System.Threading.Thread.Sleep(500); // Brief delay between launches
                }
                catch
                {
                    attempts.AppendLine($"✗ Failed: {ip}:{port}");
                }
            }

            attempts.AppendLine("\nMultiple connection attempts launched!");
            attempts.AppendLine("Check Steam for connection dialogs.");

            ShowMessage(attempts.ToString(), "Connection Attempts", MessageBoxIcon.Information);
        }

        private void CreateLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                string serverIp = serverIpTextBox.Text.Trim();
                string[] parts = serverIp.Split(':');

                if (parts.Length == 2)
                {
                    string steamUrl = $"steam://connect/{parts[0]}:{parts[1]}";
                    Clipboard.SetText(steamUrl);

                    // Create custom link display form
                    ShowLinkDialog(steamUrl);
                }
                else
                {
                    ShowMessage("Invalid server address format. Use IP:PORT", "Error", MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Failed to create link:\n{ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void ShowLinkDialog(string steamUrl)
        {
            Form linkForm = new Form()
            {
                Text = "Steam Connect Link",
                Size = new Size(600, 200),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = BackgroundColor,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false
            };

            Panel panel = new Panel()
            {
                Location = new Point(20, 20),
                Size = new Size(540, 120),
                BackColor = PanelColor
            };

            Label titleLabel = new Label()
            {
                Text = "Steam Connect Link Created",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = PurpleAccent,
                Location = new Point(20, 15),
                Size = new Size(500, 25)
            };

            TextBox linkTextBox = new TextBox()
            {
                Text = steamUrl,
                Location = new Point(20, 45),
                Size = new Size(500, 25),
                ReadOnly = true,
                BackColor = InputColor,
                ForeColor = TextColor,
                Font = new Font("Consolas", 9)
            };

            Label infoLabel = new Label()
            {
                Text = "✅ Link copied to clipboard! Share this link with others to let them connect directly.",
                Location = new Point(20, 75),
                Size = new Size(500, 40),
                ForeColor = GreenAccent,
                Font = new Font("Segoe UI", 9)
            };

            Button okButton = new Button()
            {
                Text = "OK",
                Location = new Point(460, 150),
                Size = new Size(100, 30),
                BackColor = PurpleAccent,
                ForeColor = TextColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            okButton.Click += (s, e) => linkForm.Close();

            panel.Controls.AddRange(new Control[] { titleLabel, linkTextBox, infoLabel });
            linkForm.Controls.AddRange(new Control[] { panel, okButton });

            AddPanelBorder(panel);
            ApplyButtonStyle(okButton, PurpleAccent, PurpleHover);

            linkForm.ShowDialog();
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        private void AddToSteamFavorites(string serverAddress)
        {
            string[] parts = serverAddress.Split(':');
            if (parts.Length != 2 || !int.TryParse(parts[1], out int port))
                throw new ArgumentException("Invalid server address format. Use IP:PORT");

            string ip = parts[0];

            try
            {
                using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Valve\Steam\apps\346110\Favorites"))
                {
                    if (key != null)
                    {
                        string serverEntry = $"{ip}:{port}";
                        string[] valueNames = key.GetValueNames();

                        // Check if server already exists
                        foreach (string valueName in valueNames)
                        {
                            if (key.GetValue(valueName)?.ToString() == serverEntry)
                            {
                                throw new InvalidOperationException("Server is already in favorites!");
                            }
                        }

                        int nextIndex = valueNames.Length;
                        key.SetValue(nextIndex.ToString(), serverEntry);
                    }
                    else
                    {
                        throw new InvalidOperationException("Unable to access Steam registry. Make sure Steam is installed.");
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw new InvalidOperationException("Access denied to registry. Try running as administrator.");
            }
        }
    }

    public class ArkServerInfo
    {
        public string Name { get; set; }
        public string Map { get; set; }
        public string Players { get; set; }
        public string Ping { get; set; }
        public string Version { get; set; }
        public bool IsOnline { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
    }
}