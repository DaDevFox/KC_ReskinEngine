# Simplified Overview
The KCRE Scripting API consists of a main class, `ReskinProfile` that will contain all of your modifications to the game, and many many classes that all extend from the base class `Skin`. Different types of skins will have different fields that you can modify and almost all will have a corresponding entry in the **Skin-Index**. The main reason they would not have an entry is due to version discrepencies or not having been implemented yet. 

## The BuildingSkin
Buildings in Kingdoms and Castles are extremely diverse in their implementation which causes a massive amount of research to be necessary for each one and very little generic processes that can apply to all buildings, however they do have some common traits, and as such the skins for buildings will share these common traits. 
Any `Skin` extending from `BuildingSkin` will have the following class members:


Optional; the positions peasants stand at while working at the building; directly corresponds to number of jobs a building employs
If left null this field will be set to its default value;
```cs
Vector3[] personPositions;
```
Paths relative to the building root to all the meshes that will be included in the outline effect when selecting the building (each item in list requires MeshRenderer component)
```cs
string[] outlineMeshes;
```
Paths relative to the building root to all the skinned mesh renderers that will be included in the outline effect when selecting the building (each item in list requires SkinnedMeshRenderer component)
```cs
string[] outlineSkinnedMeshes;
```
Paths to the colliders used for building selection
```cs
string[] colliders;
```
Renderers that use the building shader; all renderers tagged as such will become involved with game effects targeted at buildings like the happiness overlay, snow, and damage however they will also have their material set to the unimaterial specified in the alpha version of the game (see colorsets and alpha compatability)
```cs
string[] renderersWithBuildingShader;
```




