# Cat Printer GUI - Windows Executable

## Quick Start (快速开始)

Simply double-click `CatPrinterGUI.exe` to run the application!

只需双击 `CatPrinterGUI.exe` 即可运行应用程序！

## What's Included (包含内容)

This folder contains a **self-contained** build of the Cat Printer GUI application for Windows 64-bit systems. All necessary dependencies are included - no .NET runtime installation required!

本文件夹包含 Cat Printer GUI 应用程序的 Windows 64 位**独立版本**。所有必需的依赖项都已包含 - 无需安装 .NET 运行时！

### Files (文件说明)

- **CatPrinterGUI.exe** - Main application executable (主应用程序可执行文件)
- **CatPrinterGUI.pdb** - Debug symbols (optional, for troubleshooting) (调试符号文件，可选，用于故障排除)
- **D3DCompiler_47_cor3.dll** - DirectX shader compiler (DirectX 着色器编译器)
- **PenImc_cor3.dll** - Pen input manager component (笔输入管理器组件)
- **PresentationNative_cor3.dll** - WPF presentation layer (WPF 表示层)
- **vcruntime140_cor3.dll** - Visual C++ runtime (Visual C++ 运行库)
- **wpfgfx_cor3.dll** - WPF graphics library (WPF 图形库)

## System Requirements (系统要求)

- **Operating System**: Windows 10 version 19041 or higher (操作系统：Windows 10 版本 19041 或更高)
- **Architecture**: 64-bit (x64) (架构：64 位)
- **Bluetooth**: Bluetooth Low Energy (BLE) adapter (蓝牙：蓝牙低功耗适配器)
- **Hardware**: MXW01 Cat Thermal Printer (硬件：MXW01 猫咪热敏打印机)

## How to Use (使用方法)

1. **Turn on your printer** - Make sure your MXW01 printer is powered on (打开打印机 - 确保您的 MXW01 打印机已开机)
2. **Run the application** - Double-click CatPrinterGUI.exe (运行应用程序 - 双击 CatPrinterGUI.exe)
3. **Connect** - Click the "🔍 Scan & Connect" button in the application (连接 - 点击应用程序中的"🔍 Scan & Connect"按钮)
4. **Print** - Select an image, adjust settings, and click "🖨️ Print Image" (打印 - 选择图像，调整设置，然后点击"🖨️ Print Image")

## Features (功能特性)

- ✨ Modern Material Design UI (现代化 Material Design 界面)
- 🔗 One-click printer connection (一键连接打印机)
- 🖼️ Support for multiple image formats (支持多种图像格式)
- ⚙️ Adjustable print intensity and quality (可调节打印强度和质量)
- 📄 Paper feed and retract controls (纸张进纸和回缩控制)
- 📊 Real-time status and logging (实时状态和日志记录)
- 🔋 Battery level monitoring (电池电量监控)

## Troubleshooting (故障排除)

### Application won't start (应用程序无法启动)
- Make sure you're running Windows 10 version 19041 or higher (确保您运行的是 Windows 10 版本 19041 或更高)
- Check if your antivirus is blocking the application (检查您的杀毒软件是否阻止了该应用程序)

### Can't find the printer (找不到打印机)
- Ensure the printer is turned on and has sufficient battery (确保打印机已开机且电量充足)
- Check that Bluetooth is enabled on your computer (检查您的计算机上是否已启用蓝牙)
- Make sure your computer has a BLE-compatible Bluetooth adapter (确保您的计算机有 BLE 兼容的蓝牙适配器)

### Print quality issues (打印质量问题)
- Try adjusting the intensity slider (尝试调整强度滑块)
- Experiment with different dithering algorithms (尝试不同的抖动算法)
- Use 4bpp mode for better quality (slower) (使用 4bpp 模式获得更好的质量，但速度较慢)

## File Size Note (文件大小说明)

The executable is approximately 72 MB because it includes the entire .NET runtime and all dependencies. This allows the application to run on any compatible Windows system without requiring users to install .NET separately.

可执行文件大约 72 MB，因为它包含了完整的 .NET 运行时和所有依赖项。这使得应用程序可以在任何兼容的 Windows 系统上运行，而无需用户单独安装 .NET。

## License (许可证)

This application inherits the license from the main CatPrinterBLE project. See the LICENSE file in the project root for details.

此应用程序继承了主 CatPrinterBLE 项目的许可证。有关详细信息，请参阅项目根目录中的 LICENSE 文件。

---

For more information, visit the main project documentation or check the CatPrinterGUI folder for source code.

有关更多信息，请访问主项目文档或查看 CatPrinterGUI 文件夹中的源代码。
