# Introduction
This file documents the different patterns that buildings and/or environment in KC commonly use. 

# Material/Shader Patterns
## Unimaterial/Livery Material Pattern | `um`/`lm`
This is a general term involving a unimaterial (in stable branch) or livery material (in alpha), although they are essentially the same thing. 

A unimaterial/livery material is a material with a texture that holds all the colors the buildings and environment in the game use. 

Models must be designed with their UVs in coordination with this material pattern to use the colors defined in the texture. 

The reason for the distinction between the alpha and stable versions (livery and unimaterial respectively) is because in the alpha this texture will change colors on different landmasses depending on the player/ai's color

If you want to create a model that uses the `um`/`lm` pattern there are reference images extracted from the game in the Unity Tool's files at 

For Alpha: `[Parent Directory for Tool Folder]/Reskin Engine/Resources/Reskin Engine/Materials/Alpha/Liveries/simplified`

For Stable `[Parent Directory for Tool Folder]/Reskin Engine/Resources/Reskin Engine/Materials/Stable/Unimaterials`

For the stable branch `Unimaterial 0` is most commonly used for buildings

## Building Shader Pattern
(This pattern ties into the [Unimaterial/Livery Material Pattern](https://github.com/DaDevFox/KCReskinEngine/blob/master/Building_Patterns.md#unimateriallivery-material-pattern--umlm))

Most buildings have a list of mesh renderers which all use a special shader called the Building Shader. (This list is [`BuildingSkin`](https://github.com/DaDevFox/KCReskinEngine/blob/master/API_Reference.md#the-buildingskin)`.renderersWithBuildingShader`)

The building shader has some special shader code on it that allows cool effects like snow, health/happiness/integrity colors and the building scaffolding animation. 

**All** renderers registered in the list that have a material using the building shader on them will be automatically updated with all the building animations, placement effects, and other stuff the game does and will have their materials' textures set to a unimaterial or livery material. 

## Outline Mesh Pattern

Similar to the [Building Shader Pattern](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#building-shader-pattern), there's a list for meshes that will be registered to the outline effect that shows when a building is highlighted or selected. (This list is [`BuildingSkin`](https://github.com/DaDevFox/KCReskinEngine/blob/master/API_Reference.md#the-buildingskin)`.outlineMeshes` and there's a seperate list for skinnedMeshes; [`BuildingSkin`](https://github.com/DaDevFox/KCReskinEngine/blob/master/API_Reference.md#the-buildingskin)`.outlineSkinnedMeshes`)


# Modelling Patterns
## Modular Pattern | `m`

*Pattern specifications*: 

- **No `GameObject` transformations (position, rotation, scaling from within the editor) will be saved**

- **Only the mesh on the root object of the GameObject will be used; not the mesh renderer's material nor the other components**

> Some modular pattern implementations may offer an option for you to provide a material, however if it is not offered or you leave the field blank, the majority of modular models will use a game-filled [unimaterial or livery material](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#unimateriallivery-material-pattern--umlm) (for the stable and alpha branch respectively)

The modular pattern is a special pattern some game systems use to display a model in the game's 3D world. 

*Uses*

- Mesh-swapping objects
    - Roads
    - Bridges
    - Gardens
    - Castleblocks
- Systems that use massive amounts of the determined mesh
    - Trees
    - Birds
    - Pigs
    - Fish
    - It has been said that peasants may switch to this system at some point


Usually to display a model (which is an `Asset` or `UnityEngine.Object` in the form of a `Mesh`) something creates a `GameObject` in the game hierarchy with `MeshRenderer` and `MeshFilter` components and assigns the Mesh Filter a mesh. 

A modular model, however, is most often used when the user (usually a building although it is used by environmental systems as well) doesn't want to deal with all the extra overhead of an entire `GameObject` for whatever reason and so only wants to deal with the `Mesh` that gets used by the `GameObject` and control the rest by itself. 

It is often used in buildings with a changing model like a castleblock, road, bridge, or garden, so that it can have one `GameObject` child with a `MeshFilter` + `MeshRenderer` but the mesh that child uses gets changed dynamically depending on another factor (most often the neighboring cells of its position). 

When filling a modular field, it will ask for a `GameObject` but only the `sharedMesh` field of the `MeshFilter` component attached to the root of the `GameObject` will ever be used. 

> The modular pattern alone is rarely used (except by game systems with a single mesh) as most things that involve a modular pattern model system require multiple meshes and so use a form of the [Modular-Numerical](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#modular-numerical-pattern--mx) or `mx` pattern.  

## Instance Pattern | `i`

*Pattern specifications*: 

- **Saves entire tree structure of selected GameObject, including components, children, components of children, but NOT user-written scripts**
- **If modder desires scripts to be attached, they must be made through the mod files, not in Unity as they won't be savd in the AssetBundle**

The Instance Pattern is the most common pattern for displaying a 3D model in the game's 3D world. It involves simply Instantiating a `GameObject`. 

The instance pattern is fairly deist in practice as it just instantiates the `GameObject` intended to be instantiated and is fairly hands off from that point forward, unlike the [modular](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#modular-pattern--m) pattern which often involves actively switching. Therefore, the instance pattern is far more flexible as it allows for the model to have components, children, and other types of data appended to it and it all will be retained in the final game form. 

Note for those wishing to attach scripts to their object (for modders with knowledge of C# & Unity):

This pattern allows the object to be translated almost perfectly from Unity to in-game because it gets packed into an AssetBundle and then extracted from the bundle at runtime and AssetBundles can hold any kind of object that extends `UnityEngine.Object`. Anything, except code, that is. Because of this limitation, custom component scripts that are user-defined have to be added to the built mod files' directory and attached to the object properly at runtime through the mod code. 

> Skusch's Day-Night Cycle mod accomplishes this successfully by having some extra classes in the mod folder that extend `MonoBehaviour` to change the emission of the torches and windows of some buildings and at runtime attaching them to the appropriate models. 
 
## Instance-Generic Pattern | `ig`

A common pattern many buildings follow where the building follows a certain structure that includes a single model that makes up the visuals of the building. Every building in the game has a first child called `Offset` and in an instance-generic pattern abforming building that `GameObject`'s first child is the main model fo the building.

More succinctly, if the first child of the first child of the building's root is the base model of the building, it follows the instance-generic pattern. 

> This is by far the most common building pattern. Most buildings without any special characteristics like moving pieces or parts that appear over time will follow this pattern. 

## Instance-Generic-Modular Pattern | `igm`
## Modular-Numerical Pattern | `mx`
### `m2`
### `m4`
### `m6`
### `m8`
## Instance-Numerical Pattern | `ix`
### `i2`
### `i9`

// add colliders pattern
// add person positions pattern