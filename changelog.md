# Changelog

## 0.1.0
- Initial release

## 0.2.0
- Added `MockModule`
- Added `MockModuleBuilder`
- Deleted `ModuleBuilder`

## 0.3.0
- Prefix test environment directories

## 0.4.0
- The `TestEnvironmentProvider` no longer fails during `Dispose` when files are removed manually.
- Readonly files created manually (after the `TestEnvironmentProvider`) has been created are also deleted properly.