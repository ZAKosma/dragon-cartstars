# **Racing Game Project**

## **Project Overview**
This project is a racing game featuring player-controlled and AI-controlled vehicles. It includes checkpoint-based racing, a customizable vehicle upgrade system, and dynamic AI behavior. Players can participate in circuit and sprint races, manage vehicle upgrades in a garage, and explore an open world.

---

## **Features**
### **Core Gameplay**
- **Player-Controlled Vehicles**: Fully interactive vehicles with upgrades and nitro boosts.
- **AI Vehicles**:
  - **Racer AI**: Competes in races by following checkpoints.
  - **Wanderer AI**: Randomly navigates an open world.
- **Checkpoint System**: Highlights the next checkpoint, upcoming checkpoint, and the race's finish line.
- **Race Modes**:
  - **Circuit**: Multiple laps with checkpoints.
  - **Sprint/Drag**: Start-to-finish races with a single lap.
- **Garage System**: Equip and manage vehicle upgrades.

### **Vehicle Upgrades**
- Modular upgrades for speed, nitro boosts, and more.
- Shared upgrade system for both player and AI vehicles.
- Upgrade slots with configurable types and limits.

### **Camera System**
- Smooth follow camera for player vehicles.
- Configurable offset, look-ahead, and smoothness.

---

## **Folder Structure**
```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── Vehicle.cs           # Base vehicle functionality
│   │   ├── PlayerVehicleController.cs  # Handles player input for vehicles
│   │   ├── AIVehicleController.cs      # Handles AI behavior for vehicles
│   │   ├── VehicleUpgradeSystem.cs     # Manages vehicle upgrades
│   ├── Race/
│   │   ├── Race.cs              # Race definition (type, checkpoints, laps)
│   │   ├── CheckPoint.cs        # Individual checkpoint logic
│   │   ├── CheckpointManager.cs # Handles race progression and checkpoint updates
│   ├── UI/
│   │   ├── Garage.cs            # Manages vehicle upgrades and inventory in the garage
│   │   ├── RacePoint.cs         # Race initiation and UI interaction
│   │   ├── CameraController.cs  # Smooth follow camera
├── Prefabs/
│   ├── Vehicle.prefab           # Base vehicle prefab
│   ├── CheckPoint.prefab        # Checkpoint prefab
│   ├── GarageUI.prefab          # Garage interface prefab
├── Materials/
│   ├── CheckpointDefault.mat    # Default checkpoint material
│   ├── CheckpointNext.mat       # Highlighted next checkpoint
│   ├── CheckpointUpcoming.mat   # Upcoming checkpoint material
│   ├── Flag.mat                 # Finish line material
└── Resources/
    ├── Upgrades/
        ├── SpeedUpgrade.asset   # ScriptableObject for speed upgrades
        ├── NitroUpgrade.asset   # ScriptableObject for nitro upgrades
```

---

## **Getting Started**
### Prerequisites
- Unity 2021.3 LTS or later
- TextMeshPro (pre-installed with Unity)

### Setting Up the Project
1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/racing-game.git
   cd racing-game
   ```
2. Open the project in Unity.
3. Ensure all necessary assets are installed (e.g., TextMeshPro).

---

## **How to Play**
### **Player Vehicle**
- Use `W/A/S/D` or `Arrow Keys` to control the vehicle.
- Use `Space` to apply the handbrake.
- Use `Left Shift` to activate nitro.

### **AI Vehicles**
- Racer AI will automatically follow checkpoints during races.
- Wanderer AI will navigate random waypoints.

### **Garage**
- Access the garage to equip, unequip, and manage vehicle upgrades.
- Vehicle upgrades improve speed, nitro, and handling.

### **Races**
1. Interact with a race point to view race details.
2. Start the race to be moved to the starting position.
3. Complete checkpoints and reach the finish line to win.

---

## **Key Scripts**
### **Vehicle.cs**
Handles core vehicle functionality, including movement, upgrades, and physics.

### **PlayerVehicleController.cs**
Processes player inputs for controlling the vehicle.

### **AIVehicleController.cs**
Handles AI logic for both racer and wanderer modes.

### **CheckpointManager.cs**
Manages checkpoint progression, highlighting, and race completion.

### **Garage.cs**
Enables inventory and upgrade management for vehicles.

---

## **Development Tasks**
### **Future Features**
- Implement multiplayer racing.
- Add environmental obstacles (e.g., traffic, weather).
- Expand vehicle customization with cosmetics.
- Create additional AI behavior modes.

### **Bug Fixes**
- Optimize AI navigation for complex track layouts.
- Fine-tune camera smoothness during fast-paced movements.

---

## **Contributors**
- **Your Name**: Lead Developer
- **Team Member 1**: AI and Checkpoint Logic
- **Team Member 2**: Vehicle Physics and Upgrades
- **Team Member 3**: UI/UX and Garage System

---

## **License**
This project is licensed under the MIT License. See `LICENSE` for details.

---

This README provides a complete overview of the project for developers, contributors, and users. Let me know if you need to expand it further!