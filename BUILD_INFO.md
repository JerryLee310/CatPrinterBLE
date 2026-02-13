# Build Information / 构建信息

## Pre-compiled GUI Build / 预编译 GUI 版本

This repository now includes a pre-compiled, ready-to-run Windows executable for the Cat Printer GUI application.

本仓库现在包含 Cat Printer GUI 应用程序的预编译、即用型 Windows 可执行文件。

### Location / 位置
- **Folder**: `Releases/`
- **Main Executable**: `CatPrinterGUI.exe`

### Build Details / 构建详情

- **Build Configuration**: Release
- **Target Platform**: Windows x64 (win-x64)
- **Build Type**: Self-contained
- **Single File**: Yes (with compression)
- **.NET Version**: .NET 8.0
- **Target Framework**: net8.0-windows10.0.19041.0

### How It Was Built / 构建方式

The executable was built using the following command:

使用以下命令构建可执行文件：

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -p:EnableWindowsTargeting=true
```

### Files Included / 包含的文件

The `Releases` folder contains:

1. **CatPrinterGUI.exe** (72 MB) - Main application executable with embedded .NET runtime
2. **D3DCompiler_47_cor3.dll** (4.7 MB) - DirectX shader compiler
3. **PenImc_cor3.dll** (155 KB) - Pen input manager component
4. **PresentationNative_cor3.dll** (1.2 MB) - WPF presentation layer
5. **vcruntime140_cor3.dll** (122 KB) - Visual C++ runtime
6. **wpfgfx_cor3.dll** (1.9 MB) - WPF graphics library
7. **README.md** - Detailed instructions in English and Chinese

Total size: ~80 MB

### Rebuilding / 重新构建

To rebuild the executable yourself:

若要自己重新构建可执行文件：

1. Ensure you have .NET 8.0 SDK installed
2. Navigate to the `CatPrinterGUI` folder
3. Run the publish command shown above
4. The output will be in `bin/Release/net8.0-windows10.0.19041.0/win-x64/publish/`

### Code Changes / 代码更改

The following fix was applied to make the build work:

应用了以下修复使构建正常工作：

- **File**: `CatPrinterGUI/App.xaml.cs`
- **Change**: Added `using System;` directive to resolve `AppDomain` reference

### System Requirements / 系统要求

- **OS**: Windows 10 version 19041 or higher
- **Architecture**: 64-bit (x64)
- **Bluetooth**: BLE-compatible adapter
- **Hardware**: MXW01 Cat Thermal Printer

### Running the Application / 运行应用程序

Simply double-click `Releases/CatPrinterGUI.exe` - no installation or .NET runtime needed!

只需双击 `Releases/CatPrinterGUI.exe` - 无需安装或 .NET 运行时！

---

**Build Date**: February 13, 2026
**Build Environment**: .NET SDK 8.0.418
