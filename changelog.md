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

## unreleased
- Add `System`, `Windows` and `Internal` to excluded namespace list.
- Improve detection of ignored namespaces.
