# Introduction
This file documents the different patterns that buildings and/or environment in KC commonly use. 

Many of the patterns have a short code usually 1-3 letters long to use in spreadsheets such as the [KCRE Skin Support Spreadsheet](https://docs.google.com/spreadsheets/d/1ow1hWDYpN2fDug6KnrOqCa8hPSPgnouY06qliUtIWpA/edit#gid=0) 

# Building-Specific Patterns

## Material/Shader Patterns

Patterns relating to the building shaders and materials that all of the base game's buildings follow. Implementing all of these patterns in a building reskin will complete all the steps to make the building indistinguishable as it follows the process vanilla buildings do.  

### Unimaterial/Livery Material Pattern | `um`/`lm`
This is a general term involving a unimaterial (in stable branch) or livery material (in alpha), although they are essentially the same thing. 

A unimaterial/livery material is a material with a texture that holds all the colors the buildings and environment in the game use. 

Models must be designed with their UVs in coordination with this material pattern to use the colors defined in the texture. 

The reason for the distinction between the alpha and stable versions (livery and unimaterial respectively) is because in the alpha this texture will change colors on different landmasses depending on the player/ai's color

If you want to create a model that uses the `um`/`lm` pattern there are reference images extracted from the game in the Unity Tool's files at 

For Alpha: `[Parent Directory for Tool Folder]/Reskin Engine/Resources/Reskin Engine/Materials/Alpha/Liveries/simplified`

For Stable `[Parent Directory for Tool Folder]/Reskin Engine/Resources/Reskin Engine/Materials/Stable/Unimaterials`

For the stable branch `Unimaterial 0` is most commonly used for buildings

### Building Shader Pattern
(This pattern ties into the [Unimaterial/Livery Material Pattern](https://github.com/DaDevFox/KCReskinEngine/blob/master/Building_Patterns.md#unimateriallivery-material-pattern--umlm))

Most buildings have a list of mesh renderers which all use a special shader called the Building Shader. (This list is [`BuildingSkin`](https://github.com/DaDevFox/KCReskinEngine/blob/master/API_Reference.md#the-buildingskin)`.renderersWithBuildingShader`)

The building shader has some special shader code on it that allows cool effects like snow, health/happiness/integrity colors and the building scaffolding animation. 

**All** renderers registered in the list that have a material using the building shader on them will be automatically updated with all the building animations, placement effects, and other stuff the game does and will have their materials' textures set to a unimaterial or livery material. 

### Outline Mesh Pattern

Similar to the [Building Shader Pattern](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#building-shader-pattern), there's a list for meshes that will be registered to the outline effect that shows when a building is highlighted or selected. (This list is [`BuildingSkin`](https://github.com/DaDevFox/KCReskinEngine/blob/master/API_Reference.md#the-buildingskin)`.outlineMeshes` and there's a seperate list for skinnedMeshes; [`BuildingSkin`](https://github.com/DaDevFox/KCReskinEngine/blob/master/API_Reference.md#the-buildingskin)`.outlineSkinnedMeshes`)

## Misc Patterns

Patterns relating less to the visuals and more to syncing the visuals with the functionality of the building

### Collider Pattern

All buildings have some kind of collider or multiple colliders somewhere in their `GameObject` structure that the game uses to select the shape of the building through primary clicks. 

> Setting the list of colliders in a Skin through the Unity Tool or through the API will override the vanilla buidling's colliders but leaving it blank/null/size 0 will use the vanilla building's colliders 

### Person Positions Pattern

Person Positions is the game's name for the list of positions peasants will stand at while working in the building (if the building has jobs). The Person Positions list should be the same size as the amount of jobs the building has.   




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

*Pattern specifications*: 

- **A building where the first child of the first child of the building root is the base model**

A common pattern many buildings follow where the building follows a certain structure that includes a single model that makes up the visuals of the building. Every building in the game has a first child called `Offset` and in an instance-generic pattern abforming building that `GameObject`'s first child is the main model fo the building.

More succinctly, if the first child of the first child of the building's root is the base model of the building, it follows the instance-generic pattern. 

> This is by far the most common building pattern. Most buildings without any special characteristics like moving pieces or parts that appear over time will follow this pattern. 

## Instance-Generic-Modular Pattern | `igm`

Simply an [Instance-Generic Pattern](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#instance-generic-pattern--ig) with a single modular piece added on, like the blades of an otherwise mundane windmill, or the waterwheel of a noria. Sometimes those pieces will be modular however sometimes they can be instanced, in which case this actually follows the `i2`, or [Two-Piece-Instance Pattern](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#i2).  

## Modular-Numerical Pattern | `mx`

Represents a form of a [modular](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#modular-pattern--m) pattern with multiple modular pieces, usually to be swapped out with each other

### `m2`

A two-piece modular pattern, usually used for mass-use models that come in pairs, like a troop model + a sword, or an archer + a bow. 

Notably used in units and Viking raiders. 

### `m4`

A four-piece modular pattern, used for permutable pieces that can only change on the X and Z axes, as that makes 4 total possible unique combinations. 

Notably used in roads and bridges. 

### `m6`

A six-piece modular pattern, only notably used for castle blocks, as they require 2 extra models due to the Y-dimension as well as X and Z (one block for the top of a tower and one for the tower middle). 

### `m8`

An eight-piece modular pattern, only notably used for the garden as it has 4 permutable pieces for normal usage and a special  fancy set for use when irrigated as well. 

## Instance-Numerical Pattern | `ix`
### `i2`

Similar to the [Two-Piece-Modular Pattern](https://github.com/DaDevFox/KCReskinEngine/blob/master/Common_Game_Patterns.md#m2) except using instance models. 

### `i9`

Nine-piece instance pattern, only notably used by the ballista due to its animated pieces for the bowstring draw animation. 


# Unsupported Patterns
Patterns that are known but as of yet unsupported by the Engine

## Particles
Particles like smoke, build effects, etc. 

## Stacks/ResourceStacks
The locations/arrrangement of the visual stacks of resources that fill up as the building's storage fills


