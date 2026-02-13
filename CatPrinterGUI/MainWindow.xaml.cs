using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;
using static CatPrinterGUI.CatPrinter;
using static CatPrinterGUI.ImageProcessor;

namespace CatPrinterGUI;

public partial class MainWindow : Window
{
    private CatPrinter? _printer;
    private bool _isConnected;
    private string? _selectedImagePath;
    private readonly DispatcherTimer _logClearTimer;

    public MainWindow()
    {
        InitializeComponent();
        
        _logClearTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(3)
        };
        _logClearTimer.Tick += (s, e) =>
        {
            _logClearTimer.Stop();
        };
        
        LogMessage("Application started. Please connect to your MXW01 printer.", LogType.Info);
    }

    private enum LogType
    {
        Info,
        Success,
        Warning,
        Error
    }

    private void LogMessage(string message, LogType type = LogType.Info)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        string prefix = type switch
        {
            LogType.Success => "✅",
            LogType.Warning => "⚠️",
            LogType.Error => "❌",
            _ => "ℹ️"
        };
        
        string logLine = $"[{timestamp}] {prefix} {message}\n";
        
        Dispatcher.Invoke(() =>
        {
            LogTextBox.AppendText(logLine);
            if (AutoScrollCheckBox.IsChecked == true)
            {
                LogTextBox.ScrollToEnd();
            }
        });
    }

    private void UpdateConnectionStatus(bool connected)
    {
        _isConnected = connected;
        Dispatcher.Invoke(() =>
        {
            if (connected)
            {
                StatusIndicator.Fill = new SolidColorBrush(Color.FromRgb(76, 175, 80)); // Green
                StatusText.Text = "Connected";
                ConnectButton.IsEnabled = false;
                DisconnectButton.IsEnabled = true;
                
                // Enable printer controls
                PrintButton.IsEnabled = true;
                EjectButton.IsEnabled = true;
                RetractButton.IsEnabled = true;
                BatteryButton.IsEnabled = true;
                StatusButton.IsEnabled = true;
                InfoButton.IsEnabled = true;
                PrintTypeButton.IsEnabled = true;
            }
            else
            {
                StatusIndicator.Fill = new SolidColorBrush(Color.FromRgb(244, 67, 54)); // Red
                StatusText.Text = "Disconnected";
                ConnectButton.IsEnabled = true;
                DisconnectButton.IsEnabled = false;
                
                // Disable printer controls
                PrintButton.IsEnabled = false;
                EjectButton.IsEnabled = false;
                RetractButton.IsEnabled = false;
                BatteryButton.IsEnabled = false;
                StatusButton.IsEnabled = false;
                InfoButton.IsEnabled = false;
                PrintTypeButton.IsEnabled = false;
            }
        });
    }

    private async void ConnectButton_Click(object sender, RoutedEventArgs e)
    {
        ConnectButton.IsEnabled = false;
        ConnectButton.Content = "🔄 Scanning...";
        LogMessage("Starting Bluetooth scan for MXW01 printer...", LogType.Info);

        try
        {
            _printer = new CatPrinter();
            bool success = await _printer.ConnectAsync();
            
            if (success)
            {
                UpdateConnectionStatus(true);
                LogMessage("Successfully connected to printer!", LogType.Success);
            }
            else
            {
                LogMessage("Failed to connect. Make sure the printer is powered on and in range.", LogType.Error);
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Connection error: {ex.Message}", LogType.Error);
        }
        finally
        {
            ConnectButton.Content = "🔍 Scan & Connect";
            if (!_isConnected)
            {
                ConnectButton.IsEnabled = true;
            }
        }
    }

    private async void DisconnectButton_Click(object sender, RoutedEventArgs e)
    {
        if (_printer != null)
        {
            await _printer.DisposeAsync();
            _printer = null;
        }
        
        UpdateConnectionStatus(false);
        LogMessage("Disconnected from printer.", LogType.Info);
    }

    private void BrowseImage_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new OpenFileDialog
        {
            Title = "Select Image to Print",
            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*",
            CheckFileExists = true
        };

        if (dialog.ShowDialog() == true)
        {
            _selectedImagePath = dialog.FileName;
            ImagePathTextBox.Text = _selectedImagePath;
            LogMessage($"Selected image: {Path.GetFileName(_selectedImagePath)}", LogType.Info);
        }
    }

    private async void PrintButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedImagePath))
        {
            MessageBox.Show("Please select an image first.", "No Image Selected", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!File.Exists(_selectedImagePath))
        {
            MessageBox.Show("The selected image file does not exist.", "Invalid Image", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        PrintButton.IsEnabled = false;
        PrintButton.Content = "🖨️ Printing...";
        
        byte intensity = (byte)IntensitySlider.Value;
        
        PrintModes printMode = PrintModeComboBox.SelectedIndex == 0 
            ? PrintModes.Monochrome 
            : PrintModes.Grayscale;
        
        DitheringMethods ditheringMethod = (DitheringMethods)DitheringComboBox.SelectedIndex;

        LogMessage($"Starting print job...", LogType.Info);
        LogMessage($"  Intensity: {intensity}%", LogType.Info);
        LogMessage($"  Mode: {(printMode == PrintModes.Monochrome ? "1bpp Monochrome" : "4bpp Grayscale")}", LogType.Info);
        LogMessage($"  Dithering: {ditheringMethod}", LogType.Info);

        try
        {
            await _printer!.Print(_selectedImagePath, intensity, printMode, ditheringMethod);
            LogMessage("Print job completed!", LogType.Success);
        }
        catch (Exception ex)
        {
            LogMessage($"Print error: {ex.Message}", LogType.Error);
        }
        finally
        {
            PrintButton.Content = "🖨️ Print Image";
            PrintButton.IsEnabled = true;
        }
    }

    private async void EjectPaper_Click(object sender, RoutedEventArgs e)
    {
        if (!ushort.TryParse(LineCountTextBox.Text, out ushort lineCount))
        {
            MessageBox.Show("Please enter a valid number of lines.", "Invalid Input", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        EjectButton.IsEnabled = false;
        LogMessage($"Ejecting {lineCount} lines of paper...", LogType.Info);

        try
        {
            await _printer!.EjectPaper(lineCount);
            LogMessage("Paper ejected successfully!", LogType.Success);
        }
        catch (Exception ex)
        {
            LogMessage($"Error ejecting paper: {ex.Message}", LogType.Error);
        }
        finally
        {
            EjectButton.IsEnabled = true;
        }
    }

    private async void RetractPaper_Click(object sender, RoutedEventArgs e)
    {
        if (!ushort.TryParse(LineCountTextBox.Text, out ushort lineCount))
        {
            MessageBox.Show("Please enter a valid number of lines.", "Invalid Input", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        RetractButton.IsEnabled = false;
        LogMessage($"Retracting {lineCount} lines of paper...", LogType.Info);

        try
        {
            await _printer!.RetractPaper(lineCount);
            LogMessage("Paper retracted successfully!", LogType.Success);
        }
        catch (Exception ex)
        {
            LogMessage($"Error retracting paper: {ex.Message}", LogType.Error);
        }
        finally
        {
            RetractButton.IsEnabled = true;
        }
    }

    private async void BatteryLevel_Click(object sender, RoutedEventArgs e)
    {
        BatteryButton.IsEnabled = false;
        LogMessage("Querying battery level...", LogType.Info);

        try
        {
            await _printer!.GetBatteryLevelAsync();
        }
        catch (Exception ex)
        {
            LogMessage($"Error: {ex.Message}", LogType.Error);
        }
        finally
        {
            BatteryButton.IsEnabled = true;
        }
    }

    private async void PrinterStatus_Click(object sender, RoutedEventArgs e)
    {
        StatusButton.IsEnabled = false;
        LogMessage("Querying printer status...", LogType.Info);

        try
        {
            await _printer!.GetPrinterStatusAsync();
        }
        catch (Exception ex)
        {
            LogMessage($"Error: {ex.Message}", LogType.Error);
        }
        finally
        {
            StatusButton.IsEnabled = true;
        }
    }

    private async void DeviceInfo_Click(object sender, RoutedEventArgs e)
    {
        InfoButton.IsEnabled = false;
        LogMessage("Querying device information...", LogType.Info);

        try
        {
            await _printer!.PrintDeviceInfoAsync();
        }
        catch (Exception ex)
        {
            LogMessage($"Error: {ex.Message}", LogType.Error);
        }
        finally
        {
            InfoButton.IsEnabled = true;
        }
    }

    private async void PrintType_Click(object sender, RoutedEventArgs e)
    {
        PrintTypeButton.IsEnabled = false;
        LogMessage("Querying print type...", LogType.Info);

        try
        {
            await _printer!.GetPrintType();
        }
        catch (Exception ex)
        {
            LogMessage($"Error: {ex.Message}", LogType.Error);
        }
        finally
        {
            PrintTypeButton.IsEnabled = true;
        }
    }

    private void ClearLog_Click(object sender, RoutedEventArgs e)
    {
        LogTextBox.Clear();
    }

    protected override async void OnClosed(EventArgs e)
    {
        if (_printer != null)
        {
            await _printer.DisposeAsync();
        }
        base.OnClosed(e);
    }
}
