
# AI4U for Unity

This is the Unity version of the AI4U Agent Abstraction Framework (AI4UAAF). AI4UAAF provides a alternative approach to modeling Non-Player Characters (NPC).

Agent abstraction defines an agent living in a environment and interacting with this environment by means of sensors and actuators. So, NPC specification is a kind of agent specification. Agent's components are: sensors, actuators, events, reward functions and brain. Sensors and actuators are the interface between agents and environments. A sensor provides data to an agent's brain, while actuators send actions from agent to environment. A brain is a script that proccessing sensors' data e made a decision (selects an action by time).

We map components of the Unity architecture to agents components. So, agents components are stored as prefabs. A prefab is made component of game objects. A Game Object is a visible or not visible element of the game. Furthermore, the visible elements are statics or dynamics. Shortly, we will publish a full article on this relationship between Unity and agent abstraction. 

# Setup

AI4UAAF is a Unity package that can be installed locally. Create a new 3D project in Unity. From the Window menu, select the "Package Manager" option. In the Package Manager window, click on the "add package from disk" option. The following image shows the entire process of installing the package and opening a ready-to-train scene.

<video src='https://github.com/gilcoder/AI4UUE/blob/main/doc/img/package_setup.webm.mov?raw=true' />