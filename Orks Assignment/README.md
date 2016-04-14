<a name="HOLTop"></a>
# Introduction to Unity #

----------------

<a name="Overview"></a>
## Overview ##

Unity is a major player in the cross platform game engine space with over 21 supported platforms, including Windows 10, MacOS, Linux, Android and iOS, all of which can use the same code base. In this workshop you will build "Orks with Forks (and Knives)" - a fun top down 2D game. Keep in mind Unity is not a 3D asset creation system, but instead a system in which you can arrange your assets, write code to animate, use physics, audio, and more. There are other software packages such as Autodesk Maya or Blender that can be used to 3D model, although Unity does have a built in terrain modelling system.

![alt](/Images/Orks Screenshot.PNG)

<a name="Objectives"></a>
### Objectives ###

In this module, you will see how to:

- Use Unity's Editor to design a 2D level with sprites
- Handle User Input
- Work with the Camera
- Handle Input from Keyboard
- Move the player
- Setup an animation
- Test your game

<a name="Prerequisites"></a>
### Prerequisites ###

The following is required to complete this module:

- [Visual Studio Community 2015][1] or greater.
- [Unity 5.3][2] or greater.

[1]: https://www.visualstudio.com/products/visual-studio-community-vs
[2]: unity3d.com

---

<a name="Exercises"></a>
## Exercises ##
This module includes the following exercise:

1.  [Getting Started : Creating your first game](#Exercise1)

Estimated time to complete this module: **60 minutes**

>**Note:** When you first start Visual Studio, you must select one of the predefined settings collections. Each predefined collection is designed to match a particular development style and determines window layouts, editor behavior, IntelliSense code snippets, and dialog box options. The procedures in this module describe the actions necessary to accomplish a given task in Visual Studio when using the **General Development Settings** collection. If you choose a different settings collection for your development environment, there may be differences in the steps that you should take into account.

<a name="Getting Started"></a>
### Getting Started : Creating your first game ###

In this exercise you will create your first Unity game. But first let's explore the Editor interface in Unity.
1. The Hierarchy window contains everything in your scene. A scene is essentially a level in your game. When the game loads, it will load the first scene in your build settings (control-shift-b) or if you are working in the Editor, the current scene will load when you play test your game.
2. The Scene tab contains your design surface for your level. Here you drag/drop objects and arrange your objects. 
3. Next is the Game window where you will see your game when test playing it. 
4. The Play bar or Tool bar allows you to play your game right in Unity without having to build externally. It also allows you to pause and run your game one frame at a time. 
5. After that is the Inspector window that has the properties of the currently selected Game Object. 
6. Finally the Project window is what contains all of the art, models, images, scripts, audio, and files (assets) that make up your project.

![The Editor](/Images/Editor.PNG "The Editor")

Unity's Asset Store allows you to buy (or get some for free) various assets related to your game. 
AssetStore

![The Asset Store](/Images/AssetStore.PNG "The Asset Store")

---

<a name="Ex1Task1"></a>
#### Task 1 - Open Project and Scene ####

To get started we need to open the starter Unity Project and find the scene file. Scenes are your levels in Unity. You will need to find your scene and open it to start working on it, just as you would have to find a web page to work on in a web project. The project may or may not load with it already open, so it is important to understand how to find them.

1. Open Unity
2. Choose "Open Project" 
3. Select the project folder on the desktop "OrksWithForks Starter"
4. Unity will re-open and load the project. 
5. When Unity opens, find the scene file /Assets/Scenes/scene01 and double click it to open it.

![Open Scene 01](/Images/Scene.PNG)

---
<a name="Ex1Task2"></a>
#### Task 2 - Run the game and modify camera settings ####

Note the Play, Pause, and Frame Advance button on the toolbar. This allows you to play your game without having to manually compile and export a build. Mono is used as the runtime for your scripts (Plus the native engine code) and Unity runs it right in the editor.
When you click play, you'll go into Play Mode. This is a temporary testing mode and most changes you make will be lost so it is important to know when you are in play mode. 

Everything in your game is visible because of a camera. There are two camera types in Unity - Orthographic and Perspective. These are simply options on a camera component. Perspective cameras see the world as we do, Orthographic cameras have no scaling with distance, which is good for 2D games. Also Orthographic camera size keeps a fixed height no matter the screen we are running the game on. The only thing that varies is how much width is visible.

![Wide View with Orthographic Camera](/Images/OrthoWide.PNG "Wide View with Orthographic Camera")
_Orthographic Camera, note the height_

![Thin View with Orthographic Camera](/Images/OrthoThin.PNG "Thin View with Orthographic Camera")

_Orthographic Camera, after changing screen width the height is still the same_


1. Click play on the toolbar and run the game.

    ![Play Bar](/Images/PlayBar.PNG "Play Bar")
1. Move across the screen to the exit. Note the camera doesn't follow.

1. When you are done ** make sure you click play again to get out of play mode. **

1. In the project window at the bottom, Navigate to /Scripts/

1. Drag and drop the CameraFollow.cs script onto the Camera Game Object in the hierarchy.  

1. Click play again and notice how the camera now follows the character.

1. Get out of play mode.

1. ** Verify you are not in play mode ** Press Control-S to save your scene. If you are in play mode you'll get an error.

1. In the hierarchy window, select the Main Camera game object. This acts as our eyes and ears. These are just components on this game object, an Audio Listener Component and a Camera Component.
![Camera Game Object](/Images/CameraSelected.PNG "Camera Game Object")

1. In the Inspector window, change the size on the orthographic camera component to customize how much vertical height the user will see no matter the device they run it on.
![Orthographic Height](/Images/CameraSize.PNG "Orthographic Height")

---
<a name="Ex1Task3"></a>
#### Task 3 - Organize the Hierarchy Window ####

The hierarchy window lists all of the game objects in the current scene. This window has been cleaned up quite a bit already but is still a bit messy. Some of the game objects should be organized better.

A game object can be a zombie, but it can also just be container for other objects. Every item in the hierarchy is a game object, which may or may not have any visible properties (like an invisibe spawn point). Let's organize this window a bit more by moving the enemies and some walls around.

1. Select the menu GameObject / Create Empty

1. Rename this new game object to "Enemies". Ensure you press Enter when done (don't just click away)
    
    ![Renaming Enemies](/Images/InspectorEnemies.PNG "Renaming Enemies")
    
1. In the Hierarchy window only, drag and drop the Orks and Goblin onto this new game object.
![Drag Enemies](/Images/DragOrks.PNG "Drag Enemies")

    _Drag enemies onto the Enemies Game Object_
1. In the Hierarchy window, locate the Environment game object. There's already a bunch of objects underneath it.

1. Next (still in the Hierarchy window), drag and drop all the 'walls' onto the "Environment" game object to clean up the view. You can control-click or click one and shift click another item, just as you would in Windows Explorer to select files. 
![Game Objects that could be organized](/Images/EditorWallCleanup.PNG "Game objects that could be organized")
1. Collapse the Environment and Enemies game objects to clean up the Hierarchy window. 
    ![Collapse Game Objects](/Images/CollapseGameObjects.PNG "Collapse Game Objects")

---
<a name="Ex1Task4"></a>
#### Task 4 - Add Level Pieces  ####

Prefabs are a concept in Unity of a 'prefabricated object'. These are game object settings, components like physics and scripts. Our walls need to have physics on them. 

The level is incomplete. The left edge has been removed. Add some level pieces from the /Prefabs folder to build out the level a bit more.

1. Use the /Sprites/Floors folder to drag/drop sprites into the Scene tab to draw out a floor plan. This folder is being used because we can simply run across an image. The case we need to handle is how we stop at a wall
    
1. Use the /Prefabs folder to drag/drop walls into the scene and draw the level. The walls are ordered by top/bottom after each center piece. Because of the top down scaling perspective view, the walls on each side of the center pieces have bricks directed in opposite directions. Enemies will move until they hit any object with a collider on it.
    
1. Now the tricky part. Vertex snapping. In order to get everything in place all night and neat we need to snap one vertex to the next closest vertex. Hold down V and mouse near the edge of a piece you want to snap to another.

    ![Vertex Snap](/Images/VertexSnap.PNG "Vertex Snap")

---    
<a name="Ex1Task5"></a>
#### Task 5 - Read basic input and move the player ####

Before we look at adding code to a more complete version of the player, let's take a look at some simple mechanics to make our player move. In Unity when we want to work with any game object we typically get a reference to a component via a GetComponent<> function call.

1. Open the scene /Scenes/SimplePlayer

1. Locate the game object "Player" in the Hierarchy window.

1. Look in the Inspector window at the components that bring this Game Object to life. We use various components in code to control an object

 * Sprite Renderer - Displays a 2D image on the screen
 * Animator - Controls the Sprite Renderer to play different images to animate this game object
 * RigidBody 2D - Give this object mass and make it under the influence of gravity
 * Circle Collider 2D - Give this object a shape the physics engine will use to simulate physics. The object will act like its a circle shape from a physics standpoint (collisions)
 
 ![Inspector Window for the Player](/Images/InspectorSimplePlayer.PNG "Inspector Window for the Player")

1. Click Play and notice that using the arrow keys we cannot control out player.

1. Get out of play mode

1. Open up /Scripts/SimplePlayer.cs and find the Start() method. Notice that here we ask Unity for this Game Object's RigidBody component by simply calling the following method. This component is how we'll eventually move our Player.

    ````C#
        _rigidBody = GetComponent<Rigidbody2D>();
    ````
1. Back in Unity's Editor, Open Edit/Project Settings/Input.  Note the Input settings for Horizontal are A/D or Left/Right arrow keys.
![Input Manager Horizontal](/Images/InputHorizontal.PNG "Input Manager Horizontal")

1. Locate the Update() method back in Visual Studio. Here we will need to read  input (left/right up/down or ASDW keys). Replace the //TODO content in the Update() method so the code is complete as per below

    ````C#
    void Update () {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }
    ````
    
1. Our input has a value of -1 to +1 for both Horizontal and Veritcal. We can use these values read in Update() (which is where keyboard input should be read) and apply those values in FixedUpdate, which is where we typically do physics operations.
Replace the //ToDo line with the method content below. This will set the velocity in meters/second of our game object. Note that we'll max out at 1 m/s in each direction because our input only goes from -1 to +1.

    ````C#
    void FixedUpdate()
    {
        //Move the actual object by setting its velocity
        _rigidBody.velocity = new Vector2(_horizontal, _vertical);
     }
    ````
1. Save your changes and go back to Unity. There will be a brief pause while Unity compiles your code automatically, no work is needed. You can view any errors in the Console window. You shouldn't have any, this is just an FYI that errors will show up here and also in Visual Studio, although performing a build in Visual Studio is _not_ necessary. Drag and Drop the SimplePlayer.cs script onto the Player game object. 
![Console Errors](/Images/ConsoleErrors.PNG "Console Errors")
  
1. Press Control-S to save your Unity scene changes and then click play to run the Unity project. Press the left/right up/down arrows or ASDW keys to move the character around.
![Play Simple PLayer](/Images/PlaySimplePlayer.PNG "Play Simple Player")

---
<a name="Ex1Task6"></a>
#### Task 6 - Allow the player to play the attack and walk animations ####

Now that we see how to apply basic movement, let's add code to allow the character to play an animation that has been setup already. This animation will be triggered by "Fire1", which according to the input system is the left control key or the first mouse button (mouse 0 - ie left click). When we asked for Input previously, we used GetAxis. To check for buttons we can use GetButtonDown (or GetButton, GetButtonUp)

![Input Manager](/Images/InputManager.PNG "Input Manager")

1. In the /Scripts/SimplePlayer.cs file, find the empty Update() method again and add the following code to it. This will run every frame and check for Fire1. It will only be true once and that's during the frame that runs and finds it held down. If the button is held down, this will not repeat, although that is possible to do with GetButton() 
    ````C#
    void Update()
    {
        
        //existing code here
        
        //Now add this code
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("Attack");
        }
    }
    ````

1. Save your code and go back to Unity. There will be a few second pause while your code is compiled by Unity.

1. Click Play to test out the changes. You should now get a walking animation when you move and an attack animation when you press the left mouse button or left control key.

---
<a name="Ex1Task7"></a>
#### Task 7 - Add a trigger to the coin to allow the play to detect it  ####

In Unity, we can detect when one object has come within range of another object in several ways, one of them being what's called a Trigger. The other is a collider. A collider will stop one object from passing through another. A trigger is a special case of a collider that allows objects to pass through but gives us the opportunity to be notified in code when object A intersects with object B. A collider/trigger is simply a predefined shape (sphere, box, capsule, etc) that the physics engine uses. It doesn't matter what an object physically looks like to determine how it behaves physically, that is determined by the collider/trigger shape.

1. Find a coin in the scene and click on it. It may help to press T to select the 2D tool.

1. Right click on the coin in the Hierarchy and choose "Select Prefab". This coin is a prefab, that's why it is blue in the Hierarchy window. This is an instance of a shared object in your project folder. If you change the shared object, you'll change every instance of that coin.
 
1. In the Project Window, ensure the coin is highlighted. Note - we're talking about the project window note not the Hierarchy window. Click the Add Component button and type in Circle.

    ![Add Circle Collider](/Images/AddCircleCollider.PNG "Add Circle Collider")

1. On the newly added component, check off IsTrigger. This will turn it from an immoveable object to something you can run through (ie - pick up)

    ![Check Off IsTrigger](/Images/IsTrigger.PNG "Check Off IsTrigger")

1. Notice that any coin you click on now has this trigger on it.

1. Open the /Scenes/scene01 and find the Player game object near the top of the hierarchy. Note that this game object has a PlayerController script which is a more completed version of the SimplePlayer script.

1. Open the /Scripts/PlayerController.cs file either by double clicking on it or double clicking on it from the Inspector for the Player game object

    ![Select Script from Inspector](/Images/SelectScriptFromInspector.PNG "Select Script from Inspector")

1. Find the OnTriggerEnter2D method. This will get called when the player runs over any objects with a 2D trigger on it, like we created for the coin.

1. Back in Unity's Editor, find any coin in the scene or the /Prefabs/coin they are all made from. In the Inspector note  that each coin has a PickupProperties.cs script on it that simply contains it's value in points. We can see this in the Editor. Since code is just another component on a game object, wee can ask Unity for these values.
![Pickup Points](/Images/PickupPoints.PNG "Pickup Points")

1. In Visual Studio, uncomment the code for the first **TODO**. This will increment our coin score when we run over a coin. We know we run over a coin because it has a tag. A tag is just text we can use to identity an object, (just like you can in Windows Forms and XAML). You can create your own tags easily and then assign them in a dropdown list for an object. Here's an image showing the coin tag that is set. Creating a new tag is just a matter of selecting "Add Tag".
Your code should now look like this
    ````C#
    if (collision.gameObject.tag == "Coin")
    {
        //Get its coin score by asking for the value of PickupProperties.CoinAmount
        //That value is visible from the Editor, it's just a public variable in PickupProperties.

        var pickupProperties = collision.gameObject.GetComponent<PickupProperties>();
        CoinUp(pickupProperties.CoinAmount);

        //TODO - destroy game object we just picked up
    }
    ```` 
    ![Coin Tag](/Images/CoinTag.PNG "Coin Tag")
 
1. In Visual Studio find the "TODO - destroy game object we just picked up and replace it with the code shown below. Our completed block of code now looks like the following. Notice how we check the tag of the object we've just intersected (triggrered) with. Each coin has a PickupProperties.cs script on it that 
    ````C#
    if (collision.gameObject.tag == "Coin")
    {
        //Get its coin score by asking for the value of PickupProperties.CoinAmount
        //That value is visible from the Editor, it's just a public variable in PickupProperties.

        var pickupProperties = collision.gameObject.GetComponent<PickupProperties>();
        CoinUp(pickupProperties.CoinAmount);

        Destroy(collision.gameObject);
    }
    ````
         
1. Save your code and go back to Unity's Editor and play your game. When you run over coins now you should see them disappear and your coin score increase.

---                
<a name="Ex1Task8"></a>
#### Task 8 - Add the Walk animation ####
Some existing animations have already been setup for our fearless hero. Animations in Unity simply control components over time via point in time snapshots called keyframes. Animations require two things - an Animator component on the game object which in turn points to an animation controller. 

The items below are
* 1) The selected play with its Components showing in the Inspector window.
* 2) The Animator component which points to 3
* 3) The Animation Controller file. This contains the sequence of animations to play. Think of it as a flow chart for animations
* 4) Each rectangle represents a different animation to play. We'll create a new one momentarily.
* 5) The arrows are called the transsitions that move between each animation. Its the properties on these transitions that define when to play each animation.

    ![Animation Overview](/Images/AnimationOverview.PNG "Animation Overview")

Let's go ahead and add a Walk animation to the player.
1. Navigate to the /Sprites/Hero/Walk folder
1. In the folder press Control-A to select all of the images
1. Drag and drop them onto the Player game object in the hierarchy. If prompted to create a file name it "Walk". This will be automatically created to contain the animation keyframes to cycle through each sprite.

![Creating the Walk Animation](/Images/CreateWalkAnimation.PNG "Creating the Walk Animation")

1. That last step creates a Walk.anim file with animation data in it. That data is visible in the Animation window (Menu item Windows/Animation) once you select the Player.
    
    ![Walk Animation File](/Images/WalkAnimationFile.PNG "Walk Animation File")
     
1. Now lets double click on the hero animation controller file. Ensure the Player is selected and double click on "Hero"
    ![Open the Hero Animation Controller](/Images/DoubleClickToOpenHeroController.PNG "CreOpen the Hero Animation Controller")
    
1. Note there's now a new Walk state in our Animation Controller. Drag it next to soldier_attack
    
    ![Drag Walk in Place Next to Soldier_Attack](/Images/DragWallk.PNG "Drag Walk in Place Next to Soldier_Attack")
    
1. Next we'll configure the options to play this animation. Right click on soldier_idle and select Make Transition. This will give you an arrow to dock on Walk.
    
    ![Dock Transition on Walk](/Images/DockTransitionOnWalk.PNG "Dock Transition on Walk")
    
1. Repeat the same process from Walk to soldier_idle and between Walk and soldier_attack so we have bidirectional arrows between every state.
    
    ![All Transitions in Place](/Images/FinalTransitionsForWalk.PNG "All Transistions in Place")
    
1. Let's create a boolean variable called "Walk" the animation system will use. When it becomes true, we'll move from idle to walk. Click the parameters tab on the top left and then click the plus sign. Select Bool and name it Walk (case sensitive)
    
    ![Creating the Walk Bool 1](/Images/AnimationParameters1.PNG "Creating the Walk Bool 1")
    ![Creating the Walk Bool 2](/Images/AnimationParameters2.PNG "Creating the Walk Bool 2")
    
1. Next we need to tell our transitions (the arrows, remember?) to use this variable to signal when to go from Idle to Walk. Click ** on the arrow line ** going ** from ** soldier_idle to walk. It will turn blue.

1. On the right hand side of the screen under Conditions click the plus sign to Add, and change it to Walk / True. We uncheck "Has Exit Time" because this animation can be interrupted at any time to go back to idle. We also choose 0 for Transition Duration because we're not blending between 2D animations during our transition  (we can't blend 2d images and have something that looks good) like we can with 3D model animations, so we typically set this to 0 for 2D animations.

    ![Creating the Idle to Walk Transition](/Images/ConfigureIdleToWalk.PNG "Creating the Idle to Walk Transition")
1. Do the exact same thing again on the arrow going from Walk to Idle, except make Walk / False. This says when we set Walk = false, go back to playing our Idle sprites.

    ![Creating the Walk to Idle Transition](/Images/ConfigureWalkToIdle.PNG "Creating the Walk to Idle Transition")
1. Configure our Walk<-->Attack. We don't need to create another parameter for this, we already have one called Attack on our parameters tab that was created for this lab. This is a trigger, which is acts like a boolean until its been used by a transition and then goes back to false. 

    Let's configure Walk->Soldier_Attack like we did Idle->Walk except we'll use the Attack condition. Click the transition from Walk->Soldier_Attack and configure the options as shown.
    
    ![Creating the Attack to Walk Transition](/Images/WalkToAttack.PNG "Creating the Attack to Walk Transition")

1. Lastly configure Attack to Walk. Pay careful attention. Set the condition to Walk / true (we only want to walk again if walk is true otherwise we'll go to idle instead). 

    We want our Attack animation to complete before we're allowed to go back to walk so we need to check off "Has Exit Time" and set Exit Time=1, which means the entire animation must play. Then change Transistion Offset to 0.
     
    ![Creating the Walk to Attack Transition](/Images/AttackToWalk.PNG "Creating the Walk to Attack Transition")

1. Our animation controller is all set to go. However we need code to set our Walk variable. Attack has been taken care of already in the code. When a user moves the horizontal or vertical input will be nonzero so we can set Walk = true if thats the case. 

    Open /Scripts/PlayerController.cs and navigate to the bottom of the Update() method. Uncomment the line that sets our Boolean parameter in the animation system and save your changes.
    ````C#
_animator.SetBool("Walk", _horizontal != 0 || _vertical != 0);
    ````
1. Go back to Unity and run your game. You should now get walk animations when you move. You may notice little glitches such as moving while attacking. Look at the code in the completed lab for handling various cases like that.  

---
<a name="Summary"></a>
## Summary ##

This lab took you through several aspects of Unity, while there's always more exciting things to learn, we've covered a good number of subject such as
* The Editor Layout
* Working with 2D Sprites
* Testing your game in the editor (Play Mode)
* Working with a trigger
* Working with Prefabs
* Destroying game objects
* Organizing game objects
* Using the animation system to create sprite based animations 

For additional learning resources, check out
- https://channel9.msdn.com/Shows/gamedev
- http://www.adamtuliper.com/2015/10/some-awesome-learning-resources-for.html
- http://unity3d.com/learn

