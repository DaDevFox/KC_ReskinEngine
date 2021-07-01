# Simplified Overview
---

The following reference and tutorial assumes a basic knowledge of C# and modding in Kingdoms and Castles; if you do not have knowledge in either, see further [tutorials](https://modtutorial.kingdomsandcastles.com/) or use the Unity Tool as a simplified version that can accomplish a similar process as writing a mod directly. 

> See [here](https://github.com/DaDevFox/KCReskinEngine/blob/master/Guide.md#how-the-engine-works) for a description of the Unity Tool and [here](https://github.com/DaDevFox/KCReskinEngine/tree/master/Unity%20Tool) to download it. 

---
The KCRE Scripting API consists of a main class, `ReskinProfile` that will contain all of your modifications to the game, and many many classes that all extend from the base class `Skin`. Different types of skins will have different fields that you can modify and almost all will have a corresponding entry in the **Skin-Index**. The main reason they would not have an entry is due to version discrepencies or not having been implemented yet. 
To successfuly implement a skin, first find it in the **Skin-Index** to find the fields and other specifications that need to be filled to create and register the skin. Once it is created, have the values assigned to each field of the item's corresponding `Skin` class and call `ReskinProfile.Add(Skin item)` to add it to the profile. 

As an example, say you wanted to skin a church. The Skin-Index entry is as follows:
```
-- Church --
Name: Church
UniqueName: church
Jobs: 4
Models:
	baseModel:            Instance | The base model that will replace the building
```
and the associated `Skin` class is almost always the name of the skin in the Skin-Index with the term 'Skin' added to the end I.E `ChurchSkin`. There are a few exceptions to this rule however they are usually near enough for you to find it I.E. `WoodenCastleBlock` vs `WoodCastleBlock`. 

And sure enough, when you type ChurchSkin into an IDE with Intellisense set up properly, the highlighting shows you that a class exists with that name.

![Image](https://i.ibb.co/9wkk4vp/Churchskincode.png)

## The `ReskinProfile`

To register any skins to the Engine they must be added to a ReskinProfile and the profile must be registered. These can be accomplished by the two corresponding methods in the ReskinProfile class: `ReskinProfile.Add(Skin item)` and `ReskinProfile.Register()`. 

Thereby the full workflow to register multiple skins to the Engine is:

```cs
void MyMethod()
{
    ReskinProfile profile = new ReskinProfile("MyModName", "MyModIdentifier");

    HovelSkin skin1 = new HovelSkin();
    /* set all of skin 1's fields */
    profile.Add(skin1);

    CottageSkin skin2 = new CottageSkin();
    /* set all of skin 2's fields */
    profile.Add(skin2);

    ManorSkin skin3 = new ManorSkin();
    /* set all of skin 3's fields */
    profile.Add(skin3);

    profile.Register();
}
```

And this code alone will have your mod received and executed by the Engine. 
The only prerequisite: `profile.Register()` absolutely **must** be registered before or during `SceneLoaded` no matter what, so in this case `MyMethod` could be called in the `SceneLoaded` method and it would work but any time later and it would not. 

The reason for this is simply that the Engine has to make sure it executes all the mods after they all have been registered so there needs to be some (technically arbitrary) time before which all mods must be registerd.  

## The `BuildingSkin`
Buildings in Kingdoms and Castles are extremely diverse in their implementation which causes a massive amount of research to be necessary for each one and very little generic processes that can apply to all buildings, however they do have some common traits, and as such the skins for buildings will share these common traits. 
Any `Skin` extending from `BuildingSkin` will have the following class members:

---
Optional; the positions peasants stand at while working at the building; directly corresponds to number of jobs a building employs
If left null this field will be set to its default value;
```cs
Vector3[] personPositions;
```
---
Paths relative to the building root to all the meshes that will be included in the outline effect when selecting the building (each item in list requires MeshRenderer component)
```cs
string[] outlineMeshes;
```
---
Paths relative to the building root to all the skinned mesh renderers that will be included in the outline effect when selecting the building (each item in list requires SkinnedMeshRenderer component)
```cs
string[] outlineSkinnedMeshes;
```
---
Paths to the colliders used for building selection
```cs
string[] colliders;
```
---
Renderers that use the building shader; all renderers tagged as such will become involved with game effects targeted at buildings like the happiness overlay, snow, and damage however they will also have their material set to the unimaterial specified in the alpha version of the game (see colorsets and alpha compatability)
```cs
string[] renderersWithBuildingShader;
```
---
(readonly)
The name the game uses to identify the building associated with this skin
```cs
string UniqueName;
```
---
(readonly)
The name the game uses for the building associated with this skin for menus that the player reads I.E. the build menu
```cs
string FriendlyName;
```
---

## Special Subtypes
### `GenericBuildingSkin`

This type of skin follows the '[instance-generic](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#instance-generic-pattern--ig)' pattern, coded `ig`. The `ig` pattern is extremely common and used by almost half the buildings in the game. It features a simple building with a single model that is the first child of the first child of the building root (yes you read that right). See also [`igm`](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#instance-generic-modular-pattern--igm), a variation of the `ig` pattern that also includes a single modular piece (windmill, cemetary, fishut). 

---
An [instance](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#instance-pattern--i) Mesh field that represents the main model of the building
```cs
GameObject baseModel;
```
---
