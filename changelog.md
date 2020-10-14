# Changelog

## 0.1.0
Initial release

## 0.2.0
- Added `MockModule`
- Added `MockModuleBuilder`
- Deleted `ModuleBuilder`

## 0.3.0
- Prefix test environment directories.

## 0.4.0
- The `TestEnvironmentProvider` no longer fails during `Dispose` when files are removed manually.
- Readonly files created manually (after the `TestEnvironmentProvider`) has been created are also deleted properly.

## 0.5.0
- The constructor `TestFile(string filePath)` has been removed. Use `TestFile.Create(string filePath)` instead.
- `TestFile`s can be created from an `Assembly` using `TestFile.Create(...)`.
- Added `TestEnvironmentBuilder`.

## 0.5.1
- Allow `MockModuleBuilder` to register anything `ContainerBuilder` can.
- Add shortcuts to `ContainerBuilder` functionality e.g. `RegisterModule` and `RegisterInstance`.

## 0.5.2
- Fix `RegisterModule(Mock<T> mock)` by removing `new()` restriction.

## 0.5.3
- Using `Messerli.CompositionRoot` nuget package
- Converted `MockModuleBuilders` mock functionality to `ModuleBuilderMockExtension` and `ContainerBuilderMockExtension`
- Deleted `MockModule` and `MockModuleBuilder` because the functionality is in `Messerli.CompositionRoot` nuget package.

## 0.5.4
- Changed the return type of `ModuleBuilderMockExtension` and `ContainerBuilderMockExtension`.

## 0.5.5
- Add `System`, `Windows` and `Internal` to excluded namespace list.
- Improve detection of ignored namespaces.

## 0.6.0
- Update Autofac to 5.1.
- Add support for .NET Standard 2.0.
- Seal all classes.

## 0.6.1
Rerelease of 0.6.0 with fixed nuget package.

## 0.7.0
- Add `TypesThatNeedToBeImplementedInAssemblyData` attribute which provides a list of types
  that need to implemented in an assembly to an xUnit theory.
- Remove `ModuleInterfaceEnumerable`, `ObjectArrayEnumerable`, and `ContainerInterfaceRetriever`.
  They are superseded by `TypesThatNeedToBeImplementedInAssemblyData`.

## 0.7.1
- Fix regression where static classes and generic types where treated as types that need to be implemented.

## 0.7.2
- Add extension method `GetRegisteredTypes` on `IComponentContext` to retrieve all registered types.

## 0.7.3
- Allow blacklisting of types on `TypesThatNeedToBeImplementedInAssemblyData` attribute using a new attribute: `ExcludedTypes`.

## 0.8.0
- Make sure that the exception thrown by the `TypesThatNeedToBeImplementedInAssemblyData` attribute
  when the specified assembly can't be found is displayed in the test explorer.
- Add `TypesRegisteredInContainerDataAttribute` attribute which provides the types
  registered in an Autofac container as xUnit theory data.
- Seal `ExcludedTypesAttribute` and make its `Types` member internal.
- Seal `TypesThatNeedToBeImplementedInAssemblyDataAttribute`.

## 0.8.1
- `TypesRegisteredInContainerData` now ignores keyed and named registrations.

## 0.8.2
- `[TypesRegisterdInContainerData]` now respects the `[ExcludedTypes]` attribute.

## Unreleased
* Only depend on `System.Collections.Immutable` when targeting .NET Standard.
* Enable nullable annotations introduced with C# 8.
