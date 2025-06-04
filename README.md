# BitCheckerApp – Device Architecture and ABI Inspector

## What This App Does

**BitCheckerApp** is a .NET MAUI application that:

* Detects whether the current device is running a **32-bit** or **64-bit** architecture.
* On **Android**, lists the **supported ABIs (Application Binary Interfaces)** reported by the operating system.
* On **iOS** and **Windows**, falls back to displaying the current runtime architecture, with platform-specific considerations.

---

## What Are "Bitness" and "ABI"?

### Bitness (32-bit vs. 64-bit)

* This refers to how a device's processor and operating system handle memory addresses.
* **32-bit** systems can address up to \~4GB of memory and use 32-bit wide registers.
* **64-bit** systems can handle vastly larger amounts of memory and offer better performance in most modern apps.

### ABI (Application Binary Interface)

* Defines how compiled native code interfaces with the system at runtime.
* On Android, apps are often compiled for specific ABIs such as:

  * `armeabi-v7a` (32-bit ARM)
  * `arm64-v8a` (64-bit ARM)
  * `x86`, `x86_64` (Intel/AMD)
* Devices may support **multiple ABIs** and select the appropriate one at install time.

---

## Platform Behavior

### **Android**

* The app uses `Android.OS.Build.SupportedAbis` to get a list of ABIs the device supports.
* It also uses `.NET's RuntimeInformation.ProcessArchitecture` to determine 32-bit vs. 64-bit.
* Example Output:

  ```
  Supported ABIs: arm64-v8a, armeabi-v7a
  Device is running: 64-bit (Arm64)
  ```

---

### **iOS**

* Apple **dropped 32-bit app support starting with iOS 11 (released in 2017)**.

* Modern iPhones (since iPhone 5s, 2013) use **64-bit ARM processors (Arm64)**.

* Apple does **not expose ABI information** like Android. You **cannot access supported ABI lists**.

* The app uses `.NET's RuntimeInformation.ProcessArchitecture` to detect architecture.

* Example Output:

  ```
  Device is running: 64-bit (Arm64)
  ```

---

### **Windows**

* Windows systems don’t expose an ABI list like Android.
* Typically a PC runs either a **64-bit** or **32-bit** version of Windows, depending on CPU and OS installation.
* The app uses:

  * `RuntimeInformation.ProcessArchitecture`
  * `RuntimeInformation.OSArchitecture` *(optional)*
* Example Output:

  ```
  Device is running: 64-bit (X64)
  ```

---

## Summary Table

| Platform | Bitness Detection | ABI List | Notes                                  |
| -------- | ----------------- | -------- | -------------------------------------- |
| Android  | Yes             | Yes    | Uses `Build.SupportedAbis`             |
| iOS      | Yes             | No     | 64-bit only since iOS 11               |
| Windows  | Yes             | No     | ABI concept doesn't apply the same way |

---

## Files to Look At

* `MainPage.xaml`: UI with label and button
* `MainPage.xaml.cs`: Handles logic to detect architecture and ABIs per platform using `#if` directives
