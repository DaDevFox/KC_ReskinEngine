Kingdoms and Castles Reskin Engine Guide | Kingdoms and Castles Modding
=
Buildings in Kingdoms and Castles don't have a standard format that can be used to generically reskin any building in the game. Becuase of this, every building has different models and specifications to be built to. This makes reskinning buildings in KC more difficult than it needs to be, because of this, We've brought a framework for people with ranging levels of skill in either coding or art, so that someone less experienced in either shouldn't have to be deterred from reskinning anything!

![Image](https://media.discordapp.net/attachments/294162953337307138/704799796325515315/Sky_Over_View.png "Amazing Models by TPunko")
*This wonderful blocky scene was made by TPunko from the Kingdoms and Castles Discord in MagicaVoxel*

# How the Framework works

Every building in the game has different specifications, so in order to create a model for any, you first have to do research and find out what kinds of models it uses, how many models it uses, and how to change those models, which is a different process for each building. This process is made even more of a headache when you consider the variuos particle systems, peasant positions, and resource stacks that must be corrected in order to make the model look good.  

This is what the Framework takes care of for you, we've already gone ahead and done the research and the coding so you can simply put in your models with little effort.

To reskin a building through the framework, first a model, or multiple models, depending on the buliding, must be made to be used in the reskinning, those models will then be processed by the framework and injected into the building, doing its best to preserve its tree structure so animations (yes, some buildings have animations), funcitonality, and certain visual elements can stay unbroken. 
The Framework also takes into account your various collections, their compatability with other collections, and it also does it's best to allow multiple reskins to run at the same time. 

# Collections and Compatibility
|| Subject to rework


When you create your reskin through the Framework, you must give it a *Collection*. A *Collection* is a set of building reskins that are used together, usually collections are themed, like a Halloween-themed collection, or a Christmas one. 

When you add buildings to a collection, the framework will reskin them as usual, and if there is more than one reskin for the same building, it will randomize the reskin for that building each time it is placed, allowing for visual variety. 

This all works within the same collection, but what if you want a set of collections that all have an overarching theme, and should work together? For that you have to now consider *Compatability*. When you create a collection, part of the information you must fill out is a *Compatability Identifier*. This is an ID that the Framework uses to determine which collections can mix and match with each other, and which shouldn't. 

Usually when you give a Compatability Identifier it's the mod name, or if you want a themed modpack, some kind of name such as "My Wonderful Modpack". 

> A mod does not have to be limited to only one collection, however I recomend that you only put one collection in every mod so that anybody who want's one of the collections but not the other can subscribe to them seperately.  


# How to use the **Skin Index**
|| Subject to rework

The **Skin Index** is organized into entries of information which usually look like this for buildings, sometimes with a little bit of extra information. 
```
-- BuildingName --
Name: ...
UniqueName: ...
Models: ...
```
Every entry in the Skin Index corresponds to a building in Kingdoms and Castles.
Entries usually contain multiple elements, which can include:

`[Not Supported]`: If this is present, it means the building is not supported by the Framework 

`Name`: The friendly name shown in the build menu, what you likely know it by *

`UniqueName`: The name of the building used in many places in code *

`Jobs`: If present, the number of jobs the building provides, if not, the building provides 0 jobs

`Models`: A list of the different models the building incorporates and their types and descriptions

`Anchors`: A list of GameObjects whose positions are used in certain animations

`[Dynamic Stacks]`: If this is present, it means Resource Stacks can be added or removed from the building

`Stacks`: A list of all the Stacks a building uses

`ResourceStacks`: A list of all the ResourceStacks a building uses


*Items with a * are always present*



# Step by Step Guide
## 0 | Prerequisites
1. The framework must be used through a mod, using code, mods are written in the language C# (.NET Framework 4.5), using the UnityEngine libraries. A tutorial for getting setup making mods is covered in the basic modding tutorial [here](https://docs.google.com/document/d/1hRrV92_n9zuYxuB-7yCaiftzFsGEsoRRkMnTbaaKRqM/edit) .
2. You will need some kind of code editor (I recommend Visual Studio 2017 or Visual Studio Code).
3. You will need the [Unity Editor](https://store.unity.com/#plans-individual). it is free if you choose Unity personal, but not for businesses or if you want the pro version. 



## 1 | Model

The first step is, somewhat obviously, to create a model to use. 

To start, pick a building to begin modelling, and look it up in the **Skin Index** section. Once you've found it, look at all the corresponding information below its name, most of it is important and could affect how you need to make your art, so you should know what it means and how it will affect you.  

### Know what to model

The first piece of information you need to look at is the *Models* section, this will tell you what and how to model for a reskin of your chosen building, below is a highlight of all the important information you'll need to understand while reading the **Skin Index** section. 

*Models*: The different 3D Models the building uses, these can be *Instance* models or *Modular* models, you will have to model differently according to the type

> **Note**: Unity is the game engine the game uses, and it is what this framework uses. If you are not familiar with Unity's structure, I recommend you read up on concepts such as GameObjects and Components, [this](https://docs.unity3d.com/520/Documentation/Manual/UnityBasics.html) is a good place to start. 


**Instance Models**:
An **Instance** model is a Unity `GameObject` that will be instanced inside of the original building, meaning that model, and all of its children will be *added* to the building, so you can add extra things to the model, like lights or doors, and even add code to make it move or do something during game. With this type of model, all you need to make sure to do is make sure that your scaling and translation is correct, and that it's in a `GameObject` form, the framework won't take raw mesh data, I.E. `.fbx`, `.obj`, or any other export format from your chosen modelling application except `.prefab` More information on this in the **To Make a Model** section.  

**Modular Models**: 
A *Modular* model isn't the same as an *Instance* model, and in some ways it's even the opposite. Certain buildings interchange their models in a different way, in which they use *only* the mesh data involved with the base GameObject, excluding it's children, and any transformations applied in the Unity Editor, including scale and rotation. This means that if you have a model that was rotated 90 degrees before being imported into Unity, and then you corrected it in the editor, it will disregard this, so all scaling and rotation must be applied *before* being imported into Unity. It will also disregard the children of your model, so this version cannot have arbitrary elements added on to it. 

These types of models are usually used on modular pieces, hence the name, such as Castle Block variations or Road variations. 

**Information Format**:
(model name) | (model type, either *Instance* or *Modular*): (model description)

Certain buildings have a very specific layout they must follow in order for an animation to not break, one example is the ballista. Shown below is a diagram of the ballista and the various pieces that need to be modelled.

![Image](https://i.ibb.co/bP4Mp58/ballista.png)

These pieces, disregarding the flag and veteran model, must be in the correct position and alignment for it to work. 

### To Make a Model

First, as talked about already, look up your building in the **Skin Index** section of this guide and gather your information for how and what to model. 

Once that is done, make the model in your chosen art program! I recommend using [Blender](https://www.blender.org/) because it's free, although I'm not much of an artist, so you can use whatever, but make sure that it can export files in a format supported by unity: [here](https://docs.unity3d.com/Manual/3D-formats.html)'s a list of all those formats

After it is modelled, import it into Unity, I recomend importing it into the [Kingdoms and Castles Toolkit](https://github.com/LionShield/Kingdoms-and-Castles-Toolkit) project so you can see how it looks in the game world and next to other buildings. 

> If you don't have the Unity Editor or the Kingdoms and Castle Toolkit, there's a guide to download both in the Modding Tutorial [here](https://modtutorial.kingdomsandcastles.com/), as well as a more detailed desription on how to import models into Unity

Once it's imported, you have to convert it into a Prefab, the mesh alone won't work, in order to do this, find your model in the Project window, and drag it into the SampleScene if your using the toolkit, or your scene of choice if you're not. You can drag it into either the Hierarchy window, or into the Scene View. Once it's in, find it in the Hierarchy, and once you do, drag it back into the Project window. 

> If using a newer version of Unity, you might be faced with dialog saying 
    `Would you like to make a new original prefab or a prefab variant?`,
    create a new original 

![Image](https://i.ibb.co/g9WDkMG/Unity-2018-4-20f1-Personal-PREVIEW-PACKAGES-IN-USE-Sample-Scene-unity-Kingdoms-and-Castles-Toolkit-master-PC-Mac-Linux-Standalone-DX11-5-8-2020-1-08-53-PM.png "Unity Editor Diagram")

> This might seem useless at first, but how Unity works is that when you drag a mesh (file in your original format, .fbx, .obj, or whatever you got after modelling it) into the scene view, it automatically converts that into a `GameObject`, but that GameObject doesn't exist anywhere other than in that specific scene, when you drag it back into the project, it gives the GameObject a file that can be shared, opened, modified, and duplicated, this is known as a `Prefab`, and is what the BuildingFramework uses to instantiate your meshes.  

Now you should have 2 files, your original model, with your original file extension, I'll say .fbx for the sake of this example, and your new Prefab version, which should have the extension .prefab

If you're using a more recent version of Unity, you should be able to double-click the prefab and open it in the relatively new **Prefab Editor**, if not, then you can just drag it into the Hierarchy ***again*** (also make sure to delete the old GameObject in the Hierarchy, we don't need it anymore)

![Image](https://i.ibb.co/59b7sqJ/Unity-2018-4-20f1-Personal-PREVIEW-PACKAGES-IN-USE-Sample-Scene-unity-Kingdoms-and-Castles-Toolkit-master-PC-Mac-Linux-Standalone-DX11-5-8-2020-3-02-58-PM.png "new Prefab View")
*The Prefab Editor in newer versions of Unity*

## 2 | Additional Setup (optional)

Lots of buildings have more than just a base model, the Market, for example has a row of ham that is hung outside the shop, and all of the houses have chimneys that emit smoke. 

![Image](https://i.ibb.co/SXXQcSV/KC-Small-Markets.png "Small Market with fish, apples, wheat and pork all arranged nicely on stands")

These effects might make sense with the original models, but they might be misplaced, or just not neccessary with the newer models, for example, in a model for a house where the chimney no longer exists, smoke would continues to spew out where the chimney used to be, or with a reskin for the market, maybe the stacks aren't on the new counter, rather where it used to be. 

The BuildingFramework allows you to change these things optionally, but note that these things do require more code than just simply changing or adding a model. 

> Important: Only ResourceStacks and Stacks are supported at present time

### Resource Stacks
|| Not currently supported

#### Important Information

> **Note**: ResourceStack and Stack are **not** the same thing, this will be explained later

ResourceStacks/Stacks have two types, *Normal*, or *Instanced*. 

***Normal*** stacks are simple resource stacks that stack resources in a specified order, usually stacking upward. These are the kind that form in stockpiles.

***Instanced*** stacks are a bit more complex, in an Instanced stack, instead of stacking each resource on top of eachother with predefined rules, it actually has a formation that it fills, an example of this would be the ham on a string outside of a Market, or the fish in a Fishmonger. 

> Know that it is possible to make a new look for an existing resource, which is how the Baker transforms wheat into bread, it doesn't actually transform anything by the game's definition, it only produces 2 wheat for every 1 wheat and 1 charcoal that it recieves, but the wheat that it sends has a different look, and thus is designed to look like bread, but it's actually wheat. 

#### Creating a Resource Stack

Now that you know some of the basic informatino regarding resource stacks, it's time to actually make one!

> **Note**: What's refered to as a 'script' here is a MonoBehaviour, a piece of code that can run on any Unity GameObject. It's also known as a component. 

ResourceStacks are organized into `GameObject`s with scripts attached to them and, in certain cases, children with models. How to organize the tree structure of the stack depends on the type.

To start, both stacks start as a base GameObject with a `ResourceStack` or `Stack` script attached. 

These scripts will work in the Unity Editor if you have them, but when you export it to the game, the objects will lose any scripts attached to them, this means that you'll have to add the scripts through code later on. 

After that, you have to add a `ResourcePrefab` component, which will again be added through code, and this component will eventually need access to a model of a resource, if you don't want to make a new model you can just use the default wheat/apple/fish/whatever resource your using, but if you want to change it, you can make your own and put it under the Resource Stack's GameObject. 

If you're making a **Normal** stack you can stop here. 

> So what do these scripts do? The Stack/ResourceStack component will create and format the resource stack, and will make the resource it stores look like it's designated ResourcePrefab. 

An **Instanced** stack is a bit more complicated, there's really only 1 new requirement, that it has a child somewhere that contains X number of children GameObjects, each being a single resources in the stack, where X is the maximum amount of resource in the stack, you can see this at play in the market's prefab, specifically for the Ham stack:

![Image](https://i.ibb.co/gm8JFmF/Unity-2018-4-20f1-Personal-PREVIEW-PACKAGES-IN-USE-Sample-Scene-unity-Kingdoms-and-Castles-Toolkit-master-PC-Mac-Linux-Standalone-DX11-5-8-2020-3-30-30-PM.png "Market Prefab")

When the stack get's filled up, it will slowly show/hide each of the corresponding resource GameObjects, allowing you to make formations with the stack. 

> **Important**: The only way to use any model or prefab in a mod, as will be explained later, is to package it into an AssetBundle. AssetBundles can hold Meshes, GameObjects, Prefabs, Materials, anything that Unity should recognize, except scripts. 
This means that any components you add to your GameObject will have to be added through **code**.

This piece will be revisited in the code, due to the fact that the components will have to be added via code and not through the editor. 

**ResourceStacks vs Stacks**
Here, it's important to know the distinction between Stacks and ResourceStacks. 

A *Stack* is a purely graphical stack of resources, like the stacks of wood you see form in stockpiles, but it doesn't have any actual functionality. A *ResourceStack* is a Stack that also has functionality and will actually be used as storage when in game. Usually, changing a ResourceStack's values will actually affect gameplay, as well as making things look different, so be careful to make sure you don't change any of the default values

#### Changing Existing Resource Stacks

This piece will maingly be covered in the **code** section, but most of it will involve changing the positions of the resource stacks or changing their settings, so try to find new places to rearrange the resource stacks for the building you're remodelling in the Editor's Scene View, and I'd recommend adding an empty GameObject just to keep track of where it is. 

To identify which buildings have a ResourceStack vs. a Stack and what kind they are you'll have to look up the corresponding information in the **Building Model Information** section.  

Usually it will be notated like this:
```
-- My Building --
Name: My Building
UniqueName: mybuilding
Models:
    lots
    of
    models
    here
    .....
[Dynamic Stacks] (if this is present, it means stacks can be added or removed, otherwise, you can only replace existing stacks)
ResourceStacks:
    ....
    here all the ResourceStack will be listed in the format below
Stacks:
    ....
    here all the Stacks will be listed in the format below
```
**Format**:
(stack name) | (stack type): (stack description) | (stack default value and resource type)

### Coming Soon

Ability to change particles

Ability to change peasant working positions

Ability to change Stacks and ResourceStacks

#  Code
This section will require at least some technical expertise in programming, if you aren't a developer and you're just here to see the barebones of how to get your models into the game, that's fine too! I'll try and keep the guide nice and simple, but I'll have some of the more technical details towards the end for you nerds out there like me :p

To begin, you'll need to have your models set and ready to go, for this I recommend reading the section on modelling and finding information about the models in the **Skin Index** section. 

Once you have that done, you'll need to export that into an AssetBundle, this process is already covered in the Kingdoms and Castles [Mod Tutorial](https://modtutorial.kingdomsandcastles.com/) so I won't go into too much detail over it. 

Once you have your AssetBundle, set up a C# project if you haven't done already. If you're using Visual Studio, which I highly recommend, start a C# .NET Framework Class Library project and make sure to set the .NET version to 4.5. 

> This too is covered in the mod tutorial, but in a nutshell you click `File >> New >> Project >> Class Library (.NET Framework) [you may have to search that]` and then fill out the information for project name etc, and hit create. 

We now need to bring 2 things into the mod:
1. The Reskin Engine API, this is a little bit of code that allows your mod to communicate with the Reskin Engine (which is a seperate mod)
2. Your models (in the form of a Unity AssetBundle)

Using the Reskin Engine API, we will tell the Reskin Engine to use your models in place of certain buildings and/or visual elements of the game

First we have to bring in the Reskin Engine API files which you can download [here](https://github.com/DaDevFox/KCReskinEngine/tree/master/API). I recommend putting those in their own folder to organize your mod structure. 

Bring the AssetBundle files into your project so that they're in the 4 folders they were built into by the KCToolkit project; `win32`, `win64`, `osx`, and `linux`, which of course correspond to the 4 major operating systems KC is released on. These also I'd recommend putting in their own folder, so you should have 1 for the API, 1 for the AssetBundle and your code will be in the mod root.

> Note that when Unity builds the AssetBundles, it puts all the AssetBundles in the entire project into those 4 folders, `win32`, `win64`, `osx`, and `linux`. Inside of those folders you should see a few files, one for the operating system, called the same thing as the folder, a .manifest and .meta file for pretty much everything, and a varying amount of files named: `NameOfAssetBundle_(some random jibberish)` not all of them are needed, the only ones you need are the jibberish files pertaining to your AssetBundle and the one called the name of the operating system, so in the `osx` folder, look for a file called `osx` and the files with the name of your AssetBundle, then go back and bring in the .manifest files for any of the files you brought into your mod. 

At this point I strongly recommend you read through the example mod in the mod tuturial (above) and have all the basic elements down. 

Now you should set up the basic parts of your mod, I have some sample code below that you can just use as a reference if you need to; I will be following along with this code for the rest of the guide. 

```cs
using ReskinEngine.API

public class MyMod : MonoBehaviour
{
    //This will be used to log messages if we need to. 
    public KCModHelper helper;
    
    //The Framework requires we register our collection in SceneLoaded
    public void SceneLoaded(KCModHelper _helper)
    {
        helper = _helper;
        
        // Here we'll write the rest of the code later
    }
}
```
Now we need to load our AssetBundle. for that we can add the following code in SceneLoaded

```cs
    public void SceneLoaded(KCModHelper _helper)
    {
        // code from before
        
        AssetBundle bundle = KCModHelper.LoadAssetBundle(_helper.modPath + /*path of AssetBundle to relative mod folder root*/, "NameOfAssetBundle");
    }
```

That should load our AssetBundle and have it ready to use, but now we need to extract our assets from it. For the sake of this example I'm gonna assume we're reskinning the Keep and we have 4 assets in our bundle, a mesh for each of the keep upgrades. 


```cs
	public void SceneLoaded(KCModHelper _helper)
	{
        // code from before

		AssetBundle bundle = KCModHelper.LoadAssetBundle(_helper.modPath + /*path of AssetBundle to relative mod folder root*/, "NameOfAssetBundle");
		
		GameObject upgrade0 = bundle.LoadAsset("Assets/AssetBundles/ExampleMod/Keep/upgrade0.prefab") as GameObject;
		GameObject upgrade1 = bundle.LoadAsset("Assets/AssetBundles/ExampleMod/Keep/upgrade1.prefab") as GameObject;
		GameObject upgrade2 = bundle.LoadAsset("Assets/AssetBundles/ExampleMod/Keep/upgrade2.prefab") as GameObject;
		GameObject upgrade3 = bundle.LoadAsset("Assets/AssetBundles/ExampleMod/Keep/upgrade3.prefab") as GameObject;
	}
```

That code will take our 4 assets and store them in variables. 

Up until this point, all of this code has been covered in the KC Mod Tutorial, so if you're having trouble understanding something, I highly recommend you go back and review the tutorial's explanation also, but now we're gonna get into the specifics of how the Framework's code works. 

The first thing you need to know is that each skin has a class that extends `Skin`, these are just classes with variables regarding the specifics of each skin, basically a data container that gets passed to the Engine to execute the skin. Certain skins inherit from a class called `BuildingSkin` and are usually called `xxBuildingSkin`, these are skins specifically for buildings. `KeepBuildingSkin` is a skin for the Keep, `HospitalBuildingSkin` is a skin for the Hospital, and so on. 

Each of these classes have variables that you can modify, for the keep they are:

`keepUpgrade1`
`keepUpgrade2`
`keepUpgrade3`
`keepUpgrade4`

`banner1`
`banner2`

each of those correspond to an element of the model of the Keep, as you can imagine, Keep upgrades 1-4 are the different looks for the Keep as it gets upgraded, and the 2 banners are the banners that swing in the wind above the Keep. 

Every entry in the **Skin Index** section that's supported has a corresponding `BuildingSkin` in code. 

in order to make a BuildingSkin, all they have to do is create an instance of it: 

```cs
	KeepBuildingSkin keepSkin = new KeepBuildingSkin();
	keepSkin.keepUpgrade1 = upgrade0;
	keepSkin.keepUpgrade2 = upgrade1;
	keepSkin.keepUpgrade3 = upgrade2;
	keepSkin.keepUpgrade4 = upgrade3;
```

If fields are left `null`, they won't be reskinned, in this case we don't have a skin for the banners, so they will be left alone by the engine. 

So now we have our skin, but we still need to register it so it gets proccessed. 

In order to register skins to the framework, they need to be added to a `ReskinProfile` this is just a collection of skins that will be assigned a *Collection* and *Compatability Identifier* (see **Collections and Compatibility** for more info) so that the framework knows where the skins are coming from and how they should be treated. 


```cs
	// This piece of code creates a new ReskinProfile with the collection name 'My Super Awesome Collection' and compatability identifier 'Fox's Collections'
	ReskinProfile exampleModProfile = new ReskinProfile("My Super Awesome Collection" /* Collection name */, "Fox's Collections" /* Compatability Identifier */);

	// With slight variation, this block of code will be repeated for every skin

	// This part specifies which skin is being created and assigns it a name, in this case, we are creating a KeepBuildingSkin, which is a skin for the building keep, and assigning it the name 'keepSkin'
	KeepBuildingSkin keepSkin = new KeepBuildingSkin();
	// We then assign each of the variables of the keep skin to their values, these can be found in the Skin Index section
	keepSkin.keepUpgrade1 = upgrade0;
	keepSkin.keepUpgrade2 = upgrade1;
	keepSkin.keepUpgrade3 = upgrade2;
	keepSkin.keepUpgrade4 = upgrade3;

	// This part adds the skin to the ReskinProfile we created eariler
	exampleModProfile.Add(keepSkin);

	// This registers the reskin, so it will be taken into account by the engine considering other mods installed
	// Reskins must be registered at some time during SceneLoaded
	exampleModProfile.Register();
```







Skin Index
===========================
```
 -- Castle -- 
--------------

-- Keep --
Name: Keep
UniqueName: keep
Jobs: 3
Models:
	keepUpgrade1:         Instance | The base upgrade for the keep
	keepUpgrade2:         Instance | The second upgrade for the keep
	keepUpgrade3:         Instance | The third upgrade for the keep
	keepUpgrade4:         Instance | The fourth upgrade for the keep

-- Wooden Castle Block --
Name: Wooden Castle Block
UniqueName: woodcastleblock
Models:
	Open:                  Modular | The flat piece without crenelations for a castle block
	Closed:                Modular | The piece of a castleblock with all crenelations at the top and no connections
	Single:                Modular | The piece of a castleblock that only has crenelations on one side
	Opposite:              Modular | The straight piece of a castle block
	Adjacent:              Modular | The corner piece for a castle block
	Threeside:             Modular | The piece of a castleblock with crenelations on 3 sides
	---------
	doorPrefab:           Instance | The door that appears on a castleblock when it connects to other castleblocks

-- Stone Castle Block --
Name: Stone Castle Block
UniqueName: castleblock
Models:
	Open:                  Modular | The flat piece without crenelations for a castle block
	Closed:                Modular | The piece of a castleblock with all crenelations at the top and no connections
	Single:                Modular | The piece of a castleblock that only has crenelations on one side
	Opposite:              Modular | The straight piece of a castle block
	Adjacent:              Modular | The corner piece for a castle block
	Threeside:             Modular | The piece of a castleblock with crenelations on 3 sides
	---------
	doorPrefab:           Instance | The door that appears on a castleblock when it connects to other castleblocks

-- Wooden Gate --
Name: Wooden Gate
UniqueName: woodengate
Models:
	gate:                 Instance | The main model of the gate, excluding the porticulus
	porticulus:           Instance | The part of the gate that moves up and down to show opening/closing

-- Stone Gate --
Name: Stone Gate
UniqueName: gate
Models:
	gate:                 Instance | The main model of the gate, excluding the porticulus
	porticulus:           Instance | The part of the gate that moves up and down to show opening/closing

-- Castle Stairs --
Name: Castle Stairs
UniqueName: castlestairs
Models:
	stairsFront:           Modular | stairs facing +z
	stairsRight:           Modular | stairs facing +x
	stairsDown:            Modular | stairs facing -z
	stairsLeft:            Modular | stairs facing -x

-- Archer Tower --
Name: Archer Tower
UniqueName: archer
Jobs: 2
Models:
	baseModel:            Instance | The main model of the Archer Tower
	veteranModel:         Instance | An embelishment added to the archer tower when it achieves the veteran status

-- Ballista Tower --
Name: Ballista Tower
UniqueName: ballista
Jobs: 4
Models:
	veteranModel:         Instance | An embelishment added to the archer tower when it achieves the veteran status
	baseModel:            Instance | The main model of the Ballista Tower
	topBase:              Instance | The base of the rotational top half of the ballista
	-------
	armR:                 Instance | The right side arm used to animate the ballista's firing movement
	----
	armL:                 Instance | The left side arm used to animate the ballista's firing movement
	----
	stringR:              Instance | The right side of the animated string used to pull back and fire the ballista projectile
	stringL:              Instance | The left side of the animated string used to pull back and fire the ballista projectile
	-------
	projectile:           Instance | The projectile fired from the ballista
	----------
	flag:                 Instance | A decorative flag on the ballista
Anchors:
	armREnd:       The right end of the right arm of the ballista; position used for anchoring the right side of the string in animation
	armLEnd:       The left end of the left arm of the ballista; position used for anchoring the left side of the string in animation
	projectileEnd: The end of the ballista projectile that's pulled back before firing

-- Treasure Room --
Name: Treasure Room
UniqueName: throneroom
Jobs: 5
Models:
	baseModel:            Instance | The base model that will replace the building

-- Chamber Of War --
Name: Chamber Of War
UniqueName: chamberofwar
Jobs: 5
Models:
	baseModel:            Instance | The base model that will replace the building

-- Great Hall --
Name: Great Hall
UniqueName: greathall
Jobs: 3

[Not Supported]
-- Barracks --
Name: Barracks
UniqueName: barracks
Jobs: 8

-- Archer School --
Name: Archer School
UniqueName: archerschool
Jobs: 8

 -- Town -- 
------------

-- Road --
Name: Road
UniqueName: road
Models:
	straight:              Modular | The straight segment 
	elbow:                 Modular | The elbow segment
	intersection3:         Modular | The three way intersection segment
	intersection4:         Modular | The four way intersection segment

-- Stone Road --
Name: Stone Road
UniqueName: stoneroad
Models:
	straight:              Modular | The straight segment 
	elbow:                 Modular | The elbow segment
	intersection3:         Modular | The three way intersection segment
	intersection4:         Modular | The four way intersection segment

 -- Advanced Town -- 
---------------------

-- Church --
Name: Church
UniqueName: church
Jobs: 4
Models:
	baseModel:            Instance | The base model that will replace the building

 -- Maritime -- 
----------------

-- Bridge --
Name: Bridge
UniqueName: bridge
Models:
	straight:              Modular | The straight segment 
	elbow:                 Modular | The elbow segment
	intersection3:         Modular | The three way intersection segment
	intersection4:         Modular | The four way intersection segment

-- Stone Bridge --
Name: Stone Bridge
UniqueName: stonebridge
Models:
	straight:              Modular | The straight segment 
	elbow:                 Modular | The elbow segment
	intersection3:         Modular | The three way intersection segment
	intersection4:         Modular | The four way intersection segment

 -- Unsupported -- 
-------------------

[Not Supported]
-- Aqueduct --
Name: Aqueduct
UniqueName: aqueduct

[Not Supported]
-- Baker --
Name: Baker
UniqueName: baker

[Not Supported]
-- Bath House --
Name: Bath House
UniqueName: bathhouse

[Not Supported]
-- Blacksmith --
Name: Blacksmith
UniqueName: blacksmith

[Not Supported]
-- Butcher --
Name: Butcher
UniqueName: butcher

[Not Supported]
-- Cathedral --
Name: Cathedral
UniqueName: cathedral

[Not Supported]
-- Cemetery --
Name: Cemetery
UniqueName: cemetery

[Not Supported]
-- Cemeteries --
Name: Cemeteries
UniqueName: cemeterydummy

[Not Supported]
-- Cemetery --
Name: Cemetery
UniqueName: cemetery44

[Not Supported]
-- Cemetery Circle --
Name: Cemetery Circle
UniqueName: cemeteryCircle

[Not Supported]
-- Cemetery Diamond --
Name: Cemetery Diamond
UniqueName: cemeteryDiamond

[Not Supported]
-- Cemetery Caretaker --
Name: Cemetery Caretaker
UniqueName: cemeterykeeper

[Not Supported]
-- Charcoal Maker --
Name: Charcoal Maker
UniqueName: charcoalmaker

[Not Supported]
-- Clinic --
Name: Clinic
UniqueName: clinic

[Not Supported]
-- Rock Removal --
Name: Rock Removal
UniqueName: destructioncrew

[Not Supported]
-- Dock --
Name: Dock
UniqueName: dock

[Not Supported]
-- Drawbridge --
Name: Drawbridge
UniqueName: drawbridge

[Not Supported]
-- Farm --
Name: Farm
UniqueName: farm

[Not Supported]
-- Fire Brigade --
Name: Fire Brigade
UniqueName: firehouse

[Not Supported]
-- Fishing Hut --
Name: Fishing Hut
UniqueName: fishinghut

[Not Supported]
-- Fishmonger --
Name: Fishmonger
UniqueName: fishmonger

[Not Supported]
-- Forester --
Name: Forester
UniqueName: forester

[Not Supported]
-- Fountain --
Name: Fountain
UniqueName: fountain

[Not Supported]
-- Garden --
Name: Garden
UniqueName: garden

[Not Supported]
-- Granary --
Name: Granary
UniqueName: largegranary

[Not Supported]
-- Great Library --
Name: Great Library
UniqueName: greatlibrary

[Not Supported]
-- Hospital --
Name: Hospital
UniqueName: hospital

[Not Supported]
-- Hovel --
Name: Hovel
UniqueName: smallhouse

[Not Supported]
-- Iron Mine --
Name: Iron Mine
UniqueName: ironmine

[Not Supported]
-- Large Fountain --
Name: Large Fountain
UniqueName: largefountain

[Not Supported]
-- Cottage --
Name: Cottage
UniqueName: largehouse

[Not Supported]
-- Library --
Name: Library
UniqueName: library

[Not Supported]
-- Manor --
Name: Manor
UniqueName: manorhouse

[Not Supported]
-- Market --
Name: Market
UniqueName: market

[Not Supported]
-- Mason --
Name: Mason
UniqueName: Mason

[Not Supported]
-- Moat --
Name: Moat
UniqueName: moat

[Not Supported]
-- Noria --
Name: Noria
UniqueName: noria

[Not Supported]
-- Orchard --
Name: Orchard
UniqueName: orchard

[Not Supported]
-- Outpost --
Name: Outpost
UniqueName: outpost

[Not Supported]
-- Pier --
Name: Pier
UniqueName: pier

[Not Supported]
-- Produce Storage --
Name: Produce Storage
UniqueName: producestand

[Not Supported]
-- Quarry --
Name: Quarry
UniqueName: quarry

[Not Supported]
-- Reservoir --
Name: Reservoir
UniqueName: reservoir

[Not Supported]
-- Rubble --
Name: Rubble
UniqueName: rubble

[Not Supported]
-- Small Granary --
Name: Small Granary
UniqueName: smallgranary

[Not Supported]
-- Small Market --
Name: Small Market
UniqueName: smallmarket

[Not Supported]
-- Small Stockpile --
Name: Small Stockpile
UniqueName: smallstockpile

[Not Supported]
-- Queen Barbara --
Name: Queen Barbara
UniqueName: statue_barbara

[Not Supported]
-- Lord Levi --
Name: Lord Levi
UniqueName: statue_levi

[Not Supported]
-- Lord Nextraztus --
Name: Lord Nextraztus
UniqueName: statue_sam

[Not Supported]
-- Stockpile --
Name: Stockpile
UniqueName: largestockpile

[Not Supported]
-- Swineherd --
Name: Swineherd
UniqueName: swineherd

[Not Supported]
-- Tavern --
Name: Tavern
UniqueName: tavern

[Not Supported]
-- Town Square --
Name: Town Square
UniqueName: townsquare

[Not Supported]
-- Transport Ship --
Name: Transport Ship
UniqueName: transportship

[Not Supported]
-- Troop Transport Ship --
Name: Troop Transport Ship
UniqueName: trooptransportship

[Not Supported]
-- Well --
Name: Well
UniqueName: well

[Not Supported]
-- Windmill --
Name: Windmill
UniqueName: windmill
```