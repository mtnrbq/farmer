[<AutoOpen>]
module Farmer.Builders.Maps

open Farmer
open Farmer.Maps
open Farmer.Helpers
open Farmer.Arm.Maps

type MapsConfig =
    { Name : ResourceName
      Sku : Sku }
    interface IBuilder with
        member this.DependencyName = this.Name
        member this.BuildResources _ = [
            { Name = this.Name
              Location = Location "global"
              Sku = this.Sku }
        ]

type MapsBuilder() =
    member _.Yield _ =
        { Name = ResourceName.Empty
          Sku = S0 }
    member _.Run(state:MapsConfig) =
        { state with Name = state.Name |> sanitiseMaps |> ResourceName }
    /// Sets the name of the Azure Maps instance.
    [<CustomOperation("name")>]
    member _.Name(state:MapsConfig, name) = { state with Name = name }
    member this.Name(state:MapsConfig, name) = this.Name(state, ResourceName name)
    /// Sets the SKU of the Azure Maps instance.
    [<CustomOperation("sku")>]
    member _.Sku(state:MapsConfig, sku) = { state with Sku = sku }

let maps = MapsBuilder()
