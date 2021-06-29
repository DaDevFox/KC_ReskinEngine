# Introduction
# Material/Shader Patterns
## Unimaterial/Livery Material Pattern
## Building Shader Pattern

# Model Patterns
## Modular Pattern | `m`

Modular pattern specifications: 
**No `GameObject` transformations (position, rotation, scaling from within the editor) will be saved**
**Only the mesh on the root object of the GameObject will be used; not the mesh renderer's material nor the other components**

> Some modular pattern implementations may offer an option for you to provide a material, however if it is not offered or you leave the field blank, the majority of modular models will use a game-filled [unimaterial or livery material]() (for the stable and alpha branch respectively)

The modular pattern is a special pattern a game system can use to display a model in the game's 3D world. 

Usually to display a model (which is an `Asset` or `UnityEngine.Object` in the form of a `Mesh`) something creates a `GameObject` in the game hierarchy with `MeshRenderer` and `MeshFilter` components and assigns the Mesh Filter a mesh. 

A modular model, however, is most often used when the user (usually a building although it is used by environmental systems like trees and birds as well) doesn't want to deal with all the extra overhead of an entire `GameObject` for whatever reason and so only wants to deal with the `Mesh` that gets used by the `GameObject` and control the rest by itself. 

It is often used in buildings with a changing model like a castleblock, road, bridge, or garden, so that it can have one `GameObject` child with a `MeshFilter` + `MeshRenderer` but the mesh that child uses gets changed dynamically depending on another factor (most often the neighboring cells of its position). 

When filling a modular field, it will ask for a `GameObject` but only the `sharedMesh` field of the `MeshFilter` component attached to the root of the `GameObject` will ever be used. 

## Instance Pattern | `i`
## Instance-Generic Pattern | `ig`
## Instance-Generic-Modular Pattern | `igm`
## Modular-Numerical Pattern | `mx`
### `m2`
### `m4`
### `m6`
### `m8`
## Instance-Numerical Pattern | `ix`
### `i2`
### `i9`