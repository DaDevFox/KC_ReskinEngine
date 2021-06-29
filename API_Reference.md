# Simplified Overview
---

The following reference and tutorial assumes a basic knowledge of C# and modding in Kingdoms and Castles; if you do not have knowledge in either, see further [tutorials](https://modtutorial.kingdomsandcastles.com/) or use the Unity Tool as a simplified version that can accomplish a similar process as writing a mod directly. 

> See [here](https://github.com/DaDevFox/KCReskinEngine/blob/master/Guide_updated.md#how-the-engine-works) for a description of the Unity Tool and [here](https://github.com/DaDevFox/KCReskinEngine/tree/master/Unity%20Plugin) to download it. 

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



## ReskinProfile

To register any skins 

## The BuildingSkin
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

## Special Patterns



